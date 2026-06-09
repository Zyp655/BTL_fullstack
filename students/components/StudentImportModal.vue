<template>
  <teleport to="body">
    <div v-if="show" class="fixed inset-0 glass-backdrop z-[9999] flex items-center justify-center p-4">
      <div class="bg-white/90 backdrop-blur-[24px] border border-white/50 rounded-xl shadow-[0_20px_40px_rgba(0,31,63,0.12)] max-w-2xl w-full overflow-hidden animate-scale-in flex flex-col">
        <!-- Title -->
        <div class="px-6 py-4 border-b border-outline-variant/20 flex items-center justify-between">
          <h3 class="font-title-md text-[18px] font-bold text-primary flex items-center gap-2">
            <span class="material-symbols-outlined text-on-tertiary-container">publish</span>
            Nhập học viên từ Excel / CSV
          </h3>
          <button @click="$emit('close')" :disabled="importing" class="text-on-surface-variant hover:text-primary cursor-pointer disabled:opacity-50">
            <span class="material-symbols-outlined">close</span>
          </button>
        </div>

        <!-- Body -->
        <div class="p-6 space-y-4 overflow-y-auto max-h-[70vh]">
          <!-- Top bar with template download -->
          <div class="flex justify-between items-center bg-surface/50 p-4 rounded-xl border border-outline-variant/20">
            <div>
              <p class="text-body-sm font-semibold text-primary">Tải file mẫu dữ liệu</p>
              <p class="text-[11px] text-on-surface-variant">Tải file mẫu có sẵn các cột chuẩn để điền thông tin học viên.</p>
            </div>
            <button
              @click="downloadTemplate"
              class="px-5 py-2.5 rounded-lg border border-on-tertiary-container/30 text-on-tertiary-container bg-on-tertiary-container/5 font-semibold text-body-sm hover:bg-on-tertiary-container/10 transition-colors flex items-center justify-center gap-2 cursor-pointer"
            >
              <span class="material-symbols-outlined text-[18px]">download</span>
              Tải file mẫu (.CSV)
            </button>
          </div>

          <!-- Drag & Drop Zone -->
          <div
            v-if="!importFile"
            @dragover.prevent="dragOver = true"
            @dragleave.prevent="dragOver = false"
            @drop.prevent="handleFileDrop"
            :class="[
              'border-2 border-dashed rounded-xl p-8 text-center flex flex-col items-center justify-center cursor-pointer transition-all duration-200',
              dragOver ? 'border-primary bg-primary/5 scale-[0.98]' : 'border-outline-variant/50 hover:border-primary/50 hover:bg-surface/30'
            ]"
            @click="$refs.fileInput.click()"
          >
            <input
              type="file"
              ref="fileInput"
              class="hidden"
              accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
              @change="handleFileSelect"
            />
            <span class="material-symbols-outlined text-primary/40 text-[48px] mb-3">upload_file</span>
            <p class="text-body-sm font-semibold text-primary">Kéo và thả file Excel/CSV vào đây</p>
            <p class="text-[11px] text-on-surface-variant mt-1">hoặc click để chọn file từ thiết bị</p>
            <p class="text-[10px] text-on-surface-variant/70 mt-2">Hỗ trợ định dạng .xlsx, .xls, .csv</p>
          </div>

          <!-- File Info & Preview -->
          <div v-else class="space-y-4">
            <!-- Selected File Card -->
            <div class="flex items-center justify-between bg-primary/5 border border-primary/20 rounded-xl p-4">
              <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-lg bg-primary/10 text-primary flex items-center justify-center">
                  <span class="material-symbols-outlined">description</span>
                </div>
                <div>
                  <p class="text-body-sm font-semibold text-primary truncate max-w-[250px]">{{ importFile.name }}</p>
                  <p class="text-[11px] text-on-surface-variant">{{ (importFile.size / 1024).toFixed(1) }} KB</p>
                </div>
              </div>
              <button
                v-if="!importing"
                @click="importFile = null; importRows = []"
                class="text-error hover:text-error/80 p-2 rounded-full hover:bg-error/5 transition-all cursor-pointer flex items-center justify-center"
                title="Chọn file khác"
              >
                <span class="material-symbols-outlined text-[20px]">delete</span>
              </button>
            </div>

            <!-- Parsed Rows Preview -->
            <div v-if="importRows.length > 0" class="border border-outline-variant/30 rounded-xl overflow-hidden bg-white/50">
              <div class="px-4 py-2.5 bg-surface-container-high border-b border-outline-variant/20 flex justify-between items-center">
                <span class="text-[12px] font-semibold text-primary">Danh sách học viên phát hiện ({{ importRows.length }})</span>
                <span class="text-[11px] text-on-surface-variant">Hợp lệ: {{ importRows.filter(r => r.isValid).length }} / {{ importRows.length }}</span>
              </div>
              <div class="overflow-x-auto max-h-[200px] text-left">
                <table class="w-full text-[12px] border-collapse">
                  <thead>
                    <tr class="bg-surface/50 border-b border-outline-variant/10 text-on-surface-variant font-semibold">
                      <th class="py-2 px-3 text-left">Tên đăng nhập</th>
                      <th class="py-2 px-3 text-left">Mật khẩu</th>
                      <th class="py-2 px-3 text-left">Họ và tên</th>
                      <th class="py-2 px-3 text-left">Email</th>
                      <th class="py-2 px-3 text-left">Số điện thoại</th>
                      <th class="py-2 px-3 text-left">Trạng thái</th>
                    </tr>
                  </thead>
                  <tbody class="divide-y divide-outline-variant/10">
                    <tr
                      v-for="(row, idx) in importRows"
                      :key="idx"
                      :class="[
                        row.uploadStatus === 'success' ? 'bg-success/5' : '',
                        row.uploadStatus === 'error' ? 'bg-error/5' : '',
                        !row.isValid ? 'bg-warning/5' : ''
                      ]"
                    >
                      <td class="py-2 px-3 font-mono font-semibold">{{ row.username }}</td>
                      <td class="py-2 px-3 font-mono text-on-surface-variant">{{ row.password }}</td>
                      <td class="py-2 px-3 font-semibold text-primary">{{ row.fullName }}</td>
                      <td class="py-2 px-3 text-on-surface-variant truncate max-w-[120px]">{{ row.email || '-' }}</td>
                      <td class="py-2 px-3 text-on-surface-variant">{{ row.phone || '-' }}</td>
                      <td class="py-2 px-3">
                        <span
                          :class="[
                            'inline-flex items-center gap-1 font-semibold text-[11px]',
                            row.uploadStatus === 'success' ? 'text-success' : '',
                            row.uploadStatus === 'error' ? 'text-error' : '',
                            row.uploadStatus === 'uploading' ? 'text-primary animate-pulse' : '',
                            (row.uploadStatus === 'pending' && row.isValid) ? 'text-on-tertiary-container' : '',
                            !row.isValid ? 'text-error font-bold' : ''
                          ]"
                        >
                          <span v-if="row.uploadStatus === 'success'" class="material-symbols-outlined text-[14px]">check_circle</span>
                          <span v-else-if="row.uploadStatus === 'error' || !row.isValid" class="material-symbols-outlined text-[14px]">warning</span>
                          <span v-else-if="row.uploadStatus === 'uploading'" class="w-3.5 h-3.5 border border-primary border-t-transparent animate-spin rounded-full inline-block"></span>
                          {{ row.status }}
                        </span>
                        <span v-if="row.feedback" class="block text-[10px] text-error font-medium">{{ row.feedback }}</span>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>

          <!-- Progress Bar -->
          <div v-if="importing" class="space-y-1.5 p-1">
            <div class="flex justify-between text-[11px] font-semibold text-primary">
              <span>Đang xử lý nhập học viên...</span>
              <span>{{ importProgress }}%</span>
            </div>
            <div class="w-full bg-surface-container-high h-2 rounded-full overflow-hidden border border-outline-variant/10">
              <div class="bg-primary h-full transition-all duration-300 rounded-full" :style="{ width: `${importProgress}%` }"></div>
            </div>
          </div>
        </div>

        <!-- Dialog Footer -->
        <div class="px-6 py-4 border-t border-outline-variant/20 flex justify-end gap-3 bg-white/20">
          <button
            @click="$emit('close')"
            :disabled="importing"
            class="px-5 py-2.5 rounded-lg border border-outline-variant/30 text-on-surface-variant font-semibold text-[13px] hover:bg-surface-container-high transition-colors cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed"
          >
            Hủy bỏ
          </button>
          <button
            v-if="importFile && importRows.filter(r => r.isValid).length > 0"
            @click="startImport"
            :disabled="importing"
            class="px-5 py-2.5 rounded-lg bg-primary-container text-white font-semibold text-[13px] hover:bg-primary shadow-sm hover:shadow-md transition-all active:scale-95 flex items-center gap-2 cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed"
          >
            <span v-if="importing" class="animate-spin w-4 h-4 border-2 border-white border-t-transparent rounded-full"></span>
            <span v-else class="material-symbols-outlined text-[16px]">play_arrow</span>
            Bắt đầu nhập
          </button>
        </div>
      </div>
    </div>
  </teleport>
</template>

<script setup>
import { ref, watch, inject } from 'vue'
import * as XLSX from 'xlsx'
import { useStudentStore, useUserStore } from '../../../stores'

const props = defineProps({
  show: { type: Boolean, required: true }
})

const emit = defineEmits(['close', 'success'])
const showSnackbar = inject('showSnackbar')

const store = useStudentStore()
const userStore = useUserStore()

const importFile = ref(null)
const importRows = ref([])
const importing = ref(false)
const importProgress = ref(0)
const dragOver = ref(false)

watch(() => props.show, (newVal) => {
  if (newVal) {
    importFile.value = null
    importRows.value = []
    importing.value = false
    importProgress.value = 0
    dragOver.value = false
  }
})

const handleFileDrop = (e) => {
  dragOver.value = false
  const files = e.dataTransfer.files
  if (files.length > 0) {
    processFile(files[0])
  }
}

const handleFileSelect = (e) => {
  const files = e.target.files
  if (files.length > 0) {
    processFile(files[0])
  }
}

const processFile = (file) => {
  importFile.value = file
  const reader = new FileReader()
  reader.onload = (e) => {
    try {
      const data = new Uint8Array(e.target.result)
      const workbook = XLSX.read(data, { type: 'array' })
      const firstSheetName = workbook.SheetNames[0]
      const worksheet = workbook.Sheets[firstSheetName]
      const json = XLSX.utils.sheet_to_json(worksheet, { header: 1 })

      if (json.length === 0) {
        showSnackbar('File Excel trống', 'error')
        return
      }

      const headers = json[0].map(h => String(h || '').trim().toLowerCase())
      
      const usernameIdx = headers.findIndex(h => h.includes('tên đăng nhập') || h.includes('username') || h.includes('tai khoan') || h.includes('tài khoản'))
      const passwordIdx = headers.findIndex(h => h.includes('mật khẩu') || h.includes('password') || h.includes('mat khau'))
      const fullNameIdx = headers.findIndex(h => h.includes('họ và tên') || h.includes('fullname') || h.includes('ho ten') || h.includes('họ tên') || h.includes('tên'))
      const emailIdx = headers.findIndex(h => h.includes('email') || h.includes('thư điện tử'))
      const phoneIdx = headers.findIndex(h => h.includes('số điện thoại') || h.includes('phone') || h.includes('sđt') || h.includes('sdt'))

      if (usernameIdx === -1 || fullNameIdx === -1) {
        showSnackbar('File Excel thiếu các cột bắt buộc: Tên đăng nhập, Họ và tên', 'error')
        return
      }

      const rows = []
      for (let i = 1; i < json.length; i++) {
        const row = json[i]
        if (!row || row.length === 0) continue

        const username = row[usernameIdx] ? String(row[usernameIdx]).trim() : ''
        const password = (passwordIdx !== -1 && row[passwordIdx]) ? String(row[passwordIdx]).trim() : '123456'
        const fullName = row[fullNameIdx] ? String(row[fullNameIdx]).trim() : ''
        const email = (emailIdx !== -1 && row[emailIdx]) ? String(row[emailIdx]).trim() : ''
        const phone = (phoneIdx !== -1 && row[phoneIdx]) ? String(row[phoneIdx]).trim() : ''

        if (!username && !fullName) continue

        let isValid = true
        let errorMsg = ''

        if (!username) {
          errorMsg = 'Thiếu tên đăng nhập'
          isValid = false
        } else if (username.length < 3) {
          errorMsg = 'Tên đăng nhập tối thiểu 3 ký tự'
          isValid = false
        }

        if (isValid && !fullName) {
          errorMsg = 'Thiếu họ và tên'
          isValid = false
        }

        if (isValid && password && password.length < 6) {
          errorMsg = 'Mật khẩu tối thiểu 6 ký tự'
          isValid = false
        }

        rows.push({
          username,
          password,
          fullName,
          email,
          phone,
          status: errorMsg || 'Sẵn sàng',
          isValid,
          uploadStatus: 'pending',
          feedback: ''
        })
      }

      importRows.value = rows
      if (rows.length === 0) {
        showSnackbar('Không tìm thấy dòng dữ liệu nào hợp lệ', 'warning')
      } else {
        showSnackbar(`Đọc thành công ${rows.length} học viên`, 'success')
      }
    } catch (err) {
      console.error(err)
      showSnackbar('Lỗi đọc file Excel. Vui lòng kiểm tra lại định dạng.', 'error')
    }
  }
  reader.readAsArrayBuffer(file)
}

const startImport = async () => {
  const validRows = importRows.value.filter(r => r.isValid)
  if (validRows.length === 0) {
    showSnackbar('Không có dòng nào hợp lệ để nhập', 'error')
    return
  }

  importing.value = true
  importProgress.value = 0
  let completed = 0

  for (let i = 0; i < importRows.value.length; i++) {
    const row = importRows.value[i]
    if (!row.isValid) continue

    row.uploadStatus = 'uploading'
    row.status = 'Đang xử lý...'

    try {
      // Step 1: Create user account with role 'HocVien'
      const createdUser = await userStore.createUser({
        username: row.username,
        password: row.password,
        fullName: row.fullName,
        email: row.email || null,
        phone: row.phone || null,
        role: 'HocVien'
      })

      // Step 2: Create student profile linked to this userId
      await store.createStudent({
        userId: createdUser.userId,
        fullName: row.fullName,
        dateOfBirth: new Date("2000-01-01").toISOString(),
        gender: 'Nam',
        phone: row.phone || null,
        email: row.email || null,
        address: null
      })

      row.uploadStatus = 'success'
      row.status = 'Thành công'
      row.feedback = ''
    } catch (err) {
      row.uploadStatus = 'error'
      row.status = 'Thất bại'
      row.feedback = err.response?.data?.message || 'Lỗi hệ thống'
    }

    completed++
    importProgress.value = Math.round((completed / validRows.length) * 100)
  }

  importing.value = false
  showSnackbar(`Đã hoàn thành nhập học viên!`, 'success')
  emit('success')
}

const downloadTemplate = () => {
  const headers = ['Tên đăng nhập', 'Mật khẩu', 'Họ và tên', 'Email', 'Số điện thoại']
  const sampleData = [
    ['hocvien_nguyena', '123456', 'Nguyễn Văn A', 'nguyenvana@gmail.com', '0912345678'],
    ['hocvien_lethi', '123456', 'Lê Thị C', 'lethic@gmail.com', '0934567890']
  ]
  
  const content = [headers, ...sampleData].map(row => row.join(',')).join('\n')
  const blob = new Blob([new Uint8Array([0xEF, 0xBB, 0xBF]), content], { type: 'text/csv;charset=utf-8;' })
  const link = document.createElement('a')
  link.href = URL.createObjectURL(blob)
  link.setAttribute('download', 'Template_Import_HocVien.csv')
  document.body.appendChild(link)
  link.click()
  document.body.removeChild(link)
}
</script>

<template>
  <teleport to="body">
    <div v-if="show" class="fixed inset-0 glass-backdrop z-[9999] flex items-center justify-center p-4">
      <div class="bg-white/90 backdrop-blur-[24px] border border-white/50 rounded-xl shadow-[0_20px_40px_rgba(0,31,63,0.12)] max-w-lg w-full overflow-hidden animate-scale-in flex flex-col">
        <!-- Title -->
        <div class="px-6 py-4 border-b border-outline-variant/20 flex items-center justify-between">
          <h3 class="font-title-md text-[18px] font-bold text-primary flex items-center gap-2">
            <span class="material-symbols-outlined text-on-tertiary-container">{{ isEdit ? 'edit' : 'person_add' }}</span>
            {{ isEdit ? 'Cập nhật thông tin học viên' : 'Tạo hồ sơ học viên' }}
          </h3>
          <button @click="$emit('close')" class="text-on-surface-variant hover:text-primary cursor-pointer">
            <span class="material-symbols-outlined">close</span>
          </button>
        </div>

        <!-- Form Body -->
        <div class="p-6 space-y-4 overflow-y-auto max-h-[70vh]">
          <!-- Link to User Account (Only on Create) -->
          <div v-if="!isEdit" class="space-y-1">
            <label class="text-body-sm font-semibold text-primary">Liên kết tài khoản người dùng *</label>
            <div class="relative">
              <select
                v-model="formData.userId"
                class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg appearance-none px-4 py-2.5 text-body-sm text-primary bg-transparent cursor-pointer"
                @change="onUserSelect"
              >
                <option :value="null" disabled>Chọn tài khoản liên kết...</option>
                <option v-for="user in users" :key="user.userId" :value="user.userId">
                  {{ user.fullName }} ({{ user.username }}) - {{ user.email }}
                </option>
              </select>
              <span class="material-symbols-outlined absolute right-3 top-1/2 -translate-y-1/2 text-on-surface-variant pointer-events-none">expand_more</span>
            </div>
            <p class="text-[11px] text-on-surface-variant/80 italic">Chỉ tài khoản có vai trò học viên mới được liên kết.</p>
            <p v-if="validationErrors.userId" class="text-error text-[11px] font-semibold">{{ validationErrors.userId }}</p>
          </div>

          <div v-else class="bg-primary-container/[0.05] border border-primary-container/10 rounded-lg p-3 flex items-center gap-2">
            <span class="material-symbols-outlined text-on-tertiary-container">link</span>
            <span class="text-body-sm text-on-surface-variant">Tài khoản liên kết: <strong>{{ formData.email || 'Học viên' }}</strong></span>
          </div>

          <!-- Full Name -->
          <div class="space-y-1">
            <label class="text-body-sm font-semibold text-primary">Họ và tên *</label>
            <input
              v-model="formData.fullName"
              type="text"
              class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg px-4 py-2.5 text-body-sm text-primary placeholder:text-on-surface-variant/60 focus:outline-none focus:border-on-tertiary-container/30 transition-colors"
              placeholder="Nhập họ và tên học viên"
              maxlength="100"
            />
            <p v-if="validationErrors.fullName" class="text-error text-[11px] font-semibold">{{ validationErrors.fullName }}</p>
          </div>

          <!-- Gender & DoB -->
          <div class="grid grid-cols-2 gap-4">
            <div class="space-y-1">
              <label class="text-body-sm font-semibold text-primary">Giới tính *</label>
              <div class="relative">
                <select
                  v-model="formData.gender"
                  class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg appearance-none px-4 py-2.5 text-body-sm text-primary bg-transparent cursor-pointer"
                >
                  <option value="Nam">Nam</option>
                  <option value="Nữ">Nữ</option>
                  <option value="Khác">Khác</option>
                </select>
                <span class="material-symbols-outlined absolute right-3 top-1/2 -translate-y-1/2 text-on-surface-variant pointer-events-none">expand_more</span>
              </div>
              <p v-if="validationErrors.gender" class="text-error text-[11px] font-semibold">{{ validationErrors.gender }}</p>
            </div>
            <div class="space-y-1">
              <label class="text-body-sm font-semibold text-primary">Ngày sinh *</label>
              <input
                v-model="formData.dateOfBirth"
                type="date"
                class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg px-4 py-2.5 text-body-sm text-primary bg-transparent"
              />
              <p v-if="validationErrors.dateOfBirth" class="text-error text-[11px] font-semibold">{{ validationErrors.dateOfBirth }}</p>
            </div>
          </div>

          <!-- Phone & Email -->
          <div class="grid grid-cols-2 gap-4">
            <div class="space-y-1">
              <label class="text-body-sm font-semibold text-primary">Số điện thoại</label>
              <input
                v-model="formData.phone"
                type="text"
                class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg px-4 py-2.5 text-body-sm text-primary placeholder:text-on-surface-variant/60 focus:outline-none focus:border-on-tertiary-container/30 transition-colors"
                placeholder="Số điện thoại"
              />
            </div>
            <div class="space-y-1">
              <label class="text-body-sm font-semibold text-primary">Email</label>
              <input
                v-model="formData.email"
                type="email"
                class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg px-4 py-2.5 text-body-sm text-primary placeholder:text-on-surface-variant/60 focus:outline-none focus:border-on-tertiary-container/30 transition-colors"
                placeholder="Ví dụ: hocvien@gmail.com"
              />
              <p v-if="validationErrors.email" class="text-error text-[11px] font-semibold">{{ validationErrors.email }}</p>
            </div>
          </div>

          <!-- Address -->
          <div class="space-y-1">
            <label class="text-body-sm font-semibold text-primary">Địa chỉ liên hệ</label>
            <textarea
              v-model="formData.address"
              rows="3"
              class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg px-4 py-2.5 text-body-sm text-primary placeholder:text-on-surface-variant/60 focus:outline-none focus:border-on-tertiary-container/30 transition-colors"
              placeholder="Nhập địa chỉ tạm trú, thường trú..."
              maxlength="500"
            ></textarea>
          </div>
        </div>

        <!-- Footer Actions -->
        <div class="px-6 py-4 border-t border-outline-variant/20 flex justify-end gap-3 bg-surface/30">
          <button
            @click="$emit('close')"
            class="px-5 py-2.5 rounded-lg border border-outline-variant text-on-surface-variant font-semibold text-[13px] hover:bg-white/40 transition-colors cursor-pointer"
          >
            Hủy bỏ
          </button>
          <button
            @click="saveForm"
            :disabled="saving || !isFormValid"
            class="px-5 py-2.5 rounded-lg bg-primary-container text-white font-semibold text-[13px] hover:bg-primary transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-1 cursor-pointer"
          >
            <span v-if="saving" class="animate-spin w-4 h-4 border-2 border-white border-t-transparent rounded-full mr-1"></span>
            {{ isEdit ? 'Cập nhật' : 'Tạo mới' }}
          </button>
        </div>
      </div>
    </div>
  </teleport>
</template>

<script setup>
import { ref, watch, computed, inject } from 'vue'
import { useStudentStore } from '../../../stores'

const props = defineProps({
  show: { type: Boolean, required: true },
  student: { type: Object, default: null },
  users: { type: Array, required: true }
})

const emit = defineEmits(['close', 'success'])
const showSnackbar = inject('showSnackbar')
const store = useStudentStore()

const isEdit = computed(() => !!props.student)
const saving = ref(false)

const formData = ref({
  userId: null,
  fullName: '',
  gender: 'Nam',
  dateOfBirth: '',
  phone: '',
  email: '',
  address: '',
})

const validationErrors = ref({
  userId: '',
  fullName: '',
  gender: '',
  dateOfBirth: '',
  email: '',
})

const isFormValid = computed(() => {
  const isUserValid = isEdit.value || formData.value.userId !== null
  const isNameValid = formData.value.fullName.trim().length > 0 && formData.value.fullName.length <= 100
  const isGenderValid = ['Nam', 'Nữ', 'Khác'].includes(formData.value.gender)
  const isDobValid = formData.value.dateOfBirth !== ''
  const isEmailValid = formData.value.email === '' || /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.value.email)
  
  return isUserValid && isNameValid && isGenderValid && isDobValid && isEmailValid
})

watch(() => props.show, (newVal) => {
  if (newVal) {
    validationErrors.value = { userId: '', fullName: '', gender: '', dateOfBirth: '', email: '' }
    if (props.student) {
      formData.value = {
        studentId: props.student.studentId,
        userId: props.student.userId,
        fullName: props.student.fullName,
        gender: props.student.gender || 'Nam',
        dateOfBirth: props.student.dateOfBirth?.split('T')[0] || '',
        phone: props.student.phone || '',
        email: props.student.email || '',
        address: props.student.address || '',
      }
    } else {
      formData.value = {
        userId: null,
        fullName: '',
        gender: 'Nam',
        dateOfBirth: '',
        phone: '',
        email: '',
        address: '',
      }
    }
  }
})

// Watchers for reactive validation
watch(() => formData.value.userId, (val) => {
  if (!isEdit.value) {
    validationErrors.value.userId = val === null ? 'Vui lòng chọn tài khoản để liên kết' : ''
  }
})

watch(() => formData.value.fullName, (val) => {
  if (val.trim().length === 0) {
    validationErrors.value.fullName = 'Họ tên không được để trống'
  } else if (val.length > 100) {
    validationErrors.value.fullName = 'Họ tên tối đa 100 ký tự'
  } else {
    validationErrors.value.fullName = ''
  }
})

watch(() => formData.value.gender, (val) => {
  validationErrors.value.gender = ['Nam', 'Nữ', 'Khác'].includes(val) ? '' : 'Giới tính không hợp lệ'
})

watch(() => formData.value.dateOfBirth, (val) => {
  validationErrors.value.dateOfBirth = val === '' ? 'Ngày sinh không được để trống' : ''
})

watch(() => formData.value.email, (val) => {
  if (val !== '' && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val)) {
    validationErrors.value.email = 'Email không đúng định dạng'
  } else {
    validationErrors.value.email = ''
  }
})

function onUserSelect() {
  const selectedUser = props.users.find(u => u.userId === formData.value.userId)
  if (selectedUser) {
    formData.value.fullName = selectedUser.fullName || ''
    formData.value.email = selectedUser.email || ''
  }
}

async function saveForm() {
  if (!isFormValid.value) return
  saving.value = true
  try {
    if (isEdit.value) {
      await store.updateStudent(formData.value.studentId, {
        fullName: formData.value.fullName,
        dateOfBirth: new Date(formData.value.dateOfBirth).toISOString(),
        gender: formData.value.gender,
        phone: formData.value.phone || null,
        email: formData.value.email || null,
        address: formData.value.address || null
      })
      showSnackbar('Cập nhật thông tin học viên thành công', 'success')
    } else {
      await store.createStudent({
        userId: formData.value.userId,
        fullName: formData.value.fullName,
        dateOfBirth: new Date(formData.value.dateOfBirth).toISOString(),
        gender: formData.value.gender,
        phone: formData.value.phone || null,
        email: formData.value.email || null,
        address: formData.value.address || null
      })
      showSnackbar('Thêm hồ sơ học viên thành công', 'success')
    }
    emit('success')
  } catch (e) {
    showSnackbar(e.response?.data?.message || 'Lỗi khi lưu hồ sơ học viên', 'error')
  } finally {
    saving.value = false
  }
}
</script>

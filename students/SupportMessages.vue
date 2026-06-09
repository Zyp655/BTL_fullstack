<template>
  <div class="space-y-stack-lg animate-fade-in">
    <!-- Header Section -->
    <section class="flex flex-col md:flex-row md:items-center justify-between gap-4">
      <div class="flex items-center gap-3">
        <div class="w-10 h-10 rounded-lg bg-primary-container/10 flex items-center justify-center">
          <span class="material-symbols-outlined text-primary-container">chat</span>
        </div>
        <div>
          <h2 class="font-headline-lg text-headline-lg text-primary-container tracking-tight">Yêu cầu hỗ trợ & Chuyển lớp</h2>
          <p class="font-body-lg text-body-lg text-on-surface-variant mt-1">Tiếp nhận, phê duyệt yêu cầu chuyển lớp hoặc phản hồi từ học viên.</p>
        </div>
      </div>
    </section>

    <!-- Stats Cards -->
    <section class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-gutter">
      <div v-for="stat in stats" :key="stat.label" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] rounded-xl p-5 flex items-center gap-4 hover:shadow-md hover:-translate-y-0.5 transition-all duration-300">
        <div :class="[stat.bgColor, stat.textColor, 'w-12 h-12 rounded-lg flex items-center justify-center shrink-0']">
          <span class="material-symbols-outlined">{{ stat.icon }}</span>
        </div>
        <div>
          <div class="text-2xl font-bold text-primary">{{ stat.value }}</div>
          <div class="text-label-caps font-label-caps text-on-surface-variant">{{ stat.label }}</div>
        </div>
      </div>
    </section>

    <!-- Filters and Searches -->
    <section class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] rounded-xl p-4 flex flex-col md:flex-row gap-4 items-center justify-between">
      <!-- Search Input -->
      <div class="relative flex-1 w-full max-w-md">
        <span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-on-surface-variant">search</span>
        <input
          v-model="filters.search"
          class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg pl-10 pr-4 py-2.5 text-body-sm text-primary focus:outline-none focus:border-on-tertiary-container focus:ring-2 focus:ring-on-tertiary-container/10 transition-all"
          placeholder="Tìm kiếm theo tên học viên..."
          type="text"
        />
      </div>

      <!-- Status Tabs -->
      <div class="flex space-x-1 p-1 bg-primary-container/[0.05] rounded-lg border border-primary-container/10 w-full md:w-auto overflow-x-auto">
        <button
          v-for="tab in statusTabs"
          :key="tab.value"
          @click="filters.status = tab.value"
          :class="[
            filters.status === tab.value
              ? 'bg-white text-primary-container shadow-sm font-semibold'
              : 'text-on-surface-variant hover:text-primary',
            'px-4 py-1.5 rounded-md text-[12px] transition-all whitespace-nowrap cursor-pointer'
          ]"
        >
          {{ tab.label }}
        </button>
      </div>
    </section>

    <!-- Support Requests List -->
    <section>
      <!-- Loading State -->
      <div v-if="loading" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] rounded-xl p-12 text-center flex flex-col items-center justify-center">
        <div class="w-12 h-12 border-4 border-primary-container/30 border-t-primary-container rounded-full animate-spin mb-4"></div>
        <p class="text-body-sm font-semibold text-primary-container">Đang tải danh sách yêu cầu...</p>
      </div>

      <!-- Data List -->
      <div v-else-if="filteredMessages.length > 0" class="grid grid-cols-1 gap-gutter">
        <div
          v-for="msg in filteredMessages"
          :key="msg.id"
          class="bg-white/70 backdrop-blur-[24px] border border-white/50 rounded-xl p-6 shadow-[0_8px_16px_rgba(0,31,63,0.03)] hover:shadow-lg transition-all flex flex-col justify-between gap-4 border-l-4"
          :class="[
            msg.status === 'Pending' ? 'border-l-amber-500' : 
            msg.status === 'Resolved' ? 'border-l-emerald-500' : 'border-l-rose-500'
          ]"
        >
          <div class="flex flex-col sm:flex-row justify-between items-start gap-3">
            <!-- Left Info: Student details, request details -->
            <div class="space-y-3 flex-1 min-w-0">
              <div class="flex flex-wrap items-center gap-2">
                <span class="font-bold text-primary text-body-lg">{{ msg.studentName }}</span>
                <span class="bg-primary-container/10 text-primary-container rounded-full px-2.5 py-0.5 text-[11px] font-semibold">
                  Mã HV: HV-{{ String(msg.studentId).padStart(4, '0') }}
                </span>
                <span :class="[getStatusClass(msg.status), 'status-badge text-[10px] font-bold']">
                  {{ getStatusLabel(msg.status) }}
                </span>
              </div>

              <!-- Message Body -->
              <div class="bg-surface-container-low/40 p-4 rounded-xl border border-white/30 text-body-sm text-primary font-medium italic">
                "{{ msg.message }}"
              </div>

              <!-- Transfer Class Detail Flow -->
              <div v-if="msg.fromClassId && msg.toClassId" class="flex flex-col sm:flex-row items-stretch sm:items-center gap-3 bg-primary-container/5 p-4 rounded-xl border border-primary-container/10">
                <div class="flex-1 min-w-0">
                  <div class="text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1">Lớp hiện tại</div>
                  <div class="font-semibold text-primary truncate" :title="msg.fromClassName">{{ msg.fromClassName }}</div>
                  <div class="text-body-xs text-on-surface-variant">ID: {{ msg.fromClassId }}</div>
                </div>
                <div class="flex items-center justify-center shrink-0 text-on-tertiary-container">
                  <span class="material-symbols-outlined text-[28px] rotate-90 sm:rotate-0">double_arrow</span>
                </div>
                <div class="flex-1 min-w-0">
                  <div class="text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1">Lớp đăng ký chuyển</div>
                  <div class="font-semibold text-primary truncate" :title="msg.toClassName">{{ msg.toClassName }}</div>
                  <div class="text-body-xs text-on-surface-variant">ID: {{ msg.toClassId }}</div>
                </div>
              </div>

              <!-- Rejection Reason (Admin Response) -->
              <div v-if="msg.status === 'Rejected' && msg.adminResponse" class="bg-rose-500/5 p-4 rounded-xl border border-rose-500/10 text-body-sm text-error flex items-start gap-2">
                <span class="material-symbols-outlined text-[18px] text-rose-600 mt-0.5 shrink-0">info</span>
                <div>
                  <div class="text-[10px] font-bold text-rose-700 uppercase tracking-wider mb-0.5">Lý do từ chối</div>
                  <div class="font-medium text-rose-900">{{ msg.adminResponse }}</div>
                </div>
              </div>
            </div>

            <!-- Right Info: Timestamp -->
            <div class="text-right shrink-0 text-body-xs text-on-surface-variant flex flex-row sm:flex-col gap-2 items-center sm:items-end self-end sm:self-start">
              <span class="material-symbols-outlined text-[16px]">schedule</span>
              <span>{{ formatDateTime(msg.createdAt) }}</span>
            </div>
          </div>

          <!-- Bottom: Action Buttons for Pending request -->
          <div v-if="msg.status === 'Pending'" class="flex justify-end gap-3 pt-3 border-t border-white/20">
            <button
              @click="rejectRequest(msg)"
              :disabled="actioningId === msg.id"
              class="px-4 py-2 rounded-lg border border-error/30 hover:bg-error/10 text-error font-semibold text-[13px] transition-colors flex items-center gap-1 cursor-pointer active:scale-95 disabled:opacity-50"
            >
              <span class="material-symbols-outlined text-[18px]">close</span>
              Từ chối
            </button>
            <button
              @click="approveRequest(msg)"
              :disabled="actioningId === msg.id"
              class="px-4 py-2 rounded-lg bg-primary-container text-white font-semibold text-[13px] hover:bg-primary hover:shadow-sm transition-all flex items-center gap-1 cursor-pointer active:scale-95 disabled:opacity-50"
            >
              <span v-if="actioningId === msg.id" class="animate-spin w-4 h-4 border-2 border-white border-t-transparent rounded-full mr-1"></span>
              <span v-else class="material-symbols-outlined text-[18px]">done</span>
              Phê duyệt &amp; Chuyển lớp
            </button>
          </div>
        </div>
      </div>

      <!-- Empty State -->
      <div v-else class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] rounded-xl p-12 text-center flex flex-col items-center justify-center">
        <span class="material-symbols-outlined text-primary/30 text-[64px] mb-4">chat_bubble</span>
        <h3 class="font-title-md text-title-md font-bold text-primary mt-2">Không tìm thấy yêu cầu nào</h3>
        <p class="text-body-sm text-on-surface-variant mt-2">
          Hiện tại không có yêu cầu nào trùng khớp với bộ lọc của bạn.
        </p>
      </div>
    </section>

    <!-- Dialog: Admin chuyển lớp học cho học viên khi thiếu thông tin -->
    <teleport to="body">
      <div v-if="transferDialog" class="fixed inset-0 glass-backdrop z-[9999] flex items-center justify-center p-4">
        <div class="bg-white/90 backdrop-blur-[20px] border border-white/40 shadow-2xl max-w-md w-full rounded-2xl overflow-hidden animate-scale-in flex flex-col">
          <div class="px-6 py-4 border-b border-white/40 flex items-center justify-between">
            <h3 class="font-title-md text-title-md font-bold text-primary-container flex items-center gap-2">
              <span class="material-symbols-outlined text-on-tertiary-container">sync_alt</span>
              Chuyển lớp &amp; Phê duyệt
            </h3>
            <button @click="transferDialog = false" class="text-on-surface-variant hover:text-on-surface transition-colors cursor-pointer">
              <span class="material-symbols-outlined">close</span>
            </button>
          </div>

          <div class="p-6 space-y-4">
            <div class="bg-primary-container/5 p-4 rounded-xl border border-primary-container/10 text-body-sm space-y-1">
              <div>Học viên: <span class="font-bold text-primary-container">{{ transferStudentName }}</span></div>
              <div>Mã HV: <span class="font-semibold text-primary-container">HV-{{ String(transferStudentId).padStart(4, '0') }}</span></div>
            </div>

            <!-- Select Source Class (From Class) -->
            <div class="space-y-1">
              <label class="text-body-sm font-semibold text-on-surface">Lớp học hiện tại *</label>
              <div v-if="loadingEnrollments" class="flex items-center justify-center py-2 gap-2">
                <span class="animate-spin w-4 h-4 border-2 border-primary border-t-transparent rounded-full"></span>
                <span class="text-body-xs text-on-surface-variant">Đang tải danh sách lớp học...</span>
              </div>
              <div v-else-if="!transferFromClassId && studentEnrollments.length === 0" class="text-error text-body-sm font-semibold py-2">
                Học viên hiện tại không tham gia lớp học hoạt động nào.
              </div>
              <div v-else class="relative">
                <select
                  v-model="transferFromClassId"
                  :disabled="loadingEnrollments"
                  @change="handleFromClassChange"
                  class="w-full bg-white border border-outline-variant/60 rounded-lg appearance-none px-4 py-2.5 text-body-sm text-on-surface cursor-pointer focus:outline-none focus:ring-2 focus:ring-primary/20 transition-all"
                >
                  <option :value="null" disabled>-- Chọn lớp học nguồn --</option>
                  <option v-for="e in studentEnrollments" :key="e.classId" :value="e.classId">
                    {{ e.className }} (Khóa: {{ e.courseName || e.CourseName }})
                  </option>
                </select>
                <span class="material-symbols-outlined absolute right-3 top-1/2 -translate-y-1/2 text-on-surface-variant pointer-events-none">expand_more</span>
              </div>
            </div>

            <!-- Select Target Class (To Class) -->
            <div class="space-y-1" v-if="transferFromClassId">
              <label class="text-body-sm font-semibold text-on-surface">Chọn lớp học thay thế *</label>
              <div v-if="loadingAlternativeClasses" class="flex items-center justify-center py-2 gap-2">
                <span class="animate-spin w-4 h-4 border-2 border-primary border-t-transparent rounded-full"></span>
                <span class="text-body-xs text-on-surface-variant">Đang tìm lớp học...</span>
              </div>
              <div v-else-if="alternativeClasses.length === 0" class="text-error text-body-sm font-semibold py-2">
                Không tìm thấy lớp học khác nào hoạt động cho khóa này.
              </div>
              <div v-else class="relative">
                <select
                  v-model="transferToClassId"
                  class="w-full bg-white border border-outline-variant/60 rounded-lg appearance-none px-4 py-2.5 text-body-sm text-on-surface cursor-pointer focus:outline-none focus:ring-2 focus:ring-primary/20 transition-all"
                >
                  <option :value="null" disabled>-- Chọn lớp học mới --</option>
                  <option v-for="c in alternativeClasses" :key="c.classId" :value="c.classId">
                    {{ c.className }} (Sĩ số: {{ c.currentStudents }}/{{ c.maxStudents }} - Phòng: {{ c.room || 'N/A' }})
                  </option>
                </select>
                <span class="material-symbols-outlined absolute right-3 top-1/2 -translate-y-1/2 text-on-surface-variant pointer-events-none">expand_more</span>
              </div>
            </div>

            <!-- Schedule info of the selected target class to check for conflicts -->
            <div v-if="selectedAlternativeClassSchedules.length > 0" class="bg-on-tertiary-container/5 p-4 rounded-xl border border-on-tertiary-container/10">
              <div class="text-[11px] font-bold text-on-tertiary-container uppercase tracking-wide mb-1.5">Lịch học lớp mới:</div>
              <div class="space-y-1 text-body-xs font-semibold text-primary-container">
                <div v-for="s in selectedAlternativeClassSchedules" :key="s.scheduleId">
                  • Thứ {{ formatDayOfWeek(s.dayOfWeek) }} ({{ s.startTime.substring(0,5) }} - {{ s.endTime.substring(0,5) }})
                </div>
              </div>
            </div>
          </div>

          <div class="px-6 py-4 border-t border-white/40 flex justify-end gap-3 bg-white/20">
            <button
              @click="transferDialog = false"
              class="px-5 py-2.5 rounded-lg bg-transparent border border-outline-variant text-on-surface-variant font-semibold text-body-sm hover:bg-surface-container-high transition-colors cursor-pointer"
            >
              Hủy
            </button>
            <button
              @click="submitTransfer"
              :disabled="submittingTransfer || !transferFromClassId || !transferToClassId"
              class="px-5 py-2.5 rounded-lg bg-primary-container text-white font-semibold text-body-sm hover:bg-primary transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-1 cursor-pointer active:scale-95"
            >
              <span v-if="submittingTransfer" class="animate-spin w-4 h-4 border-2 border-white border-t-transparent rounded-full mr-1"></span>
              Xác nhận &amp; Chuyển lớp
            </button>
          </div>
        </div>
      </div>
    </teleport>

    <!-- Dialog: Admin nhập lý do từ chối -->
    <teleport to="body">
      <div v-if="rejectDialog" class="fixed inset-0 glass-backdrop z-[9999] flex items-center justify-center p-4">
        <div class="bg-white/90 backdrop-blur-[20px] border border-white/40 shadow-2xl max-w-md w-full rounded-2xl overflow-hidden animate-scale-in flex flex-col">
          <div class="px-6 py-4 border-b border-white/40 flex items-center justify-between">
            <h3 class="font-title-md text-title-md font-bold text-error flex items-center gap-2">
              <span class="material-symbols-outlined text-[24px]">cancel</span>
              Từ chối yêu cầu hỗ trợ
            </h3>
            <button @click="rejectDialog = false" class="text-on-surface-variant hover:text-on-surface transition-colors cursor-pointer">
              <span class="material-symbols-outlined">close</span>
            </button>
          </div>

          <div class="p-6 space-y-4">
            <div class="bg-error/5 p-4 rounded-xl border border-error/10 text-body-sm space-y-1">
              <div>Học viên: <span class="font-bold text-primary-container">{{ rejectStudentName }}</span></div>
              <div>Yêu cầu: <span class="italic text-on-surface-variant">"{{ rejectMessageText }}"</span></div>
            </div>

            <!-- Input Rejection Reason -->
            <div class="space-y-1">
              <label class="text-body-sm font-semibold text-on-surface">Lý do từ chối *</label>
              <textarea
                v-model="rejectReason"
                rows="4"
                placeholder="Nhập lý do từ chối chi tiết..."
                class="w-full bg-white border border-outline-variant/60 rounded-lg px-4 py-2.5 text-body-sm text-on-surface focus:outline-none focus:ring-2 focus:ring-primary/20 transition-all resize-none"
              ></textarea>
            </div>
          </div>

          <div class="px-6 py-4 border-t border-white/40 flex justify-end gap-3 bg-white/20">
            <button
              @click="rejectDialog = false"
              class="px-5 py-2.5 rounded-lg bg-transparent border border-outline-variant text-on-surface-variant font-semibold text-body-sm hover:bg-surface-container-high transition-colors cursor-pointer"
            >
              Hủy
            </button>
            <button
              @click="submitReject"
              :disabled="submittingReject || !rejectReason.trim()"
              class="px-5 py-2.5 rounded-lg bg-error text-white font-semibold text-body-sm hover:bg-error/90 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-1 cursor-pointer active:scale-95"
            >
              <span v-if="submittingReject" class="animate-spin w-4 h-4 border-2 border-white border-t-transparent rounded-full mr-1"></span>
              Xác nhận từ chối
            </button>
          </div>
        </div>
      </div>
    </teleport>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, onUnmounted, inject, watch } from 'vue'
import api from '../../services/api'
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import { useAuthStore } from '../../stores'

const showSnackbar = inject('showSnackbar')

const loading = ref(true)
const messages = ref([])
const actioningId = ref(null)

// Admin Transfer Dialog State
const transferDialog = ref(false)
const transferStudentId = ref(null)
const transferStudentName = ref('')
const transferFromClassId = ref(null)
const transferToClassId = ref(null)
const transferActiveMessageId = ref(null)
const studentEnrollments = ref([])
const loadingEnrollments = ref(false)
const alternativeClasses = ref([])
const loadingAlternativeClasses = ref(false)
const selectedAlternativeClassSchedules = ref([])
const submittingTransfer = ref(false)
const selectedCourseId = ref(null)

// Admin Reject Dialog State
const rejectDialog = ref(false)
const rejectStudentName = ref('')
const rejectMessageText = ref('')
const rejectReason = ref('')
const rejectActiveMessageId = ref(null)
const submittingReject = ref(false)

const filters = ref({
  search: '',
  status: 'All'
})

const statusTabs = [
  { label: 'Tất cả', value: 'All' },
  { label: 'Chờ duyệt', value: 'Pending' },
  { label: 'Đã phê duyệt', value: 'Resolved' },
  { label: 'Đã từ chối', value: 'Rejected' }
]

const stats = computed(() => {
  return [
    {
      label: 'Tổng yêu cầu',
      value: messages.value.length,
      icon: 'forum',
      bgColor: 'bg-primary-container/10',
      textColor: 'text-primary-container'
    },
    {
      label: 'Chờ xử lý',
      value: messages.value.filter(m => m.status === 'Pending').length,
      icon: 'pending_actions',
      bgColor: 'bg-amber-500/10',
      textColor: 'text-amber-600'
    },
    {
      label: 'Đã phê duyệt',
      value: messages.value.filter(m => m.status === 'Resolved').length,
      icon: 'done_all',
      bgColor: 'bg-emerald-500/10',
      textColor: 'text-emerald-600'
    },
    {
      label: 'Đã từ chối',
      value: messages.value.filter(m => m.status === 'Rejected').length,
      icon: 'cancel',
      bgColor: 'bg-rose-500/10',
      textColor: 'text-rose-600'
    }
  ]
})

const filteredMessages = computed(() => {
  let list = messages.value

  // Status Filter
  if (filters.value.status !== 'All') {
    list = list.filter(m => m.status === filters.value.status)
  }

  // Search filter (by studentName)
  const q = filters.value.search.trim().toLowerCase()
  if (q) {
    list = list.filter(m => m.studentName.toLowerCase().includes(q))
  }

  return list
})

async function fetchMessages() {
  loading.value = true
  try {
    const res = await api.get('/api/v1/support-messages')
    messages.value = res.data || []
  } catch (e) {
    console.error('Error fetching support messages:', e)
    showSnackbar('Lỗi tải danh sách yêu cầu hỗ trợ', 'error')
  } finally {
    loading.value = false
  }
}

async function approveRequest(msg) {
  if (msg.fromClassId && msg.toClassId) {
    actioningId.value = msg.id
    try {
      await api.post(`/api/v1/support-messages/${msg.id}/resolve`)
      showSnackbar(`Đã phê duyệt và chuyển lớp thành công cho học viên ${msg.studentName}!`, 'success')
      await fetchMessages()
    } catch (e) {
      console.error(e)
      showSnackbar(e.response?.data?.message || 'Có lỗi xảy ra khi phê duyệt', 'error')
    } finally {
      actioningId.value = null
    }
  } else {
    openAdminTransferModal(msg)
  }
}

async function openAdminTransferModal(msg) {
  transferActiveMessageId.value = msg.id
  transferStudentId.value = msg.studentId
  transferStudentName.value = msg.studentName
  transferFromClassId.value = msg.fromClassId || null
  transferToClassId.value = msg.toClassId || null
  studentEnrollments.value = []
  alternativeClasses.value = []
  selectedAlternativeClassSchedules.value = []
  selectedCourseId.value = null
  
  transferDialog.value = true
  
  if (msg.fromClassId) {
    loadingEnrollments.value = true
    try {
      const classRes = await api.get(`/api/v1/classes/${msg.fromClassId}`)
      const cls = classRes.data
      if (cls) {
        studentEnrollments.value = [{
          classId: cls.classId,
          className: cls.className,
          courseName: cls.courseName,
          CourseName: cls.courseName,
          courseId: cls.courseId
        }]
        selectedCourseId.value = cls.courseId
        await fetchAlternativeClasses(cls.courseId, msg.fromClassId)
      }
    } catch (e) {
      console.error(e)
      showSnackbar('Lỗi khi tải thông tin lớp học hiện tại', 'error')
    } finally {
      loadingEnrollments.value = false
    }
  } else {
    loadingEnrollments.value = true
    try {
      const res = await api.get(`/api/v1/students/${msg.studentId}/enrollments`)
      studentEnrollments.value = (res.data || []).filter(e => e.classId > 0 && (e.status === 'Active' || e.status === 'DangHoc'))
    } catch (e) {
      console.error(e)
      showSnackbar('Lỗi khi tải danh sách lớp học của học viên', 'error')
    } finally {
      loadingEnrollments.value = false
    }
  }
}

async function fetchAlternativeClasses(courseId, fromClassId) {
  loadingAlternativeClasses.value = true
  try {
    const res = await api.get('/api/v1/classes', {
      params: { courseId: courseId, pageSize: 100 }
    })
    alternativeClasses.value = (res.data?.items || []).filter(
      c => c.classId !== fromClassId && (c.status === 'Opened' || c.status === 'InProgress')
    )
  } catch (e) {
    console.error(e)
    showSnackbar('Lỗi tải danh sách lớp học thay thế', 'error')
  } finally {
    loadingAlternativeClasses.value = false
  }
}

async function handleFromClassChange() {
  transferToClassId.value = null
  selectedAlternativeClassSchedules.value = []
  alternativeClasses.value = []
  
  if (!transferFromClassId.value) {
    selectedCourseId.value = null
    return
  }
  
  const chosenEnrollment = studentEnrollments.value.find(e => e.classId === transferFromClassId.value)
  const courseId = chosenEnrollment ? (chosenEnrollment.courseId || chosenEnrollment.CourseId) : null
  
  if (courseId) {
    selectedCourseId.value = courseId
    await fetchAlternativeClasses(courseId, transferFromClassId.value)
  } else {
    loadingAlternativeClasses.value = true
    try {
      const classRes = await api.get(`/api/v1/classes/${transferFromClassId.value}`)
      const cId = classRes.data?.courseId
      selectedCourseId.value = cId
      if (cId) {
        await fetchAlternativeClasses(cId, transferFromClassId.value)
      }
    } catch (e) {
      console.error(e)
      showSnackbar('Lỗi tải thông tin lớp học', 'error')
    } finally {
      loadingAlternativeClasses.value = false
    }
  }
}

watch(transferToClassId, async (newClassId) => {
  if (!newClassId) {
    selectedAlternativeClassSchedules.value = []
    return
  }
  try {
    const res = await api.get(`/api/v1/classes/${newClassId}/schedules`)
    selectedAlternativeClassSchedules.value = res.data || []
  } catch (e) {
    console.error('Error fetching schedules:', e)
    selectedAlternativeClassSchedules.value = []
  }
})

async function submitTransfer() {
  if (!transferFromClassId.value || !transferToClassId.value || !transferStudentId.value || !transferActiveMessageId.value) return
  submittingTransfer.value = true
  try {
    await api.post('/api/v1/enrollments/transfer', {
      studentId: transferStudentId.value,
      fromClassId: transferFromClassId.value,
      toClassId: transferToClassId.value
    })
    
    await api.post(`/api/v1/support-messages/${transferActiveMessageId.value}/resolve`)
    
    showSnackbar('Phê duyệt và chuyển lớp thành công cho học viên!', 'success')
    transferDialog.value = false
    await fetchMessages()
  } catch (e) {
    console.error(e)
    showSnackbar(e.response?.data?.message || 'Có lỗi xảy ra khi phê duyệt & chuyển lớp', 'error')
  } finally {
    submittingTransfer.value = false
  }
}

function formatDayOfWeek(day) {
  const map = { 1: 'Hai', 2: 'Ba', 3: 'Tư', 4: 'Năm', 5: 'Sáu', 6: 'Bảy', 0: 'Chủ Nhật' }
  return map[day] || day
}

function rejectRequest(msg) {
  rejectActiveMessageId.value = msg.id
  rejectStudentName.value = msg.studentName
  rejectMessageText.value = msg.message
  rejectReason.value = ''
  rejectDialog.value = true
}

async function submitReject() {
  if (!rejectActiveMessageId.value || !rejectReason.value.trim()) return
  submittingReject.value = true
  try {
    await api.post(`/api/v1/support-messages/${rejectActiveMessageId.value}/reject`, {
      adminResponse: rejectReason.value.trim()
    })
    showSnackbar(`Đã từ chối yêu cầu của học viên ${rejectStudentName.value}`, 'success')
    rejectDialog.value = false
    await fetchMessages()
  } catch (e) {
    console.error(e)
    showSnackbar(e.response?.data?.message || 'Có lỗi xảy ra khi từ chối', 'error')
  } finally {
    submittingReject.value = false
  }
}

function getStatusClass(status) {
  const map = {
    Pending: 'status-inprogress', // yellow badge
    Resolved: 'status-completed', // green badge
    Rejected: 'status-cancelled'   // red badge
  }
  return map[status] || 'status-inprogress'
}

function getStatusLabel(status) {
  const map = {
    Pending: 'Chờ duyệt',
    Resolved: 'Đã duyệt',
    Rejected: 'Đã từ chối'
  }
  return map[status] || status
}

function formatDateTime(dateStr) {
  if (!dateStr) return ''
  const date = new Date(dateStr)
  return date.toLocaleString('vi-VN', {
    hour: '2-digit',
    minute: '2-digit',
    day: '2-digit',
    month: '2-digit',
    year: 'numeric'
  })
}

const authStore = useAuthStore()
let signalrConnection = null

function setupSignalR() {
  if (signalrConnection) return
  
  const token = authStore.token || authStore.currentUser?.token
  if (!token) return
  
  signalrConnection = new HubConnectionBuilder()
    .withUrl('/hubs/support', {
      accessTokenFactory: () => token
    })
    .withAutomaticReconnect()
    .configureLogging(LogLevel.Warning)
    .build()
    
  signalrConnection.on('SupportMessageCreated', async (msg) => {
    try {
      showSnackbar(`Học viên ${msg.studentName} vừa gửi yêu cầu hỗ trợ mới!`, 'info')
      await fetchMessages()
    } catch (err) {
      console.error('Error handling WebSocket message creation:', err)
    }
  })
  
  signalrConnection.start()
    .then(() => console.log('Admin connected to SupportHub WebSocket!'))
    .catch(err => console.error('Error starting SupportHub connection:', err))
}

onUnmounted(() => {
  if (signalrConnection) {
    signalrConnection.stop()
    signalrConnection = null
  }
})

onMounted(async () => {
  await fetchMessages()
  setupSignalR()
})
</script>

<style scoped>
@keyframes fadeIn {
  from { opacity: 0; transform: translateY(8px); }
  to { opacity: 1; transform: translateY(0); }
}
.animate-fade-in {
  animation: fadeIn 0.3s ease-out forwards;
}
</style>

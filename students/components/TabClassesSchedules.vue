<template>
  <div class="space-y-4">
    <!-- Schedule Conflict Warning Banner -->
    <div v-if="currentConflicts.length > 0" class="bg-error/10 text-error p-4 rounded-xl border border-error/20 flex flex-col gap-3 shadow-sm animate-scale-in">
      <div class="flex items-center gap-2 font-bold text-body-lg">
        <span class="material-symbols-outlined text-[24px]">warning</span>
        Cảnh báo trùng lịch học!
      </div>
      <div class="space-y-3 text-body-sm font-semibold">
        <div v-for="(conf, idx) in currentConflicts" :key="idx" class="flex flex-col md:flex-row md:items-center justify-between gap-3 p-3 bg-white/40 border border-error/10 rounded-xl">
          <div class="min-w-0">
            • Lớp <strong class="text-primary-container">{{ conf.classA.className }}</strong> và lớp <strong class="text-primary-container">{{ conf.classB.className }}</strong> bị trùng lịch vào 
            <strong>Thứ {{ formatDayOfWeek(conf.sessionA.dayOfWeek) }} ({{ conf.sessionA.startTime.substring(0,5) }} - {{ conf.sessionA.endTime.substring(0,5) }})</strong>.
          </div>
          <div class="flex flex-wrap items-center gap-2 shrink-0">
            <button
              @click="$emit('open-support-conflict', { targetClass: conf.classA, conflictClass: conf.classB })"
              class="px-3 py-1.5 rounded-lg bg-error hover:bg-error/95 text-white font-semibold text-[12px] transition-all flex items-center gap-1 cursor-pointer active:scale-95 shadow-sm"
            >
              <span class="material-symbols-outlined text-[16px]">sync_alt</span>
              Đổi lớp {{ conf.classA.className }}
            </button>
            <button
              @click="$emit('open-support-conflict', { targetClass: conf.classB, conflictClass: conf.classA })"
              class="px-3 py-1.5 rounded-lg bg-error hover:bg-error/95 text-white font-semibold text-[12px] transition-all flex items-center gap-1 cursor-pointer active:scale-95 shadow-sm"
            >
              <span class="material-symbols-outlined text-[16px]">sync_alt</span>
              Đổi lớp {{ conf.classB.className }}
            </button>
          </div>
        </div>
      </div>
      <p class="text-body-xs font-semibold text-on-surface-variant">
        Bạn bị trùng lịch học giữa các lớp. Hãy chọn đổi một trong hai lớp học bị trùng ở trên để gửi tin nhắn hỗ trợ đổi lớp tới Admin.
      </p>
    </div>

    <div class="grid grid-cols-1 md:grid-cols-2 gap-gutter">
      <!-- Class List -->
      <div class="space-y-3">
        <div class="flex justify-between items-center">
          <h3 class="font-title-md text-body-lg font-semibold text-primary-container flex items-center gap-2">
            <span class="material-symbols-outlined text-[20px] text-on-tertiary-container">school</span>
            Lớp học đã đăng ký
          </h3>
          <button
            v-if="authStore.isAdmin"
            @click="$emit('open-transfer-modal')"
            class="px-3 py-1.5 rounded-lg bg-primary-container/10 hover:bg-primary-container/20 text-primary-container font-semibold text-[12px] transition-all flex items-center gap-1.5 cursor-pointer active:scale-95 shrink-0"
          >
            <span class="material-symbols-outlined text-[18px]">swap_horiz</span>
            Chuyển lớp
          </button>
        </div>
        
        <div v-if="enrolledClasses.length === 0" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-8 text-center rounded-xl text-on-surface-variant">
          Bạn chưa đăng ký tham gia lớp học nào.
        </div>

        <div
          v-for="cls in enrolledClasses"
          :key="cls.enrollmentId"
          @click="$emit('select-class', cls)"
          :class="[
            selectedClass?.classId === cls.classId ? 'border-on-tertiary-container bg-white/60 shadow-sm' : 'border-white/40 hover:bg-white/40',
            'bg-white/70 backdrop-blur-[20px] p-4 rounded-xl border transition-all cursor-pointer flex justify-between items-center'
          ]"
        >
          <div class="space-y-1 min-w-0 flex-1 mr-3">
            <div class="font-semibold text-primary-container text-body-lg truncate" :title="cls.className">{{ cls.className }}</div>
            <div class="text-body-sm text-on-surface-variant truncate" :title="cls.courseName">Khóa học: {{ cls.courseName }}</div>
            <div class="text-body-sm text-on-surface-variant/80">Ngày đăng ký: {{ formatDate(cls.enrolledAt) }}</div>
          </div>
          <div class="flex flex-col items-end gap-1.5 shrink-0">
            <select
              v-if="authStore.isAdmin"
              :value="cls.status"
              @change="$emit('change-enrollment-status', { cls, newStatus: $event.target.value })"
              @click.stop
              class="bg-white/80 border border-outline-variant/30 rounded px-2 py-0.5 text-[11px] font-semibold text-primary cursor-pointer focus:outline-none"
            >
              <option value="DangHoc">Đang học</option>
              <option value="DaXong">Hoàn thành</option>
              <option value="BoHoc">Đã bỏ học</option>
              <option value="PendingPayment">Chờ thanh toán</option>
              <option value="Pending">Chờ duyệt</option>
            </select>
            <span v-else :class="[getEnrollmentStatusClass(cls.status), 'status-badge text-[10px] font-bold']">
              {{ getEnrollmentStatusLabel(cls.status) }}
            </span>
            <span v-if="isClassUnpaid(cls.classId)" class="text-body-sm text-error flex items-center gap-1 font-semibold">
              <span class="material-symbols-outlined text-[16px]">lock</span>
              Chưa đóng học phí
            </span>
            <span v-else class="text-body-sm text-on-tertiary-container flex items-center font-semibold">
              Xem lịch học
              <span class="material-symbols-outlined text-[16px]">chevron_right</span>
            </span>
          </div>
        </div>
      </div>

      <!-- Schedule Viewer for selected class -->
      <div class="space-y-3">
        <h3 class="font-title-md text-body-lg font-semibold text-primary-container flex items-center gap-2">
          <span class="material-symbols-outlined text-[20px] text-on-tertiary-container">calendar_today</span>
          Lịch học chi tiết {{ selectedClass ? `— ${selectedClass.className}` : '' }}
        </h3>

        <div v-if="!selectedClass" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-12 text-center rounded-xl text-on-surface-variant flex flex-col items-center justify-center">
          <span class="material-symbols-outlined text-[48px] text-primary-container/30 mb-2">click_to_shrink</span>
          Chọn một lớp bên trái để xem thời khóa biểu chi tiết.
        </div>

        <!-- Lock schedule if class tuition is unpaid -->
        <div v-else-if="isClassUnpaid(selectedClass.classId)" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-8 text-center rounded-xl text-error flex flex-col items-center justify-center gap-3">
          <span class="material-symbols-outlined text-[48px] text-error/40 animate-pulse">lock</span>
          <div class="font-semibold text-body-lg text-primary-container">Lịch học đã bị khóa</div>
          <p class="text-body-sm text-on-surface-variant max-w-xs mx-auto">
            Bạn chưa hoàn thành đóng học phí cho lớp học này. Vui lòng thanh toán học phí tại tab "Học phí & Thanh toán" để mở khóa xem lịch học.
          </p>
          <button @click="$emit('switch-tab', 'payments')" class="mt-2 px-4 py-2 rounded-lg bg-primary-container text-white font-semibold text-body-sm hover:bg-primary transition-colors flex items-center gap-1.5 cursor-pointer active:scale-95">
            <span class="material-symbols-outlined text-[18px]">receipt_long</span>
            Đóng học phí ngay
          </button>
        </div>

        <div v-else-if="loadingSchedules" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-12 text-center rounded-xl text-primary-container flex flex-col items-center justify-center">
          <div class="w-8 h-8 border-2 border-primary-container/30 border-t-primary-container rounded-full animate-spin mb-2"></div>
          Đang tải lịch học...
        </div>

        <div v-else-if="schedules.length === 0" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-8 text-center rounded-xl text-on-surface-variant">
          Không tìm thấy thời khóa biểu cho lớp học này.
        </div>

        <div v-else class="space-y-2 max-h-[400px] overflow-y-auto pr-1">
          <div
            v-for="schedule in schedules"
            :key="schedule.scheduleId"
            class="bg-white/70 backdrop-blur-[20px] border border-white/40 p-3.5 rounded-xl flex justify-between items-center hover:bg-white/50 transition-colors"
          >
            <div class="space-y-1">
              <div class="font-semibold text-primary-container text-body-lg">Thứ {{ formatDayOfWeek(schedule.dayOfWeek) }}</div>
              <div class="text-body-sm text-on-surface-variant">
                Phòng học: <span class="font-semibold text-primary-container">{{ schedule.classroom }}</span>
              </div>
            </div>
            <div class="text-right">
              <div class="font-semibold text-on-tertiary-container text-body-sm bg-on-tertiary-container/10 px-2.5 py-1 rounded-lg">
                {{ schedule.startTime.substring(0, 5) }} - {{ schedule.endTime.substring(0, 5) }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Support and Transfer Requests List for Student -->
    <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-6 rounded-xl space-y-4">
      <div class="flex justify-between items-center border-b border-white/40 pb-2">
        <h4 class="font-title-md text-body-lg font-bold text-primary-container flex items-center gap-2">
          <span class="material-symbols-outlined text-[20px] text-on-tertiary-container">forum</span>
          Yêu cầu hỗ trợ &amp; đổi lớp đã gửi
        </h4>
        <button
          @click="$emit('open-support-dialog')"
          class="px-3 py-1.5 rounded-lg bg-primary-container/10 hover:bg-primary-container/20 text-primary-container font-semibold text-[12px] transition-colors flex items-center gap-1 cursor-pointer active:scale-95"
        >
          <span class="material-symbols-outlined text-[16px]">add</span>
          Gửi yêu cầu mới
        </button>
      </div>
      <div v-if="mySupportMessages.length === 0" class="text-center py-4 text-body-sm text-on-surface-variant font-medium">
        Bạn chưa gửi yêu cầu hỗ trợ nào.
      </div>
      <div v-else class="space-y-3 max-h-[250px] overflow-y-auto pr-1">
        <div v-for="msg in mySupportMessages" :key="msg.id" class="bg-white/50 p-4 rounded-xl border border-white/40 flex flex-col sm:flex-row justify-between gap-3 text-body-sm">
          <div class="space-y-2 flex-1">
            <div class="flex items-center gap-2 flex-wrap">
              <span :class="[getStatusClass(msg.status), 'status-badge text-[10px] font-bold']">
                {{ getStatusLabel(msg.status) }}
              </span>
              <span class="text-body-xs text-on-surface-variant">{{ formatDateTime(msg.createdAt) }}</span>
            </div>
            <div class="text-primary font-medium italic">"{{ msg.message }}"</div>
            <div v-if="msg.fromClassId && msg.toClassId" class="text-body-xs bg-primary-container/5 px-2.5 py-1.5 rounded-lg border border-primary-container/10 inline-flex items-center gap-1.5">
              <strong>{{ msg.fromClassName }}</strong> 
              <span class="material-symbols-outlined text-[14px]">arrow_forward</span> 
              <strong>{{ msg.toClassName }}</strong>
            </div>
            <!-- Rejection Reason (Admin Response) -->
            <div v-if="msg.status === 'Rejected' && msg.adminResponse" class="mt-2 bg-error/5 p-3 rounded-lg border border-error/10 text-body-xs text-error flex items-start gap-2">
              <span class="material-symbols-outlined text-[16px] mt-0.5 shrink-0">info</span>
              <div>
                <span class="font-bold">Lý do từ chối:</span> {{ msg.adminResponse }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useAuthStore } from '../../../stores'

const authStore = useAuthStore()

const props = defineProps({
  enrolledClasses: { type: Array, required: true },
  selectedClass: { type: Object, default: null },
  schedules: { type: Array, required: true },
  loadingSchedules: { type: Boolean, default: false },
  payments: { type: Array, required: true },
  mySupportMessages: { type: Array, required: true },
  currentConflicts: { type: Array, required: true }
})

defineEmits([
  'select-class',
  'open-support-conflict',
  'open-transfer-modal',
  'change-enrollment-status',
  'open-support-dialog',
  'switch-tab'
])

function isClassUnpaid(classId) {
  if (!props.payments) return false
  const payment = props.payments.find(p => p.classId === classId)
  return payment && payment.status === 'ChuaTT'
}

function formatDate(dateStr) {
  if (!dateStr) return '—'
  const date = new Date(dateStr)
  return date.toLocaleDateString('vi-VN')
}

function formatDateTime(dateStr) {
  if (!dateStr) return '—'
  const date = new Date(dateStr)
  return date.toLocaleString('vi-VN')
}

function formatDayOfWeek(day) {
  const map = { 1: 'Chủ Nhật', 2: 'Hai', 3: 'Ba', 4: 'Tư', 5: 'Năm', 6: 'Sáu', 7: 'Bảy' }
  return map[day] || day
}

function getEnrollmentStatusClass(status) {
  const map = {
    DangHoc: 'status-inprogress',
    DaXong: 'status-completed',
    BoHoc: 'status-cancelled',
    PendingPayment: 'bg-amber-500/10 text-amber-600 border border-amber-500/20',
    Pending: 'bg-purple-500/10 text-purple-600 border border-purple-500/20'
  }
  return map[status] || 'bg-outline/10 text-on-surface'
}

function getEnrollmentStatusLabel(status) {
  const map = {
    DangHoc: 'Đang học',
    DaXong: 'Hoàn thành',
    BoHoc: 'Đã bỏ học',
    PendingPayment: 'Chờ học phí',
    Pending: 'Chờ duyệt'
  }
  return map[status] || status
}

function getStatusClass(status) {
  const map = {
    Pending: 'bg-amber-500/10 text-amber-600 border border-amber-500/20',
    Approved: 'status-completed',
    Rejected: 'status-cancelled'
  }
  return map[status] || 'bg-outline/10 text-on-surface'
}

function getStatusLabel(status) {
  const map = {
    Pending: 'Chờ duyệt',
    Approved: 'Đã duyệt',
    Rejected: 'Từ chối'
  }
  return map[status] || status
}
</script>

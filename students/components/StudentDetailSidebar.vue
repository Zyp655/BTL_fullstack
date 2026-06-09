<template>
  <teleport to="body">
    <div v-if="show" class="fixed inset-0 glass-backdrop z-[9999] flex items-center justify-center p-4">
      <div class="bg-white/90 backdrop-blur-[24px] border border-white/50 rounded-xl shadow-[0_20px_40px_rgba(0,31,63,0.12)] max-w-2xl w-full overflow-hidden animate-scale-in flex flex-col">
        <!-- Title -->
        <div class="px-6 py-4 border-b border-outline-variant/20 flex items-center justify-between">
          <h3 class="font-title-md text-[18px] font-bold text-primary flex items-center gap-2">
            <span class="material-symbols-outlined text-on-tertiary-container">school</span>
            Lịch sử học tập: {{ student?.fullName }}
          </h3>
          <button @click="$emit('close')" class="text-on-surface-variant hover:text-primary cursor-pointer">
            <span class="material-symbols-outlined">close</span>
          </button>
        </div>

        <!-- Content Body -->
        <div class="p-6 space-y-4 overflow-y-auto max-h-[60vh]">
          <div v-if="loading" class="text-center py-8">
            <span class="animate-spin inline-block w-8 h-8 border-4 border-on-tertiary-container border-t-transparent rounded-full"></span>
            <p class="mt-2 text-body-sm text-on-surface-variant">Đang tải lịch sử đăng ký học...</p>
          </div>

          <div v-else-if="enrollments.length > 0" class="space-y-4">
            <div
              v-for="en in enrollments"
              :key="en.enrollmentId"
              class="bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-4 flex justify-between items-center gap-4"
            >
              <div>
                <h4 class="font-bold text-primary text-base">{{ en.className }}</h4>
                <p class="text-[12px] text-on-surface-variant mt-1">Khóa học: {{ en.courseName }}</p>
                <p class="text-[12px] text-on-surface-variant mt-0.5">Ngày đăng ký: {{ formatDate(en.enrollDate || en.enrolledAt) }}</p>
                <!-- Attendance summary -->
                <p v-if="en.totalSessions > 0" class="text-[12px] text-on-surface-variant mt-1.5 flex items-center gap-1.5">
                  <span class="material-symbols-outlined text-[16px] text-emerald-600">done_all</span>
                  Chuyên cần: <span class="font-bold text-primary">{{ en.attendanceRate }}%</span> ({{ en.present }}/{{ en.totalSessions }} buổi)
                </p>
                <p v-else class="text-[12px] text-on-surface-variant/60 mt-1.5 flex items-center gap-1.5">
                  <span class="material-symbols-outlined text-[16px] text-on-surface-variant/40">info</span>
                  Chuyên cần: Chưa có dữ liệu điểm danh
                </p>
              </div>
              <div class="text-right flex flex-col items-end gap-2">
                <span
                  :class="[
                    en.status === 'Active' || en.status === 'DangHoc' ? 'bg-emerald-500/10 text-emerald-700 border-emerald-500/20' : 
                    en.status === 'Completed' || en.status === 'HoanThanh' ? 'bg-purple-500/10 text-purple-700 border-purple-500/20' : 
                    'bg-amber-500/10 text-amber-700 border-amber-500/20',
                    'px-2.5 py-1 rounded-full text-[10px] font-bold border uppercase tracking-wider'
                  ]"
                >
                  {{ getEnrollmentStatusLabel(en.status) }}
                </span>
                <span v-if="en.finalGrade !== null" class="text-sm font-bold text-primary">Điểm: {{ en.finalGrade }}</span>
              </div>
            </div>
          </div>

          <div v-else class="text-center py-12 bg-surface/50 rounded-xl border border-outline-variant/20">
            <span class="material-symbols-outlined text-primary-container/30 text-[48px] mb-2">assignment_ind</span>
            <p class="text-body-sm text-on-surface-variant font-medium">Học viên này chưa đăng ký lớp học nào.</p>
          </div>
        </div>

        <!-- Footer -->
        <div class="px-6 py-4 border-t border-outline-variant/20 flex justify-end bg-surface/30">
          <button
            @click="$emit('close')"
            class="px-5 py-2.5 rounded-lg bg-primary-container text-white font-semibold text-[13px] hover:bg-primary transition-colors cursor-pointer"
          >
            Đóng
          </button>
        </div>
      </div>
    </div>
  </teleport>
</template>

<script setup>
defineProps({
  show: { type: Boolean, required: true },
  student: { type: Object, default: null },
  enrollments: { type: Array, required: true },
  loading: { type: Boolean, required: true }
})

defineEmits(['close'])

function formatDate(date) {
  if (!date) return ''
  return new Date(date).toLocaleDateString('vi-VN')
}

function getEnrollmentStatusLabel(status) {
  const map = { Active: 'Đang học', Completed: 'Hoàn thành', Cancelled: 'Đã hủy', Pending: 'Chờ duyệt' }
  return map[status] || status
}
</script>

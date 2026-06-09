<template>
  <div class="space-y-4">
    <h3 class="font-title-md text-body-lg font-semibold text-primary-container flex items-center gap-2">
      <span class="material-symbols-outlined text-[20px] text-emerald-600">how_to_reg</span>
      Tỷ lệ chuyên cần & Nhật ký điểm danh
    </h3>

    <div v-if="attendanceSummaries.length === 0" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-8 text-center rounded-xl text-on-surface-variant">
      Bạn chưa có dữ liệu điểm danh.
    </div>

    <div v-else class="grid grid-cols-1 md:grid-cols-2 gap-gutter">
      <div
        v-for="summary in attendanceSummaries"
        :key="summary.classId"
        class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-5 rounded-xl flex flex-col justify-between space-y-4"
      >
        <div>
          <!-- Header -->
          <div class="flex justify-between items-start border-b border-outline-variant/30 pb-3">
            <div>
              <h4 class="font-bold text-primary-container text-body-lg">{{ summary.courseName || 'Môn học' }}</h4>
              <p class="text-body-sm text-on-surface-variant font-semibold">Lớp: {{ summary.className || 'Chờ xếp lớp' }}</p>
              <p class="text-[12px] text-on-surface-variant/80 mt-1">Tổng số buổi đã học: {{ summary.totalSessions }} buổi</p>
            </div>
            <div class="text-right">
              <span :class="[summary.attendanceRate >= 80 ? 'status-inprogress' : 'status-cancelled', 'status-badge text-[12px] font-extrabold']">
                {{ summary.attendanceRate.toFixed(1) }}%
              </span>
            </div>
          </div>

          <!-- Stats detail -->
          <div class="grid grid-cols-4 gap-2 text-center text-body-sm mt-4">
            <div class="p-2 rounded-lg bg-emerald-500/5 border border-emerald-500/10 text-emerald-600">
              <div class="font-extrabold text-[16px]">{{ summary.present }}</div>
              <div class="text-[9px] font-bold">Đi học</div>
            </div>
            <div class="p-2 rounded-lg bg-amber-500/5 border border-amber-500/10 text-amber-600">
              <div class="font-extrabold text-[16px]">{{ summary.late }}</div>
              <div class="text-[9px] font-bold">Đi muộn</div>
            </div>
            <div class="p-2 rounded-lg bg-blue-500/5 border border-blue-500/10 text-blue-600">
              <div class="font-extrabold text-[16px]">{{ summary.excused }}</div>
              <div class="text-[9px] font-bold">Có phép</div>
            </div>
            <div class="p-2 rounded-lg bg-red-500/5 border border-red-500/10 text-red-500">
              <div class="font-extrabold text-[16px]">{{ summary.absent }}</div>
              <div class="text-[9px] font-bold">Vắng mặt</div>
            </div>
          </div>

          <!-- Progress bar -->
          <div class="space-y-1 mt-4">
            <div class="w-full bg-surface-container-high h-2 rounded-full overflow-hidden border border-outline-variant/20">
              <div
                :class="[summary.attendanceRate >= 80 ? 'bg-emerald-500' : 'bg-red-500', 'h-full rounded-full']"
                :style="`width: ${summary.attendanceRate}%`"
              ></div>
            </div>
          </div>
        </div>

        <!-- Session details list -->
        <div class="pt-3 border-t border-outline-variant/20 mt-2">
          <div class="text-body-sm font-semibold text-primary-container mb-2 flex items-center gap-1.5">
            <span class="material-symbols-outlined text-[16px] text-on-tertiary-container">history</span>
            Chi tiết lịch sử điểm danh
          </div>
          <div v-if="!summary.sessions || summary.sessions.length === 0" class="text-center py-4 text-body-sm text-on-surface-variant/70 italic">
            Chưa có lịch sử điểm danh nào.
          </div>
          <div v-else class="space-y-2 max-h-48 overflow-y-auto pr-1">
            <div 
              v-for="session in summary.sessions" 
              :key="session.attendanceId"
              class="flex justify-between items-center bg-white/40 p-2 rounded-lg text-body-sm border border-white/20 hover:bg-white/60 transition-colors"
            >
              <div class="flex items-center gap-2 min-w-0">
                <span class="material-symbols-outlined text-[15px] text-on-surface-variant/70 shrink-0">calendar_today</span>
                <span class="font-semibold text-primary-container shrink-0">{{ formatDate(session.sessionDate) }}</span>
                <span v-if="session.note" class="text-body-xs text-on-surface-variant/70 italic truncate" :title="session.note">
                  - {{ session.note }}
                </span>
              </div>
              <select
                v-if="authStore.isAdmin"
                :value="session.status"
                @change="$emit('change-attendance-status', { session, newStatus: $event.target.value })"
                class="bg-white/80 border border-outline-variant/30 rounded px-1.5 py-0.5 text-[11px] font-semibold text-primary cursor-pointer focus:outline-none"
              >
                <option value="CoMat">Đi học</option>
                <option value="Vang">Vắng mặt</option>
                <option value="DiTre">Đi muộn</option>
                <option value="CoPhep">Có phép</option>
              </select>
              <span v-else :class="[getAttendanceStatusClass(session.status), 'px-2 py-0.5 rounded text-[9px] font-bold shrink-0']">
                {{ getAttendanceStatusLabel(session.status) }}
              </span>
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

defineProps({
  attendanceSummaries: { type: Array, required: true }
})

defineEmits(['change-attendance-status'])

function formatDate(dateStr) {
  if (!dateStr) return '—'
  const date = new Date(dateStr)
  return date.toLocaleDateString('vi-VN')
}

function getAttendanceStatusClass(status) {
  const map = {
    CoMat: 'bg-emerald-500/10 text-emerald-600 border border-emerald-500/20',
    Vang: 'bg-red-500/10 text-red-500 border border-red-500/20',
    DiTre: 'bg-amber-500/10 text-amber-600 border border-amber-500/20',
    CoPhep: 'bg-blue-500/10 text-blue-600 border border-blue-500/20'
  }
  return map[status] || 'bg-outline-variant/10 text-on-surface-variant'
}

function getAttendanceStatusLabel(status) {
  const map = {
    CoMat: 'Đi học',
    Vang: 'Vắng mặt',
    DiTre: 'Đi muộn',
    CoPhep: 'Có phép'
  }
  return map[status] || status
}
</script>

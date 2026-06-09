<template>
  <div class="space-y-4">
    <h3 class="font-title-md text-body-lg font-semibold text-primary-container flex items-center gap-2">
      <span class="material-symbols-outlined text-[20px] text-purple-600">history_edu</span>
      Bảng điểm kết quả học tập
    </h3>

    <div v-if="enrolledClasses.length === 0" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-8 text-center rounded-xl text-on-surface-variant">
      Bạn chưa có kết quả thi nào.
    </div>

    <div v-else class="space-y-4">
      <div
        v-for="cls in enrolledClasses"
        :key="cls.classId"
        class="bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl overflow-hidden shadow-[0_12px_24px_rgba(0,0,0,0.05)]"
      >
        <!-- Header class -->
        <div class="bg-primary-container/5 px-5 py-3 border-b border-white/40 flex justify-between items-center">
          <div>
            <span class="font-semibold text-primary-container text-body-lg">{{ cls.className }}</span>
            <span class="text-body-sm text-on-surface-variant ml-2">({{ cls.courseName }})</span>
          </div>
          <div class="flex items-center gap-4">
            <button
              v-if="authStore.isAdmin"
              @click="$emit('open-grading-dialog', cls)"
              class="px-2.5 py-1.5 rounded-lg bg-primary-container text-white text-[11px] font-semibold hover:bg-primary transition-all flex items-center gap-1 cursor-pointer active:scale-95 shrink-0"
            >
              <span class="material-symbols-outlined text-[15px]">edit_note</span>
              Nhập/Sửa điểm
            </button>
            <div class="text-body-sm font-semibold text-primary-container">
              ĐTB môn:
              <span :class="[getAverageScoreForClass(cls.enrollmentId) >= 5 ? 'text-emerald-600' : 'text-red-500', 'font-extrabold']">
                {{ getAverageScoreForClass(cls.enrollmentId) > 0 ? getAverageScoreForClass(cls.enrollmentId).toFixed(1) : 'Chưa tổng kết' }}
              </span>
            </div>
          </div>
        </div>

        <!-- Body: Score list -->
        <div class="p-4">
          <div v-if="!examResults[cls.enrollmentId] || examResults[cls.enrollmentId].length === 0" class="text-center py-4 text-body-sm text-on-surface-variant">
            Chưa có điểm thi/kiểm tra cho lớp học này.
          </div>
          <div v-else class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
            <div
              v-for="res in examResults[cls.enrollmentId]"
              :key="res.resultId"
              class="bg-white/50 backdrop-blur-[10px] p-3.5 rounded-xl border border-white/40 flex justify-between items-center"
            >
              <div class="space-y-0.5">
                <div class="font-semibold text-primary-container text-body-sm">{{ getExamTypeLabel(res.examType) }}</div>
                <div class="text-[10px] text-on-surface-variant/60">Ngày nhập: {{ formatDate(res.examDate || res.createdAt) }}</div>
              </div>
              <div class="text-right">
                <div :class="[res.score >= 5 ? 'bg-emerald-500/10 text-emerald-600 border-emerald-500/20' : 'bg-red-500/10 text-red-500 border-red-500/20', 'font-extrabold text-[16px] px-3 py-1 rounded-lg border']">
                  {{ res.score }}
                </div>
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
  examResults: { type: Object, required: true }
})

defineEmits(['open-grading-dialog'])

function formatDate(dateStr) {
  if (!dateStr) return '—'
  const date = new Date(dateStr)
  return date.toLocaleDateString('vi-VN')
}

function getExamTypeLabel(type) {
  const map = { GiuaKy: 'Giữa kỳ', CuoiKy: 'Cuối kỳ', KiemTra: 'Kiểm tra' }
  return map[type] || type
}

function getAverageScoreForClass(enrollmentId) {
  const scores = props.examResults[enrollmentId] || []
  if (scores.length === 0) return 0
  let total = 0
  scores.forEach(s => {
    total += s.score
  })
  return total / scores.length
}
</script>

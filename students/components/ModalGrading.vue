<template>
  <teleport to="body">
    <div v-if="show" class="fixed inset-0 glass-backdrop z-[9999] flex items-center justify-center p-4">
      <div class="bg-white/90 backdrop-blur-[24px] border border-white/50 rounded-xl shadow-[0_20px_40px_rgba(0,31,63,0.12)] max-w-md w-full p-6 space-y-4 animate-scale-in flex flex-col">
        <div class="flex items-center gap-3 text-primary-container border-b border-white/40 pb-3 justify-between">
          <h3 class="font-title-md text-[18px] font-bold flex items-center gap-2">
            <span class="material-symbols-outlined text-[24px] text-on-tertiary-container">edit_note</span>
            Nhập/Sửa điểm: {{ gradingClass?.className }}
          </h3>
          <button @click="$emit('close')" class="text-on-surface-variant hover:text-primary-container cursor-pointer">
            <span class="material-symbols-outlined">close</span>
          </button>
        </div>

        <div class="space-y-4 overflow-y-auto max-h-[60vh] pr-1">
          <div v-for="type in ['KiemTra', 'GiuaKy', 'CuoiKy']" :key="type" class="space-y-2 p-3 bg-primary-container/[0.02] border border-primary-container/5 rounded-lg">
            <div class="flex justify-between items-center">
              <span class="text-body-sm font-bold text-primary-container">{{ getExamTypeLabel(type) }}</span>
            </div>
            <div class="grid grid-cols-3 gap-2">
              <div class="col-span-1 space-y-1">
                <label class="text-[11px] font-semibold text-on-surface-variant block">Điểm số *</label>
                <input
                  v-model="gradingForm[type].score"
                  type="number"
                  min="0"
                  max="10"
                  step="0.1"
                  placeholder="0 - 10"
                  class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg px-2.5 py-1.5 text-body-sm text-primary focus:outline-none focus:border-on-tertiary-container transition-colors"
                />
              </div>
              <div class="col-span-2 space-y-1">
                <label class="text-[11px] font-semibold text-on-surface-variant block">Ghi chú</label>
                <input
                  v-model="gradingForm[type].note"
                  type="text"
                  placeholder="Ghi chú bài kiểm tra..."
                  class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg px-2.5 py-1.5 text-body-sm text-primary focus:outline-none focus:border-on-tertiary-container transition-colors"
                />
              </div>
            </div>
          </div>
        </div>

        <div class="flex justify-end gap-3 pt-3 border-t border-white/40">
          <button
            @click="$emit('close')"
            class="px-4 py-2 rounded-lg border border-white/60 text-on-surface-variant font-semibold text-[13px] hover:bg-white/40 transition-colors cursor-pointer"
          >
            Hủy
          </button>
          <button
            @click="submitGrades"
            :disabled="saving"
            class="px-4 py-2 rounded-lg bg-primary-container text-white font-semibold text-[13px] hover:bg-primary transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-1 cursor-pointer active:scale-95"
          >
            <span v-if="saving" class="animate-spin w-4 h-4 border-2 border-white border-t-transparent rounded-full mr-1"></span>
            Lưu điểm số
          </button>
        </div>
      </div>
    </div>
  </teleport>
</template>

<script setup>
import { ref, watch, inject } from 'vue'
import api from '../../../services/api'

const props = defineProps({
  show: { type: Boolean, required: true },
  gradingClass: { type: Object, default: null },
  examResults: { type: Object, required: true },
  studentProfile: { type: Object, required: true }
})

const emit = defineEmits(['close', 'success'])
const showSnackbar = inject('showSnackbar')

const saving = ref(false)
const gradingForm = ref({
  KiemTra: { resultId: null, score: '', note: '' },
  GiuaKy: { resultId: null, score: '', note: '' },
  CuoiKy: { resultId: null, score: '', note: '' }
})

watch(() => props.show, (newVal) => {
  if (newVal && props.gradingClass) {
    gradingForm.value = {
      KiemTra: { resultId: null, score: '', note: '' },
      GiuaKy: { resultId: null, score: '', note: '' },
      CuoiKy: { resultId: null, score: '', note: '' }
    }
    
    const scores = props.examResults[props.gradingClass.enrollmentId] || []
    scores.forEach(s => {
      if (gradingForm.value[s.examType]) {
        gradingForm.value[s.examType] = {
          resultId: s.examResultId || s.id,
          score: s.score,
          note: s.note || ''
        }
      }
    })
  }
})

function getExamTypeLabel(type) {
  const map = { GiuaKy: 'Giữa kỳ', CuoiKy: 'Cuối kỳ', KiemTra: 'Kiểm tra' }
  return map[type] || type
}

async function submitGrades() {
  if (!props.gradingClass || !props.studentProfile) return
  saving.value = true
  try {
    const promises = []
    for (const type of ['KiemTra', 'GiuaKy', 'CuoiKy']) {
      const f = gradingForm.value[type]
      if (f.score !== '' && f.score !== null) {
        const scoreVal = Number(f.score)
        if (scoreVal < 0 || scoreVal > 10) {
          showSnackbar(`Điểm số ${getExamTypeLabel(type)} phải từ 0 đến 10`, 'error')
          saving.value = false
          return
        }
        
        if (f.resultId) {
          promises.push(api.put(`/api/v1/results/${f.resultId}`, {
            id: f.resultId,
            score: scoreVal,
            note: f.note || null
          }))
        } else {
          promises.push(api.post('/api/v1/results', {
            enrollmentId: props.gradingClass.enrollmentId,
            examType: type,
            score: scoreVal,
            note: f.note || null,
            examDate: new Date().toISOString()
          }))
        }
      }
    }
    
    if (promises.length > 0) {
      await Promise.all(promises)
      showSnackbar('Đã cập nhật điểm số thành công!', 'success')
    }
    emit('success')
  } catch (err) {
    console.error('Error saving grades:', err)
    showSnackbar(err.response?.data?.message || 'Lỗi khi cập nhật điểm số', 'error')
  } finally {
    saving.value = false
  }
}
</script>

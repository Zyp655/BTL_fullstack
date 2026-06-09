<template>
  <teleport to="body">
    <div v-if="show" class="fixed inset-0 glass-backdrop z-[9999] flex items-center justify-center p-4">
      <div class="bg-white/90 backdrop-blur-[24px] border border-white/50 rounded-xl shadow-[0_20px_40px_rgba(0,31,63,0.12)] max-w-md w-full p-6 space-y-4 animate-scale-in flex flex-col">
        <div class="flex items-center gap-3 text-primary-container border-b border-white/40 pb-3 justify-between">
          <h3 class="font-title-md text-[18px] font-bold flex items-center gap-2">
            <span class="material-symbols-outlined text-[24px] text-on-tertiary-container">swap_horiz</span>
            Chuyển lớp học viên
          </h3>
          <button @click="$emit('close')" class="text-on-surface-variant hover:text-primary-container cursor-pointer">
            <span class="material-symbols-outlined">close</span>
          </button>
        </div>
        
        <div class="space-y-4">
          <div class="space-y-1">
            <label class="text-body-sm font-semibold text-primary-container block">Chọn lớp hiện tại cần chuyển *</label>
            <select
              v-model="transferFromClassId"
              class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg appearance-none px-4 py-2.5 text-body-sm text-primary focus:outline-none focus:border-on-tertiary-container transition-all cursor-pointer"
            >
              <option :value="null" disabled>-- Chọn lớp học hiện tại --</option>
              <option v-for="cls in enrolledClasses.filter(c => c.status === 'DangHoc')" :key="cls.classId" :value="cls.classId">
                {{ cls.className }} ({{ cls.courseName }})
              </option>
            </select>
          </div>

          <div v-if="transferFromClassId" class="space-y-1">
            <label class="text-body-sm font-semibold text-primary-container block">Chọn lớp mới muốn chuyển đến *</label>
            <div v-if="loadingAlternative" class="flex items-center justify-center py-2 gap-2">
              <span class="animate-spin w-4 h-4 border-2 border-primary border-t-transparent rounded-full"></span>
              <span class="text-body-xs text-on-surface-variant">Đang tìm lớp học...</span>
            </div>
            <div v-else class="relative">
              <select
                v-model="transferToClassId"
                class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg appearance-none px-4 py-2.5 text-body-sm text-primary focus:outline-none focus:border-on-tertiary-container transition-all cursor-pointer"
              >
                <option :value="null" disabled>-- Chọn lớp học mới --</option>
                <option v-for="c in alternativeClasses" :key="c.classId" :value="c.classId">
                  {{ c.className }} (Sĩ số: {{ c.currentStudents }}/{{ c.maxStudents }})
                </option>
              </select>
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
            @click="submitClassTransfer"
            :disabled="submitting || !transferFromClassId || !transferToClassId"
            class="px-4 py-2 rounded-lg bg-primary-container text-white font-semibold text-[13px] hover:bg-primary transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-1 cursor-pointer active:scale-95"
          >
            <span v-if="submitting" class="animate-spin w-4 h-4 border-2 border-white border-t-transparent rounded-full mr-1"></span>
            Xác nhận chuyển
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
  enrolledClasses: { type: Array, required: true },
  studentProfile: { type: Object, required: true }
})

const emit = defineEmits(['close', 'success'])
const showSnackbar = inject('showSnackbar')

const transferFromClassId = ref(null)
const transferToClassId = ref(null)
const loadingAlternative = ref(false)
const alternativeClasses = ref([])
const submitting = ref(false)

watch(() => props.show, (newVal) => {
  if (newVal) {
    transferFromClassId.value = null
    transferToClassId.value = null
    alternativeClasses.value = []
  }
})

watch(transferFromClassId, async (newClassId) => {
  if (!newClassId) {
    alternativeClasses.value = []
    transferToClassId.value = null
    return
  }
  loadingAlternative.value = true
  try {
    const currentEnrollment = props.enrolledClasses.find(c => c.classId === newClassId)
    if (!currentEnrollment) return
    
    const res = await api.get('/api/v1/classes', {
      params: { courseId: currentEnrollment.courseId, pageSize: 100 }
    })
    alternativeClasses.value = (res.data?.items || []).filter(
      c => c.classId !== newClassId && (c.status === 'Opened' || c.status === 'InProgress')
    )
  } catch (e) {
    console.error('Error fetching transfer alternative classes:', e)
  } finally {
    loadingAlternative.value = false
  }
})

async function submitClassTransfer() {
  if (!transferFromClassId.value || !transferToClassId.value || !props.studentProfile) return
  submitting.value = true
  try {
    await api.post('/api/v1/enrollments/transfer', {
      studentId: props.studentProfile.studentId,
      fromClassId: transferFromClassId.value,
      toClassId: transferToClassId.value
    })
    showSnackbar('Chuyển lớp học viên thành công!', 'success')
    emit('success')
  } catch (e) {
    console.error('Error transferring class:', e)
    showSnackbar(e.response?.data?.message || 'Lỗi khi chuyển lớp học viên', 'error')
  } finally {
    submitting.value = false
  }
}
</script>

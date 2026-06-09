<template>
  <teleport to="body">
    <div v-if="show" class="fixed inset-0 glass-backdrop z-[9999] flex items-center justify-center p-4">
      <div class="bg-white/90 backdrop-blur-[24px] border border-white/50 rounded-xl shadow-[0_20px_40px_rgba(0,31,63,0.12)] max-w-md w-full p-6 space-y-4 animate-scale-in flex flex-col">
        <div class="flex items-center gap-3 text-primary-container border-b border-white/40 pb-3 justify-between">
          <h3 class="font-title-md text-[18px] font-bold flex items-center gap-2">
            <span class="material-symbols-outlined text-[24px] text-on-tertiary-container">add_card</span>
            Ghi nhận thanh toán học phí
          </h3>
          <button @click="$emit('close')" class="text-on-surface-variant hover:text-primary-container cursor-pointer">
            <span class="material-symbols-outlined">close</span>
          </button>
        </div>

        <div class="space-y-4">
          <div class="p-3 bg-emerald-500/5 border border-emerald-500/10 rounded-lg space-y-1">
            <div class="text-body-sm text-on-surface-variant">Lớp học: <strong class="text-primary-container">{{ paymentBill?.className }}</strong></div>
            <div class="text-body-sm text-on-surface-variant">Tổng tiền: <strong>{{ formatCurrency(paymentBill?.totalAmount) }}</strong></div>
            <div class="text-body-sm text-error">Còn nợ: <strong>{{ formatCurrency(paymentBill?.remainingAmount) }}</strong></div>
          </div>

          <div class="space-y-1">
            <label class="text-body-sm font-semibold text-primary-container block">Số tiền đóng *</label>
            <input
              v-model="paymentForm.amount"
              type="number"
              min="1000"
              :max="paymentBill?.remainingAmount"
              class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg px-4 py-2.5 text-body-sm text-primary focus:outline-none focus:border-on-tertiary-container transition-colors"
              placeholder="Nhập số tiền đóng"
            />
          </div>

          <div class="space-y-1">
            <label class="text-body-sm font-semibold text-primary-container block">Phương thức thanh toán *</label>
            <select
              v-model="paymentForm.method"
              class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg appearance-none px-4 py-2.5 text-body-sm text-primary focus:outline-none focus:border-on-tertiary-container transition-all cursor-pointer"
            >
              <option value="TienMat">Tiền mặt</option>
              <option value="ChuyenKhoan">Chuyển khoản</option>
              <option value="TheTD">Thẻ tín dụng</option>
            </select>
          </div>

          <div class="space-y-1">
            <label class="text-body-sm font-semibold text-primary-container block">Ghi chú</label>
            <input
              v-model="paymentForm.note"
              type="text"
              class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg px-4 py-2.5 text-body-sm text-primary focus:outline-none focus:border-on-tertiary-container transition-colors"
              placeholder="Ví dụ: Đóng tiền mặt tại quầy..."
            />
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
            @click="submitPayment"
            :disabled="saving || !paymentForm.amount"
            class="px-4 py-2 rounded-lg bg-primary-container text-white font-semibold text-[13px] hover:bg-primary transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-1 cursor-pointer active:scale-95"
          >
            <span v-if="saving" class="animate-spin w-4 h-4 border-2 border-white border-t-transparent rounded-full mr-1"></span>
            Xác nhận thu tiền
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
  paymentBill: { type: Object, default: null },
  studentProfile: { type: Object, required: true }
})

const emit = defineEmits(['close', 'success'])
const showSnackbar = inject('showSnackbar')

const saving = ref(false)
const paymentForm = ref({
  amount: '',
  method: 'TienMat',
  note: ''
})

watch(() => props.show, (newVal) => {
  if (newVal && props.paymentBill) {
    paymentForm.value = {
      amount: props.paymentBill.remainingAmount,
      method: 'TienMat',
      note: ''
    }
  }
})

function formatCurrency(amount) {
  if (amount === undefined || amount === null) return '0 đ'
  return amount.toLocaleString('vi-VN') + ' đ'
}

async function submitPayment() {
  if (!props.paymentBill || !props.studentProfile) return
  saving.value = true
  try {
    const amountVal = Number(paymentForm.value.amount)
    if (!amountVal || amountVal <= 0) {
      showSnackbar('Số tiền đóng phải lớn hơn 0', 'error')
      saving.value = false
      return
    }
    if (amountVal > props.paymentBill.remainingAmount) {
      showSnackbar('Số tiền đóng không được lớn hơn số tiền còn nợ', 'error')
      saving.value = false
      return
    }
    
    await api.post(`/api/v1/payments/${props.paymentBill.paymentId}/transactions`, {
      amount: amountVal,
      paymentMethod: paymentForm.value.method,
      note: paymentForm.value.note || null
    })
    
    showSnackbar('Đã ghi nhận thanh toán học phí thành công!', 'success')
    emit('success')
  } catch (err) {
    console.error('Error saving payment:', err)
    showSnackbar(err.response?.data?.message || 'Lỗi khi ghi nhận thanh toán học phí', 'error')
  } finally {
    saving.value = false
  }
}
</script>

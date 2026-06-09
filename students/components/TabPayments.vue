<template>
  <div class="space-y-4">
    <h3 class="font-title-md text-body-lg font-semibold text-primary-container flex items-center gap-2">
      <span class="material-symbols-outlined text-[20px] text-amber-500">receipt_long</span>
      Danh sách phiếu học phí & Lịch sử đóng học phí
    </h3>

    <div v-if="payments.length === 0" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-8 text-center rounded-xl text-on-surface-variant">
      Bạn chưa có phiếu thu học phí nào trên hệ thống.
    </div>

    <div v-else class="space-y-4">
      <div
        v-for="pay in payments"
        :key="pay.paymentId"
        class="bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl overflow-hidden shadow-[0_12px_24px_rgba(0,0,0,0.05)]"
      >
        <!-- Header payment info -->
        <div class="bg-primary-container/5 px-5 py-4 border-b border-white/40 flex flex-col sm:flex-row justify-between sm:items-center gap-2">
          <div class="space-y-1">
            <div class="flex items-center gap-2">
              <span class="font-semibold text-primary-container text-body-lg">{{ pay.className }}</span>
              <span :class="[getPaymentStatusClass(pay.status), 'status-badge text-[10px] font-bold']">
                {{ getPaymentStatusLabel(pay.status) }}
              </span>
              <button
                v-if="authStore.isAdmin && pay.status !== 'HoanTat'"
                @click="$emit('open-payment-modal', pay)"
                class="px-2.5 py-1.5 rounded-lg bg-emerald-600 hover:bg-emerald-700 text-white text-[11px] font-semibold transition-all flex items-center gap-1 cursor-pointer active:scale-95 shadow-sm"
              >
                <span class="material-symbols-outlined text-[15px]">add_card</span>
                Ghi nhận thanh toán
              </button>
            </div>
            <div class="text-body-sm text-on-surface-variant">Môn học: {{ pay.courseName }}</div>
            <div class="text-body-sm text-on-surface-variant/70">Mã hóa đơn: <span class="font-mono font-bold">PAY{{ pay.paymentId }}</span></div>
          </div>
          <div class="text-left sm:text-right space-y-0.5">
            <div class="text-body-sm text-on-surface-variant">Hạn chót đóng: <span class="font-semibold text-primary-container">{{ formatDate(pay.dueDate) }}</span></div>
            <div class="text-body-sm text-on-surface-variant">
              Cần thanh toán: <span class="font-bold text-primary-container">{{ formatCurrency(pay.totalAmount) }}</span>
            </div>
            <div v-if="pay.remainingAmount > 0" class="text-body-sm font-bold text-error">
              Còn nợ: {{ formatCurrency(pay.remainingAmount) }}
            </div>
          </div>
        </div>

        <!-- Transaction list for this payment -->
        <div class="p-4 space-y-2">
          <div class="text-body-sm font-semibold text-primary-container mb-2 flex items-center gap-1.5">
            <span class="material-symbols-outlined text-[18px]">history</span>
            Chi tiết đóng tiền
          </div>
          
          <div v-if="pay.transactions.length === 0" class="text-center py-4 text-body-sm text-on-surface-variant">
            Chưa ghi nhận giao dịch thanh toán nào cho hóa đơn này.
          </div>
          
          <div v-else class="space-y-2">
            <div
              v-for="tx in pay.transactions"
              :key="tx.transactionId"
              class="bg-white/50 backdrop-blur-[10px] p-3 rounded-lg border border-white/40 flex justify-between items-center text-body-sm"
            >
              <div class="space-y-0.5">
                <div class="font-bold text-primary-container">Số tiền: {{ formatCurrency(tx.amount) }}</div>
                <div class="text-on-surface-variant">Phương thức: {{ formatPaymentMethod(tx.paymentMethod) }}</div>
                <div class="text-on-surface-variant/80">Thời gian: {{ formatDateTime(tx.paidAt) }}</div>
                <div v-if="tx.note" class="text-on-surface-variant/70 italic">Ghi chú: {{ tx.note }}</div>
              </div>
              <div class="text-right text-emerald-600 font-bold bg-emerald-500/10 px-2.5 py-1 rounded-lg border border-emerald-500/20">
                Đã thu
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

defineProps({
  payments: { type: Array, required: true }
})

defineEmits(['open-payment-modal'])

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

function formatCurrency(amount) {
  if (amount === undefined || amount === null) return '0 đ'
  return amount.toLocaleString('vi-VN') + ' đ'
}

function getPaymentStatusClass(status) {
  const map = { ChuaTT: 'status-cancelled', DangTT: 'status-inprogress', HoanTat: 'status-completed' }
  return map[status] || 'status-completed'
}

function getPaymentStatusLabel(status) {
  const map = { ChuaTT: 'Chưa thanh toán', DangTT: 'Đang đóng học phí', HoanTat: 'Đã hoàn thành' }
  return map[status] || status
}

function formatPaymentMethod(method) {
  const map = { TienMat: 'Tiền mặt', ChuyenKhoan: 'Chuyển khoản', TheTD: 'Thẻ tín dụng' }
  return map[method] || method
}
</script>

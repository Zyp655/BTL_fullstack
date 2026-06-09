<template>
  <div class="space-y-4">
    <div class="grid grid-cols-1 md:grid-cols-3 gap-4">
      <!-- Total available credits -->
      <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-5 rounded-xl flex items-center gap-4">
        <div class="w-12 h-12 rounded-full bg-emerald-500/10 text-emerald-600 flex items-center justify-center shrink-0">
          <span class="material-symbols-outlined text-[28px]">savings</span>
        </div>
        <div>
          <p class="text-body-sm text-on-surface-variant">Tín dụng Khả dụng (Có thể dùng)</p>
          <h3 class="font-title-md text-title-md text-emerald-600">{{ formatCurrency(creditSummary?.totalAvailable || 0) }}</h3>
        </div>
      </div>

      <!-- Total used credits -->
      <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-5 rounded-xl flex items-center gap-4">
        <div class="w-12 h-12 rounded-full bg-blue-500/10 text-blue-600 flex items-center justify-center shrink-0">
          <span class="material-symbols-outlined text-[28px]">payments</span>
        </div>
        <div>
          <p class="text-body-sm text-on-surface-variant">Đã khấu trừ / Sử dụng</p>
          <h3 class="font-title-md text-title-md text-blue-600">{{ formatCurrency(creditSummary?.totalUsed || 0) }}</h3>
        </div>
      </div>

      <!-- Total refunded credits -->
      <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-5 rounded-xl flex items-center gap-4">
        <div class="w-12 h-12 rounded-full bg-red-500/10 text-red-600 flex items-center justify-center shrink-0">
          <span class="material-symbols-outlined text-[28px]">assignment_return</span>
        </div>
        <div>
          <p class="text-body-sm text-on-surface-variant">Đã hoàn trả kế toán</p>
          <h3 class="font-title-md text-title-md text-red-600">{{ formatCurrency(creditSummary?.totalRefunded || 0) }}</h3>
        </div>
      </div>
    </div>

    <!-- Credit list table -->
    <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl overflow-hidden shadow-[0_12px_24px_rgba(0,0,0,0.05)]">
      <div class="px-gutter py-4 border-b border-white/40 bg-primary-container/5">
        <h3 class="font-title-md text-body-lg font-semibold text-primary-container flex items-center gap-2">
          <span class="material-symbols-outlined text-[20px] text-on-tertiary-container">history</span>
          Lịch sử bảo lưu & hoàn trả tín dụng học phí
        </h3>
      </div>

      <div v-if="!creditSummary || !creditSummary.credits || creditSummary.credits.length === 0" class="p-8 text-center text-on-surface-variant">
        Bạn chưa có tín dụng bảo lưu học phí nào trên hệ thống.
      </div>

      <div v-else class="overflow-x-auto">
        <table class="w-full border-collapse text-left text-body-sm">
          <thead>
            <tr class="border-b border-white/40 text-on-surface-variant bg-white/20">
              <th class="px-6 py-4 font-semibold">Mã bảo lưu</th>
              <th class="px-6 py-4 font-semibold">Nguồn bảo lưu</th>
              <th class="px-6 py-4 font-semibold">Số tiền</th>
              <th class="px-6 py-4 font-semibold">Trạng thái</th>
              <th class="px-6 py-4 font-semibold">Ngày ghi nhận</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="credit in creditSummary.credits"
              :key="credit.creditId"
              class="border-b border-white/20 hover:bg-white/40 transition-colors"
            >
              <td class="px-6 py-4 font-mono font-bold text-primary-container">CRE{{ String(credit.creditId).padStart(4, '0') }}</td>
              <td class="px-6 py-4">
                <div class="font-semibold text-primary-container">{{ credit.sourceClassName }}</div>
                <div class="text-body-sm text-on-surface-variant/80">Lớp gốc (ID: {{ credit.sourceClassId }})</div>
              </td>
              <td class="px-6 py-4 font-bold text-primary-container">{{ formatCurrency(credit.amount) }}</td>
              <td class="px-6 py-4">
                <span :class="[getCreditStatusClass(credit.status), 'status-badge text-[10px] font-bold']">
                  {{ getCreditStatusLabel(credit.status) }}
                </span>
              </td>
              <td class="px-6 py-4 text-on-surface-variant">{{ formatDateTime(credit.createdAt) }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script setup>
defineProps({
  creditSummary: { type: Object, default: null }
})

function formatDateTime(dateStr) {
  if (!dateStr) return '—'
  const date = new Date(dateStr)
  return date.toLocaleString('vi-VN')
}

function formatCurrency(amount) {
  if (amount === undefined || amount === null) return '0 đ'
  return amount.toLocaleString('vi-VN') + ' đ'
}

function getCreditStatusClass(status) {
  const map = { Available: 'status-completed', Used: 'status-inprogress', Refunded: 'status-cancelled' }
  return map[status] || 'status-opened'
}

function getCreditStatusLabel(status) {
  const map = { Available: 'Có thể sử dụng', Used: 'Đã sử dụng', Refunded: 'Đã hoàn tiền' }
  return map[status] || status
}
</script>

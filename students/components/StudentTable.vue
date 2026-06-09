<template>
  <div class="space-y-stack-lg">
    <!-- Filters -->
    <section class="bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-4 flex flex-col sm:flex-row gap-4 items-center">
      <div class="relative flex-1 w-full">
        <span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-on-surface-variant">search</span>
        <input
          :value="filters.search"
          @input="$emit('update-filter', { key: 'search', value: $event.target.value })"
          class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg pl-10 pr-4 py-2.5 text-body-sm text-primary placeholder:text-on-surface-variant/60 focus:outline-none focus:border-on-tertiary-container/30 transition-colors"
          placeholder="Tìm kiếm học viên theo tên, email, SĐT..."
          type="text"
        />
      </div>
      <!-- Class Filter -->
      <div class="relative w-full sm:w-64">
        <select
          :value="filters.classId"
          @change="$emit('update-filter', { key: 'classId', value: $event.target.value ? Number($event.target.value) : null })"
          class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg appearance-none pl-4 pr-10 py-2.5 text-body-sm text-primary bg-transparent cursor-pointer focus:outline-none focus:border-on-tertiary-container/30 transition-colors"
        >
          <option :value="null">Tất cả lớp học</option>
          <option v-for="cls in classes" :key="cls.classId" :value="cls.classId">
            {{ cls.className }}
          </option>
        </select>
        <span class="material-symbols-outlined absolute right-3 top-1/2 -translate-y-1/2 text-on-surface-variant pointer-events-none">expand_more</span>
      </div>

      <div class="relative w-full sm:w-64">
        <select
          :value="filters.gender"
          @change="$emit('update-filter', { key: 'gender', value: $event.target.value || null })"
          class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg appearance-none pl-4 pr-10 py-2.5 text-body-sm text-primary bg-transparent cursor-pointer focus:outline-none focus:border-on-tertiary-container/30 transition-colors"
        >
          <option :value="null">Tất cả giới tính</option>
          <option value="Nam">Nam</option>
          <option value="Nữ">Nữ</option>
          <option value="Khác">Khác</option>
        </select>
        <span class="material-symbols-outlined absolute right-3 top-1/2 -translate-y-1/2 text-on-surface-variant pointer-events-none">expand_more</span>
      </div>
    </section>

    <!-- Data Table -->
    <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl shadow-[0_12px_24px_rgba(0,0,0,0.05)] overflow-hidden">
      <div v-if="loading" class="p-12 space-y-4 animate-pulse">
        <div class="h-8 bg-surface-container-high rounded w-full"></div>
        <div class="h-12 bg-surface-container-high rounded w-full" v-for="i in 5" :key="i"></div>
      </div>

      <div v-else-if="students.length > 0" class="overflow-x-auto">
        <table class="w-full text-left border-collapse">
          <thead>
            <tr class="bg-surface-container-high">
              <th class="py-4 px-6 text-label-caps font-label-caps text-on-surface-variant uppercase whitespace-nowrap">Mã HV</th>
              <th class="py-4 px-6 text-label-caps font-label-caps text-on-surface-variant uppercase whitespace-nowrap">Họ và tên</th>
              <th class="py-4 px-6 text-label-caps font-label-caps text-on-surface-variant uppercase whitespace-nowrap">Giới tính</th>
              <th class="py-4 px-6 text-label-caps font-label-caps text-on-surface-variant uppercase whitespace-nowrap">Ngày sinh</th>
              <th class="py-4 px-6 text-label-caps font-label-caps text-on-surface-variant uppercase whitespace-nowrap">Số điện thoại</th>
              <th class="py-4 px-6 text-label-caps font-label-caps text-on-surface-variant uppercase whitespace-nowrap">Email</th>
              <th class="py-4 px-6 text-label-caps font-label-caps text-on-surface-variant uppercase whitespace-nowrap">Địa chỉ</th>
              <th class="py-4 px-6 text-label-caps font-label-caps text-on-surface-variant uppercase whitespace-nowrap text-right">Thao tác</th>
            </tr>
          </thead>
          <tbody class="font-body-sm text-body-sm">
            <tr
              v-for="student in students"
              :key="student.studentId"
              class="border-t border-white/40 hover:bg-primary-container/[0.03] transition-colors group cursor-pointer"
              @click="authStore.isAdmin ? $emit('open-edit-dialog', student) : $emit('view-enrollments', student)"
            >
              <td class="py-4 px-6 font-semibold text-primary">HV-{{ String(student.studentId).padStart(4, '0') }}</td>
              <td class="py-4 px-6 font-semibold text-primary">{{ student.fullName }}</td>
              <td class="py-4 px-6">
                <span
                  :class="[
                    student.gender === 'Nam' ? 'bg-on-tertiary-container/10 text-on-tertiary-container' : 
                    student.gender === 'Nữ' ? 'bg-pink-500/10 text-pink-600' : 
                    'bg-slate-500/10 text-slate-700',
                    'px-2.5 py-0.5 rounded-full text-[11px] font-semibold'
                  ]"
                >
                  {{ student.gender }}
                </span>
              </td>
              <td class="py-4 px-6 text-on-surface-variant">{{ formatDate(student.dateOfBirth) }}</td>
              <td class="py-4 px-6 font-semibold">{{ student.phone || '-' }}</td>
              <td class="py-4 px-6 text-on-surface-variant">{{ student.email || '-' }}</td>
              <td class="py-4 px-6 text-on-surface-variant max-w-xs truncate" :title="student.address">
                {{ student.address || '-' }}
              </td>
              <td class="py-4 px-6 text-right">
                <div class="flex items-center justify-end gap-1.5">
                  <button
                    @click.stop="$emit('view-enrollments', student)"
                    class="w-8 h-8 rounded-lg bg-on-tertiary-container/10 hover:bg-on-tertiary-container/20 text-on-tertiary-container flex items-center justify-center transition-colors animate-press"
                    title="Khóa học & lớp học đăng ký"
                  >
                    <span class="material-symbols-outlined text-[18px]">school</span>
                  </button>
                  <router-link
                    v-if="authStore.isAdmin"
                    :to="`/student-portal?studentId=${student.studentId}`"
                    @click.stop
                    class="w-8 h-8 rounded-lg bg-on-tertiary-container/10 hover:bg-on-tertiary-container/20 text-on-tertiary-container flex items-center justify-center transition-colors"
                    title="Xem Cổng học tập"
                  >
                    <span class="material-symbols-outlined text-[18px]">dashboard</span>
                  </router-link>
                  <button
                    v-if="authStore.isAdmin"
                    @click.stop="$emit('open-edit-dialog', student)"
                    class="w-8 h-8 rounded-lg bg-on-tertiary-container/10 hover:bg-on-tertiary-container/20 text-on-tertiary-container flex items-center justify-center transition-colors animate-press"
                    title="Sửa thông tin"
                  >
                    <span class="material-symbols-outlined text-[18px]">edit</span>
                  </button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <div v-else class="p-12 text-center flex flex-col items-center justify-center">
        <span class="material-symbols-outlined text-primary-container/30 text-[64px] mb-4">person</span>
        <p class="text-body-lg text-on-surface-variant font-medium">Không tìm thấy học viên nào phù hợp</p>
        <router-link
          v-if="authStore.isAdmin"
          to="/users"
          class="mt-4 px-6 py-2 bg-on-tertiary-container/10 text-on-tertiary-container border border-on-tertiary-container/20 rounded-lg font-semibold hover:bg-on-tertiary-container/20 transition-all inline-block cursor-pointer"
        >
          Đi tới Quản lý tài khoản để thêm học viên mới
        </router-link>
      </div>

      <!-- Pagination Footer -->
      <div v-if="students.length > 0" class="p-4 border-t border-white/40 flex justify-between items-center text-body-sm text-on-surface-variant bg-surface/50">
        <span>Hiển thị {{ (pagination.page - 1) * pagination.pageSize + 1 }} - {{ Math.min(pagination.page * pagination.pageSize, totalCount) }} trong số {{ totalCount }} học viên</span>
        <div class="flex items-center gap-2">
          <button
            @click="$emit('prev-page')"
            :disabled="pagination.page === 1"
            class="px-3 py-1.5 rounded-lg bg-white/50 hover:bg-white border border-outline-variant/30 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center"
          >
            <span class="material-symbols-outlined text-[18px]">chevron_left</span>
          </button>
          <span class="px-3 font-semibold text-primary">Trang {{ pagination.page }} / {{ totalPages }}</span>
          <button
            @click="$emit('next-page')"
            :disabled="pagination.page >= totalPages"
            class="px-3 py-1.5 rounded-lg bg-white/50 hover:bg-white border border-outline-variant/30 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center"
          >
            <span class="material-symbols-outlined text-[18px]">chevron_right</span>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useAuthStore } from '../../../stores'

const authStore = useAuthStore()

defineProps({
  students: { type: Array, required: true },
  loading: { type: Boolean, required: true },
  totalCount: { type: Number, required: true },
  pagination: { type: Object, required: true },
  totalPages: { type: Number, required: true },
  filters: { type: Object, required: true },
  classes: { type: Array, required: true }
})

defineEmits([
  'update-filter',
  'open-edit-dialog',
  'view-enrollments',
  'prev-page',
  'next-page'
])

function formatDate(date) {
  if (!date) return ''
  return new Date(date).toLocaleDateString('vi-VN')
}
</script>

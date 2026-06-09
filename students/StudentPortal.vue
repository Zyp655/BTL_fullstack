<template>
  <div class="space-y-gutter">
    <!-- Student Selector (Admin Only) -->
    <div v-if="authStore.isAdmin" class="bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl p-5 shadow-[0_12px_24px_rgba(0,0,0,0.05)] flex flex-col sm:flex-row items-center justify-between gap-4">
      <div class="flex items-center gap-3">
        <span class="material-symbols-outlined text-[28px] text-on-tertiary-container">person_search</span>
        <div>
          <h3 class="font-title-md text-body-lg font-bold text-primary-container">Chế độ Quản trị viên</h3>
          <p class="text-body-sm text-on-surface-variant">Xem và quản lý cổng học tập của học viên</p>
        </div>
      </div>
      <div class="relative w-full sm:w-80">
        <select
          v-model="selectedStudentId"
          class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg appearance-none pl-4 pr-10 py-2.5 text-body-sm text-primary bg-transparent cursor-pointer focus:outline-none focus:border-on-tertiary-container/30 transition-colors"
        >
          <option :value="null">-- Chọn học viên --</option>
          <option v-for="student in students" :key="student.studentId" :value="student.studentId">
            {{ student.fullName }} (HV-{{ String(student.studentId).padStart(4, '0') }})
          </option>
        </select>
        <span class="material-symbols-outlined absolute right-3 top-1/2 -translate-y-1/2 text-on-surface-variant pointer-events-none">expand_more</span>
      </div>
    </div>

    <template v-if="studentProfile">
      <!-- Hero Profile Section -->
      <div class="relative overflow-hidden bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl p-gutter shadow-[0_12px_24px_rgba(0,0,0,0.05)] flex flex-col md:flex-row items-center md:items-start gap-gutter">
        <!-- Background decorative gradients -->
        <div class="absolute -right-16 -top-16 w-48 h-48 rounded-full bg-primary/10 blur-3xl pointer-events-none"></div>
        <div class="absolute -left-16 -bottom-16 w-48 h-48 rounded-full bg-on-tertiary-container/10 blur-3xl pointer-events-none"></div>

        <!-- Avatar block -->
        <div class="w-24 h-24 rounded-full border-4 border-white shadow-md bg-primary-container text-white flex items-center justify-center font-bold text-[36px] shrink-0">
          {{ (studentProfile?.fullName || authStore.currentUser?.fullName || 'N').charAt(0).toUpperCase() }}
        </div>

        <!-- Profile details -->
        <div class="flex-1 text-center md:text-left space-y-3">
          <div>
            <div class="flex flex-col md:flex-row md:items-center gap-3 justify-center md:justify-start">
              <h2 class="font-headline-lg text-headline-lg text-primary-container">{{ studentProfile?.fullName || authStore.currentUser?.fullName }}</h2>
              <span class="bg-primary-container/10 text-primary-container rounded-full px-3 py-1 text-body-sm font-semibold self-center md:self-auto">
                Mã HV: {{ studentProfile?.studentId ? `HV-${String(studentProfile.studentId).padStart(4, '0')}` : 'Đang xử lý' }}
              </span>
            </div>
            <p class="text-body-sm text-on-surface-variant mt-1">Cổng thông tin học tập & học phí cá nhân</p>
          </div>

          <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-y-2 gap-x-6 text-body-sm text-on-surface pt-3 border-t border-white/20">
            <div class="flex items-center justify-center md:justify-start gap-2">
              <span class="material-symbols-outlined text-[18px] text-on-tertiary-container">mail</span>
              <span class="truncate">{{ studentProfile?.email || authStore.currentUser?.email || 'Chưa cập nhật email' }}</span>
            </div>
            <div class="flex items-center justify-center md:justify-start gap-2">
              <span class="material-symbols-outlined text-[18px] text-on-tertiary-container">call</span>
              <span>{{ studentProfile?.phone || authStore.currentUser?.phone || 'Chưa cập nhật SĐT' }}</span>
            </div>
            <div class="flex items-center justify-center md:justify-start gap-2">
              <span class="material-symbols-outlined text-[18px] text-on-tertiary-container">wc</span>
              <span>Giới tính: {{ studentProfile?.gender || 'Chưa cập nhật' }}</span>
            </div>
            <div class="flex items-center justify-center md:justify-start gap-2">
              <span class="material-symbols-outlined text-[18px] text-on-tertiary-container">calendar_month</span>
              <span>Ngày sinh: {{ formatDate(studentProfile?.dateOfBirth) }}</span>
            </div>
            <div class="flex items-center justify-center md:justify-start gap-2 sm:col-span-2">
              <span class="material-symbols-outlined text-[18px] text-on-tertiary-container">home</span>
              <span class="truncate" :title="studentProfile?.address">{{ studentProfile?.address || 'Chưa cập nhật địa chỉ' }}</span>
            </div>
          </div>
        </div>
      </div>

      <!-- Overview Statistics Grid -->
      <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-4">
        <!-- Total enrolled classes -->
        <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-5 rounded-xl flex items-center gap-4">
          <div class="w-12 h-12 rounded-full bg-primary-container/10 text-primary-container flex items-center justify-center shrink-0">
            <span class="material-symbols-outlined text-[28px]">groups</span>
          </div>
          <div>
            <p class="text-body-sm text-on-surface-variant">Lớp đang học</p>
            <h3 class="font-title-md text-title-md text-primary-container">{{ enrolledClasses.length }} lớp</h3>
          </div>
        </div>

        <!-- Average grade -->
        <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-5 rounded-xl flex items-center gap-4">
          <div class="w-12 h-12 rounded-full bg-purple-500/10 text-purple-600 flex items-center justify-center shrink-0">
            <span class="material-symbols-outlined text-[28px]">history_edu</span>
          </div>
          <div>
            <p class="text-body-sm text-on-surface-variant">Điểm trung bình</p>
            <h3 class="font-title-md text-title-md text-primary-container">{{ averageGrade > 0 ? averageGrade.toFixed(2) : 'Chưa có điểm' }}</h3>
          </div>
        </div>

        <!-- Attendance rate -->
        <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-5 rounded-xl flex items-center gap-4">
          <div class="w-12 h-12 rounded-full bg-emerald-500/10 text-emerald-600 flex items-center justify-center shrink-0">
            <span class="material-symbols-outlined text-[28px]">how_to_reg</span>
          </div>
          <div>
            <p class="text-body-sm text-on-surface-variant">Chuyên cần chung</p>
            <h3 class="font-title-md text-title-md text-primary-container">{{ averageAttendanceRate > 0 ? averageAttendanceRate.toFixed(1) + '%' : '—' }}</h3>
          </div>
        </div>

        <!-- Debt tuition fee -->
        <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-5 rounded-xl flex items-center gap-4">
          <div class="w-12 h-12 rounded-full bg-error/10 text-error flex items-center justify-center shrink-0">
            <span class="material-symbols-outlined text-[28px]">receipt_long</span>
          </div>
          <div>
            <p class="text-body-sm text-on-surface-variant">Học phí cần đóng</p>
            <h3 class="font-title-md text-title-md text-primary-container">{{ formatCurrency(totalDebt) }}</h3>
          </div>
        </div>

        <!-- Credit wallet balance -->
        <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-5 rounded-xl flex items-center gap-4">
          <div class="w-12 h-12 rounded-full bg-amber-500/10 text-amber-600 flex items-center justify-center shrink-0">
            <span class="material-symbols-outlined text-[28px]">account_balance_wallet</span>
          </div>
          <div>
            <p class="text-body-sm text-on-surface-variant">Số dư bảo lưu</p>
            <h3 class="font-title-md text-title-md text-primary-container">{{ formatCurrency(creditSummary?.totalAvailable || 0) }}</h3>
          </div>
        </div>
      </div>

      <!-- Navigation Tabs -->
      <div class="flex space-x-2 overflow-x-auto p-1 bg-white/40 backdrop-blur-[10px] rounded-xl border border-white/40">
        <button
          v-for="tab in tabs"
          :key="tab.value"
          @click="selectTab(tab.value)"
          :class="[
            activeTab === tab.value
              ? 'bg-primary-container text-white shadow-sm'
              : 'text-on-surface-variant hover:bg-surface-container-high',
            'px-5 py-2.5 text-body-sm font-semibold transition-all rounded-lg flex items-center gap-2 whitespace-nowrap cursor-pointer'
          ]"
        >
          <span class="material-symbols-outlined text-[18px]">{{ tab.icon }}</span>
          {{ tab.label }}
        </button>
      </div>

      <!-- Loading screen -->
      <div v-if="loading" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-12 rounded-xl text-center flex flex-col items-center justify-center">
        <div class="w-12 h-12 border-4 border-primary-container/30 border-t-primary-container rounded-full animate-spin mb-4"></div>
        <p class="text-body-sm font-semibold text-primary-container">Đang tải dữ liệu học tập...</p>
      </div>

      <!-- Content Sections based on Active Tab -->
      <div v-else class="space-y-gutter">
        <TabClassesSchedules
          v-if="activeTab === 'classes'"
          :enrolled-classes="enrolledClasses"
          :selected-class="selectedClass"
          :schedules="schedules"
          :loading-schedules="loadingSchedules"
          :payments="payments"
          :my-support-messages="mySupportMessages"
          :current-conflicts="currentConflicts"
          @select-class="selectClass"
          @open-support-conflict="({ targetClass, conflictClass }) => openSupportDialogWithConflict(targetClass, conflictClass)"
          @open-transfer-modal="openTransferModal"
          @change-enrollment-status="({ cls, newStatus }) => changeEnrollmentStatus(cls, newStatus)"
          @open-support-dialog="openSupportDialog"
          @switch-tab="selectTab"
        />

        <TabGrades
          v-if="activeTab === 'grades'"
          :enrolled-classes="enrolledClasses"
          :exam-results="examResults"
          @open-grading-dialog="openGradingDialog"
        />

        <TabAttendance
          v-if="activeTab === 'attendance'"
          :attendance-summaries="attendanceSummaries"
          @change-attendance-status="({ session, newStatus }) => changeAttendanceStatus(session, newStatus)"
        />

        <TabPayments
          v-if="activeTab === 'payments'"
          :payments="payments"
          @open-payment-modal="openPaymentModal"
        />

        <TabCredits
          v-if="activeTab === 'credits'"
          :credit-summary="creditSummary"
        />
      </div>

      <!-- Dialog: Gửi yêu cầu hỗ trợ -->
      <teleport to="body">
        <div v-if="supportDialog" class="fixed inset-0 glass-backdrop z-[9999] flex items-center justify-center p-4">
          <div class="bg-white/90 backdrop-blur-[24px] border border-white/50 rounded-xl shadow-[0_20px_40px_rgba(0,31,63,0.12)] max-w-md w-full p-6 space-y-4 animate-scale-in flex flex-col">
            <div class="flex items-center gap-3 text-primary-container border-b border-white/40 pb-3 justify-between">
              <h3 class="font-title-md text-[18px] font-bold flex items-center gap-2">
                <span class="material-symbols-outlined text-[24px] text-on-tertiary-container">chat</span>
                Gửi yêu cầu hỗ trợ đổi lớp
              </h3>
              <button @click="supportDialog = false" class="text-on-surface-variant hover:text-primary-container cursor-pointer">
                <span class="material-symbols-outlined">close</span>
              </button>
            </div>
            
            <div class="space-y-4">
              <div class="space-y-1">
                <label class="text-body-sm font-semibold text-primary-container block">Chọn lớp hiện tại cần đổi *</label>
                <select
                  v-model="supportFromClassId"
                  class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg appearance-none px-4 py-2.5 text-body-sm text-primary focus:outline-none focus:border-on-tertiary-container transition-all cursor-pointer"
                >
                  <option :value="null" disabled>-- Chọn lớp học hiện tại --</option>
                  <option v-for="cls in enrolledClasses.filter(c => c.status === 'DangHoc')" :key="cls.classId" :value="cls.classId">
                    {{ cls.className }} ({{ cls.courseName }})
                  </option>
                </select>
              </div>

              <div v-if="supportFromClassId" class="space-y-1">
                <label class="text-body-sm font-semibold text-primary-container block">Chọn lớp muốn chuyển đến (Không bắt buộc)</label>
                <div v-if="loadingSupportAlternativeClasses" class="flex items-center justify-center py-2 gap-2">
                  <span class="animate-spin w-4 h-4 border-2 border-primary border-t-transparent rounded-full"></span>
                  <span class="text-body-xs text-on-surface-variant">Đang tìm lớp học...</span>
                </div>
                <div v-else class="relative">
                  <select
                    v-model="supportToClassId"
                    class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg appearance-none px-4 py-2.5 text-body-sm text-primary focus:outline-none focus:border-on-tertiary-container transition-all cursor-pointer"
                  >
                    <option :value="null">-- Chọn lớp học mới (Nếu có) --</option>
                    <option v-for="c in supportAlternativeClasses" :key="c.classId" :value="c.classId">
                      {{ c.className }} (Sĩ số: {{ c.currentStudents }}/{{ c.maxStudents }})
                    </option>
                  </select>
                </div>
              </div>

              <div class="space-y-1">
                <label class="text-body-sm font-semibold text-primary-container block">Nội dung yêu cầu hỗ trợ *</label>
                <textarea
                  v-model="supportMessageText"
                  rows="3"
                  class="w-full bg-primary-container/[0.05] border border-primary-container/10 rounded-lg px-3 py-2 text-body-sm text-primary placeholder-on-surface-variant/50 focus:outline-none focus:border-on-tertiary-container transition-all resize-none"
                  placeholder="Nhập lý do hoặc tin nhắn cho Admin (ví dụ: Em bị trùng lịch học, nhờ Admin chuyển lớp giúp em)..."
                ></textarea>
              </div>
            </div>

            <div class="flex justify-end gap-3 pt-3 border-t border-white/40">
              <button
                @click="supportDialog = false"
                class="px-4 py-2 rounded-lg border border-white/60 text-on-surface-variant font-semibold text-[13px] hover:bg-white/40 transition-colors cursor-pointer"
              >
                Hủy
              </button>
              <button
                @click="submitSupportMessage"
                :disabled="submittingSupport || !supportMessageText.trim() || !supportFromClassId"
                class="px-4 py-2 rounded-lg bg-primary-container text-white font-semibold text-[13px] hover:bg-primary transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-1 cursor-pointer active:scale-95"
              >
                <span v-if="submittingSupport" class="animate-spin w-4 h-4 border-2 border-white border-t-transparent rounded-full mr-1"></span>
                Gửi yêu cầu
              </button>
            </div>
          </div>
        </div>
      </teleport>

      <!-- Modal: Admin Chuyển lớp học viên -->
      <ModalTransferClass
        :show="adminTransferModal"
        :enrolled-classes="enrolledClasses"
        :student-profile="studentProfile"
        @close="adminTransferModal = false"
        @success="async () => { adminTransferModal = false; await loadPortalDataForStudent(studentProfile.studentId, studentProfile.userId); }"
      />

      <!-- Modal: Admin Nhập/Sửa điểm -->
      <ModalGrading
        :show="adminGradingModal"
        :grading-class="gradingClass"
        :exam-results="examResults"
        :student-profile="studentProfile"
        @close="adminGradingModal = false"
        @success="async () => { adminGradingModal = false; await loadPortalDataForStudent(studentProfile.studentId, studentProfile.userId); }"
      />

      <!-- Modal: Admin Ghi nhận thanh toán -->
      <ModalPaymentRecord
        :show="adminPaymentModal"
        :payment-bill="paymentBill"
        :student-profile="studentProfile"
        @close="adminPaymentModal = false"
        @success="async () => { adminPaymentModal = false; await loadPortalDataForStudent(studentProfile.studentId, studentProfile.userId); }"
      />
    </template>

    <!-- No Student Selected State -->
    <div v-else-if="!loading" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-12 rounded-xl text-center flex flex-col items-center justify-center">
      <span class="material-symbols-outlined text-primary-container/30 text-[64px] mb-4">school</span>
      <h3 class="font-title-md text-headline-sm text-primary-container mb-2">Chưa chọn học viên</h3>
      <p class="text-body-md text-on-surface-variant max-w-md">Vui lòng chọn một học viên từ danh sách ở trên để xem thời khóa biểu, điểm số, chuyên cần và học phí của họ.</p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted, computed, inject, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useAuthStore, useStudentStore } from '../../stores'
import api from '../../services/api'
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

import TabClassesSchedules from './components/TabClassesSchedules.vue'
import TabGrades from './components/TabGrades.vue'
import TabAttendance from './components/TabAttendance.vue'
import TabPayments from './components/TabPayments.vue'
import TabCredits from './components/TabCredits.vue'
import ModalTransferClass from './components/ModalTransferClass.vue'
import ModalGrading from './components/ModalGrading.vue'
import ModalPaymentRecord from './components/ModalPaymentRecord.vue'

const route = useRoute()
const router = useRouter()

const authStore = useAuthStore()
const studentStore = useStudentStore()
const showSnackbar = inject('showSnackbar')

const students = ref([])
const selectedStudentId = ref(null)

const loading = ref(true)
const studentProfile = ref(null)
const enrolledClasses = ref([])
const selectedClass = ref(null)
const schedules = ref([])
const loadingSchedules = ref(false)
const examResults = ref({}) // mapped by enrollmentId
const attendanceSummaries = ref([])
const payments = ref([])
const creditSummary = ref(null)

const enrolledSchedulesMap = ref({})
const supportMessageText = ref('')
const submittingSupport = ref(false)
const mySupportMessages = ref([])

// Support Request dialog state
const supportDialog = ref(false)
const supportFromClassId = ref(null)
const supportToClassId = ref(null)
const supportAlternativeClasses = ref([])
const loadingSupportAlternativeClasses = ref(false)

const activeTab = ref(route.query.tab || 'classes')
const tabs = [
  { label: 'Lớp học & Lịch học', value: 'classes', icon: 'school' },
  { label: 'Bảng điểm của tôi', value: 'grades', icon: 'history_edu' },
  { label: 'Nhật ký chuyên cần', value: 'attendance', icon: 'how_to_reg' },
  { label: 'Học phí & Thanh toán', value: 'payments', icon: 'receipt_long' },
  { label: 'Ví bảo lưu', value: 'credits', icon: 'account_balance_wallet' }
]

const selectTab = (tabValue) => {
  activeTab.value = tabValue
  router.replace({ path: route.path, query: { tab: tabValue } })
}

watch(() => route.query.tab, (newTab) => {
  if (newTab && tabs.some(t => t.value === newTab)) {
    activeTab.value = newTab
  } else if (!newTab) {
    activeTab.value = 'classes'
  }
})

async function loadStudentsList() {
  try {
    const res = await api.get('/api/v1/students', { params: { pageSize: 1000 } })
    students.value = res.data.items || []
  } catch (err) {
    console.error('Error fetching students list for admin:', err)
  }
}

watch(selectedStudentId, async (newVal) => {
  if (newVal) {
    router.replace({ path: route.path, query: { ...route.query, studentId: newVal } })
    const student = students.value.find(s => s.studentId === newVal)
    if (student) {
      studentProfile.value = student
      await loadPortalDataForStudent(newVal, student.userId)
    } else {
      try {
        const res = await api.get(`/api/v1/students/${newVal}`)
        studentProfile.value = res.data
        await loadPortalDataForStudent(newVal, res.data.userId)
      } catch (e) {
        console.error('Error fetching student details:', e)
      }
    }
  } else {
    studentProfile.value = null
    enrolledClasses.value = []
    selectedClass.value = null
    schedules.value = []
    examResults.value = {}
    attendanceSummaries.value = []
    payments.value = []
    creditSummary.value = null
    mySupportMessages.value = []
    // Remove studentId from query
    const newQuery = { ...route.query }
    delete newQuery.studentId
    router.replace({ path: route.path, query: newQuery })
  }
})

// Fetch data
async function loadPortalData() {
  loading.value = true
  try {
    if (authStore.isAdmin) {
      await loadStudentsList()
      
      const queryStudentId = route.query.studentId ? parseInt(route.query.studentId, 10) : null
      if (queryStudentId) {
        selectedStudentId.value = queryStudentId
      } else {
        studentProfile.value = null
        loading.value = false
      }
      return
    }

    const userId = authStore.currentUser?.userId
    if (!userId) {
      loading.value = false
      return
    }

    // 1. Fetch aggregated student summary from API Gateway
    try {
      const summaryRes = await api.get(`/api/v1/portal/student-summary/${userId}`)
      const summary = summaryRes.data
      
      studentProfile.value = summary.profile
      payments.value = summary.payments || []
      enrolledClasses.value = summary.enrollments || []
      attendanceSummaries.value = summary.attendanceSummary || []
      creditSummary.value = summary.creditSummary || null

      const studentId = studentProfile.value.studentId

      // Load class schedules and scores which depend on enrolled classes
      if (enrolledClasses.value.length > 0) {
        selectedClass.value = enrolledClasses.value[0]
        if (!isClassUnpaid(selectedClass.value.classId)) {
          await fetchClassSchedules(selectedClass.value.classId)
        } else {
          schedules.value = []
        }
        
        examResults.value = {}
        for (const cls of enrolledClasses.value) {
          try {
            const scoreRes = await api.get(`/api/v1/results/enrollment/${cls.enrollmentId}`)
            examResults.value[cls.enrollmentId] = scoreRes.data || []
          } catch (e) {
            console.error(`Error loading score for enrollment ${cls.enrollmentId}:`, e)
          }
        }
      } else {
        selectedClass.value = null
        schedules.value = []
      }

      // Fetch schedules for each enrolled class to check for conflicts
      enrolledSchedulesMap.value = {}
      for (const cls of enrolledClasses.value) {
        if (!isClassUnpaid(cls.classId) && cls.status === 'DangHoc') {
          try {
            const schedRes = await api.get(`/api/v1/classes/${cls.classId}/schedules`)
            enrolledSchedulesMap.value[cls.classId] = schedRes.data || []
          } catch (e) {
            console.error(`Error loading schedules for class ${cls.classId}:`, e)
          }
        }
      }

      // Fetch support messages
      try {
        const msgRes = await api.get('/api/v1/support-messages/my-messages')
        mySupportMessages.value = msgRes.data || []
      } catch (e) {
        console.error('Error fetching support messages:', e)
      }

      // Initialize WebSockets
      setupSignalR()

    } catch (err) {
      console.error('Error fetching student summary:', err)
      showSnackbar('Hồ sơ học tập của bạn chưa được tạo hoặc lỗi liên kết.', 'error')
    } finally {
      loading.value = false
    }
  } catch (err) {
    console.error('Error loading portal:', err)
    showSnackbar('Có lỗi xảy ra khi tải thông tin học vụ.', 'error')
    loading.value = false
  }
}

async function loadPortalDataForStudent(studentId, userId) {
  loading.value = true
  try {
    // 2. Fetch payments / tuition first
    try {
      const payRes = await api.get(`/api/v1/payments/student/${userId}`)
      payments.value = payRes.data || []
    } catch (e) {
      console.error('Error fetching payments:', e)
    }

    // 3. Fetch enrolled classes
    const classRes = await api.get(`/api/v1/students/${studentId}/enrollments`)
    enrolledClasses.value = classRes.data || []
    
    if (enrolledClasses.value.length > 0) {
      // Pre-select first class
      selectedClass.value = enrolledClasses.value[0]
      if (!isClassUnpaid(selectedClass.value.classId)) {
        await fetchClassSchedules(selectedClass.value.classId)
      } else {
        schedules.value = []
      }
      
      // Fetch exam results for each enrollment
      examResults.value = {}
      for (const cls of enrolledClasses.value) {
        try {
          const scoreRes = await api.get(`/api/v1/results/enrollment/${cls.enrollmentId}`)
          examResults.value[cls.enrollmentId] = scoreRes.data || []
        } catch (e) {
          console.error(`Error loading score for enrollment ${cls.enrollmentId}:`, e)
        }
      }
    } else {
      selectedClass.value = null
      schedules.value = []
    }

    // 4. Fetch attendance summary
    try {
      const attRes = await api.get(`/api/v1/attendances/student/${studentId}/summary`)
      attendanceSummaries.value = attRes.data || []
    } catch (e) {
      console.error('Error fetching attendance:', e)
    }

    // 5. Fetch student credits
    try {
      const creditRes = await api.get(`/api/v1/enrollments/student-credits/${studentId}`)
      creditSummary.value = creditRes.data
    } catch (e) {
      console.error('Error fetching student credits:', e)
    }

    // 6. Fetch schedules for each enrolled class to check for conflicts
    enrolledSchedulesMap.value = {}
    for (const cls of enrolledClasses.value) {
      if (!isClassUnpaid(cls.classId) && cls.status === 'DangHoc') {
        try {
          const schedRes = await api.get(`/api/v1/classes/${cls.classId}/schedules`)
          enrolledSchedulesMap.value[cls.classId] = schedRes.data || []
        } catch (e) {
          console.error(`Error loading schedules for class ${cls.classId}:`, e)
        }
      }
    }

    // 7. Fetch support messages
    try {
      if (authStore.isAdmin) {
        const msgRes = await api.get('/api/v1/support-messages')
        mySupportMessages.value = (msgRes.data || []).filter(m => m.studentId === studentId)
      } else {
        const msgRes = await api.get('/api/v1/support-messages/my-messages')
        mySupportMessages.value = msgRes.data || []
      }
    } catch (e) {
      console.error('Error fetching support messages:', e)
    }

    // Initialize WebSockets
    if (!authStore.isAdmin) {
      setupSignalR()
    }

  } catch (err) {
    console.error('Error loading student data:', err)
    showSnackbar('Có lỗi xảy ra khi tải dữ liệu học tập.', 'error')
  } finally {
    loading.value = false
  }
}

// Check if a class has unpaid status
function isClassUnpaid(classId) {
  if (!payments.value) return false
  const payment = payments.value.find(p => p.classId === classId)
  return payment && payment.status === 'ChuaTT'
}

// Select class and load schedules
async function selectClass(cls) {
  selectedClass.value = cls
  if (!isClassUnpaid(cls.classId)) {
    await fetchClassSchedules(cls.classId)
  } else {
    schedules.value = []
  }
}

async function fetchClassSchedules(classId) {
  loadingSchedules.value = true
  try {
    const res = await api.get(`/api/v1/classes/${classId}/schedules`)
    schedules.value = res.data || []
  } catch (e) {
    console.error('Error fetching class schedules:', e)
    schedules.value = []
  } finally {
    loadingSchedules.value = false
  }
}

// Calculate summary stats
const averageGrade = computed(() => {
  let totalScore = 0
  let count = 0
  
  Object.values(examResults.value).forEach(resultsList => {
    resultsList.forEach(res => {
      totalScore += res.score
      count++
    })
  })
  
  return count > 0 ? totalScore / count : 0
})

function getAverageScoreForClass(enrollmentId) {
  const scores = examResults.value[enrollmentId] || []
  if (scores.length === 0) return 0
  let total = 0
  scores.forEach(s => {
    total += s.score
  })
  return total / scores.length
}

const averageAttendanceRate = computed(() => {
  if (attendanceSummaries.value.length === 0) return 0
  const total = attendanceSummaries.value.reduce((acc, curr) => acc + curr.attendanceRate, 0)
  return total / attendanceSummaries.value.length
})

const totalDebt = computed(() => {
  if (payments.value.length === 0) return 0
  return payments.value.reduce((acc, curr) => acc + curr.remainingAmount, 0)
})

// Helpers
function formatDate(dateStr) {
  if (!dateStr) return '—'
  const d = new Date(dateStr)
  return d.toLocaleDateString('vi-VN')
}

function formatDateTime(dateStr) {
  if (!dateStr) return '—'
  const d = new Date(dateStr)
  return d.toLocaleString('vi-VN', {
    hour: '2-digit',
    minute: '2-digit',
    day: '2-digit',
    month: '2-digit',
    year: 'numeric'
  })
}

function formatCurrency(amount) {
  if (amount === undefined || amount === null) return '0 đ'
  return amount.toLocaleString('vi-VN') + ' đ'
}

function formatDayOfWeek(day) {
  const map = { 1: 'Hai', 2: 'Ba', 3: 'Tư', 4: 'Năm', 5: 'Sáu', 6: 'Bảy', 0: 'Chủ Nhật' }
  return map[day] || day
}

function getEnrollmentStatusClass(status) {
  const map = { DangHoc: 'status-inprogress', DaXong: 'status-completed', BoHoc: 'status-cancelled' }
  return map[status] || 'status-opened'
}

function getEnrollmentStatusLabel(status) {
  const map = { DangHoc: 'Đang học', DaXong: 'Hoàn thành', BoHoc: 'Đã bỏ học' }
  return map[status] || status
}

// Helpers for schedule conflicts
function parseTimeToMinutes(timeStr) {
  if (!timeStr) return 0
  const parts = timeStr.split(':')
  const hours = parseInt(parts[0], 10) || 0
  const minutes = parseInt(parts[1], 10) || 0
  return hours * 60 + minutes
}

function isScheduleConflict(s1, s2) {
  if (s1.dayOfWeek !== s2.dayOfWeek) return false
  const start1 = parseTimeToMinutes(s1.startTime)
  const end1 = parseTimeToMinutes(s1.endTime)
  const start2 = parseTimeToMinutes(s2.startTime)
  const end2 = parseTimeToMinutes(s2.endTime)
  return start1 < end2 && start2 < end1
}

const currentConflicts = computed(() => {
  const conflicts = []
  const classes = enrolledClasses.value.filter(c => c.status === 'DangHoc')
  for (let i = 0; i < classes.length; i++) {
    for (let j = i + 1; j < classes.length; j++) {
      const classA = classes[i]
      const classB = classes[j]
      const schedsA = enrolledSchedulesMap.value[classA.classId] || []
      const schedsB = enrolledSchedulesMap.value[classB.classId] || []
      
      let hasOverlap = false
      let overlappingSession = null
      for (const sa of schedsA) {
        for (const sb of schedsB) {
          if (isScheduleConflict(sa, sb)) {
            hasOverlap = true
            overlappingSession = { sa, sb }
            break
          }
        }
        if (hasOverlap) break
      }
      
      if (hasOverlap) {
        conflicts.push({
          classA,
          classB,
          sessionA: overlappingSession.sa,
          sessionB: overlappingSession.sb
        })
      }
    }
  }
  return conflicts
})

// Watch supportFromClassId to fetch alternative classes
watch(supportFromClassId, async (newClassId) => {
  if (!newClassId) {
    supportAlternativeClasses.value = []
    supportToClassId.value = null
    return
  }
  loadingSupportAlternativeClasses.value = true
  try {
    const currentEnrollment = enrolledClasses.value.find(c => c.classId === newClassId)
    if (!currentEnrollment) return
    
    // Auto-update message text to target the selected class if not already custom edited by user
    if (!supportMessageText.value || supportMessageText.value.startsWith('Em bị trùng lịch học') || supportMessageText.value.startsWith('Em muốn đổi lớp')) {
      supportMessageText.value = `Em muốn đổi lớp ${currentEnrollment.className}. Nhờ Admin chuyển lớp giúp em.`
    }
    
    const res = await api.get('/api/v1/classes', {
      params: { courseId: currentEnrollment.courseId, pageSize: 100 }
    })
    supportAlternativeClasses.value = (res.data?.items || []).filter(
      c => c.classId !== newClassId && (c.status === 'Opened' || c.status === 'InProgress')
    )
  } catch (e) {
    console.error('Error fetching support alternative classes:', e)
  } finally {
    loadingSupportAlternativeClasses.value = false
  }
})

function openSupportDialog() {
  supportFromClassId.value = null
  supportToClassId.value = null
  supportMessageText.value = ''
  supportDialog.value = true
}

function openSupportDialogWithConflict(targetClass, conflictClass) {
  if (targetClass && conflictClass) {
    supportFromClassId.value = targetClass.classId
    supportMessageText.value = `Em bị trùng lịch học ở lớp ${targetClass.className} (trùng với lớp ${conflictClass.className}). Nhờ Admin chuyển lớp giúp em.`
  } else {
    supportFromClassId.value = null
    supportMessageText.value = ''
  }
  supportToClassId.value = null
  supportDialog.value = true
}

async function submitSupportMessage() {
  if (!supportMessageText.value.trim() || !studentProfile.value || !supportFromClassId.value) return
  submittingSupport.value = true
  try {
    const fromClass = enrolledClasses.value.find(c => c.classId === supportFromClassId.value)
    const toClassId = supportToClassId.value
    let toClassName = null
    if (toClassId) {
      const found = supportAlternativeClasses.value.find(c => c.classId === toClassId)
      if (found) toClassName = found.className
    }

    await api.post('/api/v1/support-messages', {
      studentId: studentProfile.value.studentId,
      message: supportMessageText.value.trim(),
      fromClassId: fromClass?.classId,
      fromClassName: fromClass?.className,
      toClassId: toClassId,
      toClassName: toClassName
    })

    showSnackbar('Đã gửi tin nhắn yêu cầu hỗ trợ tới Admin!', 'success')
    supportMessageText.value = ''
    supportDialog.value = false
    
    // Reload support messages
    const msgRes = await api.get('/api/v1/support-messages/my-messages')
    mySupportMessages.value = msgRes.data || []
  } catch (e) {
    console.error('Error submitting support message:', e)
    showSnackbar(e.response?.data?.message || 'Lỗi khi gửi tin nhắn cho Admin', 'error')
  } finally {
    submittingSupport.value = false
  }
}

function getStatusClass(status) {
  const map = {
    Pending: 'status-inprogress',
    Resolved: 'status-completed',
    Rejected: 'status-cancelled'
  }
  return map[status] || 'status-inprogress'
}

function getStatusLabel(status) {
  const map = {
    Pending: 'Chờ duyệt',
    Resolved: 'Đã duyệt',
    Rejected: 'Đã từ chối'
  }
  return map[status] || status
}

let signalrConnection = null

function setupSignalR() {
  if (signalrConnection) return
  
  const token = authStore.token || authStore.currentUser?.token
  if (!token) return
  
  signalrConnection = new HubConnectionBuilder()
    .withUrl('/hubs/support', {
      accessTokenFactory: () => token
    })
    .withAutomaticReconnect()
    .configureLogging(LogLevel.Warning)
    .build()
    
  signalrConnection.on('SupportMessageStatusChanged', async (msg) => {
    try {
      // Reload support messages
      if (authStore.isAdmin) {
        const msgRes = await api.get('/api/v1/support-messages')
        mySupportMessages.value = (msgRes.data || []).filter(m => m.studentId === studentProfile.value?.studentId)
      } else {
        const msgRes = await api.get('/api/v1/support-messages/my-messages')
        mySupportMessages.value = msgRes.data || []
      }
      
      if (msg.status === 'Resolved') {
        showSnackbar(`Yêu cầu đổi lớp của bạn đã được phê duyệt! Lịch học đã được cập nhật.`, 'success')
        await loadPortalData()
      } else if (msg.status === 'Rejected') {
        showSnackbar(`Yêu cầu đổi lớp của bạn đã bị từ chối.`, 'info')
      }
    } catch (err) {
      console.error('Error handling WebSocket message status change:', err)
    }
  })
  
  signalrConnection.start()
    .then(() => console.log('Connected to SupportHub WebSocket!'))
    .catch(err => console.error('Error starting SupportHub connection:', err))
}

// --- ADMIN EDIT & ACTION LOGIC ---
const adminTransferModal = ref(false)
const adminGradingModal = ref(false)
const gradingClass = ref(null)
const adminPaymentModal = ref(false)
const paymentBill = ref(null)

async function changeEnrollmentStatus(cls, newStatus) {
  try {
    await api.put(`/api/v1/enrollments/${cls.enrollmentId}/status`, {
      id: cls.enrollmentId,
      status: newStatus
    })
    showSnackbar('Đã cập nhật trạng thái lớp học!', 'success')
    await loadPortalDataForStudent(studentProfile.value.studentId, studentProfile.value.userId)
  } catch (err) {
    console.error('Error changing enrollment status:', err)
    showSnackbar(err.response?.data?.message || 'Không thể cập nhật trạng thái lớp học', 'error')
  }
}

function openTransferModal() {
  adminTransferModal.value = true
}

function openGradingDialog(cls) {
  gradingClass.value = cls
  adminGradingModal.value = true
}

async function changeAttendanceStatus(session, newStatus) {
  try {
    await api.put(`/api/v1/attendances/${session.attendanceId}`, {
      id: session.attendanceId,
      status: newStatus,
      note: session.note || null
    })
    showSnackbar('Đã cập nhật trạng thái điểm danh!', 'success')
    await loadPortalDataForStudent(studentProfile.value.studentId, studentProfile.value.userId)
  } catch (err) {
    console.error('Error updating attendance status:', err)
    showSnackbar(err.response?.data?.message || 'Lỗi khi cập nhật trạng thái điểm danh', 'error')
  }
}

function openPaymentModal(pay) {
  paymentBill.value = pay
  adminPaymentModal.value = true
}

onUnmounted(() => {
  if (signalrConnection) {
    signalrConnection.stop()
    signalrConnection = null
  }
})

onMounted(loadPortalData)
</script>

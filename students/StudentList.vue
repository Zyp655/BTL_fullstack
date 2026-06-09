<template>
  <div class="space-y-stack-lg animate-fade-in">
    <!-- Header -->
    <section class="flex flex-col md:flex-row md:items-center justify-between gap-4">
      <div>
        <h1 class="font-headline-lg text-headline-lg text-primary-container flex items-center gap-3">
          <span class="material-symbols-outlined text-[32px]">person</span>
          Quản lý học viên
        </h1>
        <p class="font-body-lg text-on-surface-variant mt-1">Quản lý hồ sơ học viên, thông tin liên hệ và xem chi tiết đăng ký lớp học.</p>
      </div>
      <div class="flex items-center gap-3">
        <button
          v-if="authStore.isAdmin"
          @click="importDialogModal = true"
          class="px-5 py-3 rounded-lg border border-outline-variant text-on-surface-variant bg-transparent font-semibold text-[14px] hover:bg-white/40 shadow-sm transition-all flex items-center gap-2 active:scale-95 cursor-pointer"
        >
          <span class="material-symbols-outlined text-[20px] text-on-tertiary-container">publish</span>
          Nhập từ Excel
        </button>
        <button
          v-if="authStore.isAdmin"
          @click="openCreateDialog"
          class="bg-primary-container text-white px-6 py-3 rounded-lg font-semibold text-[14px] shadow-sm hover:bg-primary hover:shadow-md transition-all flex items-center gap-2 active:scale-95 cursor-pointer"
        >
          <span class="material-symbols-outlined text-[20px]">person_add</span>
          Tạo tài khoản học viên mới
        </button>
      </div>
    </section>

    <!-- Stats Row -->
    <section class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-gutter">
      <div v-for="stat in stats" :key="stat.label" class="bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-5 flex items-center gap-4 hover:shadow-md hover:-translate-y-0.5 transition-all duration-300">
        <div :class="[stat.bgColor, stat.textColor, 'w-12 h-12 rounded-lg flex items-center justify-center shrink-0']">
          <span class="material-symbols-outlined">{{ stat.icon }}</span>
        </div>
        <div>
          <div class="text-2xl font-bold text-primary">{{ stat.value }}</div>
          <div class="text-[12px] text-on-surface-variant font-medium">{{ stat.label }}</div>
        </div>
      </div>
    </section>

    <!-- Student Table (incorporating search filters and pagination controls) -->
    <StudentTable
      :students="store.students"
      :loading="store.loading"
      :total-count="store.totalCount"
      :pagination="pagination"
      :total-pages="totalPages"
      :filters="filters"
      :classes="classes"
      @update-filter="updateFilter"
      @open-edit-dialog="openEditDialog"
      @view-enrollments="viewEnrollments"
      @prev-page="prevPage"
      @next-page="nextPage"
    />

    <!-- Student Dialog (Create/Edit Profile) -->
    <StudentDialog
      :show="studentDialogModal"
      :student="selectedStudent"
      :users="store.users"
      @close="studentDialogModal = false"
      @success="onDialogSuccess"
    />

    <!-- Student Details Sidebar -->
    <StudentDetailSidebar
      :show="enrollmentDialogModal"
      :student="selectedStudent"
      :enrollments="enrollments"
      :loading="loadingEnrollments"
      @close="enrollmentDialogModal = false"
    />

    <!-- Excel Import Modal -->
    <StudentImportModal
      :show="importDialogModal"
      @close="importDialogModal = false"
      @success="onImportSuccess"
    />
  </div>
</template>

<script setup>
import { ref, computed, onMounted, inject, watch } from 'vue'
import { useStudentStore, useAuthStore } from '../../stores'
import api from '../../services/api'

import StudentTable from './components/StudentTable.vue'
import StudentDialog from './components/StudentDialog.vue'
import StudentDetailSidebar from './components/StudentDetailSidebar.vue'
import StudentImportModal from './components/StudentImportModal.vue'

const store = useStudentStore()
const authStore = useAuthStore()
const showSnackbar = inject('showSnackbar')

const teacherAllStudents = ref([])
const filteredAllStudents = ref(null)

const studentDialogModal = ref(false)
const enrollmentDialogModal = ref(false)
const importDialogModal = ref(false)

const loadingEnrollments = ref(false)
const enrollments = ref([])
const selectedStudent = ref(null)

const filters = ref({
  search: '',
  gender: null,
  classId: null,
})

const classes = ref([])
const loadingClasses = ref(false)

const pagination = ref({
  page: 1,
  pageSize: 10,
})

const stats = computed(() => {
  if (filteredAllStudents.value !== null) {
    const list = filteredAllStudents.value
    const total = list.length
    const male = list.filter(s => s.gender === 'Nam').length
    const female = list.filter(s => s.gender === 'Nữ').length
    const other = total - male - female
    return [
      {
        label: 'Tổng học viên',
        value: total,
        icon: 'group',
        bgColor: 'bg-primary-container/10',
        textColor: 'text-primary-container'
      },
      {
        label: 'Học viên Nam',
        value: male,
        icon: 'male',
        bgColor: 'bg-sky-500/10',
        textColor: 'text-sky-700'
      },
      {
        label: 'Học viên Nữ',
        value: female,
        icon: 'female',
        bgColor: 'bg-rose-500/10',
        textColor: 'text-rose-700'
      },
      {
        label: 'Giới tính Khác',
        value: other,
        icon: 'transgender',
        bgColor: 'bg-slate-500/10',
        textColor: 'text-slate-600'
      },
    ]
  } else {
    return [
      {
        label: 'Tổng học viên',
        value: store.studentStats?.totalCount || store.totalCount,
        icon: 'group',
        bgColor: 'bg-primary-container/10',
        textColor: 'text-primary-container'
      },
      {
        label: 'Học viên Nam',
        value: store.studentStats?.maleCount ?? 0,
        icon: 'male',
        bgColor: 'bg-sky-500/10',
        textColor: 'text-sky-700'
      },
      {
        label: 'Học viên Nữ',
        value: store.studentStats?.femaleCount ?? 0,
        icon: 'female',
        bgColor: 'bg-rose-500/10',
        textColor: 'text-rose-700'
      },
      {
        label: 'Giới tính Khác',
        value: store.studentStats?.otherCount ?? 0,
        icon: 'transgender',
        bgColor: 'bg-slate-500/10',
        textColor: 'text-slate-600'
      },
    ]
  }
})

const totalPages = computed(() => Math.ceil(store.totalCount / pagination.value.pageSize) || 1)

let debounceTimer = null
const debouncedFetch = () => {
  clearTimeout(debounceTimer)
  debounceTimer = setTimeout(fetchData, 300)
}

async function fetchData() {
  store.loading = true
  try {
    if (filters.value.classId) {
      const stdRes = await api.get(`/api/v1/enrollments/class/${filters.value.classId}/students`)
      const studentsInClass = stdRes.data || []
      
      let filteredStudents = [...studentsInClass]
      
      if (filters.value.search) {
        const query = filters.value.search.toLowerCase().trim()
        filteredStudents = filteredStudents.filter(s => 
          s.fullName.toLowerCase().includes(query) || 
          (s.email && s.email.toLowerCase().includes(query)) ||
          (s.phone && s.phone.includes(query))
        )
      }
      if (filters.value.gender) {
        filteredStudents = filteredStudents.filter(s => s.gender === filters.value.gender)
      }
      
      filteredAllStudents.value = filteredStudents
      store.totalCount = filteredStudents.length
      
      const start = (pagination.value.page - 1) * pagination.value.pageSize
      const end = start + pagination.value.pageSize
      store.students = filteredStudents.slice(start, end)
    } else if (authStore.currentUser?.role === 'GiaoVien') {
      const teacherId = authStore.currentUser.userId
      
      const classRes = await api.get('/api/v1/classes', {
        params: { teacherId: teacherId, page: 1, pageSize: 100 }
      })
      const teacherClasses = classRes.data?.items || []
      
      if (teacherClasses.length === 0) {
        store.students = []
        teacherAllStudents.value = []
        filteredAllStudents.value = []
        store.totalCount = 0
        return
      }
      
      const allStudentsMap = new Map()
      await Promise.all(
        teacherClasses.map(async (cls) => {
          try {
            const stdRes = await api.get(`/api/v1/enrollments/class/${cls.classId}/students`)
            const studentsInClass = stdRes.data || []
            studentsInClass.forEach(s => {
              allStudentsMap.set(s.studentId, s)
            })
          } catch (err) {
            console.error(`Error fetching students for class ${cls.classId}:`, err)
          }
        })
      )
      
      const allStudentsList = Array.from(allStudentsMap.values())
      teacherAllStudents.value = allStudentsList
      
      let filteredStudents = [...allStudentsList]
      
      if (filters.value.search) {
        const query = filters.value.search.toLowerCase().trim()
        filteredStudents = filteredStudents.filter(s => 
          s.fullName.toLowerCase().includes(query) || 
          (s.email && s.email.toLowerCase().includes(query)) ||
          (s.phone && s.phone.includes(query))
        )
      }
      if (filters.value.gender) {
        filteredStudents = filteredStudents.filter(s => s.gender === filters.value.gender)
      }
      
      filteredAllStudents.value = filteredStudents
      store.totalCount = filteredStudents.length
      
      const start = (pagination.value.page - 1) * pagination.value.pageSize
      const end = start + pagination.value.pageSize
      store.students = filteredStudents.slice(start, end)
    } else {
      filteredAllStudents.value = null
      store.fetchStudentStats()
      await store.fetchStudents({
        search: filters.value.search || undefined,
        gender: filters.value.gender || undefined,
        page: pagination.value.page,
        pageSize: pagination.value.pageSize,
      })
    }
  } catch (e) {
    console.error(e)
    showSnackbar('Lỗi tải danh sách học viên', 'error')
  } finally {
    store.loading = false
  }
}

function prevPage() {
  if (pagination.value.page > 1) {
    pagination.value.page--
    fetchData()
  }
}

function nextPage() {
  if (pagination.value.page < totalPages.value) {
    pagination.value.page++
    fetchData()
  }
}

function updateFilter({ key, value }) {
  filters.value[key] = value
  pagination.value.page = 1
}

async function openCreateDialog() {
  selectedStudent.value = null
  studentDialogModal.value = true
  
  try {
    await store.fetchHocVienUsers()
  } catch (e) {
    showSnackbar('Không thể tải danh sách tài khoản liên kết', 'error')
  }
}

function openEditDialog(student) {
  selectedStudent.value = student
  studentDialogModal.value = true
}

function onDialogSuccess() {
  studentDialogModal.value = false
  fetchData()
}

function onImportSuccess() {
  importDialogModal.value = false
  fetchData()
}

async function viewEnrollments(student) {
  selectedStudent.value = student
  enrollments.value = []
  loadingEnrollments.value = true
  enrollmentDialogModal.value = true
  try {
    const [enrolls, attSummary] = await Promise.all([
      store.fetchStudentEnrollments(student.studentId),
      store.fetchStudentAttendanceSummary(student.studentId)
    ])
    enrollments.value = enrolls.map(e => {
      const summary = attSummary.find(s => s.classId === e.classId)
      return {
        ...e,
        attendanceRate: summary ? summary.attendanceRate : null,
        present: summary ? summary.present : 0,
        totalSessions: summary ? summary.totalSessions : 0
      }
    })
  } catch (e) {
    showSnackbar('Lỗi tải lịch sử học tập', 'error')
  } finally {
    loadingEnrollments.value = false
  }
}

async function fetchClasses() {
  loadingClasses.value = true
  try {
    if (authStore.currentUser?.role === 'GiaoVien') {
      const teacherId = authStore.currentUser.userId
      const classRes = await api.get('/api/v1/classes', {
        params: { teacherId: teacherId, page: 1, pageSize: 100 }
      })
      classes.value = classRes.data?.items || []
    } else {
      const classRes = await api.get('/api/v1/classes', {
        params: { page: 1, pageSize: 100 }
      })
      classes.value = classRes.data?.items || []
    }
  } catch (err) {
    console.error('Error fetching classes:', err)
  } finally {
    loadingClasses.value = false
  }
}

watch(() => filters.value.classId, () => {
  pagination.value.page = 1
  fetchData()
})
watch(() => filters.value.gender, () => {
  pagination.value.page = 1
  fetchData()
})
watch(() => filters.value.search, () => {
  pagination.value.page = 1
  debouncedFetch()
})

onMounted(async () => {
  await fetchClasses()
  fetchData()
})
</script>

<style scoped>
@keyframes fadeIn {
  from { opacity: 0; transform: translateY(8px); }
  to { opacity: 1; transform: translateY(0); }
}
@keyframes scaleIn {
  from { opacity: 0; transform: scale(0.95); }
  to { opacity: 1; transform: scale(1); }
}
.animate-fade-in {
  animation: fadeIn 0.3s ease-out forwards;
}
.animate-scale-in {
  animation: scaleIn 0.2s cubic-bezier(0.34, 1.56, 0.64, 1) forwards;
}
</style>

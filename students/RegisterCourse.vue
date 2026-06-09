<template>
  <div class="space-y-6 animate-fade-in">
    <!-- Title / Header -->
    <div class="relative overflow-hidden bg-white/70 backdrop-blur-[20px] rounded-2xl p-container-padding flex flex-col md:flex-row items-center md:items-start justify-between gap-gutter border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)]">
      <div class="absolute -right-16 -top-16 w-48 h-48 rounded-full bg-primary-container/10 blur-3xl pointer-events-none"></div>
      <div class="absolute -left-16 -bottom-16 w-48 h-48 rounded-full bg-on-tertiary-container/10 blur-3xl pointer-events-none"></div>

      <div class="space-y-2">
        <h2 class="font-headline-lg text-headline-lg text-primary-container flex items-center gap-2">
          <span class="material-symbols-outlined text-[28px] text-on-tertiary-container">import_contacts</span>
          Đăng ký khóa học
        </h2>
        <p class="text-body-sm text-on-surface-variant">Lựa chọn các khóa học và đăng ký vào danh sách hàng chờ xếp lớp mới</p>
      </div>
    </div>

    <!-- Student Selector (Admin Only) -->
    <div v-if="authStore.isAdmin" class="bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl p-5 shadow-[0_12px_24px_rgba(0,0,0,0.05)] flex flex-col sm:flex-row items-center justify-between gap-4">
      <div class="flex items-center gap-3">
        <span class="material-symbols-outlined text-[28px] text-on-tertiary-container">person_search</span>
        <div>
          <h3 class="font-title-md text-body-lg font-bold text-primary-container">Chế độ Quản trị viên</h3>
          <p class="text-body-sm text-on-surface-variant">Đăng ký khóa học thay mặt cho học viên</p>
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
      <!-- Loading screen -->
      <div v-if="loading" class="bg-white/70 backdrop-blur-[20px] p-12 rounded-xl text-center flex flex-col items-center justify-center border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)]">
        <div class="w-12 h-12 border-4 border-primary-container/30 border-t-primary-container rounded-full animate-spin mb-4"></div>
        <p class="text-body-sm font-semibold text-primary-container">Đang tải dữ liệu khóa học...</p>
      </div>

      <!-- Courses Grid -->
      <div v-else class="space-y-4">
        <div v-if="courses.length === 0" class="bg-white/70 backdrop-blur-[20px] p-8 text-center rounded-xl border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] text-on-surface-variant">
          Hiện tại trung tâm chưa mở khóa học nào.
        </div>

        <div v-else class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-gutter">
          <div
            v-for="course in courses"
            :key="course.courseId"
            class="bg-white/70 backdrop-blur-[20px] border border-white/40 rounded-xl p-gutter shadow-[0_12px_24px_rgba(0,0,0,0.05)] flex flex-col justify-between"
          >
            <div class="space-y-3">
              <div class="flex justify-between items-start gap-2">
                <h4 class="font-title-md text-title-md text-primary-container truncate" :title="course.courseName">{{ course.courseName }}</h4>
                <span class="bg-on-tertiary-container/10 text-on-tertiary-container rounded-full px-2 py-0.5 text-label-caps whitespace-nowrap">
                  {{ course.totalSessions }} buổi
                </span>
              </div>
              <p class="text-body-sm text-on-surface-variant line-clamp-3 min-h-[50px]">{{ course.description || 'Chưa có mô tả chi tiết cho khóa học này.' }}</p>
              <div class="pt-3 border-t border-outline-variant/20 flex justify-between items-center">
                <span class="text-body-sm text-on-surface-variant">Học phí khóa:</span>
                <span class="text-on-tertiary-container font-semibold text-body-lg">{{ formatCurrency(course.fee) }}</span>
              </div>
            </div>

            <div class="mt-4 space-y-2">
              <!-- Queue progress bar -->
              <div v-if="getCourseQueueCount(course.courseId) > 0 && !isEnrolledActive(course.courseId, course.courseName)" class="space-y-1">
                <div class="flex justify-between items-center">
                  <span class="text-body-sm text-on-surface-variant">Hàng chờ</span>
                  <span class="text-body-sm font-semibold text-on-tertiary-container">{{ getCourseQueueCount(course.courseId) }}/5</span>
                </div>
                <div class="w-full h-1.5 bg-outline-variant/20 rounded-full overflow-hidden">
                  <div
                    class="h-full rounded-full transition-all duration-500"
                    :class="isInQueue(course.courseId) ? 'bg-amber-500' : 'bg-on-tertiary-container'"
                    :style="{ width: (getCourseQueueCount(course.courseId) / 5 * 100) + '%' }"
                  ></div>
                </div>
              </div>

              <!-- Active Enrollment -->
              <button
                v-if="isEnrolledActive(course.courseId, course.courseName)"
                disabled
                class="w-full py-2 rounded-lg bg-emerald-500/10 text-emerald-700 border border-emerald-500/20 font-semibold text-body-sm flex items-center justify-center gap-1.5 cursor-default"
              >
                <span class="material-symbols-outlined text-[16px]">check_circle</span>
                Đang học khóa này
              </button>

              <!-- Pending Payment -->
              <router-link
                v-else-if="isPendingPayment(course.courseId, course.courseName)"
                to="/student-portal"
                class="w-full py-2 rounded-lg bg-amber-500/10 text-amber-700 border border-amber-500/20 font-semibold text-body-sm flex items-center justify-center gap-1.5 hover:bg-amber-500/20 transition-colors cursor-pointer"
              >
                <span class="material-symbols-outlined text-[16px]">payment</span>
                Chờ thanh toán học phí
              </router-link>

              <!-- In Queue Waitlist -->
              <button
                v-else-if="isInQueue(course.courseId)"
                disabled
                class="w-full py-2 rounded-lg bg-blue-500/10 text-blue-700 border border-blue-500/20 font-semibold text-body-sm flex items-center justify-center gap-1.5 cursor-default"
              >
                <span class="material-symbols-outlined text-[16px]">hourglass_empty</span>
                Đang chờ ghép lớp
              </button>

              <!-- Regular Register -->
              <button
                v-else
                @click="openEnrollConfirmation(course)"
                :disabled="submittingEnroll"
                class="w-full py-2 rounded-lg bg-primary-container text-white font-semibold text-body-sm hover:bg-primary transition-colors active:scale-[0.98] cursor-pointer flex items-center justify-center gap-1.5"
              >
                <span class="material-symbols-outlined text-[16px]">how_to_reg</span>
                Đăng ký vào hàng chờ
              </button>
            </div>
          </div>
        </div>
      </div>

      <!-- Confirm Enrollment Modal -->
      <teleport to="body">
        <div v-if="enrollModal" class="fixed inset-0 glass-backdrop z-[9999] flex items-center justify-center p-4">
          <div class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] max-w-md w-full rounded-2xl overflow-hidden animate-scale-in flex flex-col">
            <div class="px-gutter py-4 border-b border-white/40 flex items-center justify-between">
              <h3 class="font-title-md text-title-md text-primary-container flex items-center gap-2">
                <span class="material-symbols-outlined text-on-tertiary-container">info</span>
                Xác nhận đăng ký khóa học
              </h3>
              <button @click="enrollModal = false" class="text-on-surface-variant hover:text-primary-container cursor-pointer">
                <span class="material-symbols-outlined">close</span>
              </button>
            </div>

            <div class="p-gutter space-y-3">
              <p class="text-body-sm text-on-surface">Bạn có chắc chắn muốn đăng ký vào danh sách hàng chờ của khóa học:</p>
              <div class="p-4 rounded-xl bg-on-tertiary-container/5 border border-on-tertiary-container/15">
                <div class="font-title-md text-title-md text-primary-container">{{ selectedCourseToEnroll?.courseName }}</div>
                <div class="text-body-sm text-on-surface-variant mt-1">Học phí: <span class="font-semibold text-on-tertiary-container">{{ formatCurrency(selectedCourseToEnroll?.fee) }}</span></div>
                <div class="text-body-sm text-on-surface-variant">Thời gian: {{ selectedCourseToEnroll?.totalSessions }} buổi</div>
              </div>
              <p class="text-body-sm text-on-surface-variant italic">
                * Sau khi đăng ký hàng chờ thành công, hệ thống sẽ tạo hóa đơn học phí. Bạn cần hoàn thành thanh toán trong mục "Học phí & Thanh toán" ở Cổng học tập để chính thức được đưa vào danh sách chờ ghép lớp.
              </p>
            </div>

            <div class="px-gutter py-4 border-t border-white/40 flex justify-end gap-3 bg-white/20">
              <button
                @click="enrollModal = false"
                class="px-4 py-2 rounded-lg border border-outline-variant/30 text-on-surface-variant font-semibold text-body-sm hover:bg-white/40 transition-colors cursor-pointer"
              >
                Hủy bộ
              </button>
              <button
                @click="submitEnrollment"
                :disabled="submittingEnroll"
                class="px-4 py-2 rounded-lg bg-primary-container text-white font-semibold text-body-sm hover:bg-primary transition-colors disabled:opacity-50 flex items-center gap-1 cursor-pointer"
              >
                <span v-if="submittingEnroll" class="animate-spin w-4.5 h-4.5 border-2 border-on-primary border-t-transparent rounded-full mr-1"></span>
                Đăng ký ghi danh
              </button>
            </div>
          </div>
        </div>
      </teleport>
    </template>

    <!-- No Student Selected State -->
    <div v-else-if="!loading" class="bg-white/70 backdrop-blur-[20px] border border-white/40 shadow-[0_12px_24px_rgba(0,0,0,0.05)] p-12 rounded-xl text-center flex flex-col items-center justify-center">
      <span class="material-symbols-outlined text-primary-container/30 text-[64px] mb-4">import_contacts</span>
      <h3 class="font-title-md text-headline-sm text-primary-container mb-2">Chưa chọn học viên</h3>
      <p class="text-body-md text-on-surface-variant max-w-md">Vui lòng chọn một học viên từ danh sách ở trên để đăng ký khóa học cho họ.</p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, inject, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useAuthStore, useCourseStore } from '../../stores'
import api from '../../services/api'

const route = useRoute()
const router = useRouter()

const authStore = useAuthStore()
const courseStore = useCourseStore()
const showSnackbar = inject('showSnackbar')

const students = ref([])
const selectedStudentId = ref(null)

const loading = ref(true)
const studentProfile = ref(null)
const enrolledClasses = ref([])
const courses = ref([])
const studentQueueCourseIds = ref([])
const enrollModal = ref(false)
const selectedCourseToEnroll = ref(null)
const submittingEnroll = ref(false)

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
      await loadRegistrationDataForStudent(newVal)
    } else {
      try {
        const res = await api.get(`/api/v1/students/${newVal}`)
        studentProfile.value = res.data
        await loadRegistrationDataForStudent(newVal)
      } catch (e) {
        console.error('Error fetching student details:', e)
      }
    }
  } else {
    studentProfile.value = null
    enrolledClasses.value = []
    studentQueueCourseIds.value = []
    // Remove studentId from query
    const newQuery = { ...route.query }
    delete newQuery.studentId
    router.replace({ path: route.path, query: newQuery })
  }
})

async function loadRegistrationData() {
  loading.value = true
  try {
    // 1. Fetch all courses for registrations
    try {
      await courseStore.fetchCourses({ page: 1, pageSize: 100 })
      courses.value = courseStore.courses || []
    } catch (e) {
      console.error('Error fetching courses:', e)
    }

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

    // 2. Fetch student profile details from StudentService using UserId
    try {
      const response = await api.get(`/api/v1/students/by-user/${userId}`)
      studentProfile.value = response.data
    } catch (err) {
      console.error('Error fetching student profile:', err)
      showSnackbar('Hồ sơ học tập của bạn chưa được tạo hoặc lỗi liên kết.', 'error')
      loading.value = false
      return
    }

    const studentId = studentProfile.value.studentId
    await loadRegistrationDataForStudent(studentId)

  } catch (err) {
    console.error('Error loading registration data:', err)
    showSnackbar('Có lỗi xảy ra khi tải danh sách khóa học.', 'error')
    loading.value = false
  }
}

async function loadRegistrationDataForStudent(studentId) {
  loading.value = true
  try {
    // 1. Fetch enrolled classes
    const classRes = await api.get(`/api/v1/students/${studentId}/enrollments`)
    enrolledClasses.value = classRes.data || []

    // 2. Fetch student waitlisted course IDs and overall queue status
    try {
      const queueRes = await api.get(`/api/v1/enrollments/course-queue/student/${studentId}`)
      studentQueueCourseIds.value = queueRes.data || []
      await courseStore.fetchCourseQueueStatus()
    } catch (e) {
      console.error('Error fetching course queue data:', e)
    }

  } catch (err) {
    console.error('Error loading registration data for student:', err)
    showSnackbar('Có lỗi xảy ra khi tải thông tin đăng ký.', 'error')
  } finally {
    loading.value = false
  }
}

function openEnrollConfirmation(course) {
  selectedCourseToEnroll.value = course
  enrollModal.value = true
}

async function submitEnrollment() {
  const studentId = studentProfile.value?.studentId
  if (!selectedCourseToEnroll.value || !studentId) return
  submittingEnroll.value = true
  try {
    await api.post('/api/v1/enrollments/course-queue', {
      studentId: studentId,
      courseId: selectedCourseToEnroll.value.courseId
    })
    showSnackbar(`Đã đăng ký ghi danh hàng chờ khóa "${selectedCourseToEnroll.value.courseName}" thành công!`, 'success')
    
    // Refresh student waitlisted course IDs, enrolled classes, and overall queue status
    const classRes = await api.get(`/api/v1/students/${studentId}/enrollments`)
    enrolledClasses.value = classRes.data || []
    
    const queueRes = await api.get(`/api/v1/enrollments/course-queue/student/${studentId}`)
    studentQueueCourseIds.value = queueRes.data || []
    
    await courseStore.fetchCourseQueueStatus()
    
    enrollModal.value = false
  } catch (err) {
    console.error(err)
    showSnackbar(err.response?.data?.message || 'Có lỗi xảy ra khi đăng ký ghi danh.', 'error')
  } finally {
    submittingEnroll.value = false
  }
}

// Check if student is actively attending/enrolled in a class of this course
function isEnrolledActive(courseId, courseName) {
  return enrolledClasses.value.some(cls => 
    cls.courseName === courseName && 
    cls.classId > 0 && 
    cls.status === 'DangHoc'
  )
}

// Check if student has a registration that is pending payment (either in queue or class)
function isPendingPayment(courseId, courseName) {
  return enrolledClasses.value.some(cls => 
    (cls.courseName === courseName || cls.classId === -courseId) && 
    (cls.status === 'PendingPayment' || cls.status === 'Pending')
  )
}

// Check if student is currently waitlisted/queued in a course
function isInQueue(courseId) {
  if (!studentQueueCourseIds.value) return false
  return studentQueueCourseIds.value.some(id => {
    if (id === courseId) return true
    if (id && id.courseId === courseId) return true
    if (id == courseId) return true
    return false
  })
}

// Get the count of students currently in queue for a course
function getCourseQueueCount(courseId) {
  const status = courseStore.courseQueueStatuses?.find(s => s.courseId === courseId)
  return status ? status.count : 0
}

function formatCurrency(amount) {
  if (amount === undefined || amount === null) return '0 đ'
  return amount.toLocaleString('vi-VN') + ' đ'
}

onMounted(loadRegistrationData)
</script>

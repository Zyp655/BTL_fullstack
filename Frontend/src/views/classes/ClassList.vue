<template>
  <div>
    <!-- Header -->
    <div class="d-flex align-center justify-space-between mb-6">
      <div>
        <h1 class="text-h4 font-weight-bold mb-1">
          <span class="gradient-text">Lớp học</span>
        </h1>
        <p class="text-body-2 text-medium-emphasis">Quản lý lớp học, giáo viên và lịch dạy</p>
      </div>
      <v-btn color="primary" prepend-icon="mdi-plus" @click="openCreateDialog" class="glow-primary">
        Mở lớp mới
      </v-btn>
    </div>

    <!-- Stats -->
    <v-row class="mb-6">
      <v-col cols="12" sm="6" md="3" v-for="stat in stats" :key="stat.label">
        <v-card class="glass-card pa-4">
          <div class="d-flex align-center">
            <v-avatar :color="stat.color" size="48" rounded="lg" class="mr-4" variant="tonal">
              <v-icon>{{ stat.icon }}</v-icon>
            </v-avatar>
            <div>
              <div class="text-h5 font-weight-bold">{{ stat.value }}</div>
              <div class="text-caption text-medium-emphasis">{{ stat.label }}</div>
            </div>
          </div>
        </v-card>
      </v-col>
    </v-row>

    <!-- Filters -->
    <v-card class="glass-card mb-6 pa-4">
      <v-row dense>
        <v-col cols="12" sm="4">
          <v-text-field
            v-model="filters.search"
            placeholder="Tìm kiếm lớp, giáo viên..."
            prepend-inner-icon="mdi-magnify"
            clearable
            hide-details
            @update:modelValue="debouncedFetch"
          />
        </v-col>
        <v-col cols="12" sm="4">
          <v-select
            v-model="filters.courseId"
            :items="courseOptions"
            placeholder="Khóa học"
            clearable
            hide-details
            @update:modelValue="fetchData"
          />
        </v-col>
        <v-col cols="12" sm="4">
          <v-select
            v-model="filters.status"
            :items="statusOptions"
            placeholder="Trạng thái"
            clearable
            hide-details
            @update:modelValue="fetchData"
          />
        </v-col>
      </v-row>
    </v-card>

    <!-- Class Cards Grid -->
    <v-row v-if="!classStore.loading && classStore.classes.length > 0">
      <v-col cols="12" md="6" lg="4" v-for="cls in classStore.classes" :key="cls.classId">
        <v-card class="glass-card pa-5 h-100">
          <div class="d-flex align-center justify-space-between mb-3">
            <v-chip size="small" variant="tonal" color="primary">{{ cls.coarseName || cls.courseName }}</v-chip>
            <span :class="'status-badge status-' + cls.status.toLowerCase()">{{ getStatusLabel(cls.status) }}</span>
          </div>

          <h3 class="text-h6 font-weight-bold mb-2">{{ cls.className }}</h3>

          <div class="d-flex flex-column ga-2 mb-4">
            <div class="d-flex align-center text-body-2 text-medium-emphasis">
              <v-icon size="16" class="mr-2">mdi-account-tie</v-icon>
              {{ cls.teacherName || 'Chưa phân công' }}
            </div>
            <div class="d-flex align-center text-body-2 text-medium-emphasis">
              <v-icon size="16" class="mr-2">mdi-door</v-icon>
              {{ cls.room || 'Chưa có phòng' }}
            </div>
            <div class="d-flex align-center text-body-2 text-medium-emphasis">
              <v-icon size="16" class="mr-2">mdi-account-group</v-icon>
              {{ cls.currentStudents }}/{{ cls.maxStudents }} học viên
            </div>
            <div v-if="cls.startDate" class="d-flex align-center text-body-2 text-medium-emphasis">
              <v-icon size="16" class="mr-2">mdi-calendar-range</v-icon>
              {{ formatDate(cls.startDate) }} — {{ formatDate(cls.endDate) }}
            </div>
          </div>

          <!-- Schedules -->
          <div v-if="cls.schedules && cls.schedules.length > 0" class="mb-4">
            <div class="text-caption text-medium-emphasis mb-1">Lịch học:</div>
            <div class="d-flex flex-wrap ga-1">
              <v-chip v-for="s in cls.schedules" :key="s.scheduleId" size="x-small" variant="tonal" color="secondary">
                {{ s.dayOfWeekName }} {{ s.startTime }}-{{ s.endTime }}
              </v-chip>
            </div>
          </div>

          <!-- Progress bar -->
          <v-progress-linear
            :model-value="(cls.currentStudents / cls.maxStudents) * 100"
            :color="cls.currentStudents >= cls.maxStudents ? 'error' : 'primary'"
            rounded
            height="6"
            class="mb-4"
          />

          <!-- Actions -->
          <div class="d-flex ga-2">
            <v-btn size="small" variant="tonal" color="secondary" @click="$router.push(`/classes/${cls.classId}/schedules`)">
              <v-icon start size="16">mdi-calendar-clock</v-icon>
              Lịch học
            </v-btn>
            <v-spacer />
            <v-btn icon size="small" variant="text" @click="openEditDialog(cls)" color="primary">
              <v-icon size="18">mdi-pencil</v-icon>
            </v-btn>
            <v-menu>
              <template v-slot:activator="{ props }">
                <v-btn icon size="small" variant="text" v-bind="props">
                  <v-icon size="18">mdi-dots-vertical</v-icon>
                </v-btn>
              </template>
              <v-list density="compact" class="glass-card" style="background: #1A1A2E !important;">
                <v-list-item v-for="s in statusOptions" :key="s.value" @click="changeStatus(cls.classId, s.value)">
                  <v-list-item-title>{{ s.title }}</v-list-item-title>
                </v-list-item>
                <v-divider />
                <v-list-item @click="confirmDelete(cls)" class="text-error">
                  <v-list-item-title>Xóa lớp</v-list-item-title>
                </v-list-item>
              </v-list>
            </v-menu>
          </div>
        </v-card>
      </v-col>
    </v-row>

    <!-- Empty state -->
    <v-card v-else-if="!classStore.loading" class="glass-card pa-12 text-center">
      <v-icon size="64" color="primary" class="mb-4" style="opacity: 0.3;">mdi-google-classroom</v-icon>
      <p class="text-body-1 text-medium-emphasis">Chưa có lớp học nào</p>
      <v-btn color="primary" variant="tonal" class="mt-2" @click="openCreateDialog">
        Mở lớp đầu tiên
      </v-btn>
    </v-card>

    <!-- Loading -->
    <v-row v-else>
      <v-col cols="12" md="6" lg="4" v-for="i in 3" :key="i">
        <v-card class="glass-card pa-5">
          <v-skeleton-loader type="article, actions" />
        </v-card>
      </v-col>
    </v-row>

    <!-- Create/Edit Dialog -->
    <v-dialog v-model="dialog" max-width="600" persistent>
      <v-card class="glass-card" style="background: #1A1A2E !important;">
        <v-card-title class="text-h6 pa-6 pb-2">
          <v-icon class="mr-2" color="primary">{{ isEdit ? 'mdi-pencil' : 'mdi-plus-circle' }}</v-icon>
          {{ isEdit ? 'Sửa lớp học' : 'Mở lớp mới' }}
        </v-card-title>

        <v-card-text class="pa-6">
          <v-form ref="form" v-model="formValid">
            <v-select
              v-model="formData.courseId"
              :items="courseOptions"
              label="Khóa học"
              :rules="[v => !!v || 'Bắt buộc']"
              :disabled="isEdit"
              class="mb-3"
            />
            <v-text-field v-model="formData.className" label="Tên lớp" :rules="[v => !!v || 'Bắt buộc']" class="mb-3" />
            <v-row>
              <v-col cols="6">
                <v-text-field v-model="formData.teacherName" label="Tên giáo viên" />
              </v-col>
              <v-col cols="6">
                <v-text-field v-model="formData.room" label="Phòng học" />
              </v-col>
            </v-row>
            <v-text-field v-model.number="formData.maxStudents" label="Sĩ số tối đa" type="number" class="mb-3" />
            <v-row>
              <v-col cols="6">
                <v-text-field v-model="formData.startDate" label="Ngày bắt đầu" type="date" />
              </v-col>
              <v-col cols="6">
                <v-text-field v-model="formData.endDate" label="Ngày kết thúc" type="date" />
              </v-col>
            </v-row>
          </v-form>
        </v-card-text>

        <v-card-actions class="pa-6 pt-0">
          <v-spacer />
          <v-btn variant="text" @click="dialog = false">Hủy</v-btn>
          <v-btn color="primary" :loading="saving" @click="saveForm" :disabled="!formValid">
            {{ isEdit ? 'Cập nhật' : 'Mở lớp' }}
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Delete Confirmation -->
    <v-dialog v-model="deleteDialog" max-width="400">
      <v-card class="glass-card" style="background: #1A1A2E !important;">
        <v-card-title class="text-h6 pa-6 pb-2">
          <v-icon class="mr-2" color="error">mdi-alert-circle</v-icon>
          Xác nhận xóa
        </v-card-title>
        <v-card-text class="pa-6 pt-2">
          Bạn có chắc muốn xóa lớp <strong>{{ deleteTarget?.className }}</strong>?
        </v-card-text>
        <v-card-actions class="pa-6 pt-0">
          <v-spacer />
          <v-btn variant="text" @click="deleteDialog = false">Hủy</v-btn>
          <v-btn color="error" :loading="deleting" @click="doDelete">Xóa</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, inject } from 'vue'
import { useClassStore, useCourseStore } from '../../stores'

const classStore = useClassStore()
const courseStore = useCourseStore()
const showSnackbar = inject('showSnackbar')

const dialog = ref(false)
const deleteDialog = ref(false)
const isEdit = ref(false)
const formValid = ref(false)
const saving = ref(false)
const deleting = ref(false)
const deleteTarget = ref(null)

const formData = ref({
  courseId: null,
  className: '',
  teacherName: '',
  room: '',
  maxStudents: 30,
  startDate: null,
  endDate: null,
})

const filters = ref({
  search: '',
  courseId: null,
  status: null,
})

const statusOptions = [
  { title: 'Đang mở', value: 'Opened' },
  { title: 'Đang học', value: 'InProgress' },
  { title: 'Hoàn thành', value: 'Completed' },
  { title: 'Đã hủy', value: 'Cancelled' },
]

const courseOptions = computed(() =>
  courseStore.courses.map(c => ({ title: c.courseName, value: c.courseId }))
)

const stats = computed(() => [
  { label: 'Tổng lớp', value: classStore.totalCount, icon: 'mdi-google-classroom', color: 'primary' },
  { label: 'Đang mở', value: classStore.classes.filter(c => c.status === 'Opened').length, icon: 'mdi-door-open', color: 'info' },
  { label: 'Đang học', value: classStore.classes.filter(c => c.status === 'InProgress').length, icon: 'mdi-play-circle', color: 'success' },
  { label: 'Hoàn thành', value: classStore.classes.filter(c => c.status === 'Completed').length, icon: 'mdi-check-circle', color: 'secondary' },
])

let debounceTimer = null
const debouncedFetch = () => {
  clearTimeout(debounceTimer)
  debounceTimer = setTimeout(fetchData, 300)
}

async function fetchData() {
  try {
    await classStore.fetchClasses({
      search: filters.value.search || undefined,
      courseId: filters.value.courseId || undefined,
      status: filters.value.status || undefined,
      pageSize: 50,
    })
  } catch (e) {
    showSnackbar('Lỗi tải dữ liệu', 'error')
  }
}

function openCreateDialog() {
  isEdit.value = false
  formData.value = { courseId: null, className: '', teacherName: '', room: '', maxStudents: 30, startDate: null, endDate: null }
  dialog.value = true
}

function openEditDialog(cls) {
  isEdit.value = true
  formData.value = {
    classId: cls.classId,
    courseId: cls.courseId,
    className: cls.className,
    teacherName: cls.teacherName || '',
    room: cls.room || '',
    maxStudents: cls.maxStudents,
    startDate: cls.startDate?.split('T')[0] || null,
    endDate: cls.endDate?.split('T')[0] || null,
  }
  dialog.value = true
}

async function saveForm() {
  saving.value = true
  try {
    if (isEdit.value) {
      await classStore.updateClass(formData.value.classId, formData.value)
      showSnackbar('Cập nhật lớp thành công')
    } else {
      await classStore.createClass(formData.value)
      showSnackbar('Mở lớp mới thành công')
    }
    dialog.value = false
    fetchData()
  } catch (e) {
    showSnackbar(e.response?.data?.message || 'Có lỗi xảy ra', 'error')
  } finally {
    saving.value = false
  }
}

async function changeStatus(classId, status) {
  try {
    await classStore.updateClassStatus(classId, status)
    showSnackbar('Cập nhật trạng thái thành công')
    fetchData()
  } catch (e) {
    showSnackbar('Lỗi cập nhật', 'error')
  }
}

function confirmDelete(cls) {
  deleteTarget.value = cls
  deleteDialog.value = true
}

async function doDelete() {
  deleting.value = true
  try {
    await classStore.deleteClass(deleteTarget.value.classId)
    showSnackbar('Đã xóa lớp')
    deleteDialog.value = false
    fetchData()
  } catch (e) {
    showSnackbar(e.response?.data?.message || 'Lỗi khi xóa', 'error')
  } finally {
    deleting.value = false
  }
}

function getStatusLabel(status) {
  const map = { Opened: 'Đang mở', InProgress: 'Đang học', Completed: 'Hoàn thành', Cancelled: 'Đã hủy' }
  return map[status] || status
}

function formatDate(date) {
  if (!date) return ''
  return new Date(date).toLocaleDateString('vi-VN')
}

onMounted(async () => {
  await courseStore.fetchCourses({ pageSize: 100 })
  fetchData()
})
</script>

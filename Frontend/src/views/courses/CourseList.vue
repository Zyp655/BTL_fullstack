<template>
  <div>
    <!-- Header -->
    <div class="d-flex align-center justify-space-between mb-6">
      <div>
        <h1 class="text-h4 font-weight-bold mb-1">
          <span class="gradient-text">Khóa học</span>
        </h1>
        <p class="text-body-2 text-medium-emphasis">Quản lý tất cả khóa học tại trung tâm</p>
      </div>
      <v-btn color="primary" prepend-icon="mdi-plus" @click="openCreateDialog" class="glow-primary">
        Thêm khóa học
      </v-btn>
    </div>

    <!-- Stats Cards -->
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
            placeholder="Tìm kiếm khóa học..."
            prepend-inner-icon="mdi-magnify"
            clearable
            hide-details
            @update:modelValue="debouncedFetch"
          />
        </v-col>
        <v-col cols="12" sm="3">
          <v-select
            v-model="filters.category"
            :items="categoryOptions"
            placeholder="Danh mục"
            clearable
            hide-details
            @update:modelValue="fetchData"
          />
        </v-col>
        <v-col cols="12" sm="3">
          <v-select
            v-model="filters.level"
            :items="levelOptions"
            placeholder="Trình độ"
            clearable
            hide-details
            @update:modelValue="fetchData"
          />
        </v-col>
        <v-col cols="12" sm="2">
          <v-select
            v-model="filters.isActive"
            :items="[{ title: 'Đang mở', value: true }, { title: 'Đã đóng', value: false }]"
            placeholder="Trạng thái"
            clearable
            hide-details
            @update:modelValue="fetchData"
          />
        </v-col>
      </v-row>
    </v-card>

    <!-- Data Table -->
    <v-card class="glass-card">
      <v-data-table
        :headers="headers"
        :items="store.courses"
        :loading="store.loading"
        :items-per-page="pageSize"
        class="elevation-0"
        style="background: transparent;"
      >
        <template v-slot:item.courseName="{ item }">
          <div>
            <div class="font-weight-medium">{{ item.courseName }}</div>
            <div class="text-caption text-medium-emphasis text-truncate" style="max-width: 300px;">
              {{ item.description }}
            </div>
          </div>
        </template>

        <template v-slot:item.category="{ item }">
          <v-chip size="small" :color="getCategoryColor(item.category)" variant="tonal">
            {{ getCategoryLabel(item.category) }}
          </v-chip>
        </template>

        <template v-slot:item.level="{ item }">
          <v-chip size="small" :color="getLevelColor(item.level)" variant="outlined">
            {{ item.level }}
          </v-chip>
        </template>

        <template v-slot:item.fee="{ item }">
          <span class="font-weight-bold text-secondary">{{ formatCurrency(item.fee) }}</span>
        </template>

        <template v-slot:item.isActive="{ item }">
          <v-chip size="small" :color="item.isActive ? 'success' : 'error'" variant="tonal">
            {{ item.isActive ? 'Hoạt động' : 'Đã đóng' }}
          </v-chip>
        </template>

        <template v-slot:item.actions="{ item }">
          <v-btn icon variant="text" size="small" @click="openEditDialog(item)" color="primary">
            <v-icon size="18">mdi-pencil</v-icon>
            <v-tooltip activator="parent" location="top">Sửa</v-tooltip>
          </v-btn>
          <v-btn icon variant="text" size="small" @click="confirmDelete(item)" color="error">
            <v-icon size="18">mdi-delete</v-icon>
            <v-tooltip activator="parent" location="top">Xóa</v-tooltip>
          </v-btn>
        </template>

        <template v-slot:no-data>
          <div class="text-center pa-8">
            <v-icon size="64" color="primary" class="mb-4" style="opacity: 0.3;">mdi-book-open-variant</v-icon>
            <p class="text-body-1 text-medium-emphasis">Chưa có khóa học nào</p>
            <v-btn color="primary" variant="tonal" class="mt-2" @click="openCreateDialog">
              Thêm khóa học đầu tiên
            </v-btn>
          </div>
        </template>
      </v-data-table>
    </v-card>

    <!-- Create/Edit Dialog -->
    <v-dialog v-model="dialog" max-width="600" persistent>
      <v-card class="glass-card" style="background: #1A1A2E !important;">
        <v-card-title class="text-h6 pa-6 pb-2">
          <v-icon class="mr-2" color="primary">{{ isEdit ? 'mdi-pencil' : 'mdi-plus-circle' }}</v-icon>
          {{ isEdit ? 'Sửa khóa học' : 'Thêm khóa học mới' }}
        </v-card-title>

        <v-card-text class="pa-6">
          <v-form ref="form" v-model="formValid">
            <v-text-field
              v-model="formData.courseName"
              label="Tên khóa học"
              :rules="[v => !!v || 'Bắt buộc']"
              class="mb-3"
            />
            <v-textarea
              v-model="formData.description"
              label="Mô tả"
              rows="3"
              variant="outlined"
              rounded="lg"
              class="mb-3"
            />
            <v-row>
              <v-col cols="6">
                <v-select
                  v-model="formData.category"
                  :items="categoryOptions"
                  label="Danh mục"
                  :rules="[v => !!v || 'Bắt buộc']"
                />
              </v-col>
              <v-col cols="6">
                <v-select
                  v-model="formData.level"
                  :items="levelOptions"
                  label="Trình độ"
                  :rules="[v => !!v || 'Bắt buộc']"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="6">
                <v-text-field
                  v-model.number="formData.fee"
                  label="Học phí (VNĐ)"
                  type="number"
                  :rules="[v => v > 0 || 'Phải lớn hơn 0']"
                />
              </v-col>
              <v-col cols="6">
                <v-text-field
                  v-model.number="formData.totalSessions"
                  label="Tổng số buổi"
                  type="number"
                  :rules="[v => v > 0 || 'Phải lớn hơn 0']"
                />
              </v-col>
            </v-row>
            <v-switch
              v-if="isEdit"
              v-model="formData.isActive"
              label="Đang hoạt động"
              color="primary"
              hide-details
            />
          </v-form>
        </v-card-text>

        <v-card-actions class="pa-6 pt-0">
          <v-spacer />
          <v-btn variant="text" @click="dialog = false">Hủy</v-btn>
          <v-btn color="primary" :loading="saving" @click="saveForm" :disabled="!formValid">
            {{ isEdit ? 'Cập nhật' : 'Tạo mới' }}
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
          Bạn có chắc muốn xóa khóa học <strong>{{ deleteTarget?.courseName }}</strong>?
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
import { useCourseStore } from '../../stores'

const store = useCourseStore()
const showSnackbar = inject('showSnackbar')

const dialog = ref(false)
const deleteDialog = ref(false)
const isEdit = ref(false)
const formValid = ref(false)
const saving = ref(false)
const deleting = ref(false)
const deleteTarget = ref(null)
const pageSize = ref(10)

const formData = ref({
  courseName: '',
  description: '',
  category: 'NgoaiNgu',
  level: 'Beginner',
  fee: 0,
  totalSessions: 0,
  isActive: true,
})

const filters = ref({
  search: '',
  category: null,
  level: null,
  isActive: null,
})

const headers = [
  { title: 'Tên khóa học', key: 'courseName', width: '30%' },
  { title: 'Danh mục', key: 'category', width: '12%' },
  { title: 'Trình độ', key: 'level', width: '12%' },
  { title: 'Học phí', key: 'fee', width: '14%' },
  { title: 'Số buổi', key: 'totalSessions', width: '8%', align: 'center' },
  { title: 'Lớp', key: 'classCount', width: '8%', align: 'center' },
  { title: 'Trạng thái', key: 'isActive', width: '10%' },
  { title: '', key: 'actions', width: '8%', sortable: false },
]

const categoryOptions = [
  { title: 'Ngoại ngữ', value: 'NgoaiNgu' },
  { title: 'Tin học', value: 'TinHoc' },
  { title: 'Kỹ năng', value: 'KyNang' },
]

const levelOptions = [
  { title: 'Cơ bản', value: 'Beginner' },
  { title: 'Trung cấp', value: 'Intermediate' },
  { title: 'Nâng cao', value: 'Advanced' },
]

const stats = computed(() => [
  {
    label: 'Tổng khóa học',
    value: store.totalCount,
    icon: 'mdi-book-open-variant',
    color: 'primary',
  },
  {
    label: 'Đang hoạt động',
    value: store.courses.filter(c => c.isActive).length,
    icon: 'mdi-check-circle',
    color: 'success',
  },
  {
    label: 'Ngoại ngữ',
    value: store.courses.filter(c => c.category === 'NgoaiNgu').length,
    icon: 'mdi-translate',
    color: 'info',
  },
  {
    label: 'Tin học',
    value: store.courses.filter(c => c.category === 'TinHoc').length,
    icon: 'mdi-laptop',
    color: 'warning',
  },
])

let debounceTimer = null
const debouncedFetch = () => {
  clearTimeout(debounceTimer)
  debounceTimer = setTimeout(fetchData, 300)
}

async function fetchData() {
  try {
    await store.fetchCourses({
      search: filters.value.search || undefined,
      category: filters.value.category || undefined,
      level: filters.value.level || undefined,
      isActive: filters.value.isActive ?? undefined,
      page: 1,
      pageSize: 50,
    })
  } catch (e) {
    showSnackbar('Lỗi tải dữ liệu', 'error')
  }
}

function openCreateDialog() {
  isEdit.value = false
  formData.value = {
    courseName: '',
    description: '',
    category: 'NgoaiNgu',
    level: 'Beginner',
    fee: 0,
    totalSessions: 0,
    isActive: true,
  }
  dialog.value = true
}

function openEditDialog(item) {
  isEdit.value = true
  formData.value = { ...item }
  dialog.value = true
}

async function saveForm() {
  saving.value = true
  try {
    if (isEdit.value) {
      await store.updateCourse(formData.value.courseId, formData.value)
      showSnackbar('Cập nhật khóa học thành công')
    } else {
      await store.createCourse(formData.value)
      showSnackbar('Thêm khóa học thành công')
    }
    dialog.value = false
    fetchData()
  } catch (e) {
    showSnackbar('Có lỗi xảy ra', 'error')
  } finally {
    saving.value = false
  }
}

function confirmDelete(item) {
  deleteTarget.value = item
  deleteDialog.value = true
}

async function doDelete() {
  deleting.value = true
  try {
    await store.deleteCourse(deleteTarget.value.courseId)
    showSnackbar('Đã xóa khóa học')
    deleteDialog.value = false
    fetchData()
  } catch (e) {
    showSnackbar('Lỗi khi xóa', 'error')
  } finally {
    deleting.value = false
  }
}

function formatCurrency(val) {
  return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(val)
}

function getCategoryColor(cat) {
  const map = { NgoaiNgu: 'info', TinHoc: 'warning', KyNang: 'secondary' }
  return map[cat] || 'primary'
}

function getCategoryLabel(cat) {
  const map = { NgoaiNgu: 'Ngoại ngữ', TinHoc: 'Tin học', KyNang: 'Kỹ năng' }
  return map[cat] || cat
}

function getLevelColor(level) {
  const map = { Beginner: 'success', Intermediate: 'warning', Advanced: 'error' }
  return map[level] || 'primary'
}

onMounted(fetchData)
</script>

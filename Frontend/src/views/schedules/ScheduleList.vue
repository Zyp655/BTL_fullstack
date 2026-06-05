<template>
  <div>
    <!-- Back + Header -->
    <div class="d-flex align-center mb-6">
      <v-btn icon variant="text" @click="$router.back()" class="mr-3">
        <v-icon>mdi-arrow-left</v-icon>
      </v-btn>
      <div class="flex-grow-1">
        <h1 class="text-h4 font-weight-bold mb-1">
          <span class="gradient-text">Lịch học</span>
        </h1>
        <p class="text-body-2 text-medium-emphasis" v-if="classInfo">
          Lớp {{ classInfo.className }} — {{ classInfo.courseName }}
        </p>
      </div>
      <v-btn color="primary" prepend-icon="mdi-plus" @click="openCreateDialog" class="glow-primary">
        Thêm lịch
      </v-btn>
    </div>

    <!-- Class Info Card -->
    <v-card v-if="classInfo" class="glass-card pa-5 mb-6">
      <v-row>
        <v-col cols="12" sm="3">
          <div class="text-caption text-medium-emphasis">Lớp</div>
          <div class="text-h6 font-weight-bold">{{ classInfo.className }}</div>
        </v-col>
        <v-col cols="12" sm="3">
          <div class="text-caption text-medium-emphasis">Giáo viên</div>
          <div class="text-body-1">{{ classInfo.teacherName || 'Chưa phân công' }}</div>
        </v-col>
        <v-col cols="12" sm="3">
          <div class="text-caption text-medium-emphasis">Phòng</div>
          <div class="text-body-1">{{ classInfo.room || '—' }}</div>
        </v-col>
        <v-col cols="12" sm="3">
          <div class="text-caption text-medium-emphasis">Trạng thái</div>
          <span :class="'status-badge status-' + classInfo.status.toLowerCase()">
            {{ getStatusLabel(classInfo.status) }}
          </span>
        </v-col>
      </v-row>
    </v-card>

    <!-- Schedule Table -->
    <v-card class="glass-card">
      <v-data-table
        :headers="headers"
        :items="scheduleStore.schedules"
        :loading="scheduleStore.loading"
        class="elevation-0"
        style="background: transparent;"
      >
        <template v-slot:item.dayOfWeek="{ item }">
          <v-chip :color="getDayColor(item.dayOfWeek)" variant="tonal" size="small">
            {{ item.dayOfWeekName }}
          </v-chip>
        </template>

        <template v-slot:item.session="{ item }">
          <v-chip
            size="small"
            variant="outlined"
            :color="item.session === 'Sang' ? 'warning' : item.session === 'Chieu' ? 'info' : 'secondary'"
          >
            <v-icon start size="14">{{ item.session === 'Sang' ? 'mdi-weather-sunny' : item.session === 'Chieu' ? 'mdi-weather-partly-cloudy' : 'mdi-weather-night' }}</v-icon>
            {{ getSessionLabel(item.session) }}
          </v-chip>
        </template>

        <template v-slot:item.time="{ item }">
          <span class="font-weight-medium">{{ item.startTime }} — {{ item.endTime }}</span>
        </template>

        <template v-slot:item.actions="{ item }">
          <v-btn icon variant="text" size="small" @click="openEditDialog(item)" color="primary">
            <v-icon size="18">mdi-pencil</v-icon>
          </v-btn>
          <v-btn icon variant="text" size="small" @click="confirmDelete(item)" color="error">
            <v-icon size="18">mdi-delete</v-icon>
          </v-btn>
        </template>

        <template v-slot:no-data>
          <div class="text-center pa-8">
            <v-icon size="64" color="primary" class="mb-4" style="opacity: 0.3;">mdi-calendar-blank</v-icon>
            <p class="text-body-1 text-medium-emphasis">Chưa có lịch học</p>
            <v-btn color="primary" variant="tonal" class="mt-2" @click="openCreateDialog">
              Thêm lịch đầu tiên
            </v-btn>
          </div>
        </template>
      </v-data-table>
    </v-card>

    <!-- Weekly View -->
    <v-card class="glass-card mt-6 pa-5" v-if="scheduleStore.schedules.length > 0">
      <h3 class="text-h6 font-weight-bold mb-4">
        <v-icon class="mr-2" color="primary">mdi-calendar-week</v-icon>
        Lịch tuần
      </h3>
      <v-row>
        <v-col v-for="day in weekDays" :key="day.value" cols="12" sm="6" md="true">
          <div class="text-center mb-2">
            <v-chip :color="hasSchedule(day.value) ? 'primary' : 'default'" variant="tonal" size="small">
              {{ day.label }}
            </v-chip>
          </div>
          <div v-for="s in getSchedulesByDay(day.value)" :key="s.scheduleId" class="pa-2 rounded-lg mb-1" style="background: rgba(108,99,255,0.1); border: 1px solid rgba(108,99,255,0.15);">
            <div class="text-caption font-weight-bold">{{ s.startTime }} - {{ s.endTime }}</div>
            <div class="text-caption text-medium-emphasis">{{ getSessionLabel(s.session) }}</div>
          </div>
          <div v-if="!hasSchedule(day.value)" class="text-center pa-3 rounded-lg" style="background: rgba(255,255,255,0.03); border: 1px dashed rgba(255,255,255,0.1);">
            <span class="text-caption text-medium-emphasis">Nghỉ</span>
          </div>
        </v-col>
      </v-row>
    </v-card>

    <!-- Create/Edit Dialog -->
    <v-dialog v-model="dialog" max-width="500" persistent>
      <v-card class="glass-card" style="background: #1A1A2E !important;">
        <v-card-title class="text-h6 pa-6 pb-2">
          <v-icon class="mr-2" color="primary">{{ isEdit ? 'mdi-pencil' : 'mdi-plus-circle' }}</v-icon>
          {{ isEdit ? 'Sửa lịch học' : 'Thêm lịch học' }}
        </v-card-title>

        <v-card-text class="pa-6">
          <v-form ref="form" v-model="formValid">
            <v-select
              v-model="formData.dayOfWeek"
              :items="weekDays.map(d => ({ title: d.label, value: d.value }))"
              label="Thứ"
              :rules="[v => v !== null && v !== undefined || 'Bắt buộc']"
              class="mb-3"
            />
            <v-select
              v-model="formData.session"
              :items="sessionOptions"
              label="Buổi"
              :rules="[v => !!v || 'Bắt buộc']"
              class="mb-3"
            />
            <v-row>
              <v-col cols="6">
                <v-text-field v-model="formData.startTime" label="Bắt đầu" type="time" :rules="[v => !!v || 'Bắt buộc']" />
              </v-col>
              <v-col cols="6">
                <v-text-field v-model="formData.endTime" label="Kết thúc" type="time" :rules="[v => !!v || 'Bắt buộc']" />
              </v-col>
            </v-row>
          </v-form>
        </v-card-text>

        <v-card-actions class="pa-6 pt-0">
          <v-spacer />
          <v-btn variant="text" @click="dialog = false">Hủy</v-btn>
          <v-btn color="primary" :loading="saving" @click="saveForm" :disabled="!formValid">
            {{ isEdit ? 'Cập nhật' : 'Thêm' }}
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
          Bạn có chắc muốn xóa lịch này?
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
import { ref, onMounted, inject } from 'vue'
import { useRoute } from 'vue-router'
import { useScheduleStore, useClassStore } from '../../stores'

const route = useRoute()
const scheduleStore = useScheduleStore()
const classStore = useClassStore()
const showSnackbar = inject('showSnackbar')

const classId = parseInt(route.params.id)
const classInfo = ref(null)

const dialog = ref(false)
const deleteDialog = ref(false)
const isEdit = ref(false)
const formValid = ref(false)
const saving = ref(false)
const deleting = ref(false)
const deleteTarget = ref(null)

const formData = ref({
  dayOfWeek: 2,
  session: 'Sang',
  startTime: '08:00',
  endTime: '10:00',
})

const headers = [
  { title: 'Thứ', key: 'dayOfWeek', width: '20%' },
  { title: 'Buổi', key: 'session', width: '20%' },
  { title: 'Thời gian', key: 'time', width: '30%' },
  { title: '', key: 'actions', width: '15%', sortable: false },
]

const weekDays = [
  { label: 'Thứ 2', value: 2 },
  { label: 'Thứ 3', value: 3 },
  { label: 'Thứ 4', value: 4 },
  { label: 'Thứ 5', value: 5 },
  { label: 'Thứ 6', value: 6 },
  { label: 'Thứ 7', value: 7 },
  { label: 'Chủ nhật', value: 0 },
]

const sessionOptions = [
  { title: '🌅 Sáng', value: 'Sang' },
  { title: '☀️ Chiều', value: 'Chieu' },
  { title: '🌙 Tối', value: 'Toi' },
]

function hasSchedule(day) {
  return scheduleStore.schedules.some(s => s.dayOfWeek === day)
}

function getSchedulesByDay(day) {
  return scheduleStore.schedules.filter(s => s.dayOfWeek === day)
}

function getDayColor(day) {
  const colors = { 0: 'error', 2: 'primary', 3: 'info', 4: 'success', 5: 'warning', 6: 'secondary', 7: 'accent' }
  return colors[day] || 'primary'
}

function getSessionLabel(session) {
  const map = { Sang: 'Sáng', Chieu: 'Chiều', Toi: 'Tối' }
  return map[session] || session
}

function getStatusLabel(status) {
  const map = { Opened: 'Đang mở', InProgress: 'Đang học', Completed: 'Hoàn thành', Cancelled: 'Đã hủy' }
  return map[status] || status
}

function openCreateDialog() {
  isEdit.value = false
  formData.value = { dayOfWeek: 2, session: 'Sang', startTime: '08:00', endTime: '10:00' }
  dialog.value = true
}

function openEditDialog(item) {
  isEdit.value = true
  formData.value = {
    scheduleId: item.scheduleId,
    dayOfWeek: item.dayOfWeek,
    session: item.session,
    startTime: item.startTime,
    endTime: item.endTime,
  }
  dialog.value = true
}

async function saveForm() {
  saving.value = true
  try {
    if (isEdit.value) {
      await scheduleStore.updateSchedule(classId, formData.value.scheduleId, formData.value)
      showSnackbar('Cập nhật lịch thành công')
    } else {
      await scheduleStore.createSchedule(classId, formData.value)
      showSnackbar('Thêm lịch thành công')
    }
    dialog.value = false
    scheduleStore.fetchSchedules(classId)
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
    await scheduleStore.deleteSchedule(classId, deleteTarget.value.scheduleId)
    showSnackbar('Đã xóa lịch')
    deleteDialog.value = false
    scheduleStore.fetchSchedules(classId)
  } catch (e) {
    showSnackbar('Lỗi khi xóa', 'error')
  } finally {
    deleting.value = false
  }
}

onMounted(async () => {
  try {
    classInfo.value = await classStore.getClass(classId)
    await scheduleStore.fetchSchedules(classId)
  } catch (e) {
    showSnackbar('Lỗi tải dữ liệu', 'error')
  }
})
</script>

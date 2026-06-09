<template>
  <v-container class="page-container">
    <div class="d-flex align-center justify-space-between mb-6">
      <div>
        <h1 class="text-h4 font-weight-bold">Khoa hoc</h1>
        <p class="text-medium-emphasis">
          Du lieu duoc lay tu Course Service qua API Gateway.
        </p>
      </div>
      <v-btn
        icon="mdi-refresh"
        variant="text"
        :loading="loading"
        aria-label="Tai lai"
        @click="loadCourses"
      />
    </div>

    <v-alert
      v-if="error"
      type="warning"
      variant="tonal"
      class="mb-5"
    >
      {{ error }}
    </v-alert>

    <v-row>
      <v-col
        v-for="course in courses"
        :key="course.courseId"
        cols="12"
        sm="6"
        lg="4"
      >
        <v-card height="100%" rounded="lg">
          <v-card-title>{{ course.courseName }}</v-card-title>
          <v-card-subtitle>
            {{ course.category }} - {{ course.level }}
          </v-card-subtitle>
          <v-card-text>
            <p class="mb-4">{{ course.description }}</p>
            <strong>{{ formatCurrency(course.fee) }}</strong>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-progress-linear v-if="loading" indeterminate color="primary" />
  </v-container>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import api from '../services/api'

const courses = ref([])
const loading = ref(false)
const error = ref('')

async function loadCourses() {
  loading.value = true
  error.value = ''

  try {
    const response = await api.get('/courses', {
      params: { page: 1, pageSize: 12 },
    })
    courses.value = response.data.items ?? response.data.data ?? response.data
  } catch {
    error.value =
      'Chua ket noi duoc API Gateway. Hay khoi dong backend tai cong 5000.'
  } finally {
    loading.value = false
  }
}

function formatCurrency(value) {
  return new Intl.NumberFormat('vi-VN', {
    style: 'currency',
    currency: 'VND',
  }).format(value ?? 0)
}

onMounted(loadCourses)
</script>

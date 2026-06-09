<template>
  <v-container class="page-container">
    <v-row align="center">
      <v-col cols="12" md="7">
        <div class="text-overline text-primary">Training Center Platform</div>
        <h1 class="text-h2 font-weight-bold mb-4">
          Quan ly khoa hoc va hoc vien
        </h1>
        <p class="text-h6 text-medium-emphasis mb-6">
          Frontend Vue 3 da san sang ket noi Course, Student va Payment Service
          thong qua API Gateway.
        </p>
        <v-btn color="primary" size="large" to="/courses">
          Xem khoa hoc
        </v-btn>
      </v-col>

      <v-col cols="12" md="5">
        <v-card rounded="xl" elevation="4">
          <v-card-title>Trang thai khoi tao</v-card-title>
          <v-card-text>
            <v-list>
              <v-list-item
                v-for="item in services"
                :key="item.name"
                :prepend-icon="item.icon"
                :title="item.name"
                :subtitle="item.description"
              />
            </v-list>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
const services = [
  {
    name: 'Course Service',
    description: 'Khoa hoc, lop hoc va lich hoc',
    icon: 'mdi-book-open-page-variant',
  },
  {
    name: 'Student Service',
    description: 'Hoc vien, ghi danh va diem danh',
    icon: 'mdi-account-school',
  },
  {
    name: 'Payment Service',
    description: 'Tai khoan, hoc phi va bao cao',
    icon: 'mdi-credit-card-outline',
  },
]
</script>

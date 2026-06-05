<template>
  <v-app>
    <v-navigation-drawer
      v-model="drawer"
      :rail="rail"
      permanent
      color="surface"
      class="sidebar"
    >
      <!-- Logo -->
      <div class="pa-4 d-flex align-center" style="min-height: 64px;">
        <v-icon size="32" color="primary" class="mr-3">mdi-school</v-icon>
        <transition name="fade">
          <span v-if="!rail" class="text-h6 font-weight-bold gradient-text">EduCenter</span>
        </transition>
      </div>

      <v-divider class="mx-3 mb-2" style="border-color: rgba(108,99,255,0.15);" />

      <v-list density="comfortable" nav class="px-2">
        <v-list-item
          v-for="item in navItems"
          :key="item.path"
          :to="item.path"
          :prepend-icon="item.icon"
          :title="item.title"
          rounded="lg"
          class="mb-1 nav-item"
          active-class="nav-item-active"
        />
      </v-list>

      <template v-slot:append>
        <div class="pa-2">
          <v-btn
            block
            variant="text"
            :icon="rail ? 'mdi-chevron-right' : 'mdi-chevron-left'"
            @click="rail = !rail"
            class="rail-toggle"
          />
        </div>
      </template>
    </v-navigation-drawer>

    <!-- App Bar -->
    <v-app-bar flat color="transparent" class="app-bar">
      <v-app-bar-title class="text-h6 font-weight-bold">
        {{ currentTitle }}
      </v-app-bar-title>

      <v-spacer />

      <v-chip class="mr-3" color="primary" variant="tonal" size="small">
        <v-icon start size="16">mdi-circle</v-icon>
        Course Service
      </v-chip>

      <v-btn
        icon
        variant="text"
        size="small"
        href="http://localhost:5001/swagger"
        target="_blank"
      >
        <v-icon>mdi-api</v-icon>
        <v-tooltip activator="parent" location="bottom">Swagger API</v-tooltip>
      </v-btn>
    </v-app-bar>

    <!-- Main Content -->
    <v-main class="main-content">
      <v-container fluid class="pa-6">
        <router-view v-slot="{ Component }">
          <transition name="fade" mode="out-in">
            <component :is="Component" />
          </transition>
        </router-view>
      </v-container>
    </v-main>

    <!-- Global Snackbar -->
    <v-snackbar v-model="snackbar.show" :color="snackbar.color" :timeout="3000" location="top right" rounded="lg">
      <v-icon class="mr-2">{{ snackbar.icon }}</v-icon>
      {{ snackbar.text }}
    </v-snackbar>
  </v-app>
</template>

<script setup>
import { ref, computed, provide } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const drawer = ref(true)
const rail = ref(false)

const navItems = [
  { title: 'Khóa học', icon: 'mdi-book-open-variant', path: '/courses' },
  { title: 'Lớp học', icon: 'mdi-google-classroom', path: '/classes' },
]

const currentTitle = computed(() => {
  return route.meta?.title || 'Trung tâm đào tạo'
})

// Global snackbar
const snackbar = ref({ show: false, text: '', color: 'success', icon: 'mdi-check-circle' })

const showSnackbar = (text, color = 'success') => {
  const icons = {
    success: 'mdi-check-circle',
    error: 'mdi-alert-circle',
    warning: 'mdi-alert',
    info: 'mdi-information'
  }
  snackbar.value = { show: true, text, color, icon: icons[color] || icons.success }
}

provide('showSnackbar', showSnackbar)
</script>

<style scoped>
.sidebar {
  border-right: 1px solid rgba(108, 99, 255, 0.1) !important;
  background: linear-gradient(180deg, #1A1A2E 0%, #16162B 100%) !important;
}

.nav-item {
  transition: all 0.2s ease;
}

.nav-item:hover {
  background: rgba(108, 99, 255, 0.08) !important;
}

.nav-item-active {
  background: linear-gradient(135deg, rgba(108, 99, 255, 0.2), rgba(61, 217, 179, 0.1)) !important;
  border: 1px solid rgba(108, 99, 255, 0.2);
}

.rail-toggle {
  opacity: 0.5;
  transition: opacity 0.2s;
}

.rail-toggle:hover {
  opacity: 1;
}

.app-bar {
  backdrop-filter: blur(20px);
  background: rgba(15, 15, 26, 0.8) !important;
  border-bottom: 1px solid rgba(108, 99, 255, 0.08);
}

.main-content {
  background: radial-gradient(ellipse at 20% 50%, rgba(108, 99, 255, 0.04) 0%, transparent 50%),
              radial-gradient(ellipse at 80% 50%, rgba(61, 217, 179, 0.03) 0%, transparent 50%),
              #0F0F1A;
}
</style>

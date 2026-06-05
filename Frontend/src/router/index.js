import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    redirect: '/courses'
  },
  {
    path: '/courses',
    name: 'Courses',
    component: () => import('../views/courses/CourseList.vue'),
    meta: { title: 'Quản lý khóa học', icon: 'mdi-book-open-variant' }
  },
  {
    path: '/classes',
    name: 'Classes',
    component: () => import('../views/classes/ClassList.vue'),
    meta: { title: 'Quản lý lớp học', icon: 'mdi-google-classroom' }
  },
  {
    path: '/classes/:id/schedules',
    name: 'ClassSchedules',
    component: () => import('../views/schedules/ScheduleList.vue'),
    meta: { title: 'Lịch học', icon: 'mdi-calendar-clock' }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  document.title = to.meta?.title ? `${to.meta.title} — Trung tâm đào tạo` : 'Trung tâm đào tạo'
  next()
})

export default router

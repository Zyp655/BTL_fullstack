import { defineStore } from 'pinia'
import api from '../services/api'

export const useCourseStore = defineStore('course', {
  state: () => ({
    courses: [],
    totalCount: 0,
    loading: false,
    currentCourse: null,
  }),

  actions: {
    async fetchCourses(params = {}) {
      this.loading = true
      try {
        const { data } = await api.get('/api/courses', { params })
        this.courses = data.items
        this.totalCount = data.totalCount
      } catch (error) {
        console.error('Error fetching courses:', error)
        throw error
      } finally {
        this.loading = false
      }
    },

    async getCourse(id) {
      try {
        const { data } = await api.get(`/api/courses/${id}`)
        this.currentCourse = data
        return data
      } catch (error) {
        console.error('Error getting course:', error)
        throw error
      }
    },

    async createCourse(course) {
      try {
        const { data } = await api.post('/api/courses', course)
        return data
      } catch (error) {
        console.error('Error creating course:', error)
        throw error
      }
    },

    async updateCourse(id, course) {
      try {
        const { data } = await api.put(`/api/courses/${id}`, course)
        return data
      } catch (error) {
        console.error('Error updating course:', error)
        throw error
      }
    },

    async deleteCourse(id) {
      try {
        await api.delete(`/api/courses/${id}`)
      } catch (error) {
        console.error('Error deleting course:', error)
        throw error
      }
    }
  }
})

export const useClassStore = defineStore('class', {
  state: () => ({
    classes: [],
    totalCount: 0,
    loading: false,
    currentClass: null,
  }),

  actions: {
    async fetchClasses(params = {}) {
      this.loading = true
      try {
        const { data } = await api.get('/api/classes', { params })
        this.classes = data.items
        this.totalCount = data.totalCount
      } catch (error) {
        console.error('Error fetching classes:', error)
        throw error
      } finally {
        this.loading = false
      }
    },

    async getClass(id) {
      try {
        const { data } = await api.get(`/api/classes/${id}`)
        this.currentClass = data
        return data
      } catch (error) {
        console.error('Error getting class:', error)
        throw error
      }
    },

    async createClass(cls) {
      try {
        const { data } = await api.post('/api/classes', cls)
        return data
      } catch (error) {
        console.error('Error creating class:', error)
        throw error
      }
    },

    async updateClass(id, cls) {
      try {
        const { data } = await api.put(`/api/classes/${id}`, cls)
        return data
      } catch (error) {
        console.error('Error updating class:', error)
        throw error
      }
    },

    async updateClassStatus(id, status) {
      try {
        const { data } = await api.put(`/api/classes/${id}/status`, { status })
        return data
      } catch (error) {
        console.error('Error updating class status:', error)
        throw error
      }
    },

    async deleteClass(id) {
      try {
        await api.delete(`/api/classes/${id}`)
      } catch (error) {
        console.error('Error deleting class:', error)
        throw error
      }
    }
  }
})

export const useScheduleStore = defineStore('schedule', {
  state: () => ({
    schedules: [],
    loading: false,
  }),

  actions: {
    async fetchSchedules(classId) {
      this.loading = true
      try {
        const { data } = await api.get(`/api/classes/${classId}/schedules`)
        this.schedules = data
        return data
      } catch (error) {
        console.error('Error fetching schedules:', error)
        throw error
      } finally {
        this.loading = false
      }
    },

    async createSchedule(classId, schedule) {
      try {
        const { data } = await api.post(`/api/classes/${classId}/schedules`, schedule)
        return data
      } catch (error) {
        console.error('Error creating schedule:', error)
        throw error
      }
    },

    async updateSchedule(classId, scheduleId, schedule) {
      try {
        const { data } = await api.put(`/api/classes/${classId}/schedules/${scheduleId}`, schedule)
        return data
      } catch (error) {
        console.error('Error updating schedule:', error)
        throw error
      }
    },

    async deleteSchedule(classId, scheduleId) {
      try {
        await api.delete(`/api/classes/${classId}/schedules/${scheduleId}`)
      } catch (error) {
        console.error('Error deleting schedule:', error)
        throw error
      }
    }
  }
})

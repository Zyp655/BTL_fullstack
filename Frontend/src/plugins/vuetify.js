import { createVuetify } from 'vuetify'

export default createVuetify({
  theme: {
    defaultTheme: 'trainingCenterTheme',
    themes: {
      trainingCenterTheme: {
        dark: false,
        colors: {
          primary: '#155e75',
          secondary: '#f59e0b',
          background: '#f8fafc',
          surface: '#ffffff',
        },
      },
    },
  },
})

import 'vuetify/styles'
import '@mdi/font/css/materialdesignicons.css'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

const vuetify = createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: 'dark',
    themes: {
      dark: {
        dark: true,
        colors: {
          primary: '#6C63FF',
          secondary: '#3DD9B3',
          accent: '#FF6B6B',
          background: '#0F0F1A',
          surface: '#1A1A2E',
          'surface-bright': '#232340',
          'surface-variant': '#2A2A45',
          error: '#FF5252',
          info: '#2196F3',
          success: '#4CAF50',
          warning: '#FFC107',
        }
      },
      light: {
        dark: false,
        colors: {
          primary: '#6C63FF',
          secondary: '#3DD9B3',
          accent: '#FF6B6B',
          background: '#F5F5FA',
          surface: '#FFFFFF',
        }
      }
    }
  },
  defaults: {
    VBtn: {
      rounded: 'lg',
      variant: 'flat',
    },
    VCard: {
      rounded: 'xl',
      elevation: 0,
    },
    VTextField: {
      variant: 'outlined',
      density: 'comfortable',
      rounded: 'lg',
    },
    VSelect: {
      variant: 'outlined',
      density: 'comfortable',
      rounded: 'lg',
    },
    VDataTable: {
      hover: true,
    }
  }
})

export default vuetify

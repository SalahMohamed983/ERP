import React, { Suspense, lazy } from 'react'
import { CircularProgress, Box } from '@mui/material'

// تحميل الصفحات بطريقة ديناميكية
const Dashboard = lazy(() => import('../pages/Dashboard'))
const NotFound = lazy(() => import('../pages/NotFound'))

// مكون تحميل افتراضي
const LoadingFallback = () => (
  <Box
    sx={{
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      height: '100vh'
    }}
  >
    <CircularProgress />
  </Box>
)

// تعريف المسارات
export const routes = [
  {
    path: '/',
    element: <Dashboard />,
    label: 'الرئيسية'
  },
  {
    path: '/accounts',
    label: 'الحسابات'
  },
  {
    path: '/settings',
    label: 'الضبط العام'
  },
  {
    path: '/inventory',
    label: 'المخزون'
  },
  {
    path: '/sales',
    label: 'المبيعات'
  },
  {
    path: '*',
    element: <NotFound />,
    label: 'الصفحة غير موجودة'
  }
]

export { LoadingFallback }

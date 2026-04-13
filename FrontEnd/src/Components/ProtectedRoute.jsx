import React from 'react'
import { useSelector } from 'react-redux'
import { Navigate } from 'react-router-dom'
import { Box, CircularProgress } from '@mui/material'

/**
 * Component يحمي الصفحات من الوصول بدون تسجيل دخول
 * إذا لم يكن هناك مستخدم، سيتم إعادة التوجيه إلى /login
 */
export default function ProtectedRoute({ children }) {
  const user = useSelector((state) => state.auth?.user)
  const loading = useSelector((state) => state.auth?.loading)

  // إذا كان التحميل قيد الضبط، اعرض loading spinner
  if (loading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <CircularProgress />
      </Box>
    )
  }

  // إذا لم يكن هناك مستخدم، أعد التوجيه إلى /login
  if (!user) {
    return <Navigate to="/login" replace />
  }

  // إذا كان المستخدم موجود، اعرض الصفحة
  return children
}

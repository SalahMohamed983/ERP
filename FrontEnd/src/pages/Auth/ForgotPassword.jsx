import React, { useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Container, Box, Paper, TextField, Button, Typography, Alert } from '@mui/material'
import { forgotPassword } from '../../featured/AuthAndPermissions/authSlice'

export default function ForgotPassword() {
  const [email, setEmail] = useState('')
  const [sent, setSent] = useState(false)
  const [emailError, setEmailError] = useState('')
  const dispatch = useDispatch()
  const { loading, error } = useSelector((state) => state.auth)

  const validateEmail = (emailValue) => {
    if (!emailValue) return 'البريد الإلكتروني مطلوب'
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    if (!emailRegex.test(emailValue)) return 'صيغة البريد الإلكتروني غير صحيحة'
    return ''
  }

  const handleEmailChange = (e) => {
    const val = e.target.value
    setEmail(val)
    setEmailError(validateEmail(val))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    const eErr = validateEmail(email)
    setEmailError(eErr)
    if (eErr) return
    const resultAction = await dispatch(forgotPassword({ email }))

    if (forgotPassword.fulfilled.match(resultAction)) {
      setSent(true)
    } else {
      // يمكن عرض رسالة خطأ إذا لزم الأمر
    }
  }

  return (
    <Container>
      <Box sx={{ minHeight: '80vh', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
        <Paper elevation={6} sx={{ width: 520, p: 4 }}>
          <Typography variant="h6" sx={{ mb: 2, textAlign: 'center', fontWeight: 'bold' }}>
            استعادة كلمة المرور
          </Typography>

          {sent ? (
            <Alert severity="success">
              تم إرسال تعليمات استعادة كلمة المرور إلى البريد الإلكتروني (إن وُجد).
            </Alert>
          ) : (
            <Box component="form" onSubmit={handleSubmit} dir="rtl" sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
              <Typography sx={{ textAlign: 'right' }}>
                أدخل البريد الإلكتروني المسجل لاستلام رابط استعادة كلمة المرور
              </Typography>
              {error && <Alert severity="error">{error}</Alert>}
              <TextField
                label="البريد الإلكتروني"
                value={email}
                onChange={handleEmailChange}
                fullWidth
                size="small"
                error={!!emailError}
                helperText={emailError}
              />
              <Button type="submit" variant="contained" color="primary" disabled={loading || !!emailError}>
                {loading ? 'جاري الإرسال...' : 'إرسال'}
              </Button>
            </Box>
          )}
        </Paper>
      </Box>
    </Container>
  )
}


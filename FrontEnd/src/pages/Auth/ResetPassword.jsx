import React, { useState, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import {
  Container,
  Box,
  Paper,
  TextField,
  Button,
  Typography,
  InputAdornment,
  IconButton,
  Alert,
} from '@mui/material'
import { Link, useSearchParams } from 'react-router-dom'
import MailOutlineIcon from '@mui/icons-material/MailOutline'
import LockOutlinedIcon from '@mui/icons-material/LockOutlined'
import Visibility from '@mui/icons-material/Visibility'
import VisibilityOff from '@mui/icons-material/VisibilityOff'
import { resetPassword, getErrorMessage } from '../../featured/AuthAndPermissions/authSlice'

const validateEmail = (email) => {
  if (!email) return 'البريد الإلكتروني مطلوب'
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  if (!emailRegex.test(email)) return 'صيغة البريد الإلكتروني غير صحيحة'
  return ''
}

const validatePassword = (pwd) => {
  if (!pwd) return 'كلمة المرور مطلوبة'
  if (pwd.length < 6) return 'يجب أن تكون كلمة المرور على الأقل 6 أحرف'
  if (!/[a-z]/.test(pwd)) return 'يجب أن تحتوي على حرف صغير واحد على الأقل'
  if (!/[A-Z]/.test(pwd)) return 'يجب أن تحتوي على حرف كبير واحد على الأقل'
  if (!/\d/.test(pwd)) return 'يجب أن تحتوي على رقم واحد على الأقل'
  return ''
}

export default function ResetPassword() {
  const [searchParams] = useSearchParams()
  const dispatch = useDispatch()
  const { loading, error } = useSelector((state) => state.auth)

  const [email, setEmail] = useState('')
  const [token, setToken] = useState('')
  const [newPassword, setNewPassword] = useState('')
  const [confirmPassword, setConfirmPassword] = useState('')
  const [showPassword, setShowPassword] = useState(false)
  const [success, setSuccess] = useState(false)
  const [localError, setLocalError] = useState('')
  const [emailError, setEmailError] = useState('')
  const [passwordError, setPasswordError] = useState('')
  const [confirmError, setConfirmError] = useState('')

  useEffect(() => {
    const tokenFromUrl = searchParams.get('token') || ''
    const emailFromUrl = searchParams.get('email') || ''
    setToken(tokenFromUrl)
    setEmail(emailFromUrl)
    if (emailFromUrl) setEmailError(validateEmail(emailFromUrl))
  }, [searchParams])

  const handleEmailChange = (e) => {
    const val = e.target.value
    setEmail(val)
    setEmailError(validateEmail(val))
  }

  const handleNewPasswordChange = (e) => {
    const val = e.target.value
    setNewPassword(val)
    setPasswordError(validatePassword(val))
    if (confirmPassword) setConfirmError(val !== confirmPassword ? 'كلمة المرور غير متطابقة' : '')
  }

  const handleConfirmChange = (e) => {
    const val = e.target.value
    setConfirmPassword(val)
    setConfirmError(val !== newPassword ? 'كلمة المرور غير متطابقة' : '')
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setLocalError('')
    const eErr = validateEmail(email)
    const pErr = validatePassword(newPassword)
    const cErr = newPassword !== confirmPassword ? 'كلمة المرور غير متطابقة' : ''
    setEmailError(eErr)
    setPasswordError(pErr)
    setConfirmError(cErr)
    if (eErr || pErr || cErr) return
    if (!token.trim()) {
      setLocalError('رابط إعادة التعيين غير صالح (الرمز مفقود). استخدم الرابط المرسل إلى بريدك.')
      return
    }

    const resultAction = await dispatch(
      resetPassword({ email, token, newPassword, confirmPassword })
    )

    if (resetPassword.fulfilled.match(resultAction)) {
      setSuccess(true)
    } else {
      setLocalError(
        getErrorMessage(
          resultAction.payload,
          'فشل إعادة تعيين كلمة المرور — تحقق من الرابط أو حاول طلب رابط جديد.'
        )
      )
    }
  }

  const displayError = !(localError || error)
    ? ''
    : typeof (localError || error) === 'string'
      ? localError || error
      : 'حدث خطأ غير متوقع'

  if (success) {
    return (
      <Container>
        <Box sx={{ minHeight: '80vh', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
          <Paper elevation={6} sx={{ width: 420, p: 4, textAlign: 'center' }}>
            <Typography variant="h6" sx={{ mb: 2, fontWeight: 'bold' }}>
              تم تغيير كلمة المرور
            </Typography>
            <Alert severity="success" sx={{ mb: 2 }}>
              تم إعادة تعيين كلمة المرور بنجاح. يمكنك تسجيل الدخول الآن.
            </Alert>
            <Button component={Link} to="/login" variant="contained" color="primary" fullWidth>
              تسجيل الدخول
            </Button>
          </Paper>
        </Box>
      </Container>
    )
  }

  return (
    <Container>
      <Box sx={{ minHeight: '80vh', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
        <Paper elevation={6} sx={{ width: 420, p: 4, textAlign: 'center' }}>
          <Typography variant="h6" sx={{ mb: 3, fontWeight: 'bold' }}>
            إعادة تعيين كلمة المرور
          </Typography>

          {displayError && (
            <Alert severity="error" sx={{ mb: 2 }}>
              {displayError}
            </Alert>
          )}

          <Box component="form" onSubmit={handleSubmit} dir="rtl">
            <TextField
              label="البريد الإلكتروني"
              value={email}
              onChange={handleEmailChange}
              fullWidth
              size="small"
              sx={{ mb: 2 }}
              error={!!emailError}
              helperText={emailError}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <MailOutlineIcon />
                  </InputAdornment>
                ),
              }}
            />

            <TextField
              label="كلمة المرور الجديدة"
              value={newPassword}
              onChange={handleNewPasswordChange}
              fullWidth
              size="small"
              sx={{ mb: 2 }}
              error={!!passwordError}
              helperText={passwordError}
              type={showPassword ? 'text' : 'password'}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <LockOutlinedIcon />
                  </InputAdornment>
                ),
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton
                      size="small"
                      onClick={() => setShowPassword(!showPassword)}
                      edge="end"
                    >
                      {showPassword ? <VisibilityOff /> : <Visibility />}
                    </IconButton>
                  </InputAdornment>
                ),
              }}
            />

            <TextField
              label="تأكيد كلمة المرور"
              value={confirmPassword}
              onChange={handleConfirmChange}
              fullWidth
              size="small"
              sx={{ mb: 2 }}
              error={!!confirmError}
              helperText={confirmError}
              type={showPassword ? 'text' : 'password'}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <LockOutlinedIcon />
                  </InputAdornment>
                ),
              }}
            />

            <Button
              type="submit"
              variant="contained"
              color="primary"
              fullWidth
              sx={{ py: 1.5, mb: 1 }}
              disabled={
                loading ||
                !!emailError ||
                !!passwordError ||
                !!confirmError 
              }
            >
              {loading ? 'جاري الحفظ...' : 'إعادة تعيين كلمة المرور'}
            </Button>

            <Box sx={{ display: 'flex', justifyContent: 'center', mt: 1 }}>
              <Button component={Link} to="/login" size="small">
                العودة لتسجيل الدخول
              </Button>
            </Box>
          </Box>
        </Paper>
      </Box>
    </Container>
  )
}

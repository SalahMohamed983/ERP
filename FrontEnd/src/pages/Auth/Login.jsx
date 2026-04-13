import React, { useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Container, Box, Paper, TextField, Button, Typography, InputAdornment, IconButton, Alert } from '@mui/material'
import { Link, useNavigate } from 'react-router-dom'
import MailOutlineIcon from '@mui/icons-material/MailOutline'
import LockOutlinedIcon from '@mui/icons-material/LockOutlined'
import Visibility from '@mui/icons-material/Visibility'
import VisibilityOff from '@mui/icons-material/VisibilityOff'
import { login, getErrorMessage } from '../../featured/AuthAndPermissions/authSlice'

export default function Login() {
  const [username, setUsername] = useState('admin')
  const [password, setPassword] = useState('')
  const [showPassword, setShowPassword] = useState(false)
  const [localError, setLocalError] = useState('')
  const [emailError, setEmailError] = useState('')
  const [passwordError, setPasswordError] = useState('')
  const navigate = useNavigate()
  const dispatch = useDispatch()
  const { loading, error } = useSelector((state) => state.auth)

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

  const handleEmailChange = (e) => {
    const val = e.target.value
    setUsername(val)
    setEmailError(validateEmail(val))
  }

  const handlePasswordChange = (e) => {
    const val = e.target.value
    setPassword(val)
    setPasswordError(validatePassword(val))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setLocalError('')
    const eErr = validateEmail(username)
    const pErr = validatePassword(password)
    setEmailError(eErr)
    setPasswordError(pErr)
    if (eErr || pErr) return

    const resultAction = await dispatch(
      login({ userName: username, password })
    )

    if (login.fulfilled.match(resultAction)) {
      // تسجيل دخول ناجح
      navigate('/')
    } else {
      const errorMessage = getErrorMessage(
        resultAction.payload,
        'بيانات الدخول غير صحيحة — يرجى المحاولة مجدداً'
      )
      setLocalError(errorMessage)
    }
  }

  const finalError = localError || error
  const displayError = !finalError ? '' : (typeof finalError === 'string' ? finalError : 'حدث خطأ غير متوقع')

  return (
    <Container>
      <Box sx={{ minHeight: '80vh', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
        <Paper elevation={6} sx={{ width: 420, p: 4, textAlign: 'center' }}>
          <Typography variant="h6" sx={{ mb: 3, fontWeight: 'bold' }}>
            تسجيل الدخول
          </Typography>

          {displayError && <Alert severity="error" sx={{ mb: 2 }}>{displayError}</Alert>}

          <Box component="form" onSubmit={handleSubmit} dir="rtl">
            <TextField
              label="اسم المستخدم"
              value={username}
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
                )
              }}
            />

            <TextField
              label="كلمة المرور"
              value={password}
              onChange={handlePasswordChange}
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
                    <IconButton size="small" onClick={() => setShowPassword(!showPassword)} edge="end">
                      {showPassword ? <VisibilityOff /> : <Visibility />}
                    </IconButton>
                  </InputAdornment>
                )
              }}
            />

            <Button
              type="submit"
              variant="contained"
              color="primary"
              fullWidth
              sx={{ py: 1.5, mb: 1 }}
              disabled={loading || !!emailError || !!passwordError}
            >
              {loading ? 'جارِ تسجيل الدخول...' : 'تسجيل الدخول'}
            </Button>

            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mt: 1 }}>
              <Button component={Link} to="/forgot-password" size="small">نسيت كلمة المرور؟</Button>
            </Box>
          </Box>
        </Paper>
      </Box>
    </Container>
  )
}


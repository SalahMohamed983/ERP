import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { Container, Box, Paper, Avatar, Typography, Button, List, ListItem, ListItemText } from '@mui/material'
import { logout } from '../../featured/AuthAndPermissions/authSlice'
import { useNavigate } from 'react-router-dom'

function firstAvailable(obj, keys) {
  if (!obj) return ''
  for (const k of keys) {
    if (obj[k]) return obj[k]
  }
  return ''
}

export default function Profile() {
  const user = useSelector((s) => s.auth.user)
  const dispatch = useDispatch()
  const navigate = useNavigate()

  const handleLogout = async () => {
    await dispatch(logout())
    navigate('/login')
  }

  if (!user) {
    return (
      <Container>
        <Box sx={{ minHeight: '60vh', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
          <Paper sx={{ p: 4, textAlign: 'center' }}>
            <Typography variant="h6">لم يتم العثور على بيانات المستخدم</Typography>
            <Button sx={{ mt: 2 }} variant="contained" onClick={() => navigate('/login')}>الانتقال لتسجيل الدخول</Button>
          </Paper>
        </Box>
      </Container>
    )
  }

  const displayName = user?.fullName;
  const email = user?.email || '-';
  const phoneNumber = user?.phoneNumber || '-';
  const userId = user?.id  || '-';
  const roles = user?.roles  || [];

  return (
    <Container>
      <Box sx={{ mt: 4, display: 'flex', justifyContent: 'center' }}>
        <Paper sx={{ width: 700, p: 4 }}>
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 3, mb: 4, pb: 3, borderBottom: '1px solid #e0e0e0' }}>
            <Avatar sx={{ width: 80, height: 80, backgroundColor: '#1976d2', fontSize: '2rem' }}>
              {(displayName && displayName[0]?.toUpperCase()) || 'U'}
            </Avatar>
            <Box sx={{ flex: 1 }}>
              <Typography variant="h5" sx={{ fontWeight: 'bold', mb: 0.5 }}>{displayName || 'مستخدم غير معروف'}</Typography>
              <Typography variant="body2" color="text.secondary">{email}</Typography>
              {phoneNumber !== '-' && <Typography variant="body2" color="text.secondary">{phoneNumber}</Typography>}
            </Box>
            <Button variant="outlined" color="error" onClick={handleLogout} sx={{ whiteSpace: 'nowrap' }}>
              تسجيل الخروج
            </Button>
          </Box>

          <List sx={{ '& .MuiListItem-root': { py: 2, borderBottom: '1px solid #f0f0f0' } }}>
            <ListItem>
              <ListItemText 
                primary="الاسم الكامل" 
                secondary={displayName || '-'}
                primaryTypographyProps={{ sx: { fontWeight: 600, color: '#424242' } }}
              />
            </ListItem>
            <ListItem>
              <ListItemText 
                primary="البريد الإلكتروني" 
                secondary={email}
                primaryTypographyProps={{ sx: { fontWeight: 600, color: '#424242' } }}
              />
            </ListItem>
            <ListItem>
              <ListItemText 
                primary="رقم الهاتف" 
                secondary={phoneNumber}
                primaryTypographyProps={{ sx: { fontWeight: 600, color: '#424242' } }}
              />
            </ListItem>
            <ListItem>
              <ListItemText 
                primary="معرّف المستخدم" 
                secondary={userId}
                primaryTypographyProps={{ sx: { fontWeight: 600, color: '#424242' } }}
              />
            </ListItem>
            <ListItem>
              <ListItemText 
                primary="الأدوار" 
                secondary={Array.isArray(roles) ? (roles.length > 0 ? roles.join(', ') : '-') : roles || '-'}
                primaryTypographyProps={{ sx: { fontWeight: 600, color: '#424242' } }}
              />
            </ListItem>
          </List>
        </Paper>
      </Box>
    </Container>
  )
}

import React from 'react'
import { Box, Typography, Button } from '@mui/material'
import { useNavigate } from 'react-router-dom'

export default function NotFound() {
  const navigate = useNavigate()

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        height: '100vh',
        textAlign: 'center'
      }}
    >
      <Typography variant="h1" sx={{ mb: 2 }}>
        404
      </Typography>
      <Typography variant="h4" sx={{ mb: 2 }}>
        الصفحة غير موجودة
      </Typography>
      <Typography variant="body1" color="textSecondary" sx={{ mb: 3 }}>
        عذراً، لم نتمكن من العثور على الصفحة المطلوبة
      </Typography>
      <Button
        variant="contained"
        onClick={() => navigate('/')}
      >
        العودة للرئيسية
      </Button>
    </Box>
  )
}

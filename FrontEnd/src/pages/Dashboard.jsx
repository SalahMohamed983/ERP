import React from 'react'
import ImageComponent from '../Components/Common/ImageComponent'
import { Box, Container, Typography } from '@mui/material'

export default function Dashboard() {
  return (
    <main className="flex-1 p-4">
      <Container maxWidth="lg">
        <Typography variant="h4" gutterBottom sx={{ mb: 3, fontWeight: 'bold' }}>
          لوحة التحكم
        </Typography>
        
        <Box sx={{ display: 'grid', gridTemplateColumns: { xs: '1fr', md: '2fr 1fr' }, gap: 3 }}>
          {/* الصورة الرئيسية */}
          <ImageComponent
            src="https://images.unsplash.com/photo-1505682634904-d7c7e1f7d6c9?auto=format&fit=crop&w=1600&q=60"
            alt="لوحة التحكم"
            height="400px"
            shadow={true}
          />

          {/* معلومات إضافية */}
          <Box sx={{ bg: 'white', p: 2, borderRadius: 1, boxShadow: 1 }}>
            <Typography variant="h6" gutterBottom>
              ملخص النظام
            </Typography>
            <Typography variant="body2" color="textSecondary">
              نرحب بك في نظام إدارة المشاريع الخاص بنا
            </Typography>
          </Box>
        </Box>
      </Container>
    </main>
  )
}

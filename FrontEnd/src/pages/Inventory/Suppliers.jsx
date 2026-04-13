import React from 'react'
import { Container, Box, Typography } from '@mui/material'

export default function Suppliers() {
  return (
    <main className="flex-1 p-4">
      <Container maxWidth="lg">
        <Typography variant="h4" gutterBottom>
          الموردين
        </Typography>
        <Box sx={{ bg: 'white', p: 3, borderRadius: 1, boxShadow: 1 }}>
          <Typography>محتوى الموردين</Typography>
        </Box>
      </Container>
    </main>
  )
}

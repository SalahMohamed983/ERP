import React, { useState } from 'react'
import { Container, Box, Typography, Button } from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import ReusableTable from '../../Components/Common/ReusableTable'

export default function InvoiceCategories() {
  const [categories, setCategories] = useState([
    {
      id: 1,
      name: 'فقط',
      lastUpdated: '( 2022-05-06 )\n( Main Admin x صباح 12:00 )',
      modificationStatus: 'شيوخ',
      select: '☑️ مفعل'
    },
    {
      id: 2,
      name: 'سحاب',
      lastUpdated: '( 2022-05-06 )\n( Main Admin x صباح 12:00 )',
      modificationStatus: 'شيوخ',
      select: '☑️ مفعل'
    },
    {
      id: 3,
      name: 'عمدات SD',
      lastUpdated: '( 2022-05-06 )\n( Main Admin x صباح 12:00 )',
      modificationStatus: 'شيوخ',
      select: '☑️ مفعل'
    }
  ])

  const columns = [
    { label: 'اختر الكل', field: 'select', width: '15%' },
    { label: 'اسم الفئة', field: 'name', width: '20%' },
    { label: 'تاريخ التحديث', field: 'lastUpdated', width: '35%' },
    { label: 'حالة التعديل', field: 'modificationStatus', width: '30%' }
  ]

  const handleAdd = () => {
    const newCategory = {
      id: Date.now(),
      name: '',
      lastUpdated: '',
      modificationStatus: '',
      select: '☑️ مفعل'
    }
    setCategories([...categories, newCategory])
  }

  const handleEdit = (row) => {
    console.log('edit', row)
  }

  const handleDelete = (row) => {
    setCategories(categories.filter((c) => c.id !== row.id))
  }

  return (
    <main className="flex-1 p-4">
      <Container maxWidth="lg">
        <Typography variant="h4" gutterBottom sx={{ mb: 3 }}>
          فئات فواتير المبيعات
        </Typography>

        <Box sx={{ mb: 3, display: 'flex', justifyContent: 'center', gap: 2 }}>
          <Button
            variant="contained"
            sx={{ backgroundColor: '#d32f2f', color: 'white', px: 3 }}
          >
            تحويل
          </Button>
          <Button
            variant="contained"
            sx={{ backgroundColor: '#4caf50', color: 'white', px: 3 }}
          >
            تفصيل
          </Button>
          <Button
            variant="contained"
            color="primary"
            startIcon={<AddIcon />}
            onClick={handleAdd}
            sx={{ px: 3 }}
          >
            إضافة جديد
          </Button>
        </Box>

        <Typography variant="h6" sx={{ mb: 2, textAlign: 'center' }}>
          بيانات فئات فواتير المبيعات
        </Typography>

        <ReusableTable
          columns={columns}
          data={categories}
          onEdit={handleEdit}
          onDelete={handleDelete}
          headerColor="#003366"
          maxWidth="100%"
        />
      </Container>
    </main>
  )
}

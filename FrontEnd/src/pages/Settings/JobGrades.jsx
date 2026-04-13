import React, { useState } from 'react'
import { Container, Box, Button, Dialog, DialogTitle, DialogContent, DialogActions, TextField } from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import ReusableTable from '../../Components/Common/ReusableTable'

export default function JobGrades() {
  const [grades, setGrades] = useState([
    { id: 1, code: '1', name: 'مدير عام' },
    { id: 2, code: '2', name: 'رئيس قسم' }
  ])

  const [openDialog, setOpenDialog] = useState(false)
  const [editingGrade, setEditingGrade] = useState(null)
  const [formData, setFormData] = useState({ code: '', name: '' })

  const columns = [
    { label: 'كود الدرجة', field: 'code', width: '15%' },
    { label: 'اسم الدرجة', field: 'name', width: '85%' }
  ]

  const handleOpenDialog = (grade = null) => {
    if (grade) {
      setEditingGrade(grade)
      setFormData(grade)
    } else {
      setEditingGrade(null)
      setFormData({ code: '', name: '' })
    }
    setOpenDialog(true)
  }

  const handleCloseDialog = () => {
    setOpenDialog(false)
    setEditingGrade(null)
  }

  const handleSave = () => {
    if (editingGrade) {
      setGrades(grades.map(g => g.id === editingGrade.id ? { ...formData, id: editingGrade.id } : g))
    } else {
      setGrades([...grades, { ...formData, id: Date.now() }])
    }
    handleCloseDialog()
  }

  const handleDelete = (grade) => {
    setGrades(grades.filter(g => g.id !== grade.id))
  }

  return (
    <main className="flex-1 p-4">
      <Container maxWidth="lg">
        <Box sx={{ mb: 3, display: 'flex', justifyContent: 'center' }}>
          <Button
            variant="contained"
            color="primary"
            startIcon={<AddIcon />}
            onClick={() => handleOpenDialog()}
            sx={{ px: 3 }}
          >
            إضافة جديد
          </Button>
        </Box>

        <ReusableTable
          columns={columns}
          data={grades}
          onEdit={handleOpenDialog}
          onDelete={handleDelete}
          headerColor="#003366"
          maxWidth="900px"
        />
      </Container>

      <Dialog open={openDialog} onClose={handleCloseDialog} maxWidth="sm" fullWidth>
        <DialogTitle>
          {editingGrade ? 'تعديل درجة وظيفية' : 'إضافة درجة وظيفية جديدة'}
        </DialogTitle>
        <DialogContent sx={{ pt: 3, display: 'flex', flexDirection: 'column', gap: 2 }}>
          <TextField
            label="كود الدرجة"
            value={formData.code}
            onChange={(e) => setFormData({ ...formData, code: e.target.value })}
            fullWidth
            dir="rtl"
          />
          <TextField
            label="اسم الدرجة"
            value={formData.name}
            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
            fullWidth
            dir="rtl"
          />
        </DialogContent>
        <DialogActions sx={{ p: 2 }}>
          <Button onClick={handleCloseDialog}>إلغاء</Button>
          <Button onClick={handleSave} variant="contained" color="primary">
            {editingGrade ? 'تحديث' : 'إضافة'}
          </Button>
        </DialogActions>
      </Dialog>
    </main>
  )
}

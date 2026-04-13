import React, { useState } from 'react'
import { Container, Box, Button, Dialog, DialogTitle, DialogContent, DialogActions, TextField } from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import ReusableTable from '../../Components/Common/ReusableTable'

export default function SupplierCategories() {
  const [categories, setCategories] = useState([
    { id: 1, code: '1', name: 'ورشة١', accountCode: '54', email: 'خ؟ (0 ) ختم', notes: '' }
  ])

  const [openDialog, setOpenDialog] = useState(false)
  const [editingCategory, setEditingCategory] = useState(null)
  const [formData, setFormData] = useState({ code: '', name: '', accountCode: '', email: '', notes: '' })

  const columns = [
    { label: 'كود القسم', field: 'code', width: '10%' },
    { label: 'اسم القسم', field: 'name', width: '25%' },
    { label: 'رقم الحساب المالي', field: 'accountCode', width: '20%' },
    { label: 'البريد الداخلي', field: 'email', width: '30%' },
    { label: 'ملاحظات', field: 'notes', width: '15%' }
  ]

  const handleOpenDialog = (category = null) => {
    if (category) {
      setEditingCategory(category)
      setFormData(category)
    } else {
      setEditingCategory(null)
      setFormData({ code: '', name: '', accountCode: '', email: '', notes: '' })
    }
    setOpenDialog(true)
  }

  const handleCloseDialog = () => {
    setOpenDialog(false)
    setEditingCategory(null)
  }

  const handleSave = () => {
    if (editingCategory) {
      setCategories(categories.map(c => c.id === editingCategory.id ? { ...formData, id: editingCategory.id } : c))
    } else {
      setCategories([...categories, { ...formData, id: Date.now() }])
    }
    handleCloseDialog()
  }

  const handleDelete = (category) => {
    setCategories(categories.filter(c => c.id !== category.id))
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
          data={categories}
          onEdit={handleOpenDialog}
          onDelete={handleDelete}
          headerColor="#003d7a"
          maxWidth="1100px"
        />
      </Container>

      <Dialog open={openDialog} onClose={handleCloseDialog} maxWidth="sm" fullWidth>
        <DialogTitle>
          {editingCategory ? 'تعديل فئة موردين' : 'إضافة فئة موردين جديدة'}
        </DialogTitle>
        <DialogContent sx={{ pt: 3, display: 'flex', flexDirection: 'column', gap: 2 }}>
          <TextField
            label="كود القسم"
            value={formData.code}
            onChange={(e) => setFormData({ ...formData, code: e.target.value })}
            fullWidth
            dir="rtl"
          />
          <TextField
            label="اسم القسم"
            value={formData.name}
            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
            fullWidth
            dir="rtl"
          />
          <TextField
            label="رقم الحساب المالي"
            value={formData.accountCode}
            onChange={(e) => setFormData({ ...formData, accountCode: e.target.value })}
            fullWidth
            dir="rtl"
          />
          <TextField
            label="البريد الداخلي"
            value={formData.email}
            onChange={(e) => setFormData({ ...formData, email: e.target.value })}
            fullWidth
            dir="rtl"
          />
          <TextField
            label="ملاحظات"
            value={formData.notes}
            onChange={(e) => setFormData({ ...formData, notes: e.target.value })}
            fullWidth
            dir="rtl"
            multiline
            rows={2}
          />
        </DialogContent>
        <DialogActions sx={{ p: 2 }}>
          <Button onClick={handleCloseDialog}>إلغاء</Button>
          <Button onClick={handleSave} variant="contained" color="primary">
            {editingCategory ? 'تحديث' : 'إضافة'}
          </Button>
        </DialogActions>
      </Dialog>
    </main>
  )
}

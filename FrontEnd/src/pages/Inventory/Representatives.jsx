import React, { useState } from 'react'
import { Container, Box, Button, Dialog, DialogTitle, DialogContent, DialogActions, TextField } from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import ReusableTable from '../../Components/Common/ReusableTable'

export default function Representatives() {
  const [representatives, setRepresentatives] = useState([
    { id: 1, select: false, code: '13', name: 'أحمد محمود السيد', category: 'مخزنات', accountCode: '64', division1: 'خ؟ ( 0 ) ختم', status: 'فعال', email: 'info@example' },
    { id: 2, select: false, code: '12', name: 'طه احمد شاويش', category: 'مخزنات', accountCode: '62', division1: 'دالي واستغلق له ( 2500 )', status: 'فعال', email: 'info@example' },
    { id: 3, select: false, code: '11', name: 'محمود احمد عبدالرحمن', category: 'مخزنات', accountCode: '62', division1: 'دالي واستغلق له ( 2500 )', status: 'فعال', email: 'info@example' }
  ])

  const [openDialog, setOpenDialog] = useState(false)
  const [editingRep, setEditingRep] = useState(null)
  const [formData, setFormData] = useState({ code: '', name: '', category: '', accountCode: '', division1: '', status: '', email: '' })

  const handleOpenDialog = (rep = null) => {
    if (rep) {
      setEditingRep(rep)
      setFormData(rep)
    } else {
      setEditingRep(null)
      setFormData({ code: '', name: '', category: '', accountCode: '', division1: '', status: '', email: '' })
    }
    setOpenDialog(true)
  }

  const handleCloseDialog = () => {
    setOpenDialog(false)
    setEditingRep(null)
  }

  const handleSave = () => {
    if (editingRep) {
      setRepresentatives(representatives.map(r => r.id === editingRep.id ? { ...formData, id: editingRep.id } : r))
    } else {
      setRepresentatives([...representatives, { ...formData, id: Date.now(), select: false }])
    }
    handleCloseDialog()
  }

  const handleDelete = (row) => {
    setRepresentatives(representatives.filter(r => r.id !== row.id))
  }

  const columns = [
    { label: '', field: 'select', width: '5%' },
    { label: 'كود المندوب', field: 'code', width: '8%' },
    { label: 'الاسم', field: 'name', width: '20%' },
    { label: 'الفئة', field: 'category', width: '15%' },
    { label: 'رقم الحساب المالي', field: 'accountCode', width: '15%' },
    { label: 'تقسيم 1', field: 'division1', width: '12%' },
    { label: 'حالة التفصيل', field: 'status', width: '12%' },
    { label: 'البريد الداخلي', field: 'email', width: '13%' }
  ]

  const data = representatives.map(r => ({
    ...r,
    select: (
      <input
        type="checkbox"
        checked={!!r.select}
        onChange={() => setRepresentatives(representatives.map(item => item.id === r.id ? { ...item, select: !item.select } : item))}
      />
    )
  }))

  return (
    <main className="flex-1 p-4">
      <Container maxWidth="lg">
        <Box sx={{ mb: 3, display: 'flex', justifyContent: 'center', gap: 2, flexWrap: 'wrap' }}>
          <Button variant="contained" sx={{ backgroundColor: '#c85a6d', '&:hover': { backgroundColor: '#b34a5d' } }}>
            تحميل
          </Button>
          <Button variant="contained" sx={{ backgroundColor: '#5a9b7b', '&:hover': { backgroundColor: '#4a8b6b' } }}>
            تفصيل
          </Button>
          <Button variant="contained" color="primary" startIcon={<AddIcon />} onClick={() => handleOpenDialog()}>
            إضافة جديد
          </Button>
        </Box>

        <ReusableTable
          columns={columns}
          data={data}
          onEdit={handleOpenDialog}
          onDelete={handleDelete}
          headerColor="#003d7a"
          maxWidth="1200px"
        />
      </Container>

      <Dialog open={openDialog} onClose={handleCloseDialog} maxWidth="sm" fullWidth>
        <DialogTitle>
          {editingRep ? 'تعديل المندوب' : 'إضافة مندوب جديد'}
        </DialogTitle>
        <DialogContent sx={{ pt: 3, display: 'flex', flexDirection: 'column', gap: 2 }}>
          <TextField label="كود المندوب" value={formData.code} onChange={(e) => setFormData({ ...formData, code: e.target.value })} fullWidth dir="rtl" />
          <TextField label="الاسم" value={formData.name} onChange={(e) => setFormData({ ...formData, name: e.target.value })} fullWidth dir="rtl" />
          <TextField label="الفئة" value={formData.category} onChange={(e) => setFormData({ ...formData, category: e.target.value })} fullWidth dir="rtl" />
          <TextField label="رقم الحساب المالي" value={formData.accountCode} onChange={(e) => setFormData({ ...formData, accountCode: e.target.value })} fullWidth dir="rtl" />
          <TextField label="تقسيم 1" value={formData.division1} onChange={(e) => setFormData({ ...formData, division1: e.target.value })} fullWidth dir="rtl" />
          <TextField label="حالة التفصيل" value={formData.status} onChange={(e) => setFormData({ ...formData, status: e.target.value })} fullWidth dir="rtl" />
          <TextField label="البريد الداخلي" value={formData.email} onChange={(e) => setFormData({ ...formData, email: e.target.value })} fullWidth dir="rtl" />
        </DialogContent>
        <DialogActions sx={{ p: 2 }}>
          <Button onClick={handleCloseDialog}>إلغاء</Button>
          <Button onClick={handleSave} variant="contained" color="primary">{editingRep ? 'تحديث' : 'إضافة'}</Button>
        </DialogActions>
      </Dialog>
    </main>
  )
}

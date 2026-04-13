import React, { useState } from 'react'
import { Container, Box, Button, Dialog, DialogTitle, DialogContent, DialogActions, TextField } from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import ReusableTable from '../../Components/Common/ReusableTable'

export default function ManufacturingLines() {
  const [lines, setLines] = useState([
    { id: 1, code: '1', name: 'ورشة١', accountCode: '54', email: 'example@local', notes: '' }
  ])

  const [openDialog, setOpenDialog] = useState(false)
  const [editingLine, setEditingLine] = useState(null)
  const [formData, setFormData] = useState({ code: '', name: '', accountCode: '', email: '', notes: '' })

  const handleOpenDialog = (line = null) => {
    if (line) {
      setEditingLine(line)
      setFormData(line)
    } else {
      setEditingLine(null)
      setFormData({ code: '', name: '', accountCode: '', email: '', notes: '' })
    }
    setOpenDialog(true)
  }

  const handleCloseDialog = () => {
    setOpenDialog(false)
    setEditingLine(null)
  }

  const handleSave = () => {
    if (editingLine) {
      setLines(lines.map(l => l.id === editingLine.id ? { ...formData, id: editingLine.id } : l))
    } else {
      setLines([...lines, { ...formData, id: Date.now() }])
    }
    handleCloseDialog()
  }

  return (
    <main className="flex-1 p-4">
      <Container maxWidth="lg">
        <Box sx={{ mb: 3, display: 'flex', justifyContent: 'center' }}>
          <Button variant="contained" color="primary" startIcon={<AddIcon />} sx={{ px: 3 }} onClick={() => handleOpenDialog()}>
            إضافة جديد
          </Button>
        </Box>

        <ReusableTable
          columns={[
            { label: 'كود القسم', field: 'code', width: '10%' },
            { label: 'اسم القسم', field: 'name', width: '25%' },
            { label: 'رقم الحساب المالي', field: 'accountCode', width: '20%' },
            { label: 'البريد الداخلي', field: 'email', width: '30%' },
            { label: 'ملاحظات', field: 'notes', width: '15%' }
          ]}
          data={lines}
          onEdit={handleOpenDialog}
          headerColor="#003d7a"
          maxWidth="1100px"
        />
      </Container>

      <Dialog open={openDialog} onClose={handleCloseDialog} maxWidth="sm" fullWidth>
        <DialogTitle>{editingLine ? 'تعديل القسم' : 'إضافة قسم جديد'}</DialogTitle>
        <DialogContent sx={{ pt: 3, display: 'flex', flexDirection: 'column', gap: 2 }}>
          <TextField label="كود القسم" value={formData.code} onChange={(e) => setFormData({ ...formData, code: e.target.value })} fullWidth dir="rtl" />
          <TextField label="اسم القسم" value={formData.name} onChange={(e) => setFormData({ ...formData, name: e.target.value })} fullWidth dir="rtl" />
          <TextField label="رقم الحساب المالي" value={formData.accountCode} onChange={(e) => setFormData({ ...formData, accountCode: e.target.value })} fullWidth dir="rtl" />
          <TextField label="البريد الداخلي" value={formData.email} onChange={(e) => setFormData({ ...formData, email: e.target.value })} fullWidth dir="rtl" />
          <TextField label="ملاحظات" value={formData.notes} onChange={(e) => setFormData({ ...formData, notes: e.target.value })} fullWidth dir="rtl" multiline rows={2} />
        </DialogContent>
        <DialogActions sx={{ p: 2 }}>
          <Button onClick={handleCloseDialog}>إلغاء</Button>
          <Button onClick={handleSave} variant="contained" color="primary">{editingLine ? 'تحديث' : 'إضافة'}</Button>
        </DialogActions>
      </Dialog>
    </main>
  )
}

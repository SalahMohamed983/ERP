import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import {
  Container,
  Box,
  Typography,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField
} from '@mui/material'
import EditIcon from '@mui/icons-material/Edit'
import AddIcon from '@mui/icons-material/Add'
import ReusableTable from '../../Components/Common/ReusableTable'

export default function FinancialRecords() {
  const navigate = useNavigate()
  const [records, setRecords] = useState([
    {
      id: 1,
      code: '2022',
      year: '2022',
      startDate: '2022-01-01',
      endDate: '2022-12-31',
      status: 'مقفول'
    }
  ])

  const [openDialog, setOpenDialog] = useState(false)
  const [editingRecord, setEditingRecord] = useState(null)
  const [formData, setFormData] = useState({
    code: '',
    year: '',
    startDate: '',
    endDate: '',
    status: ''
  })

  const handleOpenDialog = (record = null) => {
    if (record) {
      setEditingRecord(record)
      setFormData(record)
    } else {
      setEditingRecord(null)
      setFormData({
        code: '',
        year: '',
        startDate: '',
        endDate: '',
        status: ''
      })
    }
    setOpenDialog(true)
  }

  const handleCloseDialog = () => {
    setOpenDialog(false)
    setEditingRecord(null)
  }

  const handleSave = () => {
    if (editingRecord) {
      setRecords(records.map(r => r.id === editingRecord.id ? { ...formData, id: editingRecord.id } : r))
    } else {
      setRecords([...records, { ...formData, id: Date.now() }])
    }
    handleCloseDialog()
  }

  const handleInputChange = (field, value) => {
    setFormData({ ...formData, [field]: value })
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
          columns={[
            { label: 'كود السجل المالي', field: 'code', width: '12%' },
            { label: 'السنة المالية', field: 'year', width: '12%' },
            { label: 'تاريخ بداية السنة المالية', field: 'startDate', width: '22%' },
            { label: 'تاريخ نهاية السنة المالية', field: 'endDate', width: '22%' },
            { label: 'مفتوح أو مقفول', field: 'status', width: '12%' }
          ]}
          data={records}
          onEdit={handleOpenDialog}
          headerColor="#003366"
          extraActions={[{ label: 'التفاصيل', onClick: (row) => navigate(`/settings/financial/${row.id}`), color: '#0066cc' }]}
          maxWidth="1100px"
        />
      </Container>

      {/* Dialog للإضافة والتعديل */}
      <Dialog open={openDialog} onClose={handleCloseDialog} maxWidth="sm" fullWidth>
        <DialogTitle>
          {editingRecord ? 'تعديل السجل المالي' : 'إضافة سجل مالي جديد'}
        </DialogTitle>
        <DialogContent sx={{ pt: 3, display: 'flex', flexDirection: 'column', gap: 2 }}>
          <TextField
            label="كود السجل المالي"
            value={formData.code}
            onChange={(e) => handleInputChange('code', e.target.value)}
            fullWidth
            dir="rtl"
          />
          <TextField
            label="السنة المالية"
            value={formData.year}
            onChange={(e) => handleInputChange('year', e.target.value)}
            fullWidth
            dir="rtl"
          />
          <TextField
            label="تاريخ بداية السنة المالية"
            type="date"
            value={formData.startDate}
            onChange={(e) => handleInputChange('startDate', e.target.value)}
            fullWidth
            dir="rtl"
            InputLabelProps={{ shrink: true }}
          />
          <TextField
            label="تاريخ نهاية السنة المالية"
            type="date"
            value={formData.endDate}
            onChange={(e) => handleInputChange('endDate', e.target.value)}
            fullWidth
            dir="rtl"
            InputLabelProps={{ shrink: true }}
          />
          <TextField
            label="الحالة"
            value={formData.status}
            onChange={(e) => handleInputChange('status', e.target.value)}
            fullWidth
            dir="rtl"
            select
            SelectProps={{ native: true }}
          >
            <option value=""></option>
            <option value="مفتوح">مفتوح</option>
            <option value="مقفول">مقفول</option>
          </TextField>
        </DialogContent>
        <DialogActions sx={{ p: 2 }}>
          <Button onClick={handleCloseDialog}>إلغاء</Button>
          <Button onClick={handleSave} variant="contained" color="primary">
            {editingRecord ? 'تحديث' : 'إضافة'}
          </Button>
        </DialogActions>
      </Dialog>
    </main>
  )
}

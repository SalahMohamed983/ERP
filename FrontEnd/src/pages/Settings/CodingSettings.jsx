import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import {
  Container,
  Box,
  Typography,
  Button,
  CircularProgress,
  Alert,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  FormControlLabel,
  Switch,
  Checkbox
} from '@mui/material'
import ReusableTable from '../../Components/Common/ReusableTable'
import {
  getTreasuries,
  createTreasury,
  updateTreasury,
  deleteTreasury,
  deleteTreasuries,
  clearTreasuriesError
} from '../../featured/GeneralSettings/treasurySlice'

function formatDate(value) {
  if (!value) return '—'
  try {
    const d = new Date(value)
    return isNaN(d.getTime()) ? value : d.toLocaleString('ar-EG')
  } catch {
    return value
  }
}

const emptyForm = {
  name: '',
  isMaster: false,
  active: true,
  lastIsalExhcange: 0,
  lastIsalCollect: 0,
}

export default function CodingSettings() {
  const dispatch = useDispatch()
  const { items, loading, error } = useSelector((state) => state.treasuries)
  const treasuries = items || []

  const [openDialog, setOpenDialog] = useState(false)
  const [editData, setEditData] = useState(emptyForm)
  const [formError, setFormError] = useState('')
  const [selectedIds, setSelectedIds] = useState(new Set())
  const [deleteConfirm, setDeleteConfirm] = useState(null)

  useEffect(() => {
    dispatch(getTreasuries())
  }, [dispatch])

  const hasOtherMaster = (currentId) =>
    treasuries.some((t) => t.isMaster && t.id !== currentId)

  const handleOpenAdd = () => {
    setEditData(emptyForm)
    setFormError('')
    setOpenDialog(true)
  }

  const handleOpenEdit = (row) => {
    const t = treasuries.find((x) => x.id === row.id)
    if (!t) return
    setEditData({
      id: t.id,
      name: t.name || '',
      isMaster: !!t.isMaster,
      active: t.active !== false,
      lastIsalExhcange: Number(t.lastIsalExhcange) ?? 0,
      lastIsalCollect: Number(t.lastIsalCollect) ?? 0,
    })
    setFormError('')
    setOpenDialog(true)
  }

  const handleCloseDialog = () => {
    setOpenDialog(false)
    setFormError('')
  }

  const handleSave = async () => {
    setFormError('')
    if (!editData.name?.trim()) {
      setFormError('اسم الخزنة مطلوب')
      return
    }
    if (editData.isMaster && hasOtherMaster(editData.id)) {
      setFormError('يوجد بالفعل خزنة رئيسية واحدة. لا يمكن وجود أكثر من خزنة رئيسية.')
      return
    }
    if (editData.id) {
      const result = await dispatch(updateTreasury(editData))
      if (updateTreasury.fulfilled.match(result)) handleCloseDialog()
    } else {
      const result = await dispatch(createTreasury(editData))
      if (createTreasury.fulfilled.match(result)) {
        handleCloseDialog()
        dispatch(getTreasuries())
      }
    }
  }

  const handleDelete = (row) => setDeleteConfirm(row.id)
  const handleDeleteConfirm = async () => {
    if (!deleteConfirm) return
    await dispatch(deleteTreasury(deleteConfirm))
    setDeleteConfirm(null)
  }

  const handleSelectAll = (e) => {
    if (e.target.checked) setSelectedIds(new Set(treasuries.map((t) => t.id)))
    else setSelectedIds(new Set())
  }
  const handleSelectOne = (id) => {
    const next = new Set(selectedIds)
    if (next.has(id)) next.delete(id)
    else next.add(id)
    setSelectedIds(next)
  }

  const handleDeleteSelected = async () => {
    if (selectedIds.size === 0) return
    await dispatch(deleteTreasuries([...selectedIds]))
    setSelectedIds(new Set())
  }

  const isAllSelected = treasuries.length > 0 && selectedIds.size === treasuries.length
  const isSomeSelected = selectedIds.size > 0 && selectedIds.size < treasuries.length

  return (
    <main className="flex-1 p-4">
      <Container maxWidth="xl">
        {error && (
          <Box sx={{ mb: 2 }}>
            <Alert severity="error" onClose={() => dispatch(clearTreasuriesError())}>
              {typeof error === 'string' ? error : 'حدث خطأ'}
            </Alert>
          </Box>
        )}

        <Box sx={{ mb: 3, p: 2, border: '1px solid #eee', borderRadius: 1 }}>
          <Typography variant="h5" sx={{ textAlign: 'center' }}>
            الخزن
          </Typography>
        </Box>

        <Box sx={{ mb: 3, display: 'flex', flexWrap: 'wrap', gap: 2, justifyContent: 'center' }}>
          <Button variant="contained" color="primary" sx={{ px: 3 }} onClick={handleOpenAdd}>
            إضافة جديد
          </Button>
          {selectedIds.size > 0 && (
            <Button variant="contained" color="error" onClick={handleDeleteSelected} disabled={loading}>
              حذف المحدد ({selectedIds.size})
            </Button>
          )}
        </Box>

        {loading && treasuries.length === 0 ? (
          <Box sx={{ display: 'flex', justifyContent: 'center', py: 4 }}>
            <CircularProgress />
          </Box>
        ) : (
          <ReusableTable
            columns={[
              {
                label: (
                  <Checkbox
                    checked={isAllSelected}
                    indeterminate={isSomeSelected}
                    onChange={handleSelectAll}
                    sx={{ color: 'white', '&.Mui-checked': { color: 'white' } }}
                  />
                ),
                field: 'select',
                width: '5%'
              },
              { label: 'المسلسل', field: 'id', width: '6%' },
              { label: 'اسم الخزنة', field: 'name', width: '14%' },
              { label: 'خزنة رئيسية ؟', field: 'main', width: '10%' },
              { label: 'آخر إيصال صرف', field: 'lastExchange', width: '10%' },
              { label: 'آخر إيصال تحصيل', field: 'lastCollect', width: '10%' },
              { label: 'تاريخ الإضافة', field: 'added', width: '12%' },
              { label: 'تاريخ التحديث', field: 'updated', width: '12%' },
              { label: 'حالة التفعيل', field: 'active', width: '7%' },
              { label: 'الخزن التى تستطيع الاستلام منها', field: 'receiveFrom', width: '10%' }
            ]}
            data={treasuries.map((t) => ({
              id: t.id,
              name: t.name,
              main: t.isMaster ? 'رئيسية' : 'فرعية',
              lastExchange: t.lastIsalExhcange ?? '—',
              lastCollect: t.lastIsalCollect ?? '—',
              added: formatDate(t.createdAt || t.date),
              updated: formatDate(t.updatedAt),
              active: t.active ? 'مفعل' : 'غير مفعل',
              select: (
                <Checkbox
                  checked={selectedIds.has(t.id)}
                  onChange={() => handleSelectOne(t.id)}
                />
              ),
              receiveFrom: (
                <Button size="small" variant="contained" sx={{ backgroundColor: '#1976d2', color: 'white' }}>
                  عرض الخزن
                </Button>
              )
            }))}
            onEdit={handleOpenEdit}
            onDelete={handleDelete}
            headerColor="#0a3a66"
            maxWidth="1300px"
          />
        )}
      </Container>

      {/* نافذة إضافة / تعديل - جميع حقول الجدول */}
      <Dialog open={openDialog} onClose={handleCloseDialog} maxWidth="sm" fullWidth dir="rtl">
        <DialogTitle sx={{ textAlign: 'center' }}>
          {editData.id ? 'تعديل الخزنة' : 'إضافة خزنة جديدة'}
        </DialogTitle>
        <DialogContent>
          {formError && (
            <Alert severity="error" sx={{ mb: 2 }} onClose={() => setFormError('')}>
              {formError}
            </Alert>
          )}
          <Box sx={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 2, my: 2 }}>
            <TextField
              label="اسم الخزنة"
              value={editData.name}
              onChange={(e) => setEditData({ ...editData, name: e.target.value })}
              fullWidth
              size="small"
              dir="rtl"
              required
            />
            <TextField
              label="آخر إيصال صرف"
              type="number"
              value={editData.lastIsalExhcange ?? ''}
              onChange={(e) => setEditData({ ...editData, lastIsalExhcange: parseInt(e.target.value, 10) || 0 })}
              fullWidth
              size="small"
              dir="rtl"
              inputProps={{ min: 0 }}
            />
            <TextField
              label="آخر إيصال تحصيل"
              type="number"
              value={editData.lastIsalCollect ?? ''}
              onChange={(e) => setEditData({ ...editData, lastIsalCollect: parseInt(e.target.value, 10) || 0 })}
              fullWidth
              size="small"
              dir="rtl"
              inputProps={{ min: 0 }}
            />
              </Box>
          <FormControlLabel
            control={
              <Switch
                checked={!!editData.isMaster}
                onChange={(e) => setEditData({ ...editData, isMaster: e.target.checked })}
              />
            }
            label="خزنة رئيسية (يُسمح بخزنة رئيسية واحدة فقط)"
            sx={{ mb: 2, display: 'block' }}
          />
          <FormControlLabel
            control={
              <Switch
                checked={editData.active !== false}
                onChange={(e) => setEditData({ ...editData, active: e.target.checked })}
              />
            }
            label="مفعّلة"
          />
        </DialogContent>
        <DialogActions sx={{ justifyContent: 'center', gap: 1 }}>
          <Button onClick={handleCloseDialog} color="inherit">
            إلغاء
          </Button>
          <Button onClick={handleSave} variant="contained" color="primary" disabled={loading}>
            {loading ? 'جاري الحفظ...' : editData.id ? 'تحديث' : 'إضافة'}
          </Button>
        </DialogActions>
      </Dialog>

      {/* تأكيد حذف واحدة */}
      <Dialog open={!!deleteConfirm} onClose={() => setDeleteConfirm(null)}>
        <DialogTitle>تأكيد الحذف</DialogTitle>
        <DialogContent>هل تريد حذف هذه الخزنة؟</DialogContent>
        <DialogActions>
          <Button onClick={() => setDeleteConfirm(null)}>إلغاء</Button>
          <Button onClick={handleDeleteConfirm} color="error" variant="contained" disabled={loading}>
            حذف
          </Button>
        </DialogActions>
      </Dialog>
    </main>
  )
}

import React, { useState, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import {
  Box,
  Container,
  Typography,
  TextField,
  Paper,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  FormControlLabel,
  Switch,
  CircularProgress,
  Alert
} from '@mui/material'
import ImageComponent from '../../Components/Common/ImageComponent'
import ReusableTable from '../../Components/Common/ReusableTable'
import {
  getAdminPanelSetting,
  updateAdminPanelSetting,
  createAdminPanelSetting,
  uploadAdminPanelPhoto,
  clearAdminPanelSettingError
} from '../../featured/GeneralSettings/adminPanelSettingSlice'
import { getApiOrigin } from '../../featured/axiosInstance'

const DEFAULT_ID = 1

/** بناء رابط عرض الصورة: إذا كان مساراً نسبياً نضيف أصل الـ API */
function getPhotoUrl(photo) {
  if (!photo) return ''
  if (photo.startsWith('http') || photo.startsWith('data:')) return photo
  const origin = getApiOrigin()
  return origin + (photo.startsWith('/') ? photo : '/' + photo)
}

const getDefaultDto = () => ({
  id: 0,
  systemName: '',
  photo: '',
  active: true,
  generalAlert: '',
  address: '',
  phone: '',
  customerParentAccountNumber: 0,
  suppliersParentAccountNumber: 0,
  delegateParentAccountNumber: 0,
  employeesParentAccountNumber: 0,
  productionLinesParentAccount: 0,
  comCode: 0,
  notes: '',
  isSetBatchesSetting: false,
  batchesSettingType: 0,
  defaultUnit: 0
})

function formatDate(value) {
  if (!value) return '—'
  try {
    const d = new Date(value)
    return isNaN(d.getTime()) ? value : d.toLocaleDateString('ar-EG')
  } catch {
    return value
  }
}

export default function GeneralSettings() {
  const dispatch = useDispatch()
  const { item, loading, error } = useSelector((state) => state.adminPanelSetting)

  const [openDialog, setOpenDialog] = useState(false)
  const [editData, setEditData] = useState(getDefaultDto())
  const [uploadingPhoto, setUploadingPhoto] = useState(false)
  const [uploadPhotoError, setUploadPhotoError] = useState('')

  useEffect(() => {
    dispatch(clearAdminPanelSettingError())
    dispatch(getAdminPanelSetting(DEFAULT_ID))
  }, [dispatch])

  useEffect(() => {
    if (item) setEditData({ ...getDefaultDto(), ...item })
  }, [item])

  const tableData = item
    ? [
        { label: 'اسم النظام / الشركة', value: item.systemName || '—' },
        { label: 'العنوان', value: item.address || '—' },
        { label: 'الهاتف', value: item.phone || '—' },
        { label: 'تنبيه عام', value: item.generalAlert || '—' },
        { label: 'ملاحظات', value: item.notes || '—' },
        { label: 'آخر تحديث', value: formatDate(item.updatedAt) }
      ]
    : []

  const handleOpenDialog = () => {
    setEditData(item ? { ...getDefaultDto(), ...item } : getDefaultDto())
    setUploadPhotoError('')
    setOpenDialog(true)
  }

  const handleCloseDialog = () => {
    setOpenDialog(false)
  }

  const handleInputChange = (field, value) => {
    setEditData((prev) => ({ ...prev, [field]: value }))
  }

  const handleSave = async () => {
    const dto = {
      ...editData,
      systemName: editData.systemName || '',
      photo: editData.photo || '',
      address: editData.address || '',
      phone: editData.phone || null,
      generalAlert: editData.generalAlert || null,
      notes: editData.notes || null
    }
    let result
    if (dto.id) {
      result = await dispatch(updateAdminPanelSetting(dto))
      if (updateAdminPanelSetting.fulfilled.match(result)) {
        handleCloseDialog()
      }
    } else {
      result = await dispatch(createAdminPanelSetting(dto))
      if (createAdminPanelSetting.fulfilled.match(result)) {
        handleCloseDialog()
        dispatch(getAdminPanelSetting(DEFAULT_ID))
      }
    }
  }

  const handleImageUpload = async (e) => {
    const file = e.target.files[0]
    if (!file) return
    setUploadPhotoError('')
    setUploadingPhoto(true)
    const result = await dispatch(uploadAdminPanelPhoto(file))
    setUploadingPhoto(false)
    if (uploadAdminPanelPhoto.fulfilled.match(result)) {
      handleInputChange('photo', result.payload)
    } else {
      setUploadPhotoError(typeof result.payload === 'string' ? result.payload : 'فشل رفع الصورة')
    }
    e.target.value = ''
  }

  const displayError = error && typeof error === 'string' ? error : error ? 'حدث خطأ' : ''

  return (
    <main className="flex-1 p-4">
      <Container maxWidth="lg">
        <Box sx={{ maxWidth: '1000px', mx: 'auto' }}>
          {displayError && (
            <Alert severity="error" sx={{ mb: 2 }} onClose={() => dispatch(clearAdminPanelSettingError())}>
              {displayError}
            </Alert>
          )}

          {loading && !item ? (
            <Box sx={{ display: 'flex', justifyContent: 'center', py: 4 }}>
              <CircularProgress />
            </Box>
          ) : (
            <Paper elevation={2} sx={{ overflow: 'hidden' }}>
              <ImageComponent
                src={getPhotoUrl(item?.photo)}
                alt="شعار الشركة"
                height="300px"
                width="100%"
                borderRadius="0"
                shadow={false}
              />

              <ReusableTable
                columns={[
                  { label: '', field: 'label', width: '50%' },
                  { label: '', field: 'value', width: '50%' }
                ]}
                data={tableData}
                hideHeader
                showActions={false}
                maxWidth="1000px"
              />

              <Box sx={{ p: 3, textAlign: 'center', backgroundColor: '#f9f9f9' }}>
                <Button
                  variant="contained"
                  color="primary"
                  onClick={handleOpenDialog}
                  disabled={loading}
                  sx={{ px: 4 }}
                >
                  {item ? 'تحديث البيانات' : 'إضافة إعدادات'}
                </Button>
              </Box>
            </Paper>
          )}
        </Box>
      </Container>

      <Dialog open={openDialog} onClose={handleCloseDialog} maxWidth="md" fullWidth>
        <DialogTitle sx={{ textAlign: 'center', fontWeight: 'bold', py: 3 }}>
          {item?.id ? 'تحديث بيانات الضبط العام للنظام' : 'إضافة إعدادات اللوحة'}
        </DialogTitle>

        <DialogContent dir="rtl" sx={{ pt: 0 }}>
          <Box sx={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 2, my: 3 }}>
            <TextField
              label="اسم النظام / الشركة"
              value={editData.systemName ?? ''}
              onChange={(e) => handleInputChange('systemName', e.target.value)}
              fullWidth
              dir="rtl"
              size="small"
            />
            <TextField
              label="العنوان"
              value={editData.address ?? ''}
              onChange={(e) => handleInputChange('address', e.target.value)}
              fullWidth
              dir="rtl"
              multiline
              rows={2}
              size="small"
            />
          </Box>

          <Box sx={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 2, mb: 3 }}>
            <TextField
              label="رقم الهاتف"
              value={editData.phone ?? ''}
              onChange={(e) => handleInputChange('phone', e.target.value)}
              fullWidth
              dir="rtl"
              size="small"
            />
            <TextField
              label="تنبيه عام"
              value={editData.generalAlert ?? ''}
              onChange={(e) => handleInputChange('generalAlert', e.target.value)}
              fullWidth
              dir="rtl"
              size="small"
            />
          </Box>

          <Box sx={{ mb: 3 }}>
            <TextField
              label="ملاحظات"
              value={editData.notes ?? ''}
              onChange={(e) => handleInputChange('notes', e.target.value)}
              fullWidth
              dir="rtl"
              multiline
              rows={2}
              size="small"
            />
          </Box>

          <FormControlLabel
            control={
              <Switch
                checked={!!editData.active}
                onChange={(e) => handleInputChange('active', e.target.checked)}
              />
            }
            label="مفعّل"
            sx={{ mb: 2 }}
          />

          <Box
            sx={{
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
              gap: 2,
              mb: 3,
              p: 2,
              backgroundColor: '#f5f5f5',
              borderRadius: 1
            }}
          >
            <Typography variant="subtitle2" sx={{ fontWeight: 'bold' }}>
              الشعار
            </Typography>
            <ImageComponent
              src={getPhotoUrl(editData.photo)}
              alt="شعار الشركة"
              height="200px"
              width="200px"
              shadow
            />
            {uploadPhotoError && (
              <Alert severity="error" sx={{ mt: 1 }} onClose={() => setUploadPhotoError('')}>
                {uploadPhotoError}
              </Alert>
            )}
            <Button
              variant="contained"
              color="primary"
              component="label"
              sx={{ mt: 1 }}
              disabled={uploadingPhoto}
            >
              {uploadingPhoto ? 'جاري رفع الصورة...' : 'رفع صورة جديدة'}
              <input type="file" accept="image/*" hidden onChange={handleImageUpload} />
            </Button>
          </Box>
        </DialogContent>

        <DialogActions sx={{ p: 2, justifyContent: 'center', gap: 2 }}>
          <Button
            onClick={handleCloseDialog}
            variant="contained"
            sx={{
              backgroundColor: '#ef5350',
              color: 'white',
              '&:hover': { backgroundColor: '#e53935' }
            }}
          >
            إلغاء
          </Button>
          <Button onClick={handleSave} variant="contained" color="primary" disabled={loading}>
            {loading ? 'جاري الحفظ...' : item?.id ? 'تحديث' : 'إنشاء'}
          </Button>
        </DialogActions>
      </Dialog>
    </main>
  )
}

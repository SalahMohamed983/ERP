import React from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { Container, Box, Button, Typography } from '@mui/material'
import ArrowBackIcon from '@mui/icons-material/ArrowBack'
import ReusableTable from '../../Components/Common/ReusableTable'

export default function FinancialRecordDetails() {
  const navigate = useNavigate()
  const { id } = useParams()

  const monthsData = [
    { code: 1, name: 'January', startDate: '2022-01-01', endDate: '2022-01-31', status: 'مقفول' },
    { code: 2, name: 'February', startDate: '2022-02-01', endDate: '2022-02-28', status: 'مقفول' },
    { code: 3, name: 'March', startDate: '2022-03-01', endDate: '2022-03-31', status: 'مقفول' },
    { code: 4, name: 'April', startDate: '2022-04-01', endDate: '2022-04-30', status: 'مقفول' },
    { code: 5, name: 'May', startDate: '2022-05-01', endDate: '2022-05-31', status: 'مقفول' },
    { code: 6, name: 'June', startDate: '2022-06-01', endDate: '2022-06-30', status: 'مقفول' },
    { code: 7, name: 'July', startDate: '2022-07-01', endDate: '2022-07-31', status: 'مقفول' },
    { code: 8, name: 'August', startDate: '2022-08-01', endDate: '2022-08-31', status: 'مقفول' },
    { code: 9, name: 'September', startDate: '2022-09-01', endDate: '2022-09-30', status: 'مقفول' },
    { code: 10, name: 'October', startDate: '2022-10-01', endDate: '2022-10-31', status: 'مقفول' },
    { code: 11, name: 'November', startDate: '2022-11-01', endDate: '2022-11-30', status: 'مقفول' },
    { code: 12, name: 'December', startDate: '2022-12-01', endDate: '2022-12-31', status: 'مقفول' }
  ]

  return (
    <main className="flex-1 p-4">
      <Container maxWidth="lg">
        <Box sx={{ mb: 3, display: 'flex', justifyContent: 'center' }}>
          <Button
            variant="contained"
            color="primary"
            startIcon={<ArrowBackIcon />}
            onClick={() => navigate('/settings/financial')}
            sx={{ px: 3 }}
          >
            رجوع
          </Button>
        </Box>

        <Box sx={{ mb: 3, p: 2, border: '1px solid #ddd', borderRadius: 1 }}>
          <Typography variant="h6" sx={{ textAlign: 'center', fontWeight: 'bold' }}>
            أخير السنة المالية 1
          </Typography>
        </Box>

        <ReusableTable
          columns={[
            { label: 'كود الشهر', field: 'code', width: '10%' },
            { label: 'اسم الشهر', field: 'name', width: '30%' },
            { label: 'تاريخ البداية', field: 'startDate', width: '20%' },
            { label: 'تاريخ النهاية', field: 'endDate', width: '20%' },
            { label: 'حالة الشهر', field: 'status', width: '20%' }
          ]}
          data={monthsData}
          showActions={false}
          headerColor="#003366"
          maxWidth="1000px"
        />
      </Container>
    </main>
  )
}

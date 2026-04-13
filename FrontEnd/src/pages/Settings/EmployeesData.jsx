import React, { useState } from 'react'
import { Container, Box, Typography, Button } from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import ReusableTable from '../../Components/Common/ReusableTable'

export default function EmployeesData() {
  // sample state -- would normally come from an API or Redux slice
  const [employees, setEmployees] = useState([
    {
      id: 1,
      code: '1',
      name: 'محمد كمال',
      jobGrade: 'المحاسبين',
      accountNumber: '1',
      currentBalance: '0',
      phone: '0123456789',
      active: 'مفعل'
    },
    {
      id: 2,
      code: '2',
      name: 'عاطف',
      jobGrade: 'المحاسبين',
      accountNumber: '56',
      currentBalance: '0',
      phone: '0987654321',
      active: 'مفعل'
    }
  ])

  const columns = [
    { label: 'كود الموظف', field: 'code', width: '10%' },
    { label: 'الاسم', field: 'name', width: '20%' },
    { label: 'درجة الوظيفة', field: 'jobGrade', width: '20%' },
    { label: 'رقم الحساب المالي', field: 'accountNumber', width: '15%' },
    { label: 'الرصيد الحالي', field: 'currentBalance', width: '10%' },
    { label: 'الهاتف', field: 'phone', width: '15%' },
    { label: 'التفعيل', field: 'active', width: '10%' }
  ]

  const handleAdd = () => {
    // placeholder for opening a dialog/form
    const newEmp = {
      id: Date.now(),
      code: '',
      name: '',
      jobGrade: '',
      accountNumber: '',
      currentBalance: '',
      phone: '',
      active: 'مفعل'
    }
    setEmployees([...employees, newEmp])
  }

  const handleEdit = (row) => {
    // open edit form, for now we'll just log
    console.log('edit', row)
  }

  const handleDelete = (row) => {
    setEmployees(employees.filter((e) => e.id !== row.id))
  }

  return (
    <main className="flex-1 p-4">
      <Container maxWidth="lg">
        <Typography variant="h4" gutterBottom>
          بيانات الموظفين
        </Typography>

        <Box sx={{ mb: 3, display: 'flex', justifyContent: 'center' }}>
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

        <ReusableTable
          columns={columns}
          data={employees}
          onEdit={handleEdit}
          onDelete={handleDelete}
          headerColor="#003366"
          maxWidth="100%"
        />
      </Container>
    </main>
  )
}

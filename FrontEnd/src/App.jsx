import './App.css'
import React, { useState, Suspense } from 'react'
import { BrowserRouter as Router, Routes, Route, useNavigate } from 'react-router-dom'
import { CircularProgress, Box } from '@mui/material'
import ProtectedRoute from './Components/ProtectedRoute'
import Navbar from './Components/Layout/Navbar'
import Sidebar from './Components/Layout/Sidebar'
import Dashboard from './pages/Dashboard'
import GeneralSettings from './pages/Settings/GeneralSettings'
import FinancialRecords from './pages/Settings/FinancialRecords'
import FinancialRecordDetails from './pages/Settings/FinancialRecordDetails'
import CodingSettings from './pages/Settings/CodingSettings'
import ManufacturingLines from './pages/Settings/ManufacturingLines'
import JobGrades from './pages/Settings/JobGrades'
import EmployeesData from './pages/Settings/EmployeesData'
import AttendanceRecords from './pages/Settings/AttendanceRecords'
import SalaryTypes from './pages/Settings/SalaryTypes'
import SalaryDeductions from './pages/Settings/SalaryDeductions'
import Governorates from './pages/Settings/Governorates'
import Centers from './pages/Settings/Centers'
import InvoiceCategories from './pages/Settings/InvoiceCategories'
import SettingsServices from './pages/Settings/SettingsServices'
import SupplierCategories from './pages/Inventory/SupplierCategories'
import Companies from './pages/Inventory/Companies'
import Representatives from './pages/Inventory/Representatives'
import Suppliers from './pages/Inventory/Suppliers'
import Warehouses from './pages/Inventory/Warehouses'
import TransferStatuses from './pages/Inventory/TransferStatuses'
import Units from './pages/Inventory/Units'
import ItemCategories from './pages/Inventory/ItemCategories'
import Items from './pages/Inventory/Items'
import NotFound from './pages/NotFound'
import Login from './pages/Auth/Login'
import ForgotPassword from './pages/Auth/ForgotPassword'
import ResetPassword from './pages/Auth/ResetPassword'
import Profile from './pages/Auth/Profile'

function AppLayout({ collapsed, onToggleSidebar, children }) {
  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Navbar onToggleSidebar={onToggleSidebar} />
      <div className="flex flex-1 flex-row-reverse">
        <Sidebar collapsed={collapsed} />
        {children}
      </div>
    </div>
  )
}

function AppContent() {
  const [collapsed, setCollapsed] = useState(false)

  return (
    <AppLayout collapsed={collapsed} onToggleSidebar={() => setCollapsed(v => !v)}>
      <Suspense fallback={
        <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
          <CircularProgress />
        </Box>
      }>
        <Routes>
        <Route path="/" element={<Dashboard />} />
          <Route path="/settings/general" element={<GeneralSettings />} />
          <Route path="/settings/financial" element={<FinancialRecords />} />
          <Route path="/settings/financial/:id" element={<FinancialRecordDetails />} />
          <Route path="/settings/coding" element={<CodingSettings />} />
          <Route path="/settings/manufacturing" element={<ManufacturingLines />} />
          <Route path="/settings/job-grades" element={<JobGrades />} />
          <Route path="/settings/employees" element={<EmployeesData />} />
          <Route path="/settings/attendance" element={<AttendanceRecords />} />
          <Route path="/settings/salary-types" element={<SalaryTypes />} />
          <Route path="/settings/deductions" element={<SalaryDeductions />} />
          <Route path="/settings/governorates" element={<Governorates />} />
          <Route path="/settings/centers" element={<Centers />} />
          <Route path="/settings/invoice-categories" element={<InvoiceCategories />} />
          <Route path="/settings/services" element={<SettingsServices />} />
          <Route path="/inventory/supplier-categories" element={<SupplierCategories />} />
          <Route path="/inventory/companies" element={<Companies />} />
          <Route path="/inventory/representatives" element={<Representatives />} />
          <Route path="/inventory/suppliers" element={<Suppliers />} />
          <Route path="/inventory/warehouses" element={<Warehouses />} />
          <Route path="/inventory/transfer-statuses" element={<TransferStatuses />} />
          <Route path="/inventory/units" element={<Units />} />
          <Route path="/inventory/item-categories" element={<ItemCategories />} />
          <Route path="/inventory/items" element={<Items />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Suspense>
    </AppLayout>
  )
}

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/forgot-password" element={<ForgotPassword />} />
        <Route path="/reset-password" element={<ResetPassword />} />
        <Route 
          path="/*" 
          element={
            <ProtectedRoute>
              <AppContent />
            </ProtectedRoute>
          } 
        />
      </Routes>
    </Router>
  )
}


export default App

import React, { useState } from 'react'
import { useNavigate, useLocation, Link } from 'react-router-dom'
import Avatar from '@mui/material/Avatar'
import ExpandMoreIcon from '@mui/icons-material/ExpandMore'
import { Collapse } from '@mui/material'

const menuItems = [
  {
    label: 'الضبط العام',
    path: '/settings',
    submenu: [
      { label: 'الضبط العام', path: '/settings/general' },
      { label: 'السجلات المالية', path: '/settings/financial' },
      { label: 'تكويد الخزن', path: '/settings/coding' },
      { label: 'خطوط التصنيع', path: '/settings/manufacturing' },
      { label: 'درجات الوظائف', path: '/settings/job-grades' },
      { label: 'بيانات الموظفين', path: '/settings/employees' },
      { label: 'سجل حضور الموظفين', path: '/settings/attendance' },
      { label: 'أنواع الاشتغال علي المرتب', path: '/settings/salary-types' },
      { label: 'أنواع الخصم علي المرتب', path: '/settings/deductions' },
      { label: 'المحافظات', path: '/settings/governorates' },
      { label: 'المراكز', path: '/settings/centers' },
      { label: 'فئات فواتير المبيعات', path: '/settings/invoice-categories' },
      { label: 'الخدمات الداخلية والخارجية', path: '/settings/services' }
    ]
  },
  { label: 'الحسابات والنقدية', path: '/accounts' },
  {
    label: 'ضبط المخازن',
    path: '/inventory',
    submenu: [
      { label: 'فئات الموردين', path: '/inventory/supplier-categories' },
      { label: 'الشركات', path: '/inventory/companies' },
      { label: 'المندوبين', path: '/inventory/representatives' },
      { label: 'الموردين', path: '/inventory/suppliers' },
      { label: 'المخازن', path: '/inventory/warehouses' },
      { label: 'حالات النقل', path: '/inventory/transfer-statuses' },
      { label: 'وحدات القياس', path: '/inventory/units' },
      { label: 'فئات الأصناف', path: '/inventory/item-categories' },
      { label: 'الأصناف', path: '/inventory/items' }
    ]
  },
  { label: 'حركة مخزنية', path: '/inventory/movements' },
  { label: 'حركة المبيعات', path: '/sales' },
  { label: 'خدمات داخلية وخارجية', path: '/services' },
  { label: 'حركة شفــت الخزينـة', path: '/treasury' },
  { label: 'الصلاحيات والمستخدمين', path: '/users' },
  { label: 'التقارير', path: '/reports' },
  { label: 'الدعم ومذكراتي', path: '/support' }
]

export default function Sidebar({ collapsed }) {
  const navigate = useNavigate()
  const location = useLocation()
  const [expandedMenu, setExpandedMenu] = useState(null)

  const handleNavigate = (path) => {
    navigate(path)
  }

  const handleMenuClick = (item) => {
    if (item.submenu) {
      setExpandedMenu(expandedMenu === item.path ? null : item.path)
    } else {
      handleNavigate(item.path)
    }
  }

  return (
    <aside
      dir="rtl"
      className={` bg-slate-800 text-slate-100 ${
        collapsed ? 'w-16' : 'w-72'
      } transition-width duration-200 hidden md:block overflow-y-auto`}
    >
      
               <Link to={"/profile"}>
      <div
        className="p-4 flex items-center gap-3 cursor-pointer hover:bg-slate-700 rounded transition"
      >
        <Avatar sx={{ width: 40, height: 40 }}>MA</Avatar>
        {!collapsed && (
          <div>
            <div className="text-sm">Main Admin</div>
            <div className="text-xs text-emerald-400">Online</div>
          </div>
        )}
        </div>
        </Link>

      <nav className="mt-4 px-2">
        {menuItems.map((item) => (
          <div key={item.path}>
            <div
              className="flex items-center justify-between px-3 py-2 rounded hover:bg-slate-700 cursor-pointer transition"
              onClick={() => handleMenuClick(item)}
            >
              <div className="flex items-center gap-2">
                <span className="w-2 h-2 bg-slate-400 rounded-full" />
                {!collapsed && <span>{item.label}</span>}
              </div>
              {!collapsed && item.submenu && (
                <ExpandMoreIcon
                  fontSize="small"
                  sx={{
                    transform:
                      expandedMenu === item.path ? 'rotate(180deg)' : 'rotate(0deg)',
                    transition: 'transform 0.3s'
                  }}
                />
              )}
            </div>

            {/* Submenu */}
            {item.submenu && !collapsed && (
              <Collapse in={expandedMenu === item.path} timeout="auto" unmountOnExit>
                <div className="pl-6 mt-1">
                  {item.submenu.map((subitem) => (
                    <div
                      key={subitem.path}
                      className={`px-3 py-2 rounded cursor-pointer transition text-sm ${
                        location.pathname === subitem.path
                          ? 'bg-slate-600 text-white'
                          : 'hover:bg-slate-700 text-slate-300'
                      }`}
                      onClick={() => handleNavigate(subitem.path)}
                    >
                      {subitem.label}
                    </div>
                  ))}
                </div>
              </Collapse>
            )}
          </div>
        ))}
      </nav>
    </aside>
  )
}

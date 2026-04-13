import React from 'react'
import Avatar from '@mui/material/Avatar'
import IconButton from '@mui/material/IconButton'
import MenuIcon from '@mui/icons-material/Menu'

export default function Navbar({onToggleSidebar}){
  return (
    <header className="w-full bg-sky-600 text-white">
      <div className="max-w-7xl mx-auto px-4 py-3 flex items-center justify-between">
        <div className="flex items-center gap-3">
          <IconButton onClick={onToggleSidebar} className="!text-white md:!hidden">
            <MenuIcon />
          </IconButton>
          <h1 className="text-lg font-semibold">ثلاجة طابيرن للمجمدات</h1>
        </div>
        <div className="flex items-center gap-4">
          <button className="bg-white/20 px-3 py-1 rounded">الرئيسية</button>
        </div>
      </div>
    </header>
  )
}

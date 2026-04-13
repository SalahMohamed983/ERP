import React from 'react'
import {
  Table,
  TableRow,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  Paper,
  IconButton,
  Box,
  Button
} from '@mui/material'
import EditIcon from '@mui/icons-material/Edit'
import { Button as MuiButton } from '@mui/material'

/**
 * ReusableTable Component
 * @param {Array} columns - Column config: [{label: string, field: string, width: string}]
 * @param {Array} data - Table data (array of objects)
 * @param {Function} onEdit - Edit button callback
 * @param {Function} onDelete - Delete button callback
 * @param {String} headerColor - Header background color (default: '#003366')
 * @param {Boolean} showActions - Show edit/delete buttons (default: true)
 * @param {Array} extraActions - Additional action buttons: [{ label, onClick(row), color }]
 */
export default function ReusableTable({
  columns = [],
  data = [],
  onEdit = null,
  onDelete = null,
  headerColor = '#003366',
  showActions = true,
  extraActions = [],
  hideHeader = false,
  maxWidth = '1100px'
}) {
  return (
    <TableContainer
      component={Paper}
      sx={{ maxWidth, mx: 'auto', boxShadow: 3, mt: 2, overflowX: 'auto' }}
    >
      <Table dir="rtl" sx={{ minWidth: 800, tableLayout: 'fixed' }}>
        {!hideHeader && (
          <TableHead>
            <TableRow>
              {columns.map((col) => (
                <TableCell
                  key={col.field}
                  sx={{
                    backgroundColor: headerColor,
                    color: 'white',
                    fontWeight: 'bold',
                    padding: '10px',
                    textAlign: 'right',
                    width: col.width || 'auto'
                  }}
                >
                  {col.label}
                </TableCell>
              ))}
              {showActions && (
                <TableCell
                  sx={{
                    backgroundColor: headerColor,
                    color: 'white',
                    fontWeight: 'bold',
                    padding: '10px',
                    textAlign: 'center',
                    width: '12%'
                  }}
                >
                  إجراءات
                </TableCell>
              )}
            </TableRow>
          </TableHead>
        )}
        <TableBody>
          {data.map((row, idx) => (
            <TableRow
              key={row.id || idx}
              sx={{
                backgroundColor: idx % 2 === 0 ? 'white' : '#f7f9fc',
                '&:hover': { backgroundColor: '#e8f4f8' }
              }}
            >
              {columns.map((col) => (
                <TableCell key={col.field} sx={{ padding: '10px', textAlign: 'right' }}>
                  {row[col.field]}
                </TableCell>
              ))}
              {showActions && (
                <TableCell sx={{ textAlign: 'center', padding: '10px' }}>
                  <Box sx={{ display: 'flex', gap: 1, justifyContent: 'center', alignItems: 'center' }}>
                    {extraActions && extraActions.map((act, i) => (
                      <MuiButton
                        key={i}
                        size="small"
                        variant="contained"
                        onClick={() => act.onClick(row)}
                        sx={{ backgroundColor: act.color || '#1976d2', color: 'white', padding: '4px 8px' }}
                      >
                        {act.label}
                      </MuiButton>
                    ))}
                    {onEdit && (
                      <IconButton
                        size="small"
                        onClick={() => onEdit(row)}
                        sx={{
                          backgroundColor: '#ff6b35',
                          color: 'white',
                          '&:hover': { backgroundColor: '#ff5722' }
                        }}
                      >
                        <EditIcon fontSize="small" />
                      </IconButton>
                    )}
                    {onDelete && (
                      <MuiButton
                        size="small"
                        onClick={() => onDelete(row)}
                        variant="contained"
                        sx={{
                          backgroundColor: '#d32f2f',
                          color: 'white',
                          padding: '4px 8px'
                        }}
                      >
                        حذف
                      </MuiButton>
                    )}
                  </Box>
                </TableCell>
              )}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  )
}

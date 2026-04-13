import React, { useState } from 'react'
import { CircularProgress, Box, Typography } from '@mui/material'

export default function ImageComponent({ 
  src, 
  alt = 'صورة', 
  height = '400px', 
  width = '100%',
  borderRadius = '8px',
  shadow = true,
  showLoader = true,
  className = ''
}) {
  const [isLoading, setIsLoading] = useState(true)
  const [hasError, setHasError] = useState(false)

  const handleImageLoad = () => {
    setIsLoading(false)
  }

  const handleImageError = () => {
    setIsLoading(false)
    setHasError(true)
  }

  return (
    <Box
      className={className}
      sx={{
        position: 'relative',
        width: width,
        height: height,
        borderRadius: borderRadius,
        overflow: 'hidden',
        backgroundColor: '#f5f5f5',
        boxShadow: shadow ? '0 2px 8px rgba(0, 0, 0, 0.1)' : 'none',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center'
      }}
    >
      {showLoader && isLoading && (
        <CircularProgress />
      )}

      {hasError ? (
        <Box
          sx={{
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            justifyContent: 'center',
            width: '100%',
            height: '100%',
            backgroundColor: '#f5f5f5'
          }}
        >
          <Typography color="error" variant="body2">
            فشل تحميل الصورة
          </Typography>
        </Box>
      ) : (
        <img
          src={src}
          alt={alt}
          onLoad={handleImageLoad}
          onError={handleImageError}
          style={{
            width: '100%',
            height: '100%',
            objectFit: 'cover',
            objectPosition: 'center',
            opacity: isLoading ? 0 : 1,
            transition: 'opacity 0.3s ease-in-out'
          }}
        />
      )}
    </Box>
  )
}

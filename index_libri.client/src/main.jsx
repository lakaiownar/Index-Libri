import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import Routing from './api/Routes.jsx'
import { AuthProvider } from './contexts/AuthContext'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <AuthProvider>
      <Routing />
    </AuthProvider>
  </React.StrictMode>,
)

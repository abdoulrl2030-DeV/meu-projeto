import React from 'react'
import Login from './components/Login'
import Home from './pages/Home'

export default function App() {
  const [token, setToken] = React.useState(localStorage.getItem('token'))

  return (
    <div style={{padding:20}}>
      {!token ? <Login onLogin={(t)=>{localStorage.setItem('token', t); setToken(t)}} /> : <Home token={token} onLogout={()=>{localStorage.removeItem('token'); setToken(null)}} />}
    </div>
  )
}

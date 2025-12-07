import React from 'react'
import axios from 'axios'

export default function Login({ onLogin }){
  const [username, setUsername] = React.useState('admin')
  const [password, setPassword] = React.useState('password')
  const [err, setErr] = React.useState(null)

  const submit = async (e)=>{
    e.preventDefault()
    try{
      const res = await axios.post(import.meta.env.VITE_API_URL + '/api/auth/login', { username, password })
      onLogin(res.data.token)
    }catch(e){
      setErr('Login failed')
    }
  }

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={submit}>
        <div>
          <label>Usuário</label><br />
          <input value={username} onChange={e=>setUsername(e.target.value)} />
        </div>
        <div>
          <label>Senha</label><br />
          <input type="password" value={password} onChange={e=>setPassword(e.target.value)} />
        </div>
        <button type="submit">Entrar</button>
      </form>
      {err && <p style={{color:'red'}}>{err}</p>}
    </div>
  )
}

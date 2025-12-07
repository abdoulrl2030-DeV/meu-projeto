import React from 'react'
import axios from 'axios'

export default function Home({ token, onLogout }){
  const [me, setMe] = React.useState(null)

  React.useEffect(()=>{
    const load = async ()=>{
      try{
        const res = await axios.get(import.meta.env.VITE_API_URL + '/api/users/me', { headers: { Authorization: 'Bearer ' + token } })
        setMe(res.data)
      }catch(e){
        console.error(e)
      }
    }
    load()
  }, [token])

  return (
    <div>
      <h2>Home</h2>
      {me ? <div>
        <p>Olá, {me.username} ({me.role})</p>
      </div> : <p>Carregando...</p>}
      <button onClick={onLogout}>Sair</button>
    </div>
  )
}

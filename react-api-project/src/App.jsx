import { useEffect, useState } from 'react'
import './App.css'
import axios from 'axios'
function App() {
  const [products,setProducts] = useState([])
  useEffect(()=>{
    axios.get('https://localhost:7137/api/Product/get-product')
    .then((res)=>{
      setProducts(res.data)
    })
    .catch((err)=>{
      console.log(err)
    })
  },[])
  return (
    <>
     <h4>Api Products App</h4>
     {
      products.map((product)=>{
       return <p>{product.title}</p>
      })
     }
    </>
  )
}

export default App

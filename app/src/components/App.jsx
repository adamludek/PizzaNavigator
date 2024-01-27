/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useEffect, useState } from 'react'
import axios from 'axios'
import 'bootstrap/dist/css/bootstrap.min.css'
import { Container } from 'react-bootstrap'
import { Col, Row } from 'react-bootstrap'
import { Login } from './Login'
import { RegModal } from './RegModal'
import { AddPizzeriaModal } from './AddPizzeriaModal'
import { SettingsModal } from './SettingsModal'
import { SearchBar } from './SearchBar'
import { PizzeriaList } from './PizzeriaList'

function App() {
  const [isLogged, setIsLogged] = useState(null)
  const [data, setData] = useState(null);
  
  const username = isLogged ? parseJwt(isLogged).Username : '';

  useEffect(getPosts, [])

  function getPosts(inputValue) {
  if (!inputValue) {
      axios.get('https://localhost:5021/api/Pizzeria').then((response) => {
      setData(response.data.$values)
    })
  } else {
    
      axios.get(`https://localhost:5021/api/Pizzeria/s?value=${inputValue}`).then((response) => {
      setData(response.data.$values)
    })   
  }
  }
  function handleLogin(token) {
    setIsLogged(token)
  }

  function parseJwt(token) {
    if (!token) {
      return;
    }
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace("-", "+").replace("_", "/");
    return JSON.parse(window.atob(base64));
  }

  return (
    <Container >
      <Row className="text-center">
        <Col >
      <h1>Pizza Navigator</h1>
      </Col>
      <Col className='align-self-center'>
      {!isLogged ? <Login setLogged={handleLogin}/> : <div>Hi, {username} <SettingsModal token={isLogged} setToken={setIsLogged}/> <button className='btn btn-secondary' onClick={() => setIsLogged(null)}>Logout</button></div> }
      </Col>
      </Row>
      { !isLogged && <Row>
        <Col>
        <RegModal/>
        </Col> 
        </Row> }
        <Row className='text-center'>
          <SearchBar getPosts={getPosts} />
        </Row>
        {isLogged && 
        <Row>
          <Col><AddPizzeriaModal getPosts={getPosts} token={isLogged} /></Col>
        </Row> }
        { data && <PizzeriaList data={data} setData={setData} getPosts={getPosts} token={isLogged} username={username}/>}
    </Container>
  )
}


export default App
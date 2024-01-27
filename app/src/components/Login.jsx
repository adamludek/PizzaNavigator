/* eslint-disable react/prop-types */
import { useState } from 'react';
import axios from 'axios';

export function Login({ setLogged }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  function loginHander(e) {
    e.preventDefault();
    axios.post("https://localhost:5021/api/User/login", { email: email, password: password }).then((response) => {
      setLogged(response.data);
      setEmail("");
      setPassword("");
    }, (error) => {
      console.log(error.response.data);
    });
  }
  return (
    <form onSubmit={loginHander}>
      <label>Email: </label>
      <input type='text' value={email} onChange={(e) => setEmail(e.target.value)} />
      <label>Password: </label>
      <input type='password' value={password} onChange={(e) => setPassword(e.target.value)} />
      <button className='btn btn-primary'>Login</button>
    </form>
  );
}

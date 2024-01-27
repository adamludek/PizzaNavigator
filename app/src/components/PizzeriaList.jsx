/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState } from 'react';
import axios from 'axios';
import { Pizzeria } from './Pizzeria';

export function PizzeriaList({ data, getPosts, username, token }) {
  const [vadErrors, setVadErrors] = useState("");

  function handleRemovePizzeria(id) {
    axios.delete(`https://localhost:5021/api/Pizzeria/${id}`, {
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    }).then((response) => {
      getPosts();
    }, (error) => {
      setVadErrors(error.response.data.errors);
    });
  }

  return (
    <ul className='mt-4 d-flex flex-wrap' style={{ 'listStyle': 'none' }}>
      {data && data.map(p => (
        <Pizzeria key={p.id} p={p} getPosts={getPosts} onRemove={handleRemovePizzeria} token={token} username={username} />
      ))}</ul>
  );
}

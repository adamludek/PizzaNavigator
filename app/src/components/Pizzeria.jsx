/* eslint-disable react/prop-types */
import { EditPizzeriaModal } from './EditPizzeriaModal';

export function Pizzeria({ p, getPosts, onRemove, username, token }) {

  return (
    <li className='w-25 mb-4'>
      <h3>{p.name}</h3>
      <p>{p.description}</p>
      <p>With delivery: {p.delivery ? 'Yes' : 'No'}</p>
      <p>Phone number: {p.phoneNumber}</p>
      <p>{`Address: ${p.street}, ${p.postalCode} ${p.city}`}</p>
      <p>{(!token || p.user !== username) && `Added by: ${p.user}`}</p>
      {p.user === username && <div><EditPizzeriaModal pizzeria={p} getPosts={getPosts} token={token} /> <button className='btn btn-danger' onClick={() => onRemove(p.id)}>Delete</button></div>}

    </li>
  );
}

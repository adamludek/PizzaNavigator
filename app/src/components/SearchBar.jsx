/* eslint-disable react/prop-types */
import { useState } from 'react';

export function SearchBar({ getPosts }) {
  const [inputValue, setInputValue] = useState("");

  function handleChange(e) {
    if (e.target.value === "") {
      getPosts();
    } else {
      getPosts(e.target.value);
      console.log("text");
    }
    setInputValue(e.target.value);
  }

  return (
    <div>
      <input type='text' placeholder="Search by name or city" value={inputValue} onChange={(e) => handleChange(e)} />
    </div>
  );
}

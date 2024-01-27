/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState } from 'react';
import axios from 'axios';
import { Modal } from 'react-bootstrap';
import { Col, Row } from 'react-bootstrap';

export function SettingsModal({ token, setToken }) {
  const [showModal, setShowModal] = useState(false)
  const [password, setPassword] = useState("")
  const [vadErrors, setVadErrors] = useState("")

  function handleSubmit(e) {
    e.preventDefault();
    axios.delete("https://localhost:5021/api/User", {
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }, data: { password: password }
    }).then((response) => {
      setShowModal((prev) => !prev)
      setToken(null)
      setPassword("")
      setVadErrors("")
    }, (error) => {
      setVadErrors(error.response.data.errors)
    })
  }

  return (
    <>
      <button className='btn btn-primary' onClick={() => setShowModal((prev) => !prev)}>Settings</button>
      {showModal && <Modal show={showModal} onHide={() => {
        setShowModal((prev) => !prev);
        setPassword("");
      }}>
        <Modal.Header closeButton>
          <Modal.Title>Settings</Modal.Title>
        </Modal.Header>
        <Modal.Body>
        {vadErrors && <p className='text-danger'>Some problems. Check form.</p>}
          <h4>Remove your account</h4>
          <form onSubmit={handleSubmit}>
            <Row>
              <Col>
                <label>Password: </label>
                <input type='password' value={password} onChange={(e) => setPassword(e.target.value)} />
              </Col>
              <Col>

              </Col>
            </Row>

            <Row className='text-center'>
              <Col>
                <button className='btn btn-danger'>Send</button>
              </Col>
            </Row>
          </form>
        </Modal.Body>
      </Modal>}

    </>
  );
}

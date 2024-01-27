/* eslint-disable no-unused-vars */
import { useState } from 'react';
import axios from 'axios';
import { Modal } from 'react-bootstrap';
import { Col, Row } from 'react-bootstrap';

export function RegModal() {
  const [showModal, setShowModal] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [username, setUsername] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [vadErrors, setVadErrors] = useState("");

  function handleClose() {
    setShowModal((prev) => !prev);
    setVadErrors("");
    setEmail("");
    setPassword("");
    setUsername("");
    setFirstName("");
    setLastName("");
  }
  function handleSubmit(e) {
    e.preventDefault();
    axios.post("https://localhost:5021/api/User/register", { email: email, password: password, firstName: firstName, lastName: lastName, username: username }).then((response) => {
      handleClose();
    }, (error) => {
      setVadErrors(error.response.data.errors);
    });
  }

  return (
    <>
      <button className='btn btn-primary' onClick={() => setShowModal((prev) => !prev)}>Register</button>
      {showModal && <Modal show={showModal} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Register</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {vadErrors && <p className='text-danger'>Some problems. Check form.</p>}
          <form onSubmit={handleSubmit}>
            <Row>
              <Col>
                <label>Email: </label>
                <input type='text' value={email} onChange={(e) => setEmail(e.target.value)} />
              </Col>
              <Col>
                <label>Password: </label>
                <input type='password' value={password} onChange={(e) => setPassword(e.target.value)} />
              </Col>
            </Row>
            <Row>
              <Col>
                <label>Username: </label>
                <input type='text' value={username} onChange={(e) => setUsername(e.target.value)} />
              </Col>
              <Col>
                <label>First name: </label>
                <input type='text' value={firstName} onChange={(e) => setFirstName(e.target.value)} />
              </Col>
            </Row>
            <Row>
              <Col>
                <label>Last name: </label>
                <input type='text' value={lastName} onChange={(e) => setLastName(e.target.value)} />
              </Col>
              <Col>

              </Col>
            </Row>
            <Row className='text-center'>
              <Col>
                <button className='btn btn-primary'>Send</button>
              </Col>
            </Row>
          </form>
        </Modal.Body>
      </Modal>}

    </>
  );
}

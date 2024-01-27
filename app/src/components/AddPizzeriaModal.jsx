/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
import { useState } from 'react';
import axios from 'axios';
import { Modal } from 'react-bootstrap';
import { Col, Row } from 'react-bootstrap';

export function AddPizzeriaModal({ getPosts, token }) {
  const [showModal, setShowModal] = useState(false);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [delivery, setDelivery] = useState(false);
  const [phoneNumber, setPhoneNumber] = useState("");
  const [street, setStreet] = useState("");
  const [postalCode, setPostalCode] = useState("");
  const [city, setCity] = useState("");
  const [vadErrors, setVadErrors] = useState("");

  function handleClose() {
    setShowModal((prev) => !prev);
    setName("");
    setDescription("");
    setDelivery(false);
    setPhoneNumber("");
    setStreet("");
    setPostalCode("");
    setCity("");
    setVadErrors("");
  }
  function handleSubmit(e) {
    e.preventDefault();
    axios.post("https://localhost:5021/api/Pizzeria", { name: name, description: description, delivery: delivery, phoneNumber: phoneNumber, street: street, postalCode: postalCode, city: city }, {
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    }).then((response) => {
      getPosts();
      handleClose();
    }, (error) => {
      setVadErrors(error.response.data.errors);
    });
  }

  return (
    <>
      <button className='btn btn-success' onClick={handleClose}>Add pizzeria</button>
      {showModal && <Modal show={showModal} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Add new pizzeria</Modal.Title>
          {vadErrors && console.log(vadErrors)}
        </Modal.Header>
        <Modal.Body>
        {vadErrors && <p className='text-danger'>Some problems. Check form.</p>}
          <form onSubmit={handleSubmit}>
            <Row>
              <Col>
                <label>Name: </label>
                <input type='text' value={name} onChange={(e) => setName(e.target.value)} />
              </Col>
              <Col>
                <label>Description: </label>
                <input type='text' value={description} onChange={(e) => setDescription(e.target.value)} />
              </Col>
            </Row>
            <Row>
              <Col>
                <label>Delivery: </label>
                <input type='checkbox' value={delivery} onChange={() => setDelivery(prev => !prev)} />
              </Col>
              <Col>
                <label>Phone number: </label>
                <input type='text' value={phoneNumber} onChange={(e) => setPhoneNumber(e.target.value)} />
              </Col>
            </Row>
            <Row>
              <Col>
                <label>Street: </label>
                <input type='text' value={street} onChange={(e) => setStreet(e.target.value)} />
              </Col>
              <Col>
                <label>Postal code: </label>
                <input type='text' value={postalCode} onChange={(e) => setPostalCode(e.target.value)} />
              </Col>
            </Row>
            <Row>
              <Col>
                <label>City: </label>
                <input type='text' value={city} onChange={(e) => setCity(e.target.value)} />
              </Col>
              <Col>

              </Col>
            </Row>
            <Row className='text-center'>
              <Col>
                <button className='btn btn-success'>Send</button>
              </Col>
            </Row>
          </form>
        </Modal.Body>
      </Modal>}

    </>
  );
}

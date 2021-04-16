import React from "react";
import { Navbar, Nav, Button } from "react-bootstrap";
import "./navbar.css";

const MyNavbar = () => {
  return (
    <div>
      <Navbar variant="dark" bg="chinese-black" expand="lg">
        <Navbar.Brand href="#home">Barbell Athletics</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
            <Nav.Link href="#home">Home</Nav.Link>
            <Nav.Link href="#link">Link</Nav.Link>
          </Nav>
          <Button variant="light">Sign In</Button>
        </Navbar.Collapse>
      </Navbar>
    </div>
  );
};

export default MyNavbar;

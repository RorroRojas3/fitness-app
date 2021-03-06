import React from "react";
import { Navbar, Nav, Button } from "react-bootstrap";
import "./navbar.css";

// My components
import SignInModal from "../signInModal/signInModal";

// My images
import DogeIcon from "../../images/doge.png";

const MyNavbar = () => {
  const [modalShow, setModalShow] = React.useState(false);

  return (
    <div>
      <Navbar variant="dark" bg="chinese-black" expand="lg">
        <Navbar.Brand href="#home">
          <img
            alt="doge"
            src={DogeIcon}
            width="30"
            height="30"
            className="d-inline-block align-top"
          />{" "}
          Doge Fitness
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
            <Nav.Link href="#home">Home</Nav.Link>
            <Nav.Link href="#link">Link</Nav.Link>
          </Nav>
          <Button variant="light" onClick={() => setModalShow(true)}>
            Sign In
          </Button>
        </Navbar.Collapse>
      </Navbar>
      <SignInModal
        show={modalShow}
        onHide={() => setModalShow(false)}
      ></SignInModal>
    </div>
  );
};

export default MyNavbar;

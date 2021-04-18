import React from "react";
import { Modal, Button } from "react-bootstrap";
import { GoogleLogin } from "react-google-login";
import FacebookLogin from "react-facebook-login";

const SignInModal = (props) => {
  const responseGoogle = (response) => {
    console.log(response);
  };

  const responseFacebook = (response) => {
    console.log(response);
  };

  return (
    <div>
      <Modal
        {...props}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            Modal heading
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <h4>Centered Modal</h4>
          <GoogleLogin
            clientId=""
            buttonText="Login with Google"
            onSuccess={responseGoogle}
            onFailure={responseGoogle}
            cookiePolicy={"single_host_origin"}
          />
          <FacebookLogin
            appId=""
            autoLoad={false}
            fields="name,email,picture"
            scope="public_profile,email"
            callback={responseFacebook}
            icon="fa-facebook"
          />
        </Modal.Body>
        <Modal.Footer>
          <Button onClick={props.onHide}>Close</Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default SignInModal;

import React from "react";
import { Modal, Button, Grid, Row, Col } from "react-bootstrap";
import { GoogleLogin } from "react-google-login";
import FacebookLogin from "react-facebook-login";
import MicrosoftLogin from "react-microsoft-login";

const SignInModal = (props) => {
  const responseGoogle = (response) => {
    console.log(response);
  };

  const responseFacebook = (response) => {
    console.log(response);
  };

  const authHandler = (err, data) => {
    console.log(err, data);
  };

  return (
    <div>
      <Modal
        {...props}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
      >
        <Modal.Header>
          <Modal.Title
            className="w-100 text-center"
            id="contained-modal-title-vcenter"
          >
            Please Login
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Row className="justify-content-center">
            <GoogleLogin
              clientId="526890719928-l2hfe3l742eongf9kicdsu973r0jojj3.apps.googleusercontent.com"
              buttonText="Login with Google"
              onSuccess={responseGoogle}
              onFailure={responseGoogle}
              cookiePolicy={"single_host_origin"}
            />
          </Row>
          <Row className="justify-content-center mt-2">
            <FacebookLogin
              appId="511969923263248"
              autoLoad={false}
              fields="name,email,picture"
              scope="public_profile,email"
              callback={responseFacebook}
              icon="fa-facebook"
            />
          </Row>
          <Row className="justify-content-center mt-2">
            <MicrosoftLogin
              clientId="e6a0ca00-d569-44d1-a41e-2fa6b08b59a0"
              authCallback={authHandler}
            />
          </Row>
        </Modal.Body>
        <Modal.Footer className="justify-content-center">
          <Button onClick={props.onHide}>Close</Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default SignInModal;

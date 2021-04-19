import React from "react";
import { Image } from "react-bootstrap";

import DogeIcon from "../../images/doge.png";

const Home = () => {
  return (
    <div>
      <Image src={DogeIcon} fluid></Image>
    </div>
  );
};

export default Home;

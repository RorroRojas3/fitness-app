import MyNavbar from "../src/components/navbar/navbar";
import { Container } from "react-bootstrap";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";

// My pages
import Home from "./pages/home/home";

function App() {
  return (
    <div className="App">
      <MyNavbar></MyNavbar>
      <Router>
        <Container>
          <Switch>
            <Route exact path="/">
              <Home></Home>
            </Route>
            <Route exact path="/home"></Route>
            <Route exact path="/lifts"></Route>
          </Switch>
        </Container>
      </Router>
    </div>
  );
}

export default App;

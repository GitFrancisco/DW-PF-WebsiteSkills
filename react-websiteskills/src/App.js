import './App.css';
import Navbar from "./components/Navbar";
import Home from "./Pages/Home";
import Skills from "./Pages/Skills";
import Recursos from "./Pages/Recursos";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

function App() {
  return (
    <div className="App">
      <Router>
        <Navbar/>
        <Switch>
          <Route path="/" exact component={Home} />
          <Route path="/Skills" component={Skills} />
          <Route path="/Recursos" component={Recursos} />
          <Route path="/Login" component={Login} />
          <Route path="/Register" component={Register} />
        </Switch>
      </Router>
    </div>
  );
}

export default App;
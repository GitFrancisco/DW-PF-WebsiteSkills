import './App.css';
import Navbar from "./components/Navbar";
import Home from "./Pages/Home";
import Skills from "./Pages/Skills";
import Recursos from "./Pages/Recursos";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import ApagarSkills from "./Pages/SkillsPages/ApagarSkills"; // Certifique-se de que o caminho está correto
import EditarSkills from "./Pages/SkillsPages/EditarSkills"; // Certifique-se de que o caminho está correto
import AnunciosSkills from "./Pages/SkillsPages/AnunciosSkills"; // Certifique-se de que o caminho está correto
import CriarAnunciosSkills from "./Pages/SkillsPages/CriarAnunciosSkills"; // Certifique-se de que o caminho está correto
import RecursosSkills from "./Pages/SkillsPages/RecursosSkills"; // Certifique-se de que o caminho está correto
import CriarSkills from "./Pages/SkillsPages/CriarSkills"; // Certifique-se de que o caminho está correto
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

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
          <Route path="/SkillsPages/ApagarSkills" component={ApagarSkills} />
          <Route path="/SkillsPages/EditarSkills" component={EditarSkills} />
          <Route path="/SkillsPages/AnunciosSkills" component={AnunciosSkills} />
          <Route path="/SkillsPages/CriarAnunciosSkills" component={CriarAnunciosSkills} />
          <Route path="/SkillsPages/RecursosSkills" component={RecursosSkills} />
          <Route path="/SkillsPages/CriarSkills" component={CriarSkills} />
        </Switch>
      </Router>
    </div>
  );
}

export default App;

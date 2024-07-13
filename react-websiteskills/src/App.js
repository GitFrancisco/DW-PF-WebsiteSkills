import './App.css';
import Navbar from "./components/Navbar";
import Home from "./Pages/Home";
import Skills from "./Pages/Skills";
import Recursos from "./Pages/Recursos";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import ApagarSkills from "./Pages/SkillsPages/ApagarSkills"; 
import EditarSkills from "./Pages/SkillsPages/EditarSkills"; 
import AnunciosSkills from "./Pages/SkillsPages/AnunciosSkills"; 
import CriarAnunciosSkills from "./Pages/SkillsPages/CriarAnunciosSkills"; 
import RecursosSkills from "./Pages/SkillsPages/RecursosSkills"; 
import CriarSkills from "./Pages/SkillsPages/CriarSkills"; 
import ApagarRecursos from "./Pages/RecursosPages/ApagarRecursos";
import EditarRecursos from "./Pages/RecursosPages/EditarRecursos";
import DetalhesRecursos from "./Pages/RecursosPages/DetalhesRecursos";
import CriarRecursos from "./Pages/RecursosPages/CriarRecursos";
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
          <Route path="/SkillsPages/ApagarSkills/:skillsId" component={ApagarSkills} />
          <Route path="/SkillsPages/EditarSkills/:skillsId" component={EditarSkills} />
          <Route path="/SkillsPages/AnunciosSkills/:skillsId" component={AnunciosSkills} />
          <Route path="/SkillsPages/CriarAnunciosSkills/:skillsId" component={CriarAnunciosSkills} />
          <Route path="/SkillsPages/RecursosSkills/:skillsId" component={RecursosSkills} />
          <Route path="/SkillsPages/CriarSkills" component={CriarSkills} />
          <Route path="/RecursosPages/ApagarRecursos" component={ApagarRecursos} />
          <Route path="/RecursosPages/EditarRecursos" component={EditarRecursos} />
          <Route path="/RecursosPages/DetalhesRecursos" component={DetalhesRecursos} />
          <Route path="/RecursosPages/CriarRecursos" component={CriarRecursos} />
        </Switch>
      </Router>
    </div>
  );
}

export default App;

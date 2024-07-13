import React from "react";
import "../Styles/Skills.css";
import { Link } from "react-router-dom";
import Button from 'react-bootstrap/Button';



function Skills() {
    return <div className="Skills">
      
            <div className="OpcoesSkills">
              <h1> Skills</h1>
              <Link to="/SkillsPages/EditarSkills">
                <Button variant="secondary">Editar</Button>
              </Link>
              <Link to="/SkillsPages/ApagarSkills">
                <Button variant="secondary">Apagar</Button>
              </Link>
              <Link to="/SkillsPages/AnunciosSkills">
                <Button variant="secondary">Anuncio</Button>
              </Link>
              <Link to="/SkillsPages/CriarAnunciosSkills">
                <Button variant="secondary">Criar Anuncio</Button>
              </Link>
              <Link to="/SkillsPages/RecursosSkills">
                <Button variant="secondary">Recursos</Button>
              </Link>
            </div>

            <div className="Criação Skill">
            <Link to="/SkillsPages/CriarSkills">
                <Button variant="dark">Criar Nova Skill</Button>
              </Link>
            </div>
    
          </div>; 
  }
  export default Skills;
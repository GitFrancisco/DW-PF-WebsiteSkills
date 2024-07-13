import React from "react";
import "../Styles/Skills.css";
import { Link } from "react-router-dom";
import Button from 'react-bootstrap/Button';
import "../Styles/Skills.css";
import { useState, useEffect } from 'react';

function Skills() {
    return <div className="Skills">
      
            <div className="OpcoesSkills">
              <h1> Skills</h1>
              <Link to="/SkillPages/EditarSkills">
                <Button variant="secondary">Editar</Button>
              </Link>
              <Link to="/SkillPages/EliminarSkills">
                <Button variant="secondary">Apagar</Button>
              </Link>
              <Link to="/SkillPages/AnuncioSkills">
                <Button variant="secondary">Anuncio</Button>
              </Link>
              <Link to="/SkillPages/CriarAnuncioSkills">
                <Button variant="secondary">Criar Anuncio</Button>
              </Link>
              <Link to="/SkillPages/Recursoskills">
                <Button variant="secondary">Recursos</Button>
              </Link>
            </div>

            <div className="Criação Skill">
            <Link to="/SkillPages/CriarSkills">
                <Button variant="dark">Criar Nova Skill</Button>
              </Link>
            </div>
    
          </div>; 
  }
  export default Skills;
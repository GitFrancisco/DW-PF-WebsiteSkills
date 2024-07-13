import React from "react";
import "../Styles/Skills.css";
import { Link } from "react-router-dom";
import Button from 'react-bootstrap/Button';
import "../Styles/Skills.css";
import { useState, useEffect } from 'react';

function Skills() {
  const [skills, setSkills] = useState([]);

  useEffect(() => {
    fetchSkills();
  }, []);

  const fetchSkills = () => {
    fetch('https://localhost:7263/api/ApiSkills/GetAllSkills', {
      headers: {
        'accept': 'application/json'
      }
    })
    .then(response => response.json())
    .then(data => setSkills(data))
    .catch(error => console.log(error));
  };


  return (
    <div className="Skills">
      <div>
        <h1>Skills</h1>

        <Link to="/SkillPages/EditarSkills">
          <Button variant="secondary">As nossas skills</Button>
        </Link>
        <Link to="/SkillPages/EliminarSkills">
          <Button variant="secondary">As nossas skills</Button>
        </Link>
        <Link to="/SkillPages/AnuncioSkills">
          <Button variant="secondary">As nossas skills</Button>
        </Link>
        <Link to="/SkillPages/CriarAnuncioSkills">
          <Button variant="secondary">As nossas skills</Button>
        </Link>
        <Link to="/SkillPages/Recursoskills">
          <Button variant="secondary">As nossas skills</Button>
        </Link>

      <div>
          {skills.map(skill => (
            <div key={skill.skillsId} className="skill-item">
            <img src={`https://localhost:7263/Imagens/${skill.imagem}`} alt={skill.nome} />
              <h3>{skill.nome}</h3>
              <p>{skill.descricao}</p>
            </div>
          ))}
        </div>

      
      </div>
    </div>
  );
}

export default Skills;
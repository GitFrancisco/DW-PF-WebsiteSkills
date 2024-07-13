import React from "react";
import "../Styles/Skills.css";
import { Link } from "react-router-dom";
import Button from 'react-bootstrap/Button';



function Skills() {
    return <div className="Skills">
      
            <div>
              <h1> Skills</h1>
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
            </div>
    
          </div>; 
  }
  export default Skills;
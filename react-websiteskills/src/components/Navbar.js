import React, { useState } from 'react'
import { Link } from 'react-router-dom';
import '../Styles/Navbar.css';
import '@fortawesome/fontawesome-free/css/all.css';


function Navbar() {
    return (
      <div className="navbar">
              
              <div className="left-links">
                <Link to="/" className="home-link">
                <i className="fas fa-home"></i>SkillPull</Link>
                <Link to="/Skills">Skills</Link>
                <Link to="/Recursos">Recursos</Link>
            </div>
            <div className="right-links">
                <Link to="/Login">Entrar</Link>
                <Link to="/Register">Registar</Link>
            </div>
      </div>
    );
  }
  
  export default Navbar
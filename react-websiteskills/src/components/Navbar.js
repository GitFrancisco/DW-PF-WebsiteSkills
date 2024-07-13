import React, { useState } from 'react'
import { Link } from 'react-router-dom';
import '../Styles/Navbar.css';


function Navbar() {
    // state variable that verifies whether the "links" are open or not (phone mode)
    const [openLinks, setOpenLinks] = useState(false);
    // checks if the navbar is expanded or not (phone mode)
    const toggleNavBar = () =>  {
      setOpenLinks(!openLinks);
    };
  
    return (
      <div className="navbar">  
          <div className="leftSide" id={openLinks ? "open " : "close"}>
              <Link to="/">
              {/*   <img src={Logo} style={{display: openLinks ? 'none' : 'block'}} />*/}
              </Link>
              <div className="hiddenLinks">
                <Link to="/"> SkillPull </Link>
                <Link to="/Skills"> Skills </Link>
                <Link to="/Recursos"> Recursos </Link>
                <Link to="/Login"> Entrar </Link>
                <Link to="/Register"> Registar</Link>
              </div>
          </div>
      </div>
    );
  }
  
  export default Navbar
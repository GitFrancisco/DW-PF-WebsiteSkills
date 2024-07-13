import React from 'react';
import { Link } from "react-router-dom";
import BannerImage from '../Assets/Background_MP.png';
import "../Styles/Home.css";
import Button from 'react-bootstrap/Button';


function Home() {
  return <div className="Home">
    
          <div className="headerContainer" style={{ backgroundImage: `url(${BannerImage})` }}>
            <h1> Aprenda com os melhores.</h1>
            <p> Potencialize seu talento com mentoria personalizada. </p>
            <Link to="/Skills">
              <Button variant="dark" size="lg">As nossas skills</Button>
            </Link>
          </div>
  
        </div>;
  
}

export default Home;
import React from "react";
import Button from 'react-bootstrap/Button';
import { Link } from "react-router-dom";
import '../../Styles/RecursosPages/ApagarRecursos.css';


function ApagarRecursos(){
    return (
        <div className="ApagarRecurso">
            <h1>Apagar</h1>
            <h2>Confirma a remoção deste Recurso?</h2>
            <Button variant="danger">Apagar</Button>
            <Link to="../Recursos">
                 <Button variant="secondary">Voltar à lista de Skills</Button>
           </Link>  
        </div>
    );
}

export default ApagarRecursos;

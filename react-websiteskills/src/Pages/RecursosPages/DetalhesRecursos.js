import React from "react";
import Button from 'react-bootstrap/Button';
import { Link } from "react-router-dom";


function DetalhesRecursos(){
    return (
        <div className="ApagarRecurso">
            <p>Nome do Recurso</p>
            <p>Conteúdo do Recurso</p>
            <p>Tipo de Recurso</p>
            <p>Tipo de Recurso</p>
            
            <Link to="/EditarRecurso">
                <Button variant="dark">Editar</Button>
            </Link>  
            <Link to="../Recursos">
                 <Button variant="secondary">Voltar à lista de Recursos</Button>
           </Link>  
        </div>
    );
}

export default DetalhesRecursos;
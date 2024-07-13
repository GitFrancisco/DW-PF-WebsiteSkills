import React from "react";
import "../Styles/Recursos.css";
import { Link } from "react-router-dom";
import Button from "react-bootstrap/Button";


function Recursos() {
return (
    <div className="Recursos">
      <div className="OpcoesRecursos">
        <h1> Recursos</h1>
        <Link to="/RecursosPages/EditarRecursos">
          <Button variant="secondary">Editar</Button>
        </Link>
        <Link to="/RecursosPages/ApagarRecursos">
          <Button variant="secondary">Apagar</Button>
        </Link>
        <Link to="/RecursosPages/DetalhesRecursos">
          <Button variant="secondary">Anuncio</Button>
        </Link>
      </div>
      <div className="CriaçãoRecurso">
        <Link to="/RecursosPages/CriarRecursos">
          <Button variant="dark">Criar Novo Recurso</Button>
        </Link>
      </div>
    </div>
  );
}

export default Recursos;
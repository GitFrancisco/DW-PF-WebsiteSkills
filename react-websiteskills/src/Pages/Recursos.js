import React from "react";
import "../Styles/Recursos.css";
import { Link } from "react-router-dom";
import Button from "react-bootstrap/Button";
import { useState, useEffect } from "react";


function Recursos() {
  const [recursos, setRecursos] = useState([]);

  useEffect(() => {
    fetchRecursos();
  }, []);

  const fetchRecursos = () => {
    fetch("https://localhost:7263/api/ApiRecursos/GetAllRecursos", {
      headers: {
        accept: "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => setRecursos(data))
      .catch((error) => console.log(error));
  };


return (
    <div className="Recursos">
      <div className="OpcoesRecursos">
        <h1> Recursos</h1>
      </div>
      <div className="CriaçãoRecurso">
        <Link to="/RecursosPages/CriarRecursos">
          <Button variant="dark">Criar Novo Recurso</Button>
        </Link>
      </div>

      <div>
          {recursos.map(recursos => (
            <div key={recursos.idRecurso} className="recursos-item">
              <h3>{recursos.nomeRecurso}</h3>
              <p>Conteúdo do Recurso: {recursos.conteudoRecurso}</p>
              <p>Tipo do Recurso: {recursos.tipoRecurso}</p>
              <p>Skill associada ao Recurso: {recursos.skillsFK}</p>
              <Link to={`/RecursosPages/EditarRecursos/${recursos.idRecurso}`}>
                <Button variant="secondary">Editar</Button>
              </Link>
              <Link to={`/RecursosPages/ApagarRecursos/${recursos.idRecurso}`}>
                <Button variant="secondary">Apagar</Button>
              </Link>
              <Link to={`/RecursosPages/DetalhesRecursos/${recursos.idRecurso}`}>
                <Button variant="secondary">Detalhes</Button>
              </Link>
            </div>
          ))}
      </div>

    </div>
  );
}

export default Recursos;
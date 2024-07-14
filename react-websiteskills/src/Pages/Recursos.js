import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import Button from "react-bootstrap/Button";
import "../Styles/Recursos.css";

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
    <div className="container">
      <h1 className="text-center my-4">Recursos</h1>
      <div className="row mb-3">
        <div className="col text-right">
          <Link to="/RecursosPages/CriarRecursos">
            <Button variant="dark">Criar novo Recurso</Button>
          </Link>
        </div>
      </div>
      <div className="row">
        <div className="col">
          <table className="table table-striped table-bordered table-hover text-center table-sm">
            <thead className="thead-dark">
              <tr>
                <th>Nome</th>
                <th>Conteúdo</th>
                <th>Tipo</th>
                <th>Skill</th>
                <th>Ações</th>
              </tr>
            </thead>
            <tbody>
              {recursos.map((recurso) => (
                <tr key={recurso.idRecurso}>
                  <td>{recurso.nomeRecurso}</td>
                  <td>{recurso.conteudoRecurso}</td>
                  <td>{recurso.tipoRecurso}</td>
                  <td>{recurso.skillsFK}</td>
                  <td className="d-flex justify-content-center align-items-center">
                    <Link to={`/RecursosPages/EditarRecursos/${recurso.idRecurso}`}>
                      <Button variant="secondary" size="sm">Editar</Button>
                    </Link>
                    <Link to={`/RecursosPages/DetalhesRecursos/${recurso.idRecurso}`}>
                      <Button variant="secondary" size="sm">Detalhes</Button>
                    </Link>
                    <Link to={`/RecursosPages/ApagarRecursos/${recurso.idRecurso}`}>
                      <Button variant="secondary" size="sm">Apagar</Button>
                    </Link>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Recursos;

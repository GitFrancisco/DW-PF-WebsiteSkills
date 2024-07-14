import React, { useEffect } from "react";
import Button from "react-bootstrap/Button";
import { Link } from "react-router-dom";
import { useParams, useHistory } from "react-router-dom";
import { useState } from "react";

function DetalhesRecursos() {
  // Obter o id do Recurso
  let { idRecurso } = useParams();
  // Obter o histórico de navegação
  let history = useHistory();

  // Estados para guardar os dados do recurso
  const [recurso, setRecurso] = useState([]);

  // Metodo para ir buscar um recurso especifico
  const getRecurso = () => {
    fetch(`https://localhost:7263/api/ApiRecursos/GetRecurso?id=${idRecurso}`, {
        headers: {
            'accept': 'text/plain'
        }
    })
        .then(response => response.json())
        .then(data => {
            // Processar os dados do recurso obtido
            setRecurso(data);
        })
        .catch(error => {
            console.error(error);
        });
};
    // Chamar o método para obter o recurso
    useEffect(() => {
        getRecurso();
    }, []);

  return (
    <div className="ApagarRecurso">
      <h2>Nome do Recurso</h2>
      <p>{recurso.nomeRecurso}</p>
      <h2>Conteúdo do Recurso</h2>
      {recurso.tipoRecurso === "PDF" && (
            <div>
              <embed src={`https://localhost:7263/FicheirosRecursos/${recurso.conteudoRecurso}`} type="application/pdf" width="100%" height="400px" class="mb-3" />
            </div>
          )}
          {recurso.tipoRecurso === "Imagem" && (
            <div>
              <img src={`https://localhost:7263/FicheirosRecursos/${recurso.conteudoRecurso}`} alt={recurso.nomeRecurso} width="200" />
            </div>
          )}
          {recurso.tipoRecurso === "Texto" && (
            <div>
              <h2>{recurso.nomeRecurso}</h2>
              <p>{recurso.conteudoRecurso}</p>
            </div>
          )}

      <h2>Tipo de Recurso</h2>
      <p>{recurso.tipoRecurso}</p>
      <h2>Skill</h2>
      <p>{recurso.skillsFK}</p>

      <Link to={`/RecursosPages/EditarRecursos/${idRecurso}`}>
        <Button variant="dark">Editar</Button>
      </Link>
      <Link to="../../Recursos">
        <Button variant="secondary">Voltar à lista de Recursos</Button>
      </Link>
    </div>
  );
}

export default DetalhesRecursos;

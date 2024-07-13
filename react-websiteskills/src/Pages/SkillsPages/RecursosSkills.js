import React from "react";
import Button from "react-bootstrap/Button";
import { useState, useEffect } from "react";
import { useParams, useHistory } from "react-router-dom";

function RecursosSkills() {
  // Obter o id da skill a ser editada
  let { skillsId } = useParams();
  // Obter o histórico de navegação
  let history = useHistory();

  // Estados para os campos do formulário
  const [recursos, setRecursos] = useState([]);

  useEffect(() => {
    fetchRecursos();
  }, []);

  // Método para fazer a busca de recursos
  const fetchRecursos = () => {
    fetch(
      `https://localhost:7263/api/ApiRecursos/SkillRecursos?id=${skillsId}`,
      {
        headers: {
          accept: "text/plain",
        },
      }
    )
      .then((response) => response.json())
      .then((data) => setRecursos(data))
      .catch((error) => console.log(error));
  };

  return (
    <div className="Recursos">
      <h1>Recursos</h1>
      {recursos.map((recurso, index) => (
        <div key={index}>
          {recurso.tipoRecurso === "PDF" && (
            <div>
              <h2>{recurso.nomeRecurso}</h2>
              <embed src={`https://localhost:7263/FicheirosRecursos/${recurso.conteudoRecurso}`} type="application/pdf" width="100%" height="400px" class="mb-3" />
            </div>
          )}
          {recurso.tipoRecurso === "Imagem" && (
            <div>
              <h2>{recurso.nomeRecurso}</h2>
              <img src={`https://localhost:7263/FicheirosRecursos/${recurso.conteudoRecurso}`} alt={recurso.nomeRecurso} width="200" />
            </div>
          )}
          {recurso.tipoRecurso === "Texto" && (
            <div>
              <h2>{recurso.nomeRecurso}</h2>
              <p>{recurso.conteudoRecurso}</p>
            </div>
          )}
        </div>
      ))}
    </div>
  );
}

export default RecursosSkills;
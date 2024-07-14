import React, { useState, useEffect } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
import { Card, Button } from "react-bootstrap";
import "../../Styles/RecursosPages/ApagarRecursos.css";

const ApagarRecursos = () => {
  // Obter o id do recurso a ser apagado
  let { idRecurso } = useParams();
  // Obter o histórico de navegação
  let history = useHistory();
  // Estado para guardar o recurso
  const [recurso, setRecurso] = useState(null);

  // Obter o recurso a ser apagado
  useEffect(() => {
    fetch(`https://localhost:7263/api/ApiRecursos/GetRecurso?id=${idRecurso}`, {
      headers: {
        'accept': 'text/plain',
        'Authorization': "Bearer " + localStorage.getItem("jwt")
      }
    })
      .then(response => response.json())
      .then(data => setRecurso(data))
      .catch(error => console.error(error));
  }, [idRecurso]);

  // Apagar o recurso
  const deleteRecurso = () => {
    fetch(`https://localhost:7263/api/ApiRecursos/DeleteRecurso?id=${idRecurso}`, {
      method: 'DELETE',
      headers: {
        'accept': '*/*',
        'Authorization': "Bearer " + localStorage.getItem("jwt")
      }
    })
      .then(response => {
        if (response.ok) {
          history.push('/Recursos');
        } else {
          alert('Não foi possível apagar o recurso.');
        }
      })
      .catch(error => console.error('Erro ao apagar o recurso:', error));
  };

  return (
    <div className="container">
      <h1 className="text-center title">Apagar</h1>
      <h3 className="text-center">Confirma a remoção deste Recurso?</h3>

      {recurso && (
        <div className="card shadow-sm mb-4">
          <div className="card-body">
            <h4 className="card-title">{recurso.nomeRecurso}</h4>
            <hr />
            <dl className="row">
              <dt className="col-sm-3 font-weight-bold">Nome do Recurso</dt>
              <dd className="col-sm-9">{recurso.nomeRecurso}</dd>

              <dt className="col-sm-3 font-weight-bold">Conteúdo do Recurso</dt>
              <dd className="col-sm-9">
                {recurso.tipoRecurso === "Imagem" ? (
                  <img
                    src={`https://localhost:7263/FicheirosRecursos/${recurso.conteudoRecurso}`}
                    alt={`Imagem referente a ${recurso.nomeRecurso}`}
                    title={recurso.nomeRecurso}
                    className="img-fluid"
                  />
                ) : recurso.tipoRecurso === "PDF" ? (
                  <div className="embed-responsive embed-responsive-4by3">
                    <object
                      data={`https://localhost:7263/FicheirosRecursos/${recurso.conteudoRecurso}`}
                      type="application/pdf"
                      className="embed-responsive-item"
                      style={{ width: '100%', height: '600px' }}
                    >
                      <p>
                        Seu navegador não suporta visualização de PDF. Você pode{' '}
                        <a href={`https://localhost:7263/FicheirosRecursos/${recurso.conteudoRecurso}`} target="_blank" rel="noopener noreferrer">
                          baixar o arquivo
                        </a>{' '}
                        manualmente.
                      </p>
                    </object>
                  </div>
                ) : (
                  <p>{recurso.conteudoRecurso}</p>
                )}
              </dd>

              <dt className="col-sm-3 font-weight-bold">Tipo de Recurso</dt>
              <dd className="col-sm-9">{recurso.tipoRecurso}</dd>

              <dt className="col-sm-3 font-weight-bold">Skill</dt>
              <dd className="col-sm-9">{recurso.skill?.nome}</dd>
            </dl>
          </div>
        </div>
      )}

      <div className="text-center button-container">
        <Button variant="danger" onClick={deleteRecurso} className="mx-2">Apagar</Button>
        <Link to="/Recursos" className="btn btn-secondary mx-2">Voltar à lista de Recursos</Link>
      </div>
    </div>
  );
};

export default ApagarRecursos;

import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import { Card, Button, Row, Col } from 'react-bootstrap';
import "../../Styles/RecursosPages/DetalhesRecursos.css";

const DetalhesRecurso = () => {
  let { idRecurso } = useParams();
  const [recurso, setRecurso] = useState(null);

  useEffect(() => {
    fetch(`https://localhost:7263/api/ApiRecursos/GetRecurso?id=${idRecurso}`, {
      headers: {
        'accept': 'text/plain',
        Authorization: "Bearer " + localStorage.getItem("jwt")
      }
    })
      .then(response => response.json())
      .then(data => setRecurso(data))
      .catch(error => console.error(error));
  }, [idRecurso]);

  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="col-lg-8">
          <Card className="border-0 shadow-sm mb-4">
            <Card.Body>
              <h1 className="text-center title">{recurso?.nomeRecurso}</h1>
              <hr />
              <dl className="row">
                <dt className="col-sm-3 font-weight-bold">Nome do Recurso</dt>
                <dd className="col-sm-9">{recurso?.nomeRecurso}</dd>

                <dt className="col-sm-3 font-weight-bold">Conteúdo do Recurso</dt>
                <dd className="col-sm-9">
                  {recurso?.tipoRecurso === 'Imagem' ? (
                    <img
                      src={`https://localhost:7263/FicheirosRecursos/${recurso.conteudoRecurso}`}
                      alt={`Imagem referente a ${recurso.nomeRecurso}`}
                      title={recurso.nomeRecurso}
                      className="img-fluid"
                    />
                  ) : recurso?.tipoRecurso === 'PDF' ? (
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
                    <p>{recurso?.conteudoRecurso}</p>
                  )}
                </dd>

                <dt className="col-sm-3 font-weight-bold">Tipo de Recurso</dt>
                <dd className="col-sm-9">{recurso?.tipoRecurso}</dd>
              </dl>
            </Card.Body>
          </Card>
          <div className="text-center">
            <Link to={`/RecursosPages/EditarRecursos/${recurso?.idRecurso}`} className="btn btn-dark mx-2">Editar</Link>
            <Link to="/Recursos" className="btn btn-secondary mx-2">Voltar à lista de Recursos</Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DetalhesRecurso;

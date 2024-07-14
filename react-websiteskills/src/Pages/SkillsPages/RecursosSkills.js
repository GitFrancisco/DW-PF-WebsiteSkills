import React, { useState, useEffect } from "react";
import { useParams, useHistory } from "react-router-dom";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "../../Styles/SkillsPages/RecursosSkills.css";

function RecursosSkills() {
  let { skillsId } = useParams();
  let history = useHistory();

  const [recursos, setRecursos] = useState([]);

  useEffect(() => {
    fetchRecursos();
  }, []);

  const fetchRecursos = () => {
    fetch(`https://localhost:7263/api/ApiRecursos/SkillRecursos?id=${skillsId}`, {
      headers: {
          'accept': 'text/plain',
      }
  })
      .then((response) => response.json())
      .then((data) => setRecursos(data))
      .catch((error) => console.log(error));
  };

  return (
      <div className="container">
        <h1 className="text-center my-4">Recursos</h1>
        <Row>
          {recursos.length > 0 ? (
            recursos.map((recurso, index) => (
              <Col md={4} className="mb-4" key={index}>
                <Card className="h-100 shadow-sm">
                  <Card.Body>
                    <Card.Title className="text-center">{recurso.nomeRecurso}</Card.Title>
                    <Card.Text className="text-center">
                      {recurso.tipoRecurso === "PDF" ? (
                        <embed
                          src={`https://localhost:7263/FicheirosRecursos/${recurso.conteudoRecurso}`}
                          type="application/pdf"
                          width="100%"
                          height="400px"
                          className="mb-3"
                        />
                      ) : recurso.tipoRecurso === "Imagem" ? (
                        <img
                          src={`https://localhost:7263/FicheirosRecursos/${recurso.conteudoRecurso}`}
                          alt={`Imagem referente a ${recurso.nomeRecurso}`}
                          title={recurso.nomeRecurso}
                          className="img-fluid mb-3"
                        />
                      ) : (
                        <p>{recurso.conteudoRecurso}</p>
                      )}
                    </Card.Text>
                  </Card.Body>
                </Card>
              </Col>
            ))
          ) : (
            <p>Não existem recursos disponiveis!</p>
          )}
        </Row>
      </div>
    );
}

export default RecursosSkills;
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
    fetch(
      `https://localhost:7263/api/ApiRecursos/SkillRecursos?id=${skillsId}`,
      {
        headers: {
          accept: "text/plain",
          Authorization: "Bearer " + localStorage.getItem("jwt")
        },
      }
    )
      .then((response) => response.json())
      .then((data) => setRecursos(data))
      .catch((error) => console.log(error));
  };

  const deleteRecurso = (id) => {
    fetch(`https://localhost:7263/api/ApiRecursos/DeleteRecurso?id=${id}`, {
      method: "DELETE",
    })
      .then((response) => {
        if (response.ok) {
          setRecursos(recursos.filter((recurso) => recurso.idRecurso !== id));
        } else {
          throw new Error("Erro ao apagar recurso");
        }
      })
      .catch((error) => console.log(error));
  };

  return (
    <div className="container">
      <h1 className="text-center my-4">Recursos</h1>
      <Row>
        {recursos.map((recurso, index) => (
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
              <Card.Footer className="text-center">
                <Button variant="secondary" size="sm" onClick={() => deleteRecurso(recurso.idRecurso)}>Apagar</Button>
              </Card.Footer>
            </Card>
          </Col>
        ))}
      </Row>
    </div>
  );
}

export default RecursosSkills;

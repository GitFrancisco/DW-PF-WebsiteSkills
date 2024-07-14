import React, { useState, useEffect } from 'react';
import { useParams, useHistory, Link } from 'react-router-dom';
import { Form, Button, FormControl } from 'react-bootstrap';
import "../../Styles/RecursosPages/EditarRecursos.css";

const EditarRecursos = () => {
  let { idRecurso } = useParams();
  let history = useHistory();
  const [recurso, setRecurso] = useState({});
  const [recursoNome, setNome] = useState("");
  const [recursoConteudo, setConteudo] = useState("");

  useEffect(() => {
    fetch(`https://localhost:7263/api/ApiRecursos/GetRecurso?id=${idRecurso}`, {
      headers: {
        'accept': 'text/plain'
      }
    })
      .then(response => response.json())
      .then(data => {
        setRecurso(data);
        setNome(data.nomeRecurso);
      })
      .catch(error => {
        console.error(error);
      });
  }, [idRecurso]);

  const editarRecurso = () => {
    fetch(`https://localhost:7263/api/ApiRecursos/EditRecurso?id=${idRecurso}`, {
      method: 'POST',
      headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        'nomeRecurso': recursoNome,
        'tipoRecurso': recurso.tipoRecurso,
        'skillsFK': recurso.skillsFK,
        'conteudoRecurso': recursoConteudo
      })
    })
    .then(response => {
      if (response.ok) {
        history.push("../../Recursos");
      } else {
        throw new Error("Erro ao editar Recurso");
      }
    })
    .catch(error => {
      console.error(error);
    });
  };

  const handleUpload = (e) => {
    const file = e.target.files[0];
    setConteudo(file.name);
    const formData = new FormData();
    formData.append("file", file);

    fetch("https://localhost:7263/api/ApiRecursos/UploadFile", {
      method: "POST",
      body: formData,
    })
    .then(response => response.json())
    .then(data => {
      console.log(data);
    })
    .catch(error => {
      console.error(error);
    });
  };

  return (
    <div className="container">
      <h1 className="text-center">Editar</h1>
      <div className="row justify-content-center">
        <div className="col-md-6">
          <div className="card shadow-sm p-4 mb-4 bg-white rounded">
            <div className="card-body">
              <h4 className="card-title text-center">Recurso</h4>
              <hr />
              <Form>
                <Form.Group>
                  <Form.Label>Nome do Recurso</Form.Label>
                  <FormControl
                    type="text"
                    value={recursoNome}
                    onChange={(e) => setNome(e.target.value)}
                  />
                </Form.Group>
                <Form.Group>
                  <Form.Label>Conteúdo do Recurso</Form.Label>
                  {recurso.tipoRecurso === 'Texto' ? (
                    <FormControl
                      type="text"
                      value={recursoConteudo}
                      onChange={(e) => setConteudo(e.target.value)}
                    />
                  ) : (
                    <FormControl
                      type="file"
                      onChange={handleUpload}
                    />
                  )}
                </Form.Group>
                <div className="text-center mt-4">
                  <Button variant="dark" onClick={editarRecurso}>
                    Guardar
                  </Button>
                </div>
              </Form>
            </div>
          </div>
          <div className="text-center">
            <Link to="../../Recursos">
              <Button variant="secondary">Voltar à lista de recursos</Button>
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EditarRecursos;

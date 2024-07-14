import React, { useState } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
import Button from "react-bootstrap/Button";
import FormControl from "react-bootstrap/FormControl";
import Alert from "react-bootstrap/Alert";
import "../../Styles/SkillsPages/CriarAnunciosSkills.css";

function CriarAnunciosSkills() {
  let { skillsId } = useParams();
  let history = useHistory();

  const [textoAnuncio, setTextoAnuncio] = useState("");
  const [message, setMessage] = useState(null);

    // Método para fazer a criação de um anúncio
    const addAnuncio = () => {
        fetch(`https://localhost:7263/api/ApiAnuncios/AddAnuncio?skillId=${skillsId}`, {
            method: 'POST',
            headers: {
                'accept': 'text/plain',
                'Content-Type': 'application/json',
                Authorization: "Bearer " + localStorage.getItem("jwt")
            },
            body: JSON.stringify({
                texto: textoAnuncio
            })
        })
        .then(response => {
            if (response.ok) {
                // Se o anúncio foi criado com sucesso, redireciona para a lista de anúncios
                history.push("../../Skills");
            }
        });
    }


  return (
    <div className="container">
      <h2 className="text-center my-4">Criar Anúncio</h2>
      {message && (
        <Alert variant="warning">
          {message}
        </Alert>
      )}
      <div>
        <div className="form-group">
          <label className="control-label">Texto do Anúncio</label>
          <FormControl
            as="textarea"
            rows={6}
            maxLength={200}
            value={textoAnuncio}
            onChange={(e) => setTextoAnuncio(e.target.value)}
          />
        </div>
        <Button variant="dark" className="my-2 mx-2" onClick={addAnuncio}>
          Submeter
        </Button>
        <Link to="../../Skills">
          <Button variant="secondary" className="my-2">
            Cancelar
          </Button>
        </Link>
      </div>
    </div>
  );
}

export default CriarAnunciosSkills;

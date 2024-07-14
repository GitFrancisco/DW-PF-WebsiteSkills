import React from "react";
import Button from "react-bootstrap/Button";
import FormControl from "react-bootstrap/FormControl";
import { Link } from "react-router-dom";
import "../../Styles/RecursosPages/CriarRecursos.css";
import { useState } from "react";
import { useHistory } from "react-router-dom";

function CriarRecursos() {
  // Obter o histórico de navegação
  let history = useHistory();

  // Estados para os campos do formulário
  const [recursoNome, setNome] = useState("");
  const [recursoTipo, setTipo] = useState("");
  const [recursoSkill, setSkill] = useState();

  // Metodo para fazer a adição de um novo recurso
  const adicionarRecurso = () => {
    fetch('https://localhost:7263/api/ApiRecursos/AddRecurso', {
        method: 'POST',
        headers: {
                'accept': 'text/plain',
                'Content-Type': 'application/json'
        },
        body: JSON.stringify({
                'nomeRecurso': recursoNome,
                'tipoRecurso': recursoTipo,
                'skillsFK': recursoSkill,
                'conteudoRecurso': "null"
        })
    })
    .then((response) => {
        if (response.ok) {
            // Se a requisição for bem-sucedida, redirecionar para a lista de recursos
            history.push("../../Recursos");
        } else {
            throw new Error("Erro ao adicionar Recurso");
        }
    })
    .then(data => {
        console.log(data);
    })
    .catch(error => {
        console.error(error);
    });
}

return (
    <div className="CriaçãoRecursos">
        <h1>Criar</h1>
        <h3>Recursos</h3>

        <p>Nome do Recurso</p>
        <FormControl type="text" placeholder="" onChange={(e) => setNome(e.target.value)} />
        <p>Tipo do Recurso</p>
        <FormControl type="text" placeholder="" onChange={(e) => setTipo(e.target.value)} />
        <p>Skill associada ao Recurso</p>
        <FormControl type="text" placeholder="" onChange={(e) => setSkill(e.target.value)} />

        <Button variant="dark" onClick={adicionarRecurso}>Criar</Button>
        <Link to="../../Recursos">
            <Button variant="secondary">Voltar à lista de Recursos</Button>
        </Link>
    </div>
);
}

export default CriarRecursos;

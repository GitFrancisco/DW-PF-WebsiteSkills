import React from "react";
import Button from 'react-bootstrap/Button';
import FormControl from 'react-bootstrap/FormControl';
import { useParams, useHistory } from 'react-router-dom';
import { useState } from "react";
import { Link } from 'react-router-dom';

function CriarAnunciosSkills(){
    // Obter o id da skill a ser editada
    let { skillsId } = useParams();
    // Obter o histórico de navegação
    let history = useHistory();

    // Estados para os campos do formulário
    const [textoAnuncio, setTextoAnuncio] = useState("");

    // Método para fazer a criação de um anúncio
    const addAnuncio = () => {
        fetch(`https://localhost:7263/api/ApiAnuncios/AddAnuncio?skillId=${skillsId}`, {
            method: 'POST',
            headers: {
                'accept': 'text/plain',
                'Content-Type': 'application/json'
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
        <div className="Apagar Skill">
            <h1>Criar Anúncio</h1>
            <h2>Texto do Anúncio</h2>
            <FormControl as="textarea" rows={6} placeholder="" onChange={(e) => setTextoAnuncio(e.target.value)}/>
            <Button variant="dark" onClick={addAnuncio}>Submeter</Button>
            <Link to="../../Skills">
                <Button variant="secondary">Cancelar</Button>
            </Link>
        </div>
    )
};

export default CriarAnunciosSkills;
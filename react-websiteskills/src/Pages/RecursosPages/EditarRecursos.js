import React from "react";
import Button from 'react-bootstrap/Button';
import FormControl from 'react-bootstrap/FormControl';
import { Link } from "react-router-dom";
import { useParams } from 'react-router-dom';
import { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';

function EditarRecursos(){
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

  // Estados para os campos do formulário
  const [recursoNome, setNome] = useState("");
  const [recursoConteudo, setConteudo] = useState();

  // Metodo para fazer a edição de um recurso especifico
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
    .then((response) => {
        if (response.ok) {
            // Se a requisição for bem-sucedida, redirecionar para a lista de recursos
            history.push("../../Recursos");
        } else {
            throw new Error("Erro ao editar Recurso");
        }
    })
    .then(data => {
        console.log(data);
    })
    .catch(error => {
        console.error(error);
    });
}

    // Chamar o método para obter o recurso
    useEffect(() => {
        getRecurso();
    }, []);

    // Metodo para fazer o upload de uma imagem
    const handleUpload = (e) => {
        const file = e.target.files[0];
        // Atualizar o estado da imagem
        setConteudo(file.name);
        // Criar um objeto FormData
        const formData = new FormData();
        formData.append("file", file);

        // Enviar a imagem para o servidor
        fetch("https://localhost:7263/api/ApiRecursos/UploadFile", {
            method: "POST",
            body: formData,
        })
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
            })
            .catch((error) => {
                console.error(error);
            });
    };

    return (
        <div className="EditarRecursos">
            <h1>Editar</h1>
            <h3>recursos</h3>
            <p>Nome do Recurso</p>
            <FormControl type="text" placeholder="" value={recursoNome} onChange={(e) => setNome(e.target.value)} />
            {recurso.tipoRecurso === 'Texto' ? (
                <>
                    <p>Conteúdo do Recurso</p>
                    <FormControl type="text" placeholder="" value={recursoConteudo} onChange={(e) => setConteudo(e.target.value)} />
                </>
            ) : (
                <>
                    <p>Conteúdo do Recurso</p>
                    <FormControl type="file" onChange={handleUpload} />
                </>
            )}
            <Button variant="dark" onClick={editarRecurso}>Guardar</Button>
            <Link to="../../Recursos">
                <Button variant="secondary">Voltar à lista de recursos</Button>
            </Link>  
        </div>
    );
};

export default EditarRecursos;
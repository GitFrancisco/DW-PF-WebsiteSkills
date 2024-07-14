import React from "react";
import Button from 'react-bootstrap/Button';
import FormControl from 'react-bootstrap/FormControl';
import { useState } from "react";
import { Link, useHistory } from 'react-router-dom';

function CriarSkills(){
    // Obter o histórico de navegação
    let history = useHistory();

    // Estados para os campos do formulário
    const [skillNome, setNome] = useState("");
    const [skillDificuldade, setDificuldade] = useState("");
    const [skillTempo, setTempo] = useState("");
    const [skillDescricao, setDescricao] = useState("");
    const [skillCusto, setCusto] = useState("");
    const [skillImagem, setImagem] = useState("");

    // Metodo para fazer o upload de uma imagem
    const handleImageUpload = (e) => {
        const file = e.target.files[0];
        // Atualizar o estado da imagem
        setImagem(file.name);
        // Criar um objeto FormData
        const formData = new FormData();
        formData.append("file", file);

        // Enviar a imagem para o servidor
        fetch("https://localhost:7263/api/ApiSkills/UploadImage", {
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


    // Metodo para fazer a adição de uma nova skill
    const postSkills = () => {
        fetch('https://localhost:7263/api/ApiSkills/AddSkill', {
            method: 'POST',
            headers: {
                'accept': 'text/plain',
                'Content-Type': 'application/json',
                Authorization: "Bearer " + localStorage.getItem("jwt")
            },
            body: JSON.stringify({
                'nome': skillNome,
                'dificuldade': skillDificuldade,
                'tempo': skillTempo,
                'descricao': skillDescricao,
                'custo': skillCusto,
                'imagem': skillImagem
            })
        })
        .then((response) => {
            if (response.ok) {
                // Se a requisição for bem-sucedida, redirecionar para a lista de skills
                history.push("../../Skills");
            } else {
                throw new Error("Erro ao adicionar skill");
            }
        })
        .catch((error) => {
            console.error(error);
        });
    };

    return (
        <div className="Criação Skill">
            <h1>Criar</h1>
            <h3>Skills</h3>
            <p>Nome</p>
            <FormControl type="text" placeholder="" value={skillNome} onChange={(e) => setNome(e.target.value)} />
            <p>Dificuldade</p>
            <FormControl type="text" placeholder="" value={skillDificuldade} onChange={(e) => setDificuldade(e.target.value)} />
            <p>Tempo</p>
            <FormControl type="text" placeholder="" value={skillTempo} onChange={(e) => setTempo(e.target.value)} />
            <p>Descrição</p>
            <FormControl type="text" placeholder="" value={skillDescricao} onChange={(e) => setDescricao(e.target.value)} />
            <p>Custo</p>
            <FormControl type="text" placeholder="" value={skillCusto} onChange={(e) => setCusto(e.target.value)} />
            <p>Imagens</p>
            <FormControl type="file" onChange={handleImageUpload} />

            <Button variant="dark" onClick={postSkills}>Criar</Button>
            <Link to="../../Skills">
                <Button variant="secondary">Voltar à lista de Skills</Button>
            </Link>
        </div>
    )
};

export default CriarSkills;
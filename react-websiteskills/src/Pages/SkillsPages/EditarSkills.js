import React from "react";
import Button from 'react-bootstrap/Button';
import FormControl from 'react-bootstrap/FormControl';
import { useState } from "react";
import { useParams, useHistory } from 'react-router-dom';
import { Link } from 'react-router-dom';

function EditarSkills(){
    // Obter o id da skill a ser editada
    let { skillsId } = useParams();
    // Obter o histórico de navegação
    let history = useHistory();

    // Estados para os campos do formulário
    const [skillNome, setNome] = useState("");
    const [skillDificuldade, setDificuldade] = useState("");
    const [skillTempo, setTempo] = useState("");
    const [skillDescricao, setDescricao] = useState("");
    const [skillCusto, setCusto] = useState("");
    const [skillImagem, setImagem] = useState("");

    // Método para fazer o upload de uma imagem
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

    // Método para fazer a edição de uma skill

    const editSkills = () => {
        fetch(`https://localhost:7263/api/ApiSkills/EditSkill?id=${skillsId}`, {
            method: 'POST',
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json'
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
                // Se a edição for bem-sucedida, redirecionar para a lista de skills
                history.push("../../Skills");
            } else {
                throw new Error("Erro ao editar a skill");
            }
        })
        .catch((error) => {
            console.error(error);
        });
    };



    return (
<div className="EditarSkill">
    <h1>Editar</h1>
    <h3>skills</h3>
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

    <Button variant="dark" onClick={editSkills}>Guardar</Button>
    <Link to="../../Skills">
        <Button variant="secondary">Voltar à lista de Skills</Button>
    </Link>
</div>
    )
};

export default EditarSkills;
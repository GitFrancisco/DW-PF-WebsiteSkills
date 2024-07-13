import React from "react";
import Button from 'react-bootstrap/Button';
import FormControl from 'react-bootstrap/FormControl';

function CriarSkills(){
    return (
<div className="Criação Skill">
    <h1>Criar</h1>
    <h3>skills</h3>
    <p>Nome</p>
    <FormControl type="text" placeholder="" />
    <p>Dificuldade</p>
    <FormControl type="text" placeholder="" />
    <p>Tempo</p>
    <FormControl type="text" placeholder="" />
    <p>Descrição</p>
    <FormControl type="text" placeholder="" />
    <p>Custo</p>
    <FormControl type="text" placeholder="" />

    <Button variant="dark">Criar</Button>
    <Button variant="secondary">Voltar à lista de Skills</Button>
</div>
    )
};

export default CriarSkills;
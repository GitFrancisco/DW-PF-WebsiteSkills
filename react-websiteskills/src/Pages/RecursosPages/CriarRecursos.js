import React from "react";
import Button from 'react-bootstrap/Button';
import FormControl from 'react-bootstrap/FormControl';
import { Link } from "react-router-dom";
import '../../Styles/RecursosPages/CriarRecursos.css';

function CriarRecursos(){
    return (
<div className="CriaçãoRecursos">
    <h1>Criar</h1>
    <h3>Recursos</h3>
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
    <Link to="../Recursos">
                 <Button variant="secondary">Voltar à lista de Recursos</Button>
           </Link>  
</div>
    )
};

export default CriarRecursos;
import React from "react";
import Button from 'react-bootstrap/Button';
import FormControl from 'react-bootstrap/FormControl';
import { Link } from "react-router-dom";

function EditarRecursos(){
    return (
<div className="EditarRecursos">
    <h1>Editar</h1>
    <h3>recursos</h3>
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

    <Button variant="dark">Guardar</Button>
    <Link to="../Recursos">
                 <Button variant="secondary">Voltar à lista de recursos</Button>
           </Link>  
</div>
    )
};

export default EditarRecursos;
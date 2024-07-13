import React from "react";
import Button from 'react-bootstrap/Button';
import FormControl from 'react-bootstrap/FormControl';

function CriarAnunciosSkills(){
    return (
        <div className="Apagar Skill">
            <h1>Criar Anúncio</h1>
            <h2>Texto do Anúncio</h2>
            <FormControl as="textarea" rows={6} placeholder="" />
            <Button variant="dark">Submeter</Button>
            <Button variant="secondary">Cancelar</Button>
        </div>
    )
};

export default CriarAnunciosSkills;
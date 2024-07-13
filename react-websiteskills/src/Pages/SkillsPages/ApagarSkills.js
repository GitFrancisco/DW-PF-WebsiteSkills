import React from "react";
import Button from 'react-bootstrap/Button';


function ApagarSkills(){
    return (
        <div className="Apagar Skill">
            <h1>Apagar</h1>
            <h2>Confirma a remoção desta Skill?</h2>
            <Button variant="danger">Apagar</Button>
            <Button variant="secondary">Voltar à lista de Skills</Button>
        </div>
    );
}

export default ApagarSkills;

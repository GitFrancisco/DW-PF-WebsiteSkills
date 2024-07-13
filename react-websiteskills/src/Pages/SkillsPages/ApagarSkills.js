import React from 'react';
import { useParams, useHistory } from 'react-router-dom';
import Button from 'react-bootstrap/Button';

function ApagarSkills() {
    // Obter o id da skill a ser apagada
    let { skillsId } = useParams();
    // Obter o histórico de navegação
    let history = useHistory();

    // Método para apagar a skill
    const deleteSkill = () => {
        fetch(`https://localhost:7263/api/ApiSkills/DeleteSkill?id=${skillsId}`, {
            method: 'DELETE',
            headers: {
                'accept': '*/*'
            }
        })
        .then(response => {
            if (response.ok) {
                // Se a skill foi apagada com sucesso, redireciona para a lista de skills
                history.push('../../Skills');
            } else {
                // Alerta de erro
                alert('Não foi possível apagar a skill.');
            }
        })
        .catch(error => {
            console.error('Erro ao apagar a skill:', error);
        });
    };

    return (
        <div className="ApagarSkill">
            <h1>Apagar</h1>
            <h2>Confirma a remoção desta Skill?</h2>
            <Button variant="danger" onClick={deleteSkill}>Apagar</Button>
            <Button variant="secondary" onClick={() => history.goBack()}>Voltar à lista de Skills</Button>
        </div>
    );
}

export default ApagarSkills;
import React from "react";
import Button from 'react-bootstrap/Button';
import { Link } from "react-router-dom";
import '../../Styles/RecursosPages/ApagarRecursos.css';
import { useParams, useHistory } from 'react-router-dom';


function ApagarRecursos(){
    // Obter o id da skill a ser apagada
    let { idRecurso } = useParams();
    // Obter o histórico de navegação
    let history = useHistory();

    // Método para apagar o recurso
    const DeleteRecurso = () => {
        fetch(`https://localhost:7263/api/ApiRecursos/DeleteRecurso?id=${idRecurso}`, {
            method: 'DELETE',
            headers: {
                'accept': '*/*'
            }
        })
        .then(response => {
            if (response.ok) {
                // Se o recurso foi apagado com sucesso, redireciona para a lista de recursos
                history.push('../../Recursos');
            } else {
                // Alerta de erro
                alert('Não foi possível apagar o recurso.');
            }
        })
        .catch(error => {
            console.error('Erro ao apagar o recurso:', error);
        });
    };

    return (
        <div className="ApagarRecurso">
            <h1>Apagar</h1>
            <h2>Confirma a remoção deste Recurso?</h2>
            <Button variant="danger" onClick={DeleteRecurso}>Apagar</Button>
            <Link to="../../Recursos">
                 <Button variant="secondary">Voltar à lista de Recursos</Button>
           </Link>  
        </div>
    );
}

export default ApagarRecursos;

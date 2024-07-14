import React, { useEffect, useState } from 'react';
import { useParams, useHistory } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import Container from 'react-bootstrap/Container';
import "../../Styles/SkillsPages/ApagarSkills.css";

function ApagarSkills() {
    let { skillsId } = useParams();
    let history = useHistory();

    const [skill, setSkill] = useState(null);

    useEffect(() => {
        fetchSkill();
    }, []);

    const fetchSkill = () => {
        fetch(`https://localhost:7263/api/ApiSkills/GetSkill?id=${skillsId}`, {
            headers: {
                'accept': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => setSkill(data))
        .catch(error => console.error('Erro ao buscar a skill:', error));
    };

    const deleteSkill = () => {
        fetch(`https://localhost:7263/api/ApiSkills/DeleteSkill?id=${skillsId}`, {
            method: 'DELETE',
            headers: {
                'accept': '*/*',
                Authorization: "Bearer " + localStorage.getItem("jwt")
            }
        })
        .then(response => {
            if (response.ok) {
                history.push('../../Skills');
            } else {
                alert('Não foi possível apagar a skill.');
            }
        })
        .catch(error => {
            console.error('Erro ao apagar a skill:', error);
        });
    };

    if (!skill) {
        return <div>Loading...</div>;
    }

    return (
        <Container className="text-center">
            <h1 className="header-large">Apagar</h1>
            <h3 className="header-medium">Confirma a remoção desta Skill?</h3>
            <Card className="mx-auto my-4" style={{ maxWidth: '600px' }}>
                <Card.Header>
                    <h4>{skill.nome}</h4>
                </Card.Header>
                <Card.Body>
                    <dl className="row small-text">
                        <dt className="col-sm-12">Nome</dt>
                        <dd className="col-sm-12">{skill.nome}</dd>

                        <dt className="col-sm-12">Dificuldade</dt>
                        <dd className="col-sm-12">{skill.dificuldade}</dd>

                        <dt className="col-sm-12">Tempo</dt>
                        <dd className="col-sm-12">{skill.tempo}</dd>

                        <dt className="col-sm-12">Descrição</dt>
                        <dd className="col-sm-12">{skill.descricao}</dd>

                        <dt className="col-sm-12">Custo</dt>
                        <dd className="col-sm-12">{skill.custo}</dd>

                        <dt className="col-sm-12">Imagem</dt>
                        <dd className="col-sm-12">
                            <img src={`https://localhost:7263/Imagens/${skill.imagem}`}
                                 alt={`Imagem referente a ${skill.nome}`}
                                 title={skill.nome}
                                 className="img-fluid rounded"
                                 style={{ maxWidth: '300px' }} />
                        </dd>
                    </dl>
                    <div className="button-container">
                        <Button variant="danger" className="mx-2" onClick={deleteSkill}>Apagar</Button>
                        <Button variant="secondary" className="mx-2" onClick={() => history.push('../../Skills')}>Voltar à lista de Skills</Button>
                    </div>
                </Card.Body>
            </Card>
        </Container>
    );
}

export default ApagarSkills;

import React, { useState } from "react";
import { useParams, useHistory, Link } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import FormControl from 'react-bootstrap/FormControl';
import "../../Styles/SkillsPages/EditarSkills.css";

function EditarSkills() {
    let { skillsId } = useParams();
    let history = useHistory();

    const [formData, setFormData] = useState({
        nome: "",
        dificuldade: "",
        tempo: "",
        descricao: "",
        custo: "",
        imagem: ""
    });

    const handleChange = (e) => {
        const { name, value, files } = e.target;
        setFormData({
            ...formData,
            [name]: files ? files[0].name : value
        });

        if (files) {
            const file = files[0];
            const data = new FormData();
            data.append("file", file);

            fetch("https://localhost:7263/api/ApiSkills/UploadImage", {
                method: "POST",
                body: data,
            })
                .then((response) => response.json())
                .then((data) => {
                    console.log(data);
                })
                .catch((error) => {
                    console.error(error);
                });
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        fetch(`https://localhost:7263/api/ApiSkills/EditSkill?id=${skillsId}`, {
            method: 'POST',
            headers: {
                'accept': 'text/plain',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        })
        .then((response) => {
            if (response.ok) {
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
        <div className="container">
            <h1 className="text-center my-4">Editar</h1>
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <div className="card shadow-sm p-4 mb-4 bg-white rounded">
                        <div className="card-body">
                            <h4 className="card-title text-center">Skills</h4>
                            <hr />
                            <form onSubmit={handleSubmit} encType="multipart/form-data">
                                <div className="form-group">
                                    <label>Nome</label>
                                    <FormControl type="text" name="nome" value={formData.nome} onChange={handleChange} required />
                                </div>
                                <div className="form-group">
                                    <label>Dificuldade</label>
                                    <FormControl type="text" name="dificuldade" value={formData.dificuldade} onChange={handleChange} required />
                                </div>
                                <div className="form-group">
                                    <label>Tempo</label>
                                    <FormControl type="text" name="tempo" value={formData.tempo} onChange={handleChange} required />
                                </div>
                                <div className="form-group">
                                    <label>Descrição</label>
                                    <FormControl type="text" name="descricao" value={formData.descricao} onChange={handleChange} required />
                                </div>
                                <div className="form-group">
                                    <label>Custo</label>
                                    <FormControl type="text" name="custo" value={formData.custo} onChange={handleChange} required />
                                </div>
                                <div className="form-group">
                                    <label className="d-block mb-2">Escolher Imagem</label>
                                    <FormControl type="file" name="imagem" onChange={handleChange} accept=".png,.jpg,.jpeg" />
                                </div>
                                <div className="form-group text-center">
                                    <Button variant="dark" type="submit">Guardar</Button>
                                </div>
                            </form>
                        </div>
                    </div>
                    <div className="text-center">
                        <Link to="../../Skills">
                            <Button variant="secondary">Voltar à lista de Skills</Button>
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default EditarSkills;

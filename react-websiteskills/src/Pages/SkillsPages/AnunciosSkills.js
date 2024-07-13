import React from "react";
import { useState, useEffect } from "react";
import { useParams, useHistory } from 'react-router-dom';


function AnunciosSkills(){
    // Obter o id da skill a ser editada
    let { skillsId } = useParams();
    // Obter o histórico de navegação
    let history = useHistory();

    // Estados para os campos do formulário
    const [anuncios, setAnuncios] = useState([]);

    useEffect(() => {
        fetchAnuncios();
    }, []);
  
    const fetchAnuncios = () => {
        fetch(`https://localhost:7263/api/ApiAnuncios/SkillAnuncios?id=${skillsId}`, {
            headers: {
                'accept': 'text/plain'
            }
        })
            .then((response) => response.json())
            .then((data) => setAnuncios(data))
            .catch((error) => console.log(error));
    };
    
    return (
    <>
    <h1>Anúncios</h1>
    {anuncios.map((Anuncio, index) => (
        <div key={index}>
            <p>{Anuncio.dataCriacao} {Anuncio.texto}</p>
        </div>
    ))}
    </>
    )
};

export default AnunciosSkills;
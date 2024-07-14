import React, { useState, useEffect } from "react";
import "../Styles/Skills.css";
import { Link } from "react-router-dom";
import Button from "react-bootstrap/Button";

function Skills() {
  const [skills, setSkills] = useState([]);

  useEffect(() => {
    fetchSkills();
  }, []);

  const fetchSkills = () => {
    fetch("https://localhost:7263/api/ApiSkills/GetAllSkills", {
      headers: {
        accept: "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => setSkills(data))
      .catch((error) => console.log(error));
  };
  

  const user = {
    isAuthenticated: true,  //  authentication logic
    roles: ["Administrador", "Mentor"]  //  role checking logic
  };

  return (
    <div className="Skills">
      <h1 className="text-center">Skills</h1>
      {user.isAuthenticated && user.roles.includes("Administrador") && (
        <div className="text-center mb-3">
          <Link to="/SkillsPages/CriarSkills">
            <Button variant="dark">Criar Nova Skill</Button>
          </Link>
        </div>
      )}

      <div className="container">
        <div className="row">
          {skills.map((skill) => (
            <div key={skill.skillsId} className="col-md-4 mb-4">
              <div className="card">
                <Link to={`/SkillsPages/DetalhesSkills/${skill.skillsId}`}>
                  <img
                    src={`https://localhost:7263/Imagens/${skill.imagem}`}
                    className="card-img-top"
                    alt={`Imagem referente a ${skill.nome}`}
                    title={skill.nome}
                    style={{ height: "300px", objectFit: "cover" }}
                  />
                </Link>
                <div className="card-body text-center">
                  <h5 className="card-title">{skill.nome}</h5>
                  <p className="card-text"><small>{skill.descricao}</small></p>
                  {user.isAuthenticated && (
                    <div className="d-flex justify-content-center flex-wrap">
                      <Link to={`/SkillsPages/RecursosSkills/${skill.skillsId}`}>
                        <Button variant="secondary" size="sm" className="mx-2 my-2">Recursos</Button>
                      </Link>
                      {(user.roles.includes("Mentor") || user.roles.includes("Administrador")) && (
                        <>
                          <Link to={`/SkillsPages/AdicionarSkillsMentores/${skill.skillsId}`}>
                            <Button variant="secondary" size="sm" className="mx-2 my-2">Adicionar à lista de Skills</Button>
                          </Link>
                          <Link to={`/SkillsPages/AnunciosSkills/${skill.skillsId}`}>
                            <Button variant="secondary" size="sm" className="mx-2 my-2">Anuncios</Button>
                          </Link>
                          <Link to={`/SkillsPages/CriarAnunciosSkills/${skill.skillsId}`}>
                            <Button variant="secondary" size="sm" className="mx-2 my-2">Criar Anuncios</Button>
                          </Link>
                        </>
                      )}
                      {user.roles.includes("Aluno") && !skill.isSubscribed || user.roles.includes("Administrador") && (
                        <Link to={`/SkillsPages/CheckoutSkills/${skill.skillsId}`}>
                          <Button variant="secondary" size="sm" className="mx-2 my-2">Subscrever</Button>
                        </Link>
                      )}
                      {user.roles.includes("Administrador") && (
                        <>
                          <Link to={`/SkillsPages/EditarSkills/${skill.skillsId}`}>
                            <Button variant="secondary" size="sm" className="mx-2 my-2">Editar</Button>
                          </Link>
                          <Link to={`/SkillsPages/ApagarSkills/${skill.skillsId}`}>
                            <Button variant="secondary" size="sm" className="mx-2 my-2">Apagar</Button>
                          </Link>
                        </>
                      )}
                    </div>
                  )}
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default Skills;

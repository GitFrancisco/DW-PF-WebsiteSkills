import React, { useState } from "react";
import { useHistory } from "react-router-dom";

function Register() {
  // Obter o histórico de navegação
  let history = useHistory();

  // Estados para os campos do formulário
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [nome, setNome] = useState("");
  const [dataNasc, setDataNasc] = useState("");
  const [tel, setTel] = useState("");
  const [tipoRegisto, setTipoRegisto] = useState("aluno");

  // Método para registar utilizador
  const registerUser = () => {
    fetch(
      `https://localhost:7263/api/ApiUser/createUser?tipoRegisto=${tipoRegisto}&email=${email}&password=${password}&nome=${nome}&dataNascimento=${dataNasc}&telemovel=${tel}`,
      {
        method: "POST",
        headers: {
          accept: "*/*",
          "content-type": "application/x-www-form-urlencoded",
        },
      }
    )
      .then((response) => {
        if (response.ok) {
          history.push("/"); // Redireciona para a página de inicial
        }
      })
      .catch((error) => {
        console.error("Erro ao registar utilizador:", error);
      });
  };

  return (
    <div>
      <h2>Registar</h2>
      <label htmlFor="email">Email:</label>
      <input
        type="email"
        id="email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />

      <label htmlFor="password">Password:</label>
      <input
        type="password"
        id="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />

      <label htmlFor="Nome">Nome:</label>
      <input value={nome} onChange={(e) => setNome(e.target.value)} />

      <label htmlFor="DataNascimento">Data de Nascimento:</label>
      <input value={dataNasc} onChange={(e) => setDataNasc(e.target.value)} />

      <label htmlFor="Telemovel">Telemóvel:</label>
      <input value={tel} onChange={(e) => setTel(e.target.value)} />

      <label htmlFor="tipoRegisto">Tipo de Registo:</label>
      <select id="tipoRegisto" value={tipoRegisto} onChange={(e) => setTipoRegisto(e.target.value)}>
         <option value="mentor">Mentor</option>
         <option value="aluno">Aluno</option>
      </select>

      <button onClick={registerUser}>Registar</button>
    </div>
  );
}

export default Register;

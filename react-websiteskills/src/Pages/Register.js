import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import "../Styles/Register.css";

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
  const registerUser = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(
        `https://localhost:7263/api/ApiUser/createUser?tipoRegisto=${tipoRegisto}&email=${email}&password=${password}&nome=${nome}&dataNascimento=${dataNasc}&telemovel=${tel}`,
        {
          method: "POST",
          headers: {
            accept: "*/*",
            "content-type": "application/x-www-form-urlencoded",
          },
        }
      );

      if (response.ok) {
        history.push("/"); // Redireciona para a página inicial
      } else {
        console.error("Erro ao registar utilizador:", response.statusText);
      }
    } catch (error) {
      console.error("Erro ao registar utilizador:", error);
    }
  };

  return (
    <div className="register-container">
      <h2 className="text-center my-4">Registar</h2>
      <div className="row justify-content-center">
        <div className="col-md-6">
          <div className="card shadow-sm p-4 mb-4 bg-white rounded">
            <div className="card-body">
              <Form onSubmit={registerUser}>
                <Form.Group controlId="formBasicEmail">
                  <Form.Label>Email</Form.Label>
                  <Form.Control
                    type="email"
                    placeholder="Enter email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                  />
                </Form.Group>

                <Form.Group controlId="formBasicPassword">
                  <Form.Label>Password</Form.Label>
                  <Form.Control
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                  />
                </Form.Group>

                <Form.Group controlId="formBasicNome">
                  <Form.Label>Nome</Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Enter your name"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                    required
                  />
                </Form.Group>

                <Form.Group controlId="formBasicDataNasc">
                  <Form.Label>Data de Nascimento</Form.Label>
                  <Form.Control
                    type="date"
                    value={dataNasc}
                    onChange={(e) => setDataNasc(e.target.value)}
                    required
                  />
                </Form.Group>

                <Form.Group controlId="formBasicTel">
                  <Form.Label>Telemóvel</Form.Label>
                  <Form.Control
                    type="tel"
                    placeholder="Enter your phone number"
                    value={tel}
                    onChange={(e) => setTel(e.target.value)}
                    required
                  />
                </Form.Group>

                <Form.Group controlId="formBasicTipoRegisto">
                  <Form.Label>Tipo de Registo</Form.Label>
                  <Form.Control
                    as="select"
                    value={tipoRegisto}
                    onChange={(e) => setTipoRegisto(e.target.value)}
                  >
                    <option value="mentor">Mentor</option>
                    <option value="aluno">Aluno</option>
                  </Form.Control>
                </Form.Group>

                <Button variant="dark" type="submit" className="w-100">
                  Registar
                </Button>
              </Form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Register;

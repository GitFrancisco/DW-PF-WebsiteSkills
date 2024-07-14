import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import "../Styles/Login.css";

function Login() {
  // Obter o histórico de navegação
  let history = useHistory();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  // Função para fazer login
  const loginUser = async () => {
    try {
      const response = await fetch(
        `https://localhost:7263/api/ApiUser/loginUser?email=${encodeURIComponent(
          email
        )}&password=${encodeURIComponent(password)}`,
        {
          method: "POST",
          headers: {
            Accept: "*/*",
          },
          body: "",
        }
      );

      if (response.ok) {
        const token = await response.text();
        console.log("Login bem-sucedido:", token);
        // Guarda o token (JWT) no localStorage
        localStorage.setItem("jwt", token);
        // Redireciona para a página inicial
        history.push("/");
        // Faz reload à página para garantir que a NavBar atualiza
        window.location.reload();
      } else {
        console.error("Erro ao fazer login:", response.statusText);
      }
    } catch (error) {
      console.error("Erro ao fazer login:", error);
    }
  };

  return (
    <div className="login-container">
      <h2 className="text-center my-4">Entrar</h2>
      <div className="row justify-content-center">
        <div className="col-md-6">
          <div className="card shadow-sm p-4 mb-4 bg-white rounded">
            <div className="card-body">
              <Form onSubmit={(e) => {
                e.preventDefault();
                loginUser();
              }}>
                <Form.Group controlId="formBasicEmail">
                  <Form.Label>Email</Form.Label>
                  <Form.Control
                    type="email"
                    placeholder="Email"
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

                <Button variant="dark" type="submit" className="w-100">
                  Entrar
                </Button>
              </Form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Login;

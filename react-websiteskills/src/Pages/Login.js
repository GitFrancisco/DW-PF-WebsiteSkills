import React, { useState } from "react";
import { useHistory } from "react-router-dom";

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
    <div>
      <h2>Login</h2>
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

      <button onClick={loginUser}>Login</button>
    </div>
  );
}

export default Login;

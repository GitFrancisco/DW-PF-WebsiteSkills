import React, { useState, useEffect } from 'react';
import { Link, useHistory } from 'react-router-dom';
import '../Styles/Navbar.css';
import '@fortawesome/fontawesome-free/css/all.css';
import Button from "react-bootstrap/Button";

function Navbar() {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const history = useHistory();

    useEffect(() => {
        // Verifica se o JWT existe no localStorage
        const token = localStorage.getItem('jwt');
        if (token) {
            setIsAuthenticated(true);
        }
    }, []);

    const handleLogout = () => {
        // Remove o JWT do localStorage
        localStorage.removeItem('jwt');
        setIsAuthenticated(false);
        history.push('/'); // Redireciona para a página inicial após o logout
    };

    return (
        <div className="navbar">
            <div className="left-links">
                <Link to="/" className="home-link">
                    <i className="fas fa-home"></i>SkillPull
                </Link>
                <Link to="/Skills">Skills</Link>
                <Link to="/Recursos">Recursos</Link>
            </div>
            <div className="right-links">
                {isAuthenticated ? (
                    <Button variant="secondary" className="mx-3 my-1" onClick={handleLogout}>Sair</Button>
                ) : (
                    <>
                        <Link to="/Login">Entrar</Link>
                        <Link to="/Register">Registar</Link>
                    </>
                )}
            </div>
        </div>
    );
}

export default Navbar;

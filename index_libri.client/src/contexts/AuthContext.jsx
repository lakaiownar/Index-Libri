import { createContext, useState, useEffect } from 'react';
import PropTypes from "prop-types";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        const userEmail = localStorage.getItem('userEmail');
        setIsAuthenticated(!!userEmail);
    }, []);

    const logIn = () => {
        setIsAuthenticated(true);
    };

    const logOut = () => {
        localStorage.removeItem('userEmail');
        setIsAuthenticated(false);
    };

    AuthProvider.propTypes = {
        children: PropTypes.node.isRequired,
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, logIn, logOut }}>
            {children}
        </AuthContext.Provider>
    );
};
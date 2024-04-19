import './ScriptLogin.css'
import { useEffect, useState } from 'react';
import { jwtDecode } from "jwt-decode";
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

function ScriptLogin() {
    const google = window.google;
    const [clientId, setClientId] = useState('');
    const navigate = useNavigate();

    const handleCallbackResponse = (response) => {
        console.log('Encoded JWT ID token: ' + response.credential);
        const decodedToken = jwtDecode(response.credential);
        console.log(decodedToken);

        // Create an ApplicationUser
        const user = {
            userEmail: decodedToken.email,
            token: response.credential
        };

        // Send a POST request to the backend to create the ApplicationUser
        axios.post('https://localhost:7169/register', user)
            .then(response => {
                console.log('User registered:', response.data);
                navigate('/libri');
            })
            .catch(error => {
                console.error('Error registering user:', error);
            });
    }

    useEffect(() => {
        // Fetch the ClientID from the backend
        axios.get('https://localhost:7169/auth/clientid')
            .then(response => {
                setClientId(response.data.clientID);

                google.accounts.id.initialize({
                    client_id: response.data.clientID,
                    scope: "https://www.googleapis.com/auth/books",
                    callback: handleCallbackResponse
                })

                google.accounts.id.renderButton(
                    document.getElementById("signInDiv"),
                    { theme: "outline", size: "large" }
                )
            })
            .catch(error => {
                console.error('Error fetching ClientID', error);
            });
    }, []);

    return (
        <div className="ScriptLogin">
            <header className="App-header" />
            <h1>Index-Libri</h1>
            <div id="signInDiv"></div>
        </div>
    )
}

export default ScriptLogin;

import React from 'react';
import axios from 'axios';

class LoginButton extends React.Component {
    handleLogin = async () => {
        try {
            // Redirect the user to the Google OAuth login page
            const response = await axios.get('https://localhost:7169/api/GoogleAuth/Login');
            window.location.href = response.request.responseURL;
        } catch (error) {
            console.error('Error during login', error);
            if (error.response) {
                // The request was made and the server responded with a status code
                // that falls out of the range of 2xx
                console.error('Response data', error.response.data);
                console.error('Response status', error.response.status);
                console.error('Response headers', error.response.headers);
            } else if (error.request) {
                // The request was made but no response was received
                console.error('Request', error.request);
            } else {
                // Something happened in setting up the request that triggered an Error
                console.error('Error', error.message);
            }
            console.error('Error config', error.config);
        }
    };


    render() {
        return (
            <div>
                <button onClick={this.handleLogin}>
                    Login with Google
                </button>
            </div>
        );
    }
}

export default LoginButton;

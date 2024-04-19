import './ScriptLogin.css'
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import BookList from './BookList.jsx';
import axios from 'axios';

const Libri = () => {
    const [books, setBooks] = useState(null);
    const navigate = useNavigate();
    const email = localStorage.getItem('userEmail');

    const handleLogout = () => {
        // Clear the user's email from localStorage
        localStorage.removeItem('userEmail');

        // Redirect to login page
        navigate('/login');
    };

    useEffect(() => {
        // Get the user's email from local storage
        const userEmail = localStorage.getItem('userEmail');

        if (!userEmail) {
            // Redirect to login page if not authenticated
            navigate('/login');
            return;
        }

        // Make API call to ASP.NET backend to get the booklist for the user
        axios.get('https://localhost:7169/booklist', { headers: { email: userEmail } })
            .then(response => {
                setBooks(response.data);
            })
            .catch(error => {
                console.error('Error fetching books:', error);
            });
    }, []);

    return (
        <div>
            {/* Render authenticated user's view components */}
            <h1>Welcome to Index Libri</h1>
            <button onClick={handleLogout}>Logout</button>
            <BookList books={books && Array.isArray(books.books) ? books.books : []} />
        </div>
    );
};

export default Libri;

import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useContext } from 'react';
import { AuthContext } from '../contexts/AuthContext';
import BookList from './BookList.jsx';
import axios from 'axios';
import './Libri.css';
import Book from './Book';

const Libri = () => {
    const [books, setBooks] = useState([]); // Initialize books as an empty array
    const navigate = useNavigate();
    const { logOut } = useContext(AuthContext);

    const handleLogout = () => {
        // Clear the user's email from localStorage
        localStorage.removeItem('userEmail');

        // Update isAuthenticated state
        logOut();

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
                const books = response.data.books.map(book => new Book(book.isbn, book.title, book.author, book.pages, book.rating, book.bookCover));
                setBooks(books);
            })
            .catch(error => {
                console.error('Error fetching books:', error);
            });
    }, []);

    return (
        <div>
            {/* Render authenticated user's view components */}
            <button className="logout-button" onClick={handleLogout}>Logout</button>
            <BookList books={books} />
        </div>
    );
};

export default Libri;
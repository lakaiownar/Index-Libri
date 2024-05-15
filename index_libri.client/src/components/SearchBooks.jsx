import { useState } from 'react';
import axios from 'axios';
import './SearchBooks.css';

const SearchBooks = () => {
    const [query, setQuery] = useState('');
    const [books, setBooks] = useState([]);
    const [isBookAdded, setIsBookAdded] = useState({});

    const handleInputChange = (event) => {
        setQuery(event.target.value);
    };

    const handleSubmit = (event) => {
        event.preventDefault();

        axios.get(`https://www.googleapis.com/books/v1/volumes?q=${query}`)
            .then(response => {
                setBooks(response.data.items);
            })
            .catch(error => {
                console.error('Error fetching books:', error);
            });
    };

    const addBook = (googleBook) => {
        const bookId = googleBook.id;
        setIsBookAdded(prevState => ({ ...prevState, [bookId]: false })); // Reset the state for this book


        const userEmail = localStorage.getItem('userEmail');
        console.log('User Email:', userEmail); // Check the user email

        const book = {
            isbn: googleBook.volumeInfo.industryIdentifiers[0].identifier,
            title: googleBook.volumeInfo.title,
            author: googleBook.volumeInfo.authors[0],
            pages: googleBook.volumeInfo.pageCount,
            rating: 0, // Set to a default value, this should change later.
            bookCover: googleBook.volumeInfo.imageLinks?.thumbnail
        };
        console.log('Book Object:', book); // Check the book object

        axios.post('https://localhost:7169/booklist/add?email=' + userEmail, book)
            .then(response => {
                console.log('Book added:', response.data);
                setIsBookAdded(prevState => ({ ...prevState, [bookId]: true })); // Set the state for this book to true
            })
            .catch(error => {
                console.error('Error adding book:', error);
            });
    };

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <input type="text" value={query} onChange={handleInputChange} placeholder="Search books..." />
                <button type="submit">Search</button>
            </form>
            <div className="booksContainer">
                {books && books.map((book) => (
                    <div key={book.id} className="book">
                        <img src={book.volumeInfo.imageLinks?.thumbnail} alt={book.volumeInfo.title} />
                        <h2>{book.volumeInfo.title}</h2>
                        <p>{book.volumeInfo.authors?.join(', ')}</p>
                        <p>{book.volumeInfo.pageCount} pages</p>
                        <button onClick={() => addBook(book)}>
                            {isBookAdded[book.id] ? 'Book Added' : 'Add to Booklist'} {/* Change the button text based on the isBookAdded state for this book */}
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default SearchBooks;
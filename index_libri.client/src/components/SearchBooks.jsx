import { useState } from 'react';
import axios from 'axios';
import './SearchBooks.css';

const SearchBooks = () => {
    const [query, setQuery] = useState('');
    const [books, setBooks] = useState([]);
    const [isBookAdded, setIsBookAdded] = useState({});
    const userEmail = localStorage.getItem('userEmail');
    const fallBackImageUrl = 'https://bookstoreromanceday.org/wp-content/uploads/2020/08/book-cover-placeholder.png?w=144';

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
        setIsBookAdded(prevState => ({ ...prevState, [bookId]: false }));

        const book = {
            googleid: googleBook.id,
            isbn: googleBook.volumeInfo.industryIdentifiers[0].identifier,
            title: googleBook.volumeInfo.title,
            author: googleBook.volumeInfo.authors[0],
            pages: googleBook.volumeInfo.pageCount,
            rating: 0,
            bookCover: googleBook.volumeInfo.imageLinks?.thumbnail
        };

        axios.post('https://localhost:7169/booklist/add?email=' + userEmail, book)
            .then(response => {
                console.log('Book added:', response.data);
                setIsBookAdded(prevState => ({ ...prevState, [bookId]: true }));
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
                        <img src={book.volumeInfo.imageLinks?.thumbnail} alt={fallBackImageUrl} />
                        <h2>{book.volumeInfo.title}</h2>
                        <p>{book.volumeInfo.authors?.join(', ')}</p>
                        <p>{book.volumeInfo.pageCount} pages</p>
                        <button onClick={() => addBook(book)}>
                            {isBookAdded[book.id] ? 'Book Added' : 'Add to Booklist'}
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default SearchBooks;
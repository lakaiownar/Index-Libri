import { useState, useEffect } from 'react';
import axios from 'axios';
import './SearchBooks.css';

const Recommended = () => {
    const [recommendedBooks, setRecommendedBooks] = useState([]);
    const [isBookAdded, setIsBookAdded] = useState({});
    const fallBackImageUrl = 'https://bookstoreromanceday.org/wp-content/uploads/2020/08/book-cover-placeholder.png?w=144';
    const userEmail = localStorage.getItem('userEmail');

    useEffect(() => {
        axios.get(`https://localhost:7169/booklist/favouritebook?email=${userEmail}`)
            .then(response => {
                const highestRatedBook = response.data;
                const googleId = highestRatedBook.googleId;
                return axios.get(`https://www.googleapis.com/books/v1/volumes/${googleId}/associated`);
            })
            .then(response => {
                const recommendations = response.data.items.map(item => ({
                    googleid: item.id,
                    isbn: item.volumeInfo.industryIdentifiers[0].identifier,
                    title: item.volumeInfo.title,
                    author: item.volumeInfo.authors[0],
                    pages: item.volumeInfo.pageCount,
                    rating: 0,
                    bookCover: item.volumeInfo.imageLinks?.thumbnail
                }));
                setRecommendedBooks(recommendations);
            })
            .catch(error => {
                console.error('Error fetching recommended books:', error);
            });
    }, []);

    const addBook = (book) => {
        setIsBookAdded(prevState => ({ ...prevState, [book.googleid]: false }));

        axios.post('https://localhost:7169/booklist/add?email=' + userEmail, book)
            .then(response => {
                console.log('Book added:', response.data);
                setIsBookAdded(prevState => ({ ...prevState, [book.googleid]: true }));
            })
            .catch(error => {
                console.error('Error adding book:', error);
            });
    };

    return (
        <div>
            <div className="booksContainer">
                {recommendedBooks.map(book => (
                    <div key={book.isbn} className="book">
                        <img src={book.bookCover} alt={fallBackImageUrl} />
                        <h2>{book.title}</h2>
                        <p>{book.author}</p>
                        <p>{book.pages} pages</p>
                        <button onClick={() => addBook(book)}>
                            {isBookAdded[book.googleid] ? 'Book Added' : 'Add to Booklist'}
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Recommended;
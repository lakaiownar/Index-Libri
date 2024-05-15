import { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import Book from './Book';
import StarRating from './StarRating';
import './BookList.css';
import axios from 'axios';

const BookList = ({ books }) => {
    const fallBackImageUrl = 'https://bookstoreromanceday.org/wp-content/uploads/2020/08/book-cover-placeholder.png?w=144'
    const [rating, setRating] = useState({});
    // Update the data when 'books' changes
    useEffect(() => {
        // Can add any side effects here if needed
    }, [books]);

    BookList.propTypes = {
        books: PropTypes.array.isRequired,
    };

    const handleRatingChange = (bookId, newRating) => {
        setRating(prevState => ({ ...prevState, [bookId]: newRating }));

        const userEmail = localStorage.getItem('userEmail');
        const updatedBook = { ...books.find(book => book.id === bookId), rating: newRating };

        axios.put(`https://localhost:7169/booklist/update?email=${userEmail}&isbn=${updatedBook.isbn}`, updatedBook)
            .then(response => {
                console.log('Book updated:', response.data);
            })
            .catch(error => {
                console.error('Error updating book:', error);
            });
    };

    return (
        <div>
            <h2>My Book List</h2>
            <table>
                <thead>
                <tr>
                    <th>Cover</th>
                    <th>ISBN</th>
                    <th>Title</th>
                    <th>Author</th>
                    <th>Pages</th>
                    <th>Rating</th>
                </tr>
                </thead>
                <tbody>
                {books.map((book) => {
                    return (
                        <tr key={book.isbn}>
                            <td>
                                <img
                                    src={book.bookCover ? book.bookCover : fallBackImageUrl}
                                    alt={book.title}
                                    width="100"
                                    height="150"
                                />
                            </td>
                            <td>{book.isbn}</td>
                            <td>{book.title}</td>
                            <td>{book.author}</td>
                            <td>{book.pages}</td>
                            <td>
                                <StarRating
                                    rating={rating[book.isbn] || book.rating}
                                    onRatingChange={(newRating) => handleRatingChange(book.isbn, newRating)}
                                />
                            </td>
                        </tr>
                    );
                })}
                </tbody>
            </table>
        </div>
    );
};
export default BookList;
import { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import Book from './Book';
import StarRating from './StarRating';
import './BookList.css';
import axios from 'axios';

const BookList = ({ books, setBooks }) => {
    const userEmail = localStorage.getItem('userEmail');
    const fallBackImageUrl = 'https://bookstoreromanceday.org/wp-content/uploads/2020/08/book-cover-placeholder.png?w=144'
    const [rating, setRating] = useState({});
    const [status, setStatus] = useState({});

    // Mapping for status (for updating the status in the backend)
    const statusMapping = {
        "Want to Read": 0,
        "Reading": 1,
        "On Hold": 2,
        "Finished": 3
    };
    // Reverse mapping for status (for displaying in the dropdown)
    const reverseStatusMapping = {
        0: "Want to Read",
        1: "Reading",
        2: "On Hold",
        3: "Finished"
    };

    // Update the data when 'books' changes
    useEffect(() => {
        // Can add any side effects here if needed
    }, [books]);

    BookList.propTypes = {
        books: PropTypes.array.isRequired,
        setBooks: PropTypes.func.isRequired,
    };

    // Update the rating in the backend
    const handleRatingChange = (isbn, newRating) => {
        setRating(prevState => ({ ...prevState, [isbn]: newRating }));
        const updatedBook = { ...books.find(book => book.isbn === isbn), rating: newRating };

        axios.put(`https://localhost:7169/booklist/update?email=${userEmail}&isbn=${updatedBook.isbn}`, updatedBook)
            .then(response => {
                console.log('Book updated:', response.data);
            })
            .catch(error => {
                console.error('Error updating book:', error);
            });
    };

    // Update the status in the backend
    const handleStatusChange = (isbn, newStatus) => {
        // Update the status state immediately
        setStatus(prevState => ({ ...prevState, [isbn]: statusMapping[newStatus] }));

        const userEmail = localStorage.getItem('userEmail');
        const updatedBook = { ...books.find(book => book.isbn === isbn), status: statusMapping[newStatus] };

        axios.put(`https://localhost:7169/booklist/update?email=${userEmail}&isbn=${updatedBook.isbn}`, updatedBook)
            .then(response => {
                console.log('Book updated:', response.data);
            })
            .catch(error => {
                console.error('Error updating book:', error);
            });
    };

    const handleRemoveBook = (isbn) => {
        axios.delete(`https://localhost:7169/booklist/delete?email=${userEmail}&isbn=${isbn}`)
            .then(response => {
                console.log('Book removed:', response.data);
                // Remove the book from the local state
                setBooks(books.filter(book => book.isbn !== isbn));
            })
            .catch(error => {
                console.error('Error removing book:', error);
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
                    <th>Status</th>
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
                            <td>
                                <select
                                    value={reverseStatusMapping[status[book.isbn]] || reverseStatusMapping[book.status]}
                                    onChange={(e) => handleStatusChange(book.isbn, e.target.value)}>
                                    <option value="Want to Read">Want to Read</option>
                                    <option value="Reading">Reading</option>
                                    <option value="On Hold">On Hold</option>
                                    <option value="Finished">Finished</option>
                                </select>
                            </td>
                            <td>
                                <button onClick={() => handleRemoveBook(book.isbn)}>Remove</button>
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
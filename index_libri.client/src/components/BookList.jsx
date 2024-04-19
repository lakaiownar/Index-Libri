import { useEffect, useRef } from 'react';
import Tabulator from 'tabulator-tables/dist/js/tabulator.min.js';
import 'tabulator-tables/dist/css/tabulator.min.css';

const BookList = ({ books }) => {
    const tableRef = useRef(null);

    // Initialize the table
    useEffect(() => {
        tableRef.current = new Tabulator("#books-table", {
            data: [], // start with empty data
            autoColumns: true,
        });
    }, []);

    // Update the data when 'books' changes
    useEffect(() => {
        if (tableRef.current) {
            tableRef.current.replaceData(books);
        }
    }, [books]);

    return (
        <div>
            <h1>Book List</h1>
            <div id="books-table"></div>
        </div>
    );
};

export default BookList;

import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Libri from '../components/Libri.jsx';
import NotFound from '../components/NotFound.jsx';
import App from '../App.jsx'
import SearchBooks from '../components/SearchBooks.jsx';
import Recommended from '../components/Recommended.jsx';
import { Link } from 'react-router-dom';
import { useContext } from 'react';
import { AuthContext } from '../contexts/AuthContext';

const Routing = () => {
    const { isAuthenticated } = useContext(AuthContext);

    return (
        <Router>
            {isAuthenticated && (
                <nav>
                    <Link to="/libri">My List</Link>
                    <Link to="/search">Search Books</Link>
                    <Link to="/recommended">Recommendations</Link>
                </nav>
            )}
            <Routes>
                <Route exact path="/login" element={<App />} />
                <Route exact path="/libri" element={<Libri />} />
                <Route exact path="/search" element={<SearchBooks />} />
                <Route exact path="/recommended" element={<Recommended />} />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </Router>
    );
};

export default Routing;
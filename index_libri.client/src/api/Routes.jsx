import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
/*import Login from '../components/Login.jsx';*/
/*import App from '../App.jsx';*/
import Libri from '../components/Libri.jsx';
import NotFound from '../components/NotFound.jsx';
import App from '../App.jsx'

const Routing = () => {
    return (
        <Router>
            <Routes>
                <Route exact path="/login" element={<App />} />
                <Route exact path="/libri" element={<Libri />} />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </Router>
    );
};

export default Routing;

import PropTypes from 'prop-types';
import { useState } from 'react';
import './StarRating.css';

const StarRating = ({ rating, onRatingChange }) => {
    const [hoverRating, setHoverRating] = useState(0);
    const DEFAULT_ICON = '★';

    const handleMouseEnter = (index) => {
        setHoverRating(index);
    };

    const handleMouseLeave = () => {
        setHoverRating(0);
    };

    const handleClick = (index) => {
        console.log('Star clicked:', index);
        onRatingChange(index);
    };

    StarRating.propTypes = {
        rating: PropTypes.number.isRequired,
        onRatingChange: PropTypes.func.isRequired,
    }

    return (
        <div>
            {[1, 2, 3, 4, 5].map((index) => (
                <i
                    key={index}
                    className={index <= (hoverRating || rating) ? 'filled star' : 'star'}
                    onMouseEnter={() => handleMouseEnter(index)}
                    onMouseLeave={handleMouseLeave}
                    onClick={() => handleClick(index)}
                >
                    {DEFAULT_ICON}
                </i>
            ))}
        </div>
    );
};

export default StarRating;
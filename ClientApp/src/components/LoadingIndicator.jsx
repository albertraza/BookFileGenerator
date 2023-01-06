import React from 'react';
import './LoadingIndicator.css';

const LoadingIndicator = ({ children, isLoading }) => {
	return (
		<div className="loading-indicator">
			<div className={`loading-content ${isLoading ? '' : 'd-none'}`}>
				<div className="spinner-wrap">
					<div className="spinner-border text-primary" role="status">
						<span className="visually-hidden">Loading...</span>
					</div>
				</div>
			</div>
			{children}
		</div>
	);
};

export default LoadingIndicator;

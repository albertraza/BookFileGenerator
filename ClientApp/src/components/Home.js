import React from 'react';
import FileForm from './FileForm';
import BookList from './BookList';

const Home = () => {
	return (
		<>
			<h2>Books Information</h2>
			<div className="row">
				<div className="col-12">
					<p>Input a file to get the books information.</p>
					<FileForm />
				</div>
				<div className="col-12">
					<BookList />
				</div>
			</div>
		</>
	);
};

export default Home;

import React from 'react';
import { useContext } from 'react';
import { BookContext } from '../context/BookContextProvider';

const BookList = () => {
	const { books } = useContext(BookContext);

	return books?.length ? (
		<div className="mt-4">
			<h2>Book List</h2>
			<table className="table table-striped">
				<thead>
					<tr>
						<th scope="col">Row Number</th>
						<th scope="col">Data Retrieval Type</th>
						<th scope="col">ISBN</th>
						<th scope="col">Title</th>
						<th scope="col">Subtitle</th>
						<th scope="col">Author Name(s)</th>
						<th scope="col">Number of Pages</th>
						<th scope="col">Publish Date</th>
					</tr>
				</thead>
				<tbody>
					{books.map((book, index) => (
						<tr key={book.isbn + index}>
							<th scope="row">{index + 1}</th>
							<td>{book.dataRetrivalType}</td>
							<td>{book.isbn}</td>
							<td>{book.title}</td>
							<td>{book.subtitle || 'N/A'}</td>
							<td>{book.authorsName.join(', ')}</td>
							<td>{book.numberOfPages || 'N/A'}</td>
							<td>{book.publishDate}</td>
						</tr>
					))}
				</tbody>
			</table>
		</div>
	) : null;
};

export default BookList;

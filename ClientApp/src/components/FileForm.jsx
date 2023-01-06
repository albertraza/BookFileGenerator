import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import LoadingIndicator from './LoadingIndicator';
import { useContext } from 'react';
import { BookContext } from '../context/BookContextProvider';

const supportedFiles = ['txt'];
const isSuportedFile = (file) => {
	if (!file) return false;
	const fileNameParts = file?.name.split('.');
	const fileExtension = fileNameParts[fileNameParts.length - 1];

	return supportedFiles.includes(fileExtension.toLocaleLowerCase());
};

const FileForm = () => {
	const { setBooks } = useContext(BookContext);
	const {
		formState: { errors },
		handleSubmit,
		register,
	} = useForm();
	const [isUploading, setIsUploading] = useState(false);

	const onSubmit = async (data) => {
		setIsUploading(true);
		const { bookInfoFile } = data;

		const payload = new FormData();
		payload.append('file', bookInfoFile[0]);

		try {
			const response = await fetch('https://localhost:7107/api/files', {
				method: 'POST',
				body: payload,
			});
			const body = await response.json();

			setBooks(body.fileContent);
			window.open(`https://localhost:7107/api/files/${body.fileId}`, '_blank');
		} catch (err) {
		} finally {
			setIsUploading(false);
		}
	};

	return (
		<LoadingIndicator isLoading={isUploading}>
			<form onSubmit={handleSubmit(onSubmit)}>
				<div className="mb-3">
					<label htmlFor="fileInput" className="form-label">
						Select a file to upload
					</label>
					<input
						type="file"
						className={`form-control ${
							errors.bookInfoFile ? 'is-invalid' : ''
						}`}
						id="fileInput"
						aria-describedby="fileHelper"
						accept=".txt"
						{...register('bookInfoFile', {
							required: 'The file is required.',
							validate: {
								isSupportedExtension: (fileArr) =>
									fileArr && isSuportedFile(fileArr[0])
										? true
										: `The file is not supported, supported files are: ${supportedFiles}`,
							},
						})}
					/>
					{!errors.bookInfoFile && (
						<div id="fileHelper" className="form-text">
							We only support .txt files.
						</div>
					)}
					<div className="invalid-feedback">
						{errors?.bookInfoFile?.message}
					</div>
				</div>
				<button type="submit" className="btn btn-primary">
					Submit
				</button>
			</form>
		</LoadingIndicator>
	);
};

export default FileForm;

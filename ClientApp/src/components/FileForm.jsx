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
	const [error, setError] = useState();
	const {
		formState: { errors },
		handleSubmit,
		register,
	} = useForm();
	const [isUploading, setIsUploading] = useState(false);

	const onSubmit = async (data) => {
		setIsUploading(true);
		const { fileInput } = data;

		const payload = new FormData();
		payload.append('file', fileInput[0]);

		try {
			const response = await fetch('https://localhost:7107/api/files', {
				method: 'POST',
				body: payload,
			});
			const body = await response.json();
			if (response.ok) {
				setBooks(body.fileContent);
				window.open(
					`https://localhost:7107/api/files/${body.fileId}`,
					'_blank'
				);
			} else {
				setError(body.message);
			}
		} catch (err) {
		} finally {
			setIsUploading(false);
		}
	};

	return (
		<>
			{error && (
				<div
					className="alert alert-danger alert-dismissible fade show"
					role="alert"
				>
					{error || 'An error has occurred when uploading the file.'}
					<button
						type="button"
						className="btn-close"
						data-bs-dismiss="alert"
						aria-label="Close"
						onClick={() => setError(null)}
					></button>
				</div>
			)}
			<LoadingIndicator isLoading={isUploading}>
				<form onSubmit={handleSubmit(onSubmit)}>
					<div class="row g-3">
						<div class="col-10">
							<input
								type="file"
								className={`form-control ${
									errors.fileInput ? 'is-invalid' : ''
								}`}
								id="fileInput"
								aria-describedby="fileHelper"
								accept=".txt"
								{...register('fileInput', {
									required: 'The file is required.',
									validate: {
										isSupportedExtension: (fileArr) =>
											fileArr && isSuportedFile(fileArr[0])
												? true
												: `The file is not supported, supported files are: ${supportedFiles}`,
									},
								})}
							/>
							{!errors.fileInput && (
								<div id="fileHelper" className="form-text">
									We only support .txt files.
								</div>
							)}
							<div className="invalid-feedback">
								{errors?.fileInput?.message}
							</div>
						</div>
						<div class="col">
							<div className="d-grid">
								<button type="submit" className="btn btn-primary block">
									Process File
								</button>
							</div>
						</div>
					</div>
				</form>
			</LoadingIndicator>
		</>
	);
};

export default FileForm;

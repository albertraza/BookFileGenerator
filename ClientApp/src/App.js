import './custom.css';
import { Layout } from './components/Layout';
import { Route } from 'react-router';
import BookContextProvider from './context/BookContextProvider';
import Home from './components/Home';
import React, { Component } from 'react';

export default class App extends Component {
	static displayName = App.name;

	render() {
		return (
			<Layout>
				<BookContextProvider>
					<Route exact path="/" component={Home} />
				</BookContextProvider>
			</Layout>
		);
	}
}

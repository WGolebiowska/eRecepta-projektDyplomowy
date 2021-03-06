import React, { Component } from 'react';
import { withRouter } from 'react-router';
import usersService from './UsersService';
import { Button } from 'reactstrap';

class UsersDeletePlain extends Component {
	constructor(props) {
		super(props);
		this.state = { users: [], loading: true };

		const { match } = this.props;
		this.userId = match.params.userId;
	}

	componentDidMount() {

	}

	handleClickOk = () => {
		const { history } = this.props;

		(async () => {
			await usersService.deleteUser(this.userId);
			history.push('/users');
		})();

		//usersService.deleteUser(this.userId)
		//	.then(() => history.push('/users'));
	}

	handleClickCancel = () => {
		const { history } = this.props;

		history.push('/users');
	}

	render() {
		return (
			<div>
				<h2>Czy na pewno chcesz usunąć tego użytkownika?</h2>

				<button onClick={this.handleClickOk}>Tak</button>
				<button onClick={this.handleClickCancel}>Nie</button>
			</div>
		);
	}
}

export const UsersDelete = withRouter(UsersDeletePlain);

import React, { Component } from 'react';
import usersService from './UsersService';
import { AvForm, AvField } from 'availity-reactstrap-validation';
import { FormGroup, Form, Label, Input, Button } from 'reactstrap';

class UsersPasswordChangePlain extends Component {

	constructor(props) {
		super(props);

		const { match } = this.props;
		this.userId = match.params.userId;
	}

	componentDidMount() {
	}

	handleClickCancel = () => {
		const { history } = this.props;

		history.push('/users');
	}

	handleValidSubmit = (event, values) => {
		const { history } = this.props;

		(async () => {
			await usersService.changeUserPassword(this.userId, values);
			history.push('/users');
		})();
	}

	render() {
		return (
			<AvForm onValidSubmit={this.handleValidSubmit}>
				<AvField name="password" type="password" label="Hasło" required />
				<AvField name="confirmPassword" type="password" label="Powtórz hasło" required
					validate={{ match: { value: 'password' } }}
				/>
				<FormGroup>
					<Button>Zatwierdź</Button>&nbsp;
					<Button onClick={this.handleClickCancel}>Anuluj</Button>
				</FormGroup>
			</AvForm>
		);
	}
}

export const UsersPasswordChange = (UsersPasswordChangePlain);


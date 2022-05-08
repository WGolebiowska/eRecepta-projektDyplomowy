import React, { Component } from 'react';
import { withRouter } from 'react-router';
import usersService from './UsersService';
import { AvForm, AvField } from 'availity-reactstrap-validation';
import { FormGroup, Form, Label, Input, Button } from 'reactstrap';


class UsersEditPlain extends Component {
	constructor(props) {
		super(props);
		this.state = { user: null, loading: true };

		const { match } = this.props;
		this.userId = match.params.userId;
	}

	componentDidMount() {
		this.retrieveFormData();
	}

	handleInputChange = (event) => {
		const target = event.target;
		const value = target.type === 'checkbox' ? target.checked : target.value;
		const name = target.name;

		this.state.user[name] = value;
		this.setState({ user: this.state.user });
	}

	handleClickCancel = () => {
		const { history } = this.props;

		history.push('/users');
	}

	handleValidSubmit = (event, values) => {
		const { history } = this.props;

		(async () => {
			await usersService.updateUser(this.userId, values);
			history.push('/users');
		})();
	}

	renderUserForm(user) {
		return (
			<AvForm onValidSubmit={this.handleValidSubmit}>
				<AvField value={this.state.user.name} name="name" label='Imię' required errorMessage='FieldInvalid' validate={{
					required: { value: true, errorMessage: 'Pole jest wymagane' },
					minLength: { value: 6 }
				}} />
				<AvField value={ this.state.user.surname } name="surname" label="Nazwisko" required />
				<AvField value={this.state.user.email} name="email" type="email" label="Email" required />
				<AvField value={this.state.user.pesel} name="pesel" label="PESEL" required />
				<AvField value={this.state.user.phoneNumber} name="phoneNumber" label="Nr telefonu" required />
				<AvField value={this.state.user.role} name="role" type="select" label="Rola" required>
					<option value="">---Wybierz wartość---</option>
					<option value="administrator">Administrator</option>
					<option value="doctor">Lekarz</option>
					<option value="patient">Pacjent</option>
				</AvField>
				<AvField value={this.state.user.surname} name="password" type="password" label="Hasło" required />
				<AvField value={this.state.user.surname} name="confirmPassword" type="password" label="Powtórz hasło" required
					validate={{ match: { value: 'password' } }}
				/>
				<FormGroup>
					<Button>Zapisz</Button>&nbsp;
					<Button onClick={this.handleClickCancel}>Anuluj</Button>
				</FormGroup>
			</AvForm>
		);
	}

	render() {
		let contents = this.state.loading
			? <p><em>Trwa ładowanie danych...</em></p>
			: this.renderUserForm(this.state.user);

		return (
			<div>
				<h1 id="tabelLabel">Użytkownicy</h1>
				{contents}
			</div>
		);
	}

	async retrieveFormData() {
		const data = await usersService.getUser(this.userId);
		this.setState({ user: data, loading: false });
	}
}

export const UsersEdit = (withRouter(UsersEditPlain));

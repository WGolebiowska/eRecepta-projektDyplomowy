import React, { Component } from 'react';
import usersService from './UsersService';
import { AvForm, AvField } from 'availity-reactstrap-validation';
import { FormGroup, Form, Label, Input, Button } from 'reactstrap';

class UsersAddPlain extends Component {

	constructor(props) {
		super(props);
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
			await usersService.addUser(values);
			history.push('/users');
		})();
	}

	render() {
		return (
			<AvForm onValidSubmit={this.handleValidSubmit}>
				<AvField name="name" label='Imię' required errorMessage='FieldInvalid' validate={{
					required: { value: true, errorMessage: 'Pole jest wymagane' }
				}} />
				<AvField name="surname" label="Nazwisko" required />
				<AvField name="email" type="email" label="Email" required />
				<AvField name="pesel" label="PESEL" required />
				<AvField name="phoneNumber" label="Nr telefonu" required />
				<AvField name="role" type="select" label="Rola" required>
					<option value="">---Wybierz wartość---</option>
					<option value="administrator">Administrator</option>
					<option value="doctor">Lekarz</option>
					<option value="patient">Pacjent</option>
				</AvField>
				<AvField name="specialty" label="Specjalizacja (dotyczy lekarzy)" />
				<AvField name="password" type="password" label="Hasło" required />
				<AvField name="confirmPassword" type="password" label="Powtórz hasło" required
					validate={{ match: { value: 'password' } }}
				/>
				
				<FormGroup>
					<Button>Zapisz</Button>&nbsp;
					<Button onClick={this.handleClickCancel}>Anuluj</Button>
				</FormGroup>
			</AvForm>
		);
	}
}

export const UsersAdd = (UsersAddPlain);


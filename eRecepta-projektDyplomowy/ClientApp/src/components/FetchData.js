import React, { Component } from 'react'
import authService from './api-authorization/AuthorizeService'
export class FetchData extends Component {
  static displayName = FetchData.name

  constructor(props) {
    super(props)
    this.state = {
      appointments: [],
      prescriptions: [],
      loading: true,
      message: '',
      userId: '',
      userRole: '',
      reload: false
    }
    this.getUser = this.getUser.bind(this);
    this.getAppointments = this.getAppointments.bind(this);
    this.getPrescriptions = this.getPrescriptions.bind(this);
  }

  async componentDidMount() {
    await this.getUser()
    this.getAppointments()
    this.getPrescriptions()
  }

  async componentDidUpdate(prevProps, prevState) {
    console.log("prevState.loading " + prevState.loading)
    console.log("this.state.loading " + this.state.loading)
    if (prevState.loading !== this.state.loading) {
      console.log("component DID update!")
      await this.getUser()
      this.getAppointments()
      this.getPrescriptions()
    }
  }

  changeStatus = (appointment) => {
    const token = authService.getAccessToken()
    const endpoint = 'api/Appointment/' + id
    const response = fetch(endpoint, {
      method: 'PUT',
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
      body: JSON.stringify({
        type: appointment.type,
        appointmentDate: appointment.appointmentDate,
        doctorId: appointment.doctorId,
        patientId: appointment.patientId,
        patientName: appointment.patientName,
        patientSurname: appointment.patientSurname,
        appointmentNotes: appointment.appointmentNotes,
        status: status
      })
    })
    const res = response.json()
    if (res.status === 200) {
      this.setState({ message: "Pomyślnie zmieniono status", loading: true })
    } else {
      this.setState({ message: "Wystąpił błąd", loading: false })
    }
    console.log(appointment)
  }

  static renderAppointmentsTable = (appointments, userRole, props) => {
    return (
      <><div>
        <h2>eKonsultacje</h2>
      </div><table className="table table-striped" aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Data</th>
              {userRole == "Patient" &&
                <th>Lekarz</th>}
              {userRole == "Doctor" &&
                <th>Pacjent</th>}
              <th>Typ</th>
              <th>Status</th>
              <th>Dodatkowe informacje</th>
              {userRole == "Doctor" &&
                <th>Zmień status</th>}
            </tr>
          </thead>
          <tbody>
            {appointments.map((appointment) => (
              <tr key={appointment.appointmentId}
              style={
              Date.parse(appointment.appointmentDate) < (new Date()) ? {color: 'gray'} : {}
              }>
                <td>
                  {appointment.appointmentDate.replace('T', ', ').substring(0, 17)}
                </td>
                {userRole == "Doctor" &&
                  <td>{appointment.patientFullName}</td>}
                {userRole == "Patient" &&
                  <td>{"lekarz " + appointment.specialty}<br />{appointment.doctorName + " " + appointment.doctorSurname}</td>}
                <td>
                  {appointment.type == 1 ? 'Video konferencja' : 'Tele-porada'}
                </td>
                <td>{appointment.status ?? "Niezatwierdzona"}</td>
                <td>{appointment.appointmentNotes}</td>
                {userRole == "Doctor" &&
                  <td><span style={{display: 'inline', paddingRight: '2px'}}><button style={{padding: '5px'}} onClick={async () => {
                    const token = await authService.getAccessToken()
                    const endpoint = 'api/Appointment/' + appointment.appointmentId
                    const response = await fetch(endpoint, {
                      method: 'PUT',
                      headers: !token
                        ? { 'Content-Type': 'application/json, charset=UTF-8' }
                        : {
                          'Content-Type': 'application/json, charset=UTF-8',
                          Authorization: `Bearer ${token}`,
                        },
                      body: JSON.stringify({
                        appointmentId: appointment.appointmentId,
                        type: appointment.type,
                        appointmentDate: appointment.appointmentDate,
                        doctorId: appointment.doctorId,
                        patientId: appointment.patientId,
                        patientName: appointment.patientName,
                        patientSurname: appointment.patientSurname,
                        appointmentNotes: appointment.appointmentNotes,
                        status: "Potwierdzona"
                      })
                    })
                    const res = await response.json()
                    console.log("response.status: " + response.status)
                    if (response.status === 200) {
                      window.location.reload()
                    } else {
                    }
                  }
                  }>Potwierdź</button>
                  <button  style={{padding: '5px'}} onClick={async () => {
                    const token = await authService.getAccessToken()
                    const endpoint = 'api/Appointment/' + appointment.appointmentId
                    const response = await fetch(endpoint, {
                      method: 'PUT',
                      headers: !token
                        ? { 'Content-Type': 'application/json, charset=UTF-8' }
                        : {
                          'Content-Type': 'application/json, charset=UTF-8',
                          Authorization: `Bearer ${token}`,
                        },
                      body: JSON.stringify({
                        appointmentId: appointment.appointmentId,
                        type: appointment.type,
                        appointmentDate: appointment.appointmentDate,
                        doctorId: appointment.doctorId,
                        patientId: appointment.patientId,
                        patientName: appointment.patientName,
                        patientSurname: appointment.patientSurname,
                        appointmentNotes: appointment.appointmentNotes,
                        status: "Odwołana"
                      })
                    })
                    const res = await response.json()
                    console.log("response.status: " + response.status)
                    if (response.status === 200) {
                      window.location.reload()
                    } else {
                    }
                  }
                  }>Odwołaj</button></span></td>
                }
              </tr>
            ))}
          </tbody>
        </table></>
    )
  }
  static renderPrescriptionsTable(prescriptions, userRole) {
    return (
      <><h2>eRecepty</h2><table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            {/* <th>AppointmentId</th> */}
            <th>Lek</th>
            <th>Dawka</th>
            {userRole == "Patient" &&
              <th>Lekarz</th>}
            {userRole == "Doctor" &&
              <th>Pacjent</th>}
            <th>Wystawiono</th>
            <th>Wygaśnie</th>
            <th>Ważna przez</th>
            {userRole == "Patient" &&
              <th>Kod PIN</th>
            }
            <th>Informacje od lekarza</th>
          </tr>
        </thead>
        <tbody>
          {prescriptions.map((prescription) => (

            <tr key={prescription.prescriptionId} 
            style={
              Date.parse(prescription.expiryDate) < (new Date()) ? {color: 'gray'} : 
                Date.parse(prescription.expiryDate) < (new Date().setDate(new Date().getDate() + 7)) ? {color: 'red'} : {}
              }>
              <td><b>{prescription.medicine.name}</b><br />
                {prescription.medicine.form}
              </td>
              <td>
                {prescription.prescribedDosage ?? prescription.medicine.dosage}
              </td>
              {userRole == "Doctor" &&
                <td>{prescription.patient.fullName}</td>}
              {userRole == "Patient" &&
                <td>{"lekarz " + prescription.doctor.specialty}<br/>{prescription.doctor.fullName}</td>}
              <td>
                {prescription.issueDate.replace('T', ',').substring(0, 10)}
              </td>
              <td>
                {prescription.expiryDate.replace('T', ',').substring(0, 10)}
              </td>
              <td>{prescription.validPeriod + " dni"}</td>
              {userRole == "Patient" &&
                <td>{prescription.pinCode}</td>
              }
              <td>{prescription.prescriptionNotes}</td>
            </tr>
          ))}
        </tbody>
      </table></>
    )
  }
  render() {
    let contentsA = this.state.loading ? (
      <p>
        <em>Wczytywanie danych o eKonsultacjach...</em>
      </p>
    ) : (
      FetchData.renderAppointmentsTable(this.state.appointments, this.state.userRole, this.props)
    )

    let contentsP = this.state.loading ? (
      <p>
        <em>Wczytywanie danych o eReceptach...</em>
      </p>
    ) : (
      FetchData.renderPrescriptionsTable(this.state.prescriptions, this.state.userRole)
    )

    return (
      <div>
        <h1 id="tabelLabel">eKartoteka</h1>
        {contentsA}
        {contentsP}
      </div>
    )
  }

  async getAppointments() {
    const token = await authService.getAccessToken()
    const endpoint =
      this.state.userRole == 'Administrator'
        ? '/api/Appointment'
        : '/api/Appointment/get?' +
        this.state.userRole +
        'id=' +
        this.state.userId
    const response = await fetch(endpoint, {
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
    })
    const data = await response.json()
    this.setState({ appointments: data, loading: false })
  }

  async getPrescriptions() {
    const token = await authService.getAccessToken()
    const endpoint =
      this.state.userRole == 'Administrator'
        ? '/api/Prescription'
        : '/api/Prescription/get?' +
        this.state.userRole +
        'id=' +
        this.state.userId
    const response = await fetch(endpoint, {
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
    })
    const data = await response.json()
    this.setState({ prescriptions: data, loading: false })
  }

  async getUser() {
    const token = await authService.getAccessToken()
    let res = await fetch('/api/CurrentUser', {
      method: 'GET',
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
    })
    let resJson = await res.json()
    if (res.status === 200) {
      this.setState({ userId: resJson.id, userRole: resJson.role })
    } else {
      this.setState({ userId: 'Wystąpił błąd' })
      this.setState({ userRole: 'Wystąpił błąd.' })
    }
  }
}
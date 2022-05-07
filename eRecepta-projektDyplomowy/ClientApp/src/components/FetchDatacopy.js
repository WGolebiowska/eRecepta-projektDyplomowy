import React, { Component } from 'react'
import authService from './api-authorization/AuthorizeService'

export class FetchData extends Component {
  static displayName = FetchData.name

  constructor(props) {
    super(props)
    this.state = {
      forecasts: [],
      loading: true,
      appointments: [],
      loading: true,
    }
  }

  componentDidMount() {
    //this.populateWeatherData();
    // this.getAppointments()
    this.getUser()
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map((forecast) => (
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          ))}
        </tbody>
      </table>
    )
  }

  static renderAppointmentsTable(appointments) {
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Date</th>
            <th>Doctor Id</th>
            <th>Doctor name</th>
            <th>Doctor surname</th>
            <th>Appointment notes</th>
          </tr>
        </thead>
        <tbody>
          {appointments.map((appointment) => (
            <tr key={appointment.appointmentId}>
              <td>{appointment.appointmentDate}</td>
              <td>{appointment.doctorId}</td>
              <td>{appointment.doctorName}</td>
              <td>{appointment.doctorSurname}</td>
            </tr>
          ))}
        </tbody>
      </table>
    )
  }
  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      FetchData.renderAppointmentsTable(this.state.appointments)
    )

    return (
      <div>
        <h1 id="tabelLabel">Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    )
  }

  // async populateWeatherData() {
  //   const token = await authService.getAccessToken()
  //   const response = await fetch('weatherforecast', {
  //     headers: !token ? {} : { Authorization: `Bearer ${token}` },
  //   })
  //   const data = await response.json()
  //   this.setState({ forecasts: data, loading: false })
  // }
  // async getAppointments() {
  //   const token = await authService.getAccessToken()
  //   const response = await fetch('/api/Appointment', {
  //     headers: !token ? {} : { Authorization: `Bearer ${token}` },
  //   })
  //   const data = await response.json()
  //   this.setState({ appointments: data, loading: false })
  // }

  async getUser() {
    const token = await authService.getAccessToken()
    const response = await fetch('/api/currentuser', {
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
    })
    const data = await response.json()
    this.setState({ appointments: data, loading: false })
  }
}

// insert into [aspnet-eRecepta_projektDyplomowy].dbo.Appointments (AppointmentDate, AppointmentNotes, DoctorId, PatientId, Type) values ('2000-01-01 12:00:00','notatka', '7ce17a1a-1aaa-48d6-9ed7-d90dff6c8475', '7ce17a1a-1aaa-48d6-9ed7-d90dff6c8475', 1);

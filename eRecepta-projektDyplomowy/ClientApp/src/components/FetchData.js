import React, { Component } from 'react'
import authService from './api-authorization/AuthorizeService'
import Moment from 'react-moment'
import { format } from 'date-fns'
export class FetchData extends Component {
  static displayName = FetchData.name

  constructor(props) {
    super(props)
    this.state = {
      // forecasts: [],
      // loading: true,
      appointments: [],
      loading: true,
      userId: '',
      userRole: '',
    }
  }

  async componentDidMount() {
    //this.populateWeatherData();
    await this.getUser()
    this.getAppointments()
  }

  // static renderForecastsTable(forecasts) {
  //   return (
  //     <table className="table table-striped" aria-labelledby="tabelLabel">
  //       <thead>
  //         <tr>
  //           <th>Date</th>
  //           <th>Temp. (C)</th>
  //           <th>Temp. (F)</th>
  //           <th>Summary</th>
  //         </tr>
  //       </thead>
  //       <tbody>
  //         {forecasts.map((forecast) => (
  //           <tr key={forecast.date}>
  //             <td>{forecast.date}</td>
  //             <td>{forecast.temperatureC}</td>
  //             <td>{forecast.temperatureF}</td>
  //             <td>{forecast.summary}</td>
  //           </tr>
  //         ))}
  //       </tbody>
  //     </table>
  //   )
  // }

  static renderAppointmentsTable(appointments) {
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            {/* <th>AppointmentId</th> */}
            <th>Data</th>

            <th>DoctorName</th>
            <th>DoctorSurname</th>
            <th>Specialty</th>
            <th>Notatki</th>
            <th>Status</th>
            <th>Typ</th>
            <th>URL</th>
          </tr>
        </thead>
        <tbody>
          {appointments.map((appointment) => (
            <tr key={appointment.appointmentId}>
              {/* <td>{format(appointment.appointmentDate, 'yyyy-MM-dd')}</td> */}
              <td>
                {format(
                  Date.parse(appointment.appointmentDate),
                  'yyyy MMM dd HH:mm',
                )}
              </td>

              <td>{appointment.doctorName}</td>
              <td>{appointment.doctorSurname}</td>
              <td>{appointment.specialty}</td>

              <td>{appointment.appointmentNotes}</td>
              <td>NIEZATWIERDZONA</td>
              <td>
                {appointment.type == 1 ? 'Video konferencja' : 'Tele-porada'}
              </td>
              <td>{appointment.videoConferenceURL}</td>
            </tr>
          ))}
        </tbody>
      </table>
    )
  }
  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Wczytywanie danych...</em>
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
  async getAppointments() {
    console.log('userId:' + this.state.userId)
    console.log('userRole: ' + this.state.userRole)

    const token = await authService.getAccessToken()
    const endpoint =
      this.state.userRole == 'administrator'
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

// insert into [aspnet-eRecepta_projektDyplomowy].dbo.Appointments (AppointmentDate, AppointmentNotes, DoctorId, PatientId, Type) values ('2000-01-01 12:00:00','notatka', '7ce17a1a-1aaa-48d6-9ed7-d90dff6c8475', '7ce17a1a-1aaa-48d6-9ed7-d90dff6c8475', 1);

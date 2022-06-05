import setHours from "date-fns/setHours";
import setMinutes from "date-fns/setMinutes";
import './CustomForm.css'
import { useState } from 'react'
import { React, useEffect } from 'react'
import authService from './api-authorization/AuthorizeService'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'
import { registerLocale, setDefaultLocale } from  "react-datepicker";
import pl from 'date-fns/locale/pl';
registerLocale('pl', pl)
import styled from 'styled-components'

const Styles = styled.div`
  .react-datepicker-wrapper,
  .react-datepicker__input-container,
  .react-datepicker__input-container input {
    width: 175px;
  }

  .react-datepicker__close-icon::before,
  .react-datepicker__close-icon::after {
    background-color: grey;
  }
`

function CustomForm() {
  const [illness, setIllness] = useState('')
  const [illnesses, setIllnesses] = useState([])
  const [formaKonsultacji, setFormaKonsultacji] = useState('')
  const [message, setMessage] = useState('')
  const [pacjent, setPacjent] = useState('')
  const [doctor, setDoctor] = useState('')
  const [doctors, setDoctors] = useState([])
  const [startDate, setStartDate] = useState(null)
  const [appointmentNotes, setAppointmentNotes] = useState('')

  useEffect(() => {
    const getData = async () => {
      var doctors = []

      const token = await authService.getAccessToken()

      let res = await fetch('/api/Doctor', {
        method: 'GET',
        headers: !token ? {} : { Authorization: `Bearer ${token}` },
      })
      let resJson = await res.json()
      if (res.status === 200) {
        doctors = resJson.map((d) => d.id + ':' + d.fullTitle)
        doctors = doctors.map((pair) => pair.split(':'))
        setDoctors(doctors)
      } else {
        setDoctors([':some error occured'])
      }
    }
    getData()

    const getIllnesses = async () => {
      const token = await authService.getAccessToken()

      let res = await fetch('/api/Illness', {
        method: 'GET',
        headers: !token ? {} : { Authorization: `Bearer ${token}` },
      })
      let resJson = await res.json()
      if (res.status === 200) {
        var _illnesses = resJson.map((i) => i.illnessId + ':' + i.name).map((pair) => pair.split(':'))
        setIllnesses(_illnesses)
      } else {
        setIllnesses([':wystąpił błąd'])
      }
    }
    getIllnesses()
  }, [])

  let handleSubmit = async (e) => {
    e.preventDefault()
    try {
      const token = await authService.getAccessToken()
      let res2 = await fetch('/api/CurrentUser', {
        method: 'GET',
        headers: !token ? {} : { Authorization: `Bearer ${token}` },
      })
      let res2Json = await res2.json()
      if (res2.status === 200) {
        setPacjent(res2Json.id)
      } else {
        setPacjent('Wystąpił błąd')
      }

      let res = await fetch('/api/Appointment', {
        method: 'POST',
        headers: !token
          ? { 'Content-Type': 'application/json, charset=UTF-8' }
          : {
              'Content-Type': 'application/json, charset=UTF-8',
              Authorization: `Bearer ${token}`,
            },
        body: JSON.stringify({

          type: formaKonsultacji,
          appointmentDate: startDate.toLocaleString(),
          doctorId: doctor,
          patientId: res2Json.id,
          patientName: res2Json.patientName,
          patientSurname: res2Json.patientSurname,
          appointmentNotes: appointmentNotes,
          illness: illness
        }),
      })
      let resJson = await res.json()
      if (res.status === 200) {
        setIllness('')
        setFormaKonsultacji('')
        setDoctor('')
        setStartDate(null)
        setAppointmentNotes('')
        setMessage(
          'Twoje zlecenie jest przetwarzane, status możesz sprawdzić w eKartotece',
        )
      } else {
        setMessage('Wystąpił błąd')
      }
    } catch (err) {
      console.log(err)
    }
  }

  return (
    <div className="Custom-form">
      <form onSubmit={handleSubmit}>
        <div className="Custom-form-position">
          <div className="question-form">
            <label className="Custom-form-text">Wybierz dolegliwość</label>
            <div class="select">
              <select
                  class="select"
                  value={illness}
                  onChange={
                    (e) => {
                      setIllness(e.target.value);
                    }
                  }
                  aria-label="Default select example"
                >
                  <option selected class="label-desc">
                    ...
                  </option>
                  {illnesses.map((illness) => (
                    <option value={illness[1]}>{illness[1]}</option>
                  ))}
              </select>
            </div>
          </div>

          <div className="question-form">
            <label className="Custom-form-text">Forma eKonsultacji</label>

            <div class="select">
              <select
                class="select"
                value={formaKonsultacji}
                onChange={(e) => setFormaKonsultacji(e.target.value)}
                aria-label="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="0">Tele-porada</option>
                <option value="1">Video konferencja</option>
              </select>
            </div>
          </div>
          <div className="question-form">
            <label className="Custom-form-text">Wybierz lekarza</label>
            <div class="select">
              <select
                class="select"
                value={doctor}
                onChange={(e) => setDoctor(e.target.value)}
                aria-label="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                {doctors.map((doctor) => (
                  <option value={doctor[0]}>{doctor[1]}</option>
                ))}
              </select>
            </div>
          </div>
          <div className="question-form">
            <label className="Custom-form-text">Data konsultacji</label>
              <div style={{ display: 'flex' }}>
              <DatePicker
                locale="pl"
                isClearable
                minTime={setHours(setMinutes(new Date(), 0), 8)}
                maxTime={setHours(setMinutes(new Date(), 30), 19.30)}
                filterDate={(d) => {
                  return new Date() <= d
                }}
                placeholderText="Wybierz datę konsultacji"
                showTimeSelect
                dateFormat="yyyy-MM-dd,hh:mm"
                selected={startDate}
                selectsStart
                startDate={startDate}
                onChange={(date) => setStartDate(date)}
              />
            </div>
          </div>
          <div className="question-form">
            <label className="Custom-form-text">
              Dodatkowe informacje dla lekarza
            </label>
            <div>
              <textarea
                class="form-control"
                id="exampleFormControlTextarea1"
                rows="3"
                value={appointmentNotes}
                onChange={(e) => setAppointmentNotes(e.target.value)}
                aria-label="Default select example"
              ></textarea>
            </div>
          </div>
        </div>
        <div class="form-group-button">
          <button type="submit">
            Umów konsultację
          </button>
        </div>
        <div className="message">{message ? <p>{message}</p> : null}</div>
      </form>
    </div>
  )
}

export default CustomForm
import './CustomForm.css'
import { useState } from 'react'
import { React, useEffect } from 'react'
import authService from './api-authorization/AuthorizeService'
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import styled from "styled-components";
import moment from "moment";

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
`;

function DoctorPrescriptionForm() {
  const [doctorId, setDoctorId] = useState('')
  const [issueDate, setIssueDate] = useState(moment().toDate())
  const [patientId, setPatientId] = useState('')
  const [patientName, setPatientName] = useState('')
  const [patientSurname, setPatientSurname] = useState('')
  const [patientPesel, setPatientPesel] = useState('')

  const [patients, setPatients] = useState([]);
  const [medicine, setMedicine] = useState('')
  const [prescriptionNotes, setPrescriptionNotes] = useState('')
  const [message, setMessage] = useState('')


  useEffect(() => {
    const getData = async () => {
      var patients = [];

      const token = await authService.getAccessToken();

      let res = await fetch('/api/Patient', {
        method: 'GET',
        headers: !token ? {} : { Authorization: `Bearer ${token}` },
      })
      let resJson = await res.json()
      if (res.status === 200) {
        patients = resJson.map((p) => p.id + ":" + p.fullName + ":" + p.pesel + ":" + p.name + ":" + p.surname)
        patients = patients.map(pair => pair.split(":"));
        setPatients(patients);
      } else {
        setPatients([":wystąpił błąd"])
      }
    }
    getData();
  }, []);

  let handleSubmit = async (e) => {
    e.preventDefault()
    try {
      const token = await authService.getAccessToken()
      let res = await fetch('/api/CurrentUser', {
        method: 'GET',
        headers: !token ? {} : { Authorization: `Bearer ${token}` },
      })
      let resJson = await res.json()
      if (res.status === 200) {
        setDoctorId(resJson.id)
      } else {
        setDoctorId('Wystąpił błąd')
      }

      let res2 = await fetch('/api/Prescription', {
        method: 'POST',
        headers: !token ? { 'Content-Type': 'application/json, charset=UTF-8' } : {
          'Content-Type': 'application/json, charset=UTF-8',
          Authorization: `Bearer ${token}`
        },
        body: JSON.stringify({
          doctorId: resJson.id,
          patientId: patientId,
          issueDate: issueDate,
          prescriptionNotes: prescriptionNotes
        }),
      })
      let res2Json = await res2.json()
      console.log(res2Json);
      if (res2.status === 200) {
        //setIssueDate(new Date().toISOString().slice(0, 10))
        setPatientId('')
        setPatientName('')
        setPatientSurname('')
        setPatientPesel('')
        setIssueDate(moment().toDate())
        setMedicine('')
        setPrescriptionNotes('')
        setMessage(
          'eRecepta została wystawiona',
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
            <label className="Custom-form-text">Wybierz datę wystawienia eRecepty</label>
            <DatePicker
              isClearable
              minDate={moment().toDate()}
              placeholderText="Data wystawienia"
              dateFormat="yyyy-MM-dd"
              selected={issueDate}
              selectsStart
              startDate={issueDate}
              onChange={date => setIssueDate(date)}
            />
          </div>

          <div className="question-form">
            <label className="Custom-form-text">Pacjent</label>
            <div class="select">
              <select
                class="select"
                value={patientId}
                onChange={
                  (e) => {
                    setPatientId(e.target.value);
                    patients.filter(p => p[0] == e.target.value).map((patient) => {
                        setPatientPesel(patient[2]),
                        setPatientName(patient[3]),
                        setPatientSurname(patient[4])
                    });
                  }
                }
                aria-label="Default select example"
              >
                <option selected class="label-desc">
                  Wybierz pacjenta z listy
                </option>
                {patients.map((patient) => <option value={patient[0]}>{patient[1]}</option>)}
              </select>
            </div>
            <div class="input-group mb-3">
              <input type="text" class="form-control" placeholder="Imię" aria-label="Imię" aria-describedby="basic-addon2" defaultValue={patientName} />
              <input type="text" class="form-control" placeholder="Nazwisko" aria-label="Nazwisko" aria-describedby="basic-addon2" defaultValue={patientSurname} />
              <input type="text" class="form-control" placeholder="PESEL" aria-label="PESEL" aria-describedby="basic-addon2" defaultValue={patientPesel} />
            </div>
          </div>
          <div className="question-form">
            <label className="Custom-form-text">Dodatkowe informacje dla pacjenta</label>
            <div class="form-group">
              <textarea
                class="form-control"
                id="exampleFormControlTextarea1"
                rows="4"
                className="Custom-form-text"
                value={prescriptionNotes}
                onChange={(e) => setPrescriptionNotes(e.target.value)}>
              </textarea>
            </div>
          </div>

          <div className="question-form">
            <label className="Custom-form-text">Wybierz lek</label>
            <div class="select">
              <select
                class="select"
                value={medicine}
                onChange={(e) => setMedicine(e.target.value)}
                aria-label="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="Ketonal">Ketonal</option>
                <option value="Apap">Apap</option>
                <option value="Apap">Apap</option>
              </select>
            </div>
          </div>
        </div>

        <div class="form-group-button">
          <button type="submit">
            wystaw eReceptę
          </button>
        </div>
        <div className="message">{message ? <p>{message}</p> : null}</div>
      </form>
    </div>
  )
}

export default DoctorPrescriptionForm

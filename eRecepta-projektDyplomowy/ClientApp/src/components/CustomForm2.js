import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from 'reactstrap'
import { Link } from 'react-router-dom'

import './CustomForm.css'
import { useState } from 'react'
import { React, useEffect } from 'react'
import authService from './api-authorization/AuthorizeService'
import moment from "moment";
// import DatePickerRange from "./DataPicker"
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import styled from "styled-components";


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

function CustomForm() {
  const [name, setName] = useState('')
  const [email, setEmail] = useState('')
  const [mobileNumber, setMobileNumber] = useState('')
  const [dolegliwosc, setDolegliwosc] = useState('')
  const [dataKonsultacji, setDataKonsultacji] = useState('')
  const [plecPacienta, setPlecPacienta] = useState('')
  const [formaKonsultacji, setFormaKonsultacji] = useState('')
  const [ciaza, setCiaza] = useState(false)
  const [message, setMessage] = useState('')
  const [pacjent, setPacjent] = useState('')
  const [doctor, setDoctor] = useState('')
  const [doctors, setDoctors] = useState([]);
  const [startDate, setStartDate] = useState(null);


  useEffect(() => {
    const getData = async () => {
      var doctors = [];

      const token = await authService.getAccessToken();
      
      let res = await fetch('/api/Doctor', {
        method: 'GET',
        headers: !token ? {} : { Authorization: `Bearer ${token}` },
      })
      let resJson = await res.json()
      if (res.status === 200) {
        doctors = resJson.map((d) => d.id + ":" + d.fullTitle)
        doctors = doctors.map(pair => pair.split(":"));
        setDoctors(doctors);
      } else {
        setDoctors([":some error occured"])
      }
    }
    getData();
  }, []);

  let handleSubmit = async (e) => {
    e.preventDefault()
    try {
      // let res = await fetch('https://httpbin.org/post', {

        const token = await authService.getAccessToken()
        let res2 = await fetch('/api/CurrentUser', {
            method: 'GET',
            headers: !token ? {} : { Authorization: `Bearer ${token}` },
        })
        let res2Json = await res2.json()
        if (res2.status === 200) {
            setPacjent(res2Json.id)
        } else {
            setPacjent('Some error occured')
        }

        let appointmentDateTime = (moment().format("YYYY-MM-DD") + "T" + dataKonsultacji + ":00")
        let res = await fetch('/api/Appointment', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json, charset=UTF-8' },
          body: JSON.stringify({
            // name: name,
            // email: email,
            // mobileNumber: mobileNumber,
            // dolegliwosc: dolegliwosc,
            // ciaza: ciaza,
            dataKonsultacji: dataKonsultacji,
            plecPacienta: plecPacienta,
            type: formaKonsultacji,
            appointmentDate: startDate,
            doctorId: doctor,
            patientId: res2Json.id,
            PatientName: res2Json.patientName,
            PatientSurname: res2Json.patientSurname,
          }),
        })
        let resJson = await res.json()
        if (res.status === 200) {
          setName('')
          setEmail('')
          setMobileNumber('')
          setMessage(
            'Twoje zlecenie jest przetwarzane, status możesz sprawdzić w eKartotece',
          )
        } else {
          setMessage('Some error occured')
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
                value={dolegliwosc}
                onChange={(e) => setDolegliwosc(e.target.value)}
                aria-lebel="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="Bol ucha">Bol ucha</option>
                <option value="Bol oka">Bol oka</option>
                <option value="Bol nosa">Bol nosa</option>
              </select>
            </div>
          </div>
          <div className="question-form">
            <label className="Custom-form-text">
              Wybierz godzinę eKonsultacji
            </label>

            <div class="select">
              <select
                class="select"
                value={dataKonsultacji}
                onChange={(e) => setDataKonsultacji(e.target.value)}
                aria-label="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="08:00">8:00</option>
                <option value="09:00">9:00</option>
                <option value="10:00">10:00</option>
                <option value="11:00">11:00</option>
                <option value="12:00">12:00</option>
                <option value="13:00">13:00</option>
                <option value="14:00">14:00</option>
                <option value="15:00">15:00</option>
                <option value="16:00">16:00</option>
                <option value="17:00">17:00</option>
                <option value="18:00">18:00</option>
                <option value="19:00">19:00</option>
                <option value="20:00">20:00</option>
              </select>
            </div>
          </div>

          <div className="question-form">
            <label className="Custom-form-text">Wybierz płeć</label>

            <div class="select">
              <select
                class="select"
                value={plecPacienta}
                onChange={(e) => setPlecPacienta(e.target.value)}
                aria-lebel="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="Kobieta">Kobieta</option>
                <option value="Mężczyzna">Mężczyzna</option>
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
                aria-lebel="Default select example"
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
                aria-lebel="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                {doctors.map((doctor) => <option value={doctor[0]}>{doctor[1]}</option>)}
              </select>
            </div>
        </div>
        <div style={{ display: "flex" }}>
     <DatePicker
       isClearable
       filterDate={d => {
         return new Date() <= d;
       }}
       placeholderText="Select Start Date"
       showTimeSelect
       dateFormat="yyyy-MM-dd,hh:mm"
      //  dateFormat="MMMM d, yyyy h:mmaa"
      // let appointmentDateTime = (moment().format("YYYY-MM-DD") + "T" + dataKonsultacji + ":00")

       selected={startDate}
       selectsStart
       startDate={startDate}
       onChange={date => setStartDate(date)}
     />

   </div>
            {/* <DatePickerRange /> */}
          </div>

        {/* <input
          type="text"
          value={name}
          placeholder="Podaj wiek"
          onChange={(e) => setName(e.target.value)}
        />
        <input
        type="text"
          value={email}
          placeholder="Masa ciała"
          onChange={(e) => setEmail(e.target.value)}
        />
        <input
          type="text"
          value={mobileNumber}
          placeholder="Aktualna temperatura ciała"
          onChange={(e) => setMobileNumber(e.target.value)}
        />

        <input
          type="checkbox"
          value="true"
          name="ciaza_check"
          checked={ciaza}
          // placeholder="Aktualna temperatura ciała"
          onChange={(e) => setCiaza(e.target.value)}
        />
        <label for="ciaza_check"> Czy jesteś w ciąży</label>
        
        <select
          value={dolegliwosc}
          onChange={(e) => setDolegliwosc(e.target.value)}
          class="form-select"
          aria-label="Default select example"
          >
          <option selected>Wybierz dolegliwość</option>
          <option value="Ból ucha">Ból ucha</option>
          <option value="Ból nosa">Ból nosa</option>
          <option value="Ból gardła">Ból gardła</option>
        </select> */}

        {/* <div class="form-check">
  <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault" />
  <label class="form-check-label" for="flexCheckDefault">
    Default checkbox
  </label>
</div> */}
        <div class="form-group-button">
          <button type="submit">
            {/* <NavLink tag={Link} className="text-white" to="/platnosc"> */}
            {/* <NavItem> */}
            {/* <NavLink tag={Link} className="text-white" to="/erecepta"> */}
            Umów konsultację
            {/* Dalej */}
            {/* </NavLink> */}
            {/* </NavItem> */}
            {/* <LoginMenu /> */}
            {/* Create */}
            {/* </NavLink> */}
          </button>
        </div>
        <div className="message">{message ? <p>{message}</p> : null}</div>
      </form>
    </div>
  )
}

export default CustomForm

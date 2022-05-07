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
import authService from './api-authorization/AuthorizeService'

import './CustomForm.css'
import { useState } from 'react'
import { React } from 'react'

function CustomForm() {
  const [name, setName] = useState('')
  const [email, setEmail] = useState('')
  const [mobileNumber, setMobileNumber] = useState('')
  const [dolegliwosc, setDolegliwosc] = useState('')
  const [lek, setLek] = useState('')
  const [wzrost, setWzrost] = useState('')
  const [tempBody, setTempBody] = useState('')
  const [waga, setWaga] = useState('')
  const [ciaza, setCiaza] = useState(false)
  const [message, setMessage] = useState('')

  let handleSubmit = async (e) => {
    e.preventDefault()
    try {
      // let res = await fetch('https://httpbin.org/post', {
      let res = await fetch('/api/Appointment', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json, charset=UTF-8' },
        body: JSON.stringify({
          // name: name,
          // email: email,
          // mobileNumber: mobileNumber,
          // dolegliwosc: dolegliwosc,
          // lek: lek,
          // tempBody: tempBody,
          // wzrost: wzrost,
          // waga: waga,
          // ciaza: ciaza,
          appointmentDate: '2022-12-12',
          DoctorId: 'dc616b80-8a71-4e3b-9f9a-9654699ea388',
          PatientId: 'dc616b80-8a71-4e3b-9f9a-9654699ea388',
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
                <option selected value="Choice 1">
                  ...
                </option>
                <option value="Bol ucha">Bol ucha</option>
                <option value="Bol oka">Bol oka</option>
                <option value="Bol nosa">Bol nosa</option>
              </select>
            </div>
          </div>
          <div className="question-form">
            <label className="Custom-form-text">Wybierz lek</label>
            <div class="select">
              <select
                class="select"
                value={lek}
                onChange={(e) => setLek(e.target.value)}
                aria-lebel="Default select example"
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
          <div className="question-form">
            <label className="Custom-form-text">
              Podaj aktualną temp. ciała w st. C.
            </label>
            <div class="select">
              <select
                class="select"
                value={tempBody}
                onChange={(e) => setTempBody(e.target.value)}
                aria-lebel="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="33,0">33,0</option>
                <option value="33,5">33,5</option>
                <option value="34,0">34,0</option>
                <option value="34,5">34,5</option>
                <option value="35,0">35,0</option>
                <option value="35,5">35,5</option>
                <option value="36,0">36,0</option>
                <option value="36,5">36,5</option>
                <option value="37,0">37,0</option>
                <option value="37,5">37,5</option>
                <option value="38,0">38,0</option>
                <option value="38,5">38,5</option>
                <option value="39,0">39,0</option>
                <option value="39,5">39,5</option>
                <option value="40,0">40,0</option>
                <option value="40,5">40,5</option>
              </select>
            </div>
          </div>

          <div className="question-form">
            <label className="Custom-form-text">Podaj swój wzrost w cm</label>
            <div class="select">
              <select
                class="select"
                value={wzrost}
                onChange={(e) => setWzrost(e.target.value)}
                aria-lebel="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="-150"> -150 </option>
                <option value="151-160"> 151-160 </option>
                <option value="161-170"> 161-170 </option>
                <option value="171-180"> 171-180 </option>
                <option value="181-190"> 181-190 </option>
                <option value="191-200"> 191-200 </option>
                <option value="200+"> 200+ </option>
              </select>
            </div>
          </div>
          <div className="question-form">
            <label className="Custom-form-text">
              Podaj swoją aktualną wagę w kg
            </label>
            <div
              class="select"
              value={waga}
              onChange={(e) => setWaga(e.target.value)}
              aria-lebel="Default select example"
            >
              <select class="select">
                <option selected class="label-desc">
                  ...
                </option>
                <option value="-50">-50</option>
                <option value="51-60">51-60</option>
                <option value="61-70">61-70</option>
                <option value="71-80">71-80</option>
                <option value="81-90">81-90</option>
                <option value="91-100">91-100</option>
                <option value="+100">+100</option>
              </select>
            </div>
            {/* <input type="date" id="birthday" name="birthday" /> */}
            {/* <input type="time" id="appt" name="appt" min="09:00" max="18:00" required></input> */}
          </div>
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
            umów wizytę
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

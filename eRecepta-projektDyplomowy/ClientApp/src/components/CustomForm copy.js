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
import { React } from 'react'

function CustomForm() {
  const [name, setName] = useState('')
  const [email, setEmail] = useState('')
  const [mobileNumber, setMobileNumber] = useState('')
  const [dolegliwosc, setDolegliwosc] = useState('')
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
          // ciaza: ciaza,
          appointmentDate: '2022-12-12',
          DoctorId: '7ce17a1a-1aaa-48d6-9ed7-d90dff6c8475',
          PatientId: '7ce17a1a-1aaa-48d6-9ed7-d90dff6c8475',
        }),
      })
      let resJson = await res.json()
      if (res.status === 200) {
        setName('')
        setEmail('')
        setMobileNumber('')
        setMessage('User created successfully')
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
          <div class="select">
            <select class="select">
              <option selected class="label-desc">
                Wybierz dolegliwość
              </option>
              <option value="Choice 1">Bol ucha</option>
              <option value="Choice 2">Bol oka</option>
              <option value="Choice 3">Bol nosa</option>
            </select>
          </div>

          <div class="select">
            <select class="select">
              <option selected class="label-desc">
                Wybierz godzinę eKonsultacji
              </option>
              <option value="Choice 1">8:00</option>
              <option value="Choice 2">9:00</option>
              <option value="Choice 3">10:00</option>
              <option value="Choice 4">11:00</option>
              <option value="Choice 5">12:00</option>
              <option value="Choice 6">13:00</option>
              <option value="Choice 7">14:00</option>
              <option value="Choice 8">15:00</option>
              <option value="Choice 9">16:00</option>
              <option value="Choice 10">17:00</option>
              <option value="Choice 11">18:00</option>
              <option value="Choice 12">19:00</option>
              <option value="Choice 13">20:00</option>
            </select>
          </div>

          <div class="select">
            <select class="select">
              <option selected class="label-desc">
                Wybierz płeć
              </option>
              <option value="Choice 1">Kobieta</option>
              <option value="Choice 2">Mężczyzna</option>
            </select>
          </div>

          <div class="select">
            <select class="select">
              <option selected class="label-desc">
                Forma eKonsultacji
              </option>
              <option value="Choice 1">Tele porada</option>
              <option value="Choice 2">Video konferencja</option>
            </select>
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

        <button type="submit">
          {/* <NavLink tag={Link} className="text-white" to="/platnosc"> */}
          {/* <NavItem> */}
          {/* <NavLink tag={Link} className="text-white" to="/erecepta"> */}
          eKartooooooteka
          <div className="message">{message ? <p>{message}</p> : null}</div>
          {/* Dalej */}
          {/* </NavLink> */}
          {/* </NavItem> */}
          {/* <LoginMenu /> */}
          {/* Create */}
          {/* </NavLink> */}
        </button>
      </form>
    </div>
  )
}

export default CustomForm

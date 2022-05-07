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
  const [dataKonsultacji, setDataKonsultacji] = useState('')
  const [plecPacienta, setPlecPacienta] = useState('')
  const [formaKonsultacji, setFormaKonsultacji] = useState('')
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
          dataKonsultacji: dataKonsultacji,
          plecPacienta: plecPacienta,
          formaKonsultacji: formaKonsultacji,
          appointmentDate: '2022-12-12',
          DoctorId: 'c2872281-dda2-4787-95e3-ade39d8a220c',
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
                aria-lebel="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="8:00">8:00</option>
                <option value="9:00">9:00</option>
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
                <option value="Tele-porada">Tele-porada</option>
                <option value="Video konferencja">Video konferencja</option>
              </select>
            </div>
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
            umów konsultacje
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

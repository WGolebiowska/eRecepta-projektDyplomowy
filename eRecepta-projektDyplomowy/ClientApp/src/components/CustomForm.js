import './CustomForm.css'
import { useState, useEffect } from 'react'
import { React } from 'react'
import authService from './api-authorization/AuthorizeService'
import moment from "moment-timezone";

function CustomForm() {
  const [illness, setIllness] = useState('')
  const [illnessId, setIllnessId] = useState('')
  const [illnessName, setIllnessName] = useState('')
  const [illnesses, setIllnesses] = useState([])
  const [medicine, setMedicine] = useState('')
  const [medicineIll, setMedicineIll] = useState([])
  const [medicines, setMedicines] = useState([[]])
  const [height, setHeight] = useState('')
  const [weight, setWeight] = useState('')
  const [tempBody, setTempBody] = useState('')
  const [addicted, setAddicted] = useState(false)
  const [addictions, setAddictions] = useState('')
  const [hasAllergy, setHasAllergy] = useState(false)
  const [allergies, setAllergies] = useState('')
  const [takesMedicines, setTakesMedicines] = useState(false)
  const [permMedicines, setPermMedicines] = useState('')
  const [isChronicIll, setIsChronicIll] = useState(false)
  const [chronicIllness, setChronicIllness] = useState('')
  const [gender, setGender] = useState('')
  const [pregnancy, setPregnancy] = useState(false)
  const [additionalInfo, setAdditionalInfo] = useState('')
  const [message, setMessage] = useState('')
  const [patient, setPatient] = useState('')
  const [orderDate, setOrderDate] = useState('')

  useEffect(() => {
    const getData = async () => {
      var illnesses = []

      const token = await authService.getAccessToken()

      let res = await fetch('/api/Illness', {
        method: 'GET',
        headers: !token ? {} : { Authorization: `Bearer ${token}` },
      })
      let resJson = await res.json()
      if (res.status === 200) {
        var _illnesses = resJson.map((i) => i.illnessId + ':' + i.name).map((pair) => pair.split(':'))
        setIllnesses(_illnesses)
        var _medicines = resJson.map((i) => i.illnessId + ';' + i.medicines.map((m) => m.medicineId + ':' + m.name + ':' + m.form + ':' + m.dosage)).map((pair) => pair.split(';'))
        setMedicines(_medicines)
      } else {
        setIllnesses([':wyst??pi?? b????d'])
      }
    }
    getData()
  }, [])

  let handleSubmit = async (e) => {
    e.preventDefault()
    setOrderDate(moment().tz("Europe/Warsaw").toDate())
    try {
      const token = await authService.getAccessToken()
      let res2 = await fetch('/api/CurrentUser', {
        method: 'GET',
        headers: !token ? {} : { Authorization: `Bearer ${token}` },
      })
      let res2Json = await res2.json()
      if (res2.status === 200) {
        setPatient(res2Json.id)
      } else {
        setPatient('Wyst??pi?? b????d')
      }

      //new Date(diff);
      let res = await fetch('/api/PrescriptionForm', {
        method: 'POST',
        headers: !token ? { 'Content-Type': 'application/json, charset=UTF-8' } : {
          'Content-Type': 'application/json, charset=UTF-8',
          Authorization: `Bearer ${token}`
        },
        body: JSON.stringify({
          PatientId: res2Json.id,
          IllnessId: illnessId,
          MedicineId: medicine,
          Height: height,
          Weight: weight,
          BodyTemp: tempBody,
          IsAddicted: addicted,
          Addictions: addictions,
          HasAllergy: hasAllergy,
          Allergies: allergies,
          TakesPermMedicines: takesMedicines,
          PermMedicines: permMedicines,
          IsChronicallyIll: isChronicIll,
          ChronicIllnesses: chronicIllness,
          Gender: gender,
          IsPregnant: pregnancy,
          AdditionalInfo: additionalInfo,
          OrderDate: new Date().toLocaleString()
        }),
      })
      let resJson = await res.json()
      if (res.status === 200) {
        setIllness('')
        setIllnessId('')
        setIllnessName('')
        setMedicine('')
        setAdditionalInfo('')
        setOrderDate('')
        setMessage('Twoje zlecenie zosta??o przekazane do lekarza. Otrzymasz wiadomo???? Email z informacjami o eRecepcie gdy zostanie wystawiona. Wystawione eRecepty mo??esz sprawdzi?? w eKartotece.')
      } else {
        setMessage('Wyst??pi?? b????d')
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
            <label className="Custom-form-text">Wybierz dolegliwo????</label>
            <div class="select">
              <select
                class="select"
                value={illness}
                onChange={
                  (e) => {
                    var _ill = (e.target.value).split(':');
                    setIllness(e.target.value);
                    setIllnessId(_ill[0]);
                    setIllnessName(_ill[1]);
                    medicines.filter(m => m[0] == _ill[0]).map((m) => {
                        setMedicineIll(m[1].split(/,(?=\d)/))
                    });
                  }
                }
                aria-label="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                {illnesses.map((illness) => (
                  <option value={illness[0] + ":" + illness[1]}>{illness[1]}</option>
                ))}
              </select>
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
                <option value="" selected class="label-desc">
                  Niech lekarz wybierze za mnie odpowiedni lek
                </option>
                
                {Object.values(medicineIll).map((m) => m.split(':')).map((medicine) =>
                  <option value={medicine[0]}>{medicine[1] + " - " + medicine[2]}</option>
                )}
              </select>
            </div>
          </div>
          <div className="question-form">
            <label className="Custom-form-text">
              Podaj swoj?? p??e??
            </label>
            <div class="select">
              <select
                class="select"
                value={gender}
                onChange={(e) => setGender(e.target.value)}
                aria-label="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="M????czyzna">M????czyzna</option>
                <option value="Kobieta">Kobieta</option>
              </select>
            </div>
          </div>
          {
            gender == "Kobieta" &&
            (
              <div class="custom-control custom-checkbox">
              <label for="ciaza_check"> Czy jeste?? w ci????y?</label>
              <input
              type="checkbox"
              value={false}
              name="pregnancy"
              onChange={(e) => setPregnancy(e.target.checked)}
              checked={pregnancy}
              />
            </div>)
          }
          <div className="question-form">
            <label className="Custom-form-text">
              Podaj aktualn?? temperatur?? cia??a w {'\u00B0'} C.
            </label>
            <div class="select">
              <select
                class="select"
                value={tempBody}
                onChange={(e) => setTempBody(e.target.value)}
                aria-label="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="33.0">33.0</option>
                <option value="33.5">33.5</option>
                <option value="34.0">34.0</option>
                <option value="34.5">34.5</option>
                <option value="35.0">35.0</option>
                <option value="35.5">35.5</option>
                <option value="36.0">36.0</option>
                <option value="36.5">36.5</option>
                <option value="37.0">37.0</option>
                <option value="37.5">37.5</option>
                <option value="38.0">38.0</option>
                <option value="38.5">38.5</option>
                <option value="39.0">39.0</option>
                <option value="39.5">39.5</option>
                <option value="40.0">40.0</option>
                <option value="40.5">40.5</option>
              </select>
            </div>
          </div>

          <div className="question-form">
            <label className="Custom-form-text">Podaj sw??j wzrost w cm</label>
            <div class="select">
              <select
                class="select"
                value={height}
                onChange={(e) => setHeight(e.target.value)}
                aria-label="Default select example"
              >
                <option selected class="label-desc">
                  ...
                </option>
                <option value="<150"> {'<150'} </option>
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
              Podaj swoj?? aktualn?? wag?? w kg
            </label>
            <div
              class="select"
              value={weight}
              onChange={(e) => setWeight(e.target.value)}
              aria-label="Default select example"
            >
              <select class="select">
                <option selected class="label-desc">
                  ...
                </option>
                <option value="<50">{'<50'}</option>
                <option value="51-60">51-60</option>
                <option value="61-70">61-70</option>
                <option value="71-80">71-80</option>
                <option value="81-90">81-90</option>
                <option value="91-100">91-100</option>
                <option value="+100">+100</option>
              </select>
            </div>
          </div>
          <div class="custom-control custom-checkbox">
              <label for="addictions_check"> Czy palisz obecnie papierosy, nadu??ywasz alkoholu, b??d?? przyjmujesz u??ywki psychoaktywne?</label>
              <input
              type="checkbox"
              value={false}
              name="isAddicted"
              onChange={(e) => setAddicted(e.target.checked)}
              checked={addicted}
              />
          </div>
          {
            addicted &&
            (
              <div className="question-form">
                <label className="Custom-form-text">Wymie?? i opisz swoje u??ywki, ile papieros??w dziennie palisz, jakie substancje psychoaktywne przyjmujesz?</label>
                <div class="form-group">
                  <textarea
                    class="form-control"
                    id="exampleFormControlTextarea1"
                    rows="3"
                    className="Custom-form-text"
                    value={addictions}
                    onChange={(e) => setAddictions(e.target.value)}>
                  </textarea>
                </div>
              </div>
            )
          }
          <div class="custom-control custom-checkbox">
              <label for="addictions_check"> Czy masz uczulenia (alergi??) lub nietolerancj?? na jakiekolwiek leki b??d?? substancje, w tym ??ywno????, barwniki spo??ywcze, jady owad??w?</label>
              <input
              type="checkbox"
              value={false}
              name="hasAllergy"
              onChange={(e) => setHasAllergy(e.target.checked)}
              checked={hasAllergy}
              />
          </div>
          {
            hasAllergy &&
            (
              <div className="question-form">
                <label className="Custom-form-text">Podaj jakich substancji dotyczy uczulenie/nietolerancja i jak si?? objawia (np. wysypka, duszno??ci, obrz??ki).</label>
                <div class="form-group">
                  <textarea
                    class="form-control"
                    id="exampleFormControlTextarea1"
                    rows="3"
                    className="Custom-form-text"
                    value={allergies}
                    onChange={(e) => setAllergies(e.target.value)}>
                  </textarea>
                </div>
              </div>
            )
          }
          <div class="custom-control custom-checkbox">
              <label for="addictions_check"> Czy przyjmujesz leki na sta??e?</label>
              <input
              type="checkbox"
              value={false}
              name="takesMedicines"
              onChange={(e) => setTakesMedicines(e.target.checked)}
              checked={takesMedicines}
              />
          </div>
          {
            takesMedicines &&
            (
              <div className="question-form">
                <label className="Custom-form-text">Podaj nazwy przyjmowanych lek??w wraz z dawkowaniem.</label>
                <div class="form-group">
                  <textarea
                    class="form-control"
                    id="exampleFormControlTextarea1"
                    rows="3"
                    className="Custom-form-text"
                    value={permMedicines}
                    onChange={(e) => setPermMedicines(e.target.value)}>
                  </textarea>
                </div>
              </div>
            )
          }
          <div class="custom-control custom-checkbox">
              <label for="addictions_check"> Czy chorujesz na choroby przewlek??e?</label>
              <input
              type="checkbox"
              value={false}
              name="isChronicIll"
              onChange={(e) => setIsChronicIll(e.target.checked)}
              checked={isChronicIll}
              />
          </div>
          {
            isChronicIll &&
            (
              <div className="question-form">
                <label className="Custom-form-text">Podaj swoje choroby przewlek??e.</label>
                <div class="form-group">
                  <textarea
                    class="form-control"
                    id="exampleFormControlTextarea1"
                    rows="3"
                    className="Custom-form-text"
                    value={chronicIllness}
                    onChange={(e) => setChronicIllness(e.target.value)}>
                  </textarea>
                </div>
              </div>
            )
          }
          <div className="question-form">
            <label className="Custom-form-text">Dodatkowe informacje dla lekarza</label>
            <div class="form-group">
              <textarea
                class="form-control"
                id="exampleFormControlTextarea1"
                rows="4"
                className="Custom-form-text"
                value={additionalInfo}
                onChange={(e) => setAdditionalInfo(e.target.value)}>
              </textarea>
            </div>
          </div>
        </div>
        <div class="form-group-button">
          <button type="submit">
            Wy??lij
          </button>
        </div>
        <div className="message">{message ? <p>{message}</p> : null}</div>
      </form>
    </div>
  )
}

export default CustomForm
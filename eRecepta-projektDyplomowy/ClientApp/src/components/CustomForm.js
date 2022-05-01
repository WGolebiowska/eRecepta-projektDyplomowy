import "./CustomForm.css";
import { useState } from "react";
import {React} from 'react';
import AuthorizeRoute from '../components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from '../components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from '../components/api-authorization/ApiAuthorizationConstants';
import { Route } from 'react-router';


function CustomForm() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [mobileNumber, setMobileNumber] = useState("");
  const [dolegliwosc, setDolegliwosc] = useState("");
  const [ciaza, setCiaza] = useState(false)
  const [message, setMessage] = useState("");

  let handleSubmit = async (e) => {
    e.preventDefault();
    try {
      let res = await fetch("https://httpbin.org/post", {
        method: "POST",
        body: JSON.stringify({
          name: name,
          email: email,
          mobileNumber: mobileNumber,
          dolegliwosc: dolegliwosc,
          ciaza: ciaza,
        }),
      });
      let resJson = await res.json();
      if (res.status === 200) {
        setName("");
        setEmail("");
        setMobileNumber("");
        setMessage("User created successfully");
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />

      } else {
        setMessage("Some error occured");
      }
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <div className="App">
      <form onSubmit={handleSubmit}>
        <input
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

          <select value={dolegliwosc} onChange={(e) => setDolegliwosc(e.target.value)}  class="form-select" aria-label="Default select example">
            <option selected>Wybierz dolegliwość</option>
            <option value="Ból ucha">Ból ucha</option>
            <option value="Ból nosa">Ból nosa</option>
            <option value="Ból gardła">Ból gardła</option>
          </select>

          {/* <div class="form-check">
  <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault" />
  <label class="form-check-label" for="flexCheckDefault">
    Default checkbox
  </label>
</div> */}


        <button type="submit">Create</button>

        <div className="message">{message ? <p>{message}</p> : null}</div>
      </form>
    </div>
  );
}

export default CustomForm;

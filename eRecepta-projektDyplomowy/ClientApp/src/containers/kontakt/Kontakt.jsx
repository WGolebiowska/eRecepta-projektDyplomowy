import React from 'react';
import './kontakt.css';

const Possibility = () => (
  <div className="er_kontakt" id="kontakt">
    <div className="er_kontakt_content">

    <div className="er_kontakt_dane-firmy">
      <h4>Dane firmy</h4>
      <h1>Administratorem danych serwisu jest:</h1>
      <p>ERECEPT SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ wpisany do rejestru podmiotów wykonujacych działalność leczniczą o numerze księgi rejestrowej:000000240179 KRS: 0000876354, NIP: 8393220109, REGON: 387804589 Serwis Erecept.pl umożliwia otrzymanie recepty elektronicznej lub przedłużanie recepty bez wychodzenia z domu. Świadczymy usługi w sposób bardzo wygodny dla pacjenta – poprzez formularz z e-receptą. Wypełnienie prostego i przejrzystego formularza zajmie Ci zaledwie kilka minut, a następnie zostaniesz przekierowany do bezpiecznej płatności PayU. Własny, unikalny kod niezbędny do realizacji recepty otrzymasz prosto na swoją skrzynkę poczty elektronicznej.</p>
    </div>
    <div className="er_kontakt_formularz-kontaktowy">
      <h4>Formularz kontaktowy</h4>
      <input type="text" class="form-control w-100" id="formGroupExampleInput" placeholder="Twoje imię..." required="" name="name"></input>
      <input type="text" class="form-control w-100 " id="formGroupExampleInput2 " placeholder="Twój email..." name="email" required=""></input>
      <textarea class="form-control w-100" id="contactMessage" rows="3" placeholder="Twoja wiadomość..." required="" name="msg"></textarea>
      <button type="submit" class="btn btn-primary ">Wyślij wiadomość</button>
    </div>
    </div>
  </div>
);

export default Possibility;

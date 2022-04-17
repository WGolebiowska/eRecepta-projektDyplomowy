import React from 'react';
import gpt3Logo from '../../assets/ooo.png';
import './footer.css';

const Footer = () => (
  <div className="gpt3__footer section__padding">
    <div className="gpt3__footer-links">
      <div className="gpt3__footer-links_logo">
        <img src={gpt3Logo} alt="gpt3_logo" />
        <p>Kontakt<br />e-mail: kontakt@erecept.pl</p>
        <a href="https://www.facebook.com/ereceptpl" class="pt-1 mr-2 d-inline-block"><i class="fab fa-facebook" aria-hidden="true"></i></a>
        <a href="https://www.instagram.com/erecept_pl/" class="pt-1 d-inline-block"><i class="fab fa-instagram" aria-hidden="true"></i></a>
      </div>
      <div className="gpt3__footer-links_div">
        <p class="lead font-weight-bold text-white text-uppercase mb-2 ">Regulacje prawne:</p>
        <a href="/regulamin.pdf">Regulamin platformy erecept.pl</a>
        <a href="/polityka.pdf">Polityka Prywatności</a>
        <a href="/polityka_promocji.pdf">Regulamin promocji "Recepta od 29.74 zł"</a>
        <a href="https://www.gov.pl/web/rpp">Rzecznik praw pacjenta</a>
        <a href="https://pacjent.gov.pl/e-skierowanie/ikp-nie-tylko-w-czasie-pandemii">Internetowe Konto Pacjenta</a>
      </div>
      <div className="gpt3__footer-links_div">
        <p class="lead font-weight-bold text-white text-uppercase mb-2 ">Dla pacjentów:</p>
        <a href="#faq">Pytania i odpowiedzi</a>
      </div>
      <div className="gpt3__footer-links_div">
        <p class="lead font-weight-bold text-white text-uppercase mb-2 ">Mapa strony:</p>
        <a href="/">Strona główna</a>
        <a href="/tabletka-dzien-po-antykoncepcja-awaryjna">Antykoncepcja awaryjna</a>
        <a href="/psycholog-online">Psycholog/Psychoterapia online</a>
        <a href="/konsultacje-telefoniczne">Teleporady online</a>
      </div>
    </div>

    <div className="gpt3__footer-copyright">
      <p>@2022 eRecepta.pl</p>
    </div>
  </div>
);

export default Footer;

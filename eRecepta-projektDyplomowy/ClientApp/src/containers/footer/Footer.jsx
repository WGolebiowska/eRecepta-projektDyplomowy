import React from 'react';
import gpt3Logo from '../../logo.svg';
import './footer.css';
import logo from '../../assets/ooo.png';

const Footer = () => (
  <div className="gpt3_footer-wave">
    <svg viewBox="0 0 1440 320">
      <path fill="#5f9ea0" fill-opacity="1" d="M0,256L60,266.7C120,277,240,299,360,298.7C480,299,600,277,720,240C840,203,960,149,1080,128C1200,107,1320,117,1380,122.7L1440,128L1440,320L1380,320C1320,320,1200,320,1080,320C960,320,840,320,720,320C600,320,480,320,360,320C240,320,120,320,60,320L0,320Z"></path>
    </svg>
    <div className="gpt3__footer1 section__padding">
        <div className="gpt3__footer-links">
      <div className="gpt3__footer-links_logo">
        <img src={logo} alt="gpt3_logo" />
      </div>
      <div className="gpt3__footer-links_div">
        <h4>O nas</h4>
        <p>FAQ</p>
        <p>O nas</p>
        <p>Kariera</p>
        <p>Kontakt</p>
      </div>
      <div className="gpt3__footer-links_div">
        <h4>REGULACJE PRAWNE:</h4>

          <p>Regulamin platformy erecept.pl</p>
          <p>Polityka Prywatności</p>
          <p>Regulamin promocji "Recepta od 29.74 zł"</p>
          <p>Rzecznik praw pacjenta</p>
          <p>Internetowe Konto Pacjenta</p>
      </div>
      <div className="gpt3__footer-links_div">
        <h4>Centrum Lecznicze wykonujące Działalność Leczniczą pod numerem 000000233076</h4>
        <p>Biuro obsługi pacjentów:</p>
        <p>ul. Partyzantów 17/47</p>
        <p>81-423 Gdynia</p>
        <p>e-mail: kontakt@medyk.online</p>
      </div>
    </div>

    <div className="gpt3__footer-copyright">

      <p>@2022 eRecepta.pl All rights reserved.</p>
    </div>
  </div>
  </div>
);

export default Footer;

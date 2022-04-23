import React from 'react';
import people from '../../assets/people.png';
import ai from '../../assets/ai.png';
import doctors from '../../assets/bt-doctors.jpg';
import './header.css';

const Header = () => (
  <div className="gpt3__header section__padding" id="home">
    {/* <div className="gpt3_header_bg"> */}

    <div className="gpt3__header-content">
      <h1 className="gradient__textl">E-recepta – recepta online</h1>
      <p>Konsultacje lekarskie online. Bez rezerwacji, bez kolejek, bez stresu - jesteśmy dla Ciebie 24 godziny, 7 dni w tygodniu. Stały koszt usług, bez żadnych dodatkowych opłat. Wystawiamy refundowane e-recepty, zwolnienia lekarskie z pracy lub uczelni.</p>

      {/* <div className="gpt3__header-content__input">
        <input type="email" placeholder="Your Email Address" />
        <button type="button">Get Started</button>
      </div> */}

    </div>

    {/* <div className="gpt3__header-image">
      <img src={doctors} />
    </div> */}
    {/* </div> */}
  </div>
);

export default Header;

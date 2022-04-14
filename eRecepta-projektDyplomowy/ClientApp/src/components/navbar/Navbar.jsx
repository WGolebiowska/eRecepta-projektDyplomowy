import React, { useState } from 'react';
import { RiMenu3Line, RiCloseLine } from 'react-icons/ri';
import logo from '../../assets/ooo.png';
import './navbar.css';

const Navbar = () => {
  const [toggleMenu, setToggleMenu] = useState(false);

  return (
    <div className="gpt3__navbar">
      <div className="gpt3__navbar-links">
        <div className="gpt3__navbar-links_logo">
          <img src={logo} />
        </div>
        <div className="gpt3__navbar-links_container">
        </div>
      </div>
      <div className="gpt3__navbar-sign">
        {/* <p>Sign in</p> */}
          <p><a href="#erecepta">eRecepta</a></p>
          <p><a href="#ekonsultacja">eKonsultacja</a></p>
        <button type="button">Zaloguj</button>
      </div>
      <div className="gpt3__navbar-menu">
        {toggleMenu
          ? <RiCloseLine color="#fff" size={27} onClick={() => setToggleMenu(false)} />
          : <RiMenu3Line color="#fff" size={27} onClick={() => setToggleMenu(true)} />}
        {toggleMenu && (
        <div className="gpt3__navbar-menu_container scale-up-center">
            <p><a href="#erecepta">eRecepta</a></p>
            <p><a href="#ekonsultacja">eKonsultacja</a></p>
          <div className="gpt3__navbar-menu_container-links">

          </div>
          <div className="gpt3__navbar-menu_container-links-sign">
            {/* <p>Sign in</p> */}
            <button type="button">Zaloguj</button>
          </div>
        </div>
        )}
      </div>
    </div>
  );
};

export default Navbar;

import React, { useState } from 'react'

// import React, { Component } from 'react';
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
import { LoginMenu } from './api-authorization/LoginMenu'
// import './NavMenu.css';
import './navbar/navbar.css'
import logo from '../assets/ooo.png'
import { RiMenu3Line, RiCloseLine } from 'react-icons/ri'

//export class NavMenu extends Component {
//  static displayName = NavMenu.name;
//
//  constructor (props) {
//    super(props);
//
//    this.toggleNavbar = this.toggleNavbar.bind(this);
//    this.state = {
//      collapsed: true
//    };
//  }
//
//  toggleNavbar () {
//    this.setState({
//      collapsed: !this.state.collapsed
//    });
//  }
//
//  render () {
//    return (
//      <header>
//        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
//          <Container>
//            <NavbarBrand tag={Link} to="/">eRecepta_projektDyplomowy</NavbarBrand>
//            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
//            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
//              <ul className="navbar-nav flex-grow">
//                <NavItem>
//                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
//                </NavItem>
//                <NavItem>
//                  <NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
//                </NavItem>
//                <NavItem>
//                  <NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch data</NavLink>
//                </NavItem>
//                <LoginMenu>
//                </LoginMenu>
//              </ul>
//            </Collapse>
//          </Container>
//        </Navbar>
//      </header>
//    );
//  }
//}

const NavMenu = () => {
  const [toggleMenu, setToggleMenu] = useState(false)

  return (
    <div className="gpt3__navbar">
      <div className="gpt3__navbar-links">
        <div className="gpt3__navbar-links_logo">
          <img src={logo} />
        </div>
        <div className="gpt3__navbar-links_container"></div>
      </div>
      <div className="gpt3__navbar-sign">
        {/* <p>Sign in</p> */}
        <NavItem>
          <NavLink tag={Link} className="text-dark" to="/counter">
            eRecepta
          </NavLink>
        </NavItem>
        <p>
          <a href="#erecepta">eRecepta</a>
        </p>
        <p>
          <a href="#ekonsultacja">eKonsultacja</a>
        </p>
        <button type="button">Zaloguj</button>
        <LoginMenu></LoginMenu>
      </div>
      <div className="gpt3__navbar-menu">
        {toggleMenu ? (
          <RiCloseLine
            color="#fff"
            size={27}
            onClick={() => setToggleMenu(false)}
          />
        ) : (
          <RiMenu3Line
            color="#fff"
            size={27}
            onClick={() => setToggleMenu(true)}
          />
        )}
        {toggleMenu && (
          <div className="gpt3__navbar-menu_container scale-up-center">
            <p>
              <a href="#erecepta">eRecepta</a>
            </p>
            <p>
              <a href="#ekonsultacja">eKonsultacja</a>
            </p>
            <div className="gpt3__navbar-menu_container-links"></div>
            <div className="gpt3__navbar-menu_container-links-sign">
              {/* <p>Sign in</p> */}
              <button type="button">Zaloguj</button>
            </div>
          </div>
        )}
      </div>
    </div>
  )
}

export default NavMenu

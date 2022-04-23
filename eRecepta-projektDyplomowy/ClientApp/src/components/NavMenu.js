import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import './NavMenu.css';
import logo from '../assets/ooo.png';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3 bg-info text-white" light>
          <Container>
          {/* <NavbarBrand tag={Link} to="/">eRecepta_projektDyplomowy</NavbarBrand> */}
            <NavbarBrand tag={Link} to="/" >
            <img src={logo} />
            </NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                {/* <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem> */}
                <NavItem>
                  <NavLink tag={Link} className="text-white" to="/ekonsultacja">eKonsultacja</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-white" to="/erecepta">eRecepta</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-white" to="/fetch-data">eKartoteka</NavLink>
                </NavItem>
                <LoginMenu>
                </LoginMenu>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}

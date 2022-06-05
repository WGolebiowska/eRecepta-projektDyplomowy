import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import './NavMenu.css';
import logo from '../assets/ooo.png';
import authService from './api-authorization/AuthorizeService'
import { UserRoles } from './api-authorization/ApiAuthorizationConstants';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
      this.state = {
      hasAdminRole: false,
      hasDoctorRole: false,
      hasPatientRole: false,
      collapsed: true
    };
  }
    componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        this.populateState();
    }

    async populateState() {
        const hasAdminRole = await authService.hasRole(UserRoles.Administrator);
        const hasDoctorRole = await authService.hasRole(UserRoles.Doctor);
        const hasPatientRole = await authService.hasRole(UserRoles.Patient);

        this.setState({
            hasAdminRole,
            hasDoctorRole,
            hasPatientRole
        });
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
            <NavbarBrand tag={Link} to="/" >
            <img src={logo} />
            </NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                {
                  this.state.hasPatientRole &&
                  <NavItem>
                    <NavLink tag={Link} className="text-white" to="/ekonsultacja">eKonsultacja</NavLink>
                  </NavItem>
                }
                {
                    this.state.hasDoctorRole
                    ?(<NavItem><NavLink tag={Link} className="text-white" to="/prescription">eRecepta</NavLink></NavItem>)
                    : this.state.hasPatientRole ? (<NavItem><NavLink tag={Link} className="text-white" to="/erecepta">eRecepta</NavLink></NavItem>) : ('')
                }
                {
                  (this.state.hasDoctorRole || this.state.hasPatientRole) &&
                  <NavItem>
                    <NavLink tag={Link} className="text-white" to="/fetch-data">eKartoteka</NavLink>
                  </NavItem>
                }
                  {
                    this.state.hasAdminRole &&
                    <NavItem>
                        <NavLink tag={Link} className="text-white" to="/users">Panel administracyjny</NavLink>
                    </NavItem>
                  }
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

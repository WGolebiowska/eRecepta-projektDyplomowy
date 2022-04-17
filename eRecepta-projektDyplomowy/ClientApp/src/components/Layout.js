import React, { Component } from 'react';
import { Container } from 'reactstrap';
// import { NavMenu } from './NavMenu';
import  NavMenu  from './NavMenu';
// import Navbar from './navbar/Navbar';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}


//const Layout = () => {
//  <div>
//    {/* <Navbar /> */}
//    <NavMenu />
//    <Container>
//      {/* {this.props.children} */}
//    </Container>
//  </div>
//}
//
//export default Layout;

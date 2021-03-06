import React, { Component } from 'react'
// import { Footer, Blog, Possibility, Features, WhatGPT3, Header, Recepta, Konsultacje, Personel, Kontakt } from './containers';
import { CTA, Brand, Navbar } from './components'
import { Home } from './components/Home'
import { Personel } from './containers/'
import { FetchData } from './components/FetchData'
import { Counter } from './components/Counter'
import { Counter2 } from './components/Counter2'
import { DoctorPrescription } from './components/PrescriptionDoctor'
import { Route } from 'react-router'
import { Layout } from './components/Layout'
import { UsersRouter } from './components/users/UsersRouter'
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute'
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes'
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants'

import './App.css'
import './custom.css'

import { BrowserRouter as Router } from 'react-router-dom'

export default class App extends Component {
  static displayName = App.name

  render() {
    return (
      <Router>
        <Layout>
          <Route exact path="/" component={Home} />
          {/* <Route path='/counter' component={Counter} /> */}
          <AuthorizeRoute path="/erecepta" component={Counter} />
          <AuthorizeRoute path="/ekonsultacja" component={Counter2} />
          <AuthorizeRoute path="/fetch-data" component={FetchData} />
          <AuthorizeRoute path="/users" component={UsersRouter} />
          <AuthorizeRoute path="/prescription" component={DoctorPrescription} />
          <AuthorizeRoute path="/platnosc" component={Personel} />
          {/* <Route path='/fetch-data' component={FetchData} /> */}
          <Route
            path={ApplicationPaths.ApiAuthorizationPrefix}
            component={ApiAuthorizationRoutes}
          />
        </Layout>
      </Router>
    )
  }
}

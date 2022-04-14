import React, { Component } from 'react';
import { Footer, Blog, Possibility, Features, WhatGPT3, Header, Recepta, Konsultacje } from './containers';
import { CTA, Brand, Navbar } from './components';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './App.css';
import './custom.css'


import { BrowserRouter as Router } from "react-router-dom";



export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Router>
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <AuthorizeRoute path='/fetch-data' component={FetchData} />
                <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
                </Layout>
                <div className="App">
                    <div className="gradient__bg">
                        <Navbar />
                        <Recepta />
                        <Konsultacje />
                        <Header />
                    </div>
                    <Brand />
                    <WhatGPT3 />
                    <Features />
                    <Possibility />
                    <CTA />
                    <Blog />
                    <Footer />
                </div>
                </Router>
        );
    }
}

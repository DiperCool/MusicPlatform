import React, { Component } from 'react';
import { Route } from 'react-router';
import { Container } from 'reactstrap';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import {ApiAuthorizationRoutes} from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'
import { GetTest } from './components/Test/GetTest';
import { SetTest } from './components/Test/SetTest';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <div>
        <Route path="/getTest" component={GetTest}/>
        <AuthorizeRoute path="/setTest" component={SetTest}/>
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </div>
    );
  }
}

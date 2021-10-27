import React from 'react';
import { Route } from 'react-router';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import {ApiAuthorizationRoutes} from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'
import { GetTest } from './components/Test/GetTest';
import { SetTest } from './components/Test/SetTest';
import { AuthorizationProvider } from './components/api-authorization/AuthorizationProvider';
import { LoginMenu } from './components/api-authorization/LoginMenu';

export const App = ()=> {

    return (
      <AuthorizationProvider>
        <LoginMenu/>
        <AuthorizeRoute path="/getTest" component={GetTest}/>
        <Route path="/setTest" component={SetTest}/>
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </AuthorizationProvider>
    );
}

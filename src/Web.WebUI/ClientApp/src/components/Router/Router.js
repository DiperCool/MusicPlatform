import React from "react";
import { Route } from 'react-router';
import AuthorizeRoute from '../api-authorization/AuthorizeRoute';
import {ApiAuthorizationRoutes} from '../api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from '../api-authorization/ApiAuthorizationConstants';
import { GetTest } from '../Test/GetTest';
import { SetTest } from '../Test/SetTest';
export const Router = ()=>{
    return (
        <main style={{gridArea: "main"  , overflow: "auto"}}>
            <AuthorizeRoute path="/getTest" component={GetTest}/>
            <Route path="/setTest" component={SetTest}/>
            <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
        </main>
    )
}
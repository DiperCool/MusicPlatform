import React from "react";
import { Route } from 'react-router';
import AuthorizeRoute from '../api-authorization/AuthorizeRoute';
import {ApiAuthorizationRoutes} from '../api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from '../api-authorization/ApiAuthorizationConstants';
import "./Router.css";
import { AlbumCreate } from "../Album/Create/AlbumCreate";
import { SettingsAlbum } from "../Album/SettingsAlbum/SettingsAlbum";
export const Router = ()=>{
    return (
        <main className="main" style={{gridArea: "main"  , overflow: "auto"}}>
            <AuthorizeRoute path="/artist/album/create" component={AlbumCreate} roles={["Artist"]}/>
            <AuthorizeRoute path="/settings/artist/album/:id" component={SettingsAlbum} roles={["Artist"]}/>
            <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
        </main>
    )
}
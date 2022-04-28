import React from "react";
import { Route } from 'react-router';
import AuthorizeRoute from '../api-authorization/AuthorizeRoute';
import {ApiAuthorizationRoutes} from '../api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from '../api-authorization/ApiAuthorizationConstants';
import "./Router.css";
import { AlbumCreate } from "../Album/CreateAlbum/AlbumCreate";
import { SettingsAlbum } from "../Album/SettingsAlbum/SettingsAlbum";
import { ChangeAlbum } from "../Album/ChangeAlbum/ChangeAlbum";
import { DeleteAlbum } from "../Album/DeleteAlbum/DeleteAlbum";
import { CreateSong } from "../Song/CreateSong/CreateSong";
import { ChangeSong } from "../Song/ChangeSong/ChangeSong";
import { DeleteSong } from "../Song/DeleteSong/DeleteSong";
import { Search } from "../Search/Search";
export const Router = ()=>{
    return (
        <main className="main" style={{gridArea: "main"  , overflow: "auto"}}>
            <AuthorizeRoute path="/artist/album/create" component={AlbumCreate} roles={["Artist"]}/>
            <AuthorizeRoute path="/settings/artist/album/change/:id" exact component={ChangeAlbum} roles={["Artist"]}/>
            <AuthorizeRoute path="/settings/artist/album/:id" exact component={SettingsAlbum} roles={["Artist"]}/>
            
            <AuthorizeRoute path="/settings/artist/album/delete/:id" exact component={DeleteAlbum} roles={["Artist"]}/>
            
            <AuthorizeRoute path="/artist/album/createSong/:id" exact component={CreateSong} roles={["Artist"]}/>
            <AuthorizeRoute path="/settings/artist/song/change/:id" exact component={ChangeSong} roles={["Artist"]}/>
            <AuthorizeRoute path="/settings/artist/song/delete/:id" exact component={DeleteSong} roles={["Artist"]}/>
            

            <AuthorizeRoute path="/search" exact component={Search} roles={["Listener"]}/>


            <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
        </main>
    )
}
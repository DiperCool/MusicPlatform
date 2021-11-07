import React from 'react';
import { AuthorizationProvider } from './components/api-authorization/AuthorizationProvider';
import { Header } from './components/Header/Header';
import { NavBar } from './components/NavBar/NavBar';
import { Player } from './components/Player/Player';
import { Router } from './components/Router/Router';
import "./main.css";
export const App = ()=> {

    return (
      <AuthorizationProvider>
        <div className="Page">
        <Header/>
        <Router/>
        <NavBar/>
        <Player/>
        </div>
      </AuthorizationProvider>
    );
}

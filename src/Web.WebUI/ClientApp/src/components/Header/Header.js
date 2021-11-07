import React from "react";
import "./Header.css";
import { LoginMenu } from '../api-authorization/LoginMenu';

export const Header = ()=>{
    return (
        <header className="header">
            <div className="name-project">
                Music-Platform
            </div>
            <LoginMenu/>
        </header>
    )
}
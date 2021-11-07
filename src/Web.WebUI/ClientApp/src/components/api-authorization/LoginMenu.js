import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { ApplicationPaths } from './ApiAuthorizationConstants';
import {  Menu,  MenuList,  MenuButton, MenuLink,} from "@reach/menu-button";
import "@reach/menu-button/styles.css";
import { AuthorizationContext } from './AuthorizationContext';
import "./css/loginMenu.css"
export class LoginMenu extends Component {
    static contextType = AuthorizationContext;
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            userName: null
        };
    }

    componentDidMount() {
        this._subscription = this.context.subscribe(() => this.populateState());
        this.populateState();
    }

    componentWillUnmount() {
        this.context.unsubscribe(this._subscription);
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([this.context.isAuthenticated(), this.context.getUser()])
        this.setState({
            isAuthenticated,
            userName: user && user?.nickname
        });
    }

    render() {
        const { isAuthenticated, userName } = this.state;
        if (!isAuthenticated) {
            const registerPath = `${ApplicationPaths.Register}`;
            const loginPath = `${ApplicationPaths.Login}`;
            return this.anonymousView(registerPath, loginPath);
        } else {
            const profilePath = `${ApplicationPaths.Profile}`;
            const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
            return this.authenticatedView(userName, profilePath, logoutPath);
        }
    }

    authenticatedView(userName, profilePath, logoutPath) {
        return (
        <div className="loginMenu">
            <Menu>      
                <MenuButton>{userName} <span aria-hidden>▾</span></MenuButton>      
                <MenuList className="slide-down">        
                     <MenuLink as={Link} to={profilePath}>Profile</MenuLink>        
                     <MenuLink as={Link} to={logoutPath}>Logout</MenuLink>                        
                </MenuList>    
            </Menu>
        </div>);

    }

    anonymousView(registerPath, loginPath) {
        return (
            <div className="loginMenu">
                <div>
                    <a className="disabled-styles-link" href={registerPath}>
                        <button className="base-button register-button">
                            Register
                        </button>
                    </a>
                </div>
                <div>
                    <a className="disabled-styles-link" href={loginPath}>
                        <button className="base-button login-button">
                            Login
                        </button>
                    </a>
                </div>
            </div>
        );
    }
}

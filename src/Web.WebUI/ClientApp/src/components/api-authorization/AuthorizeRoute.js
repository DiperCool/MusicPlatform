import React from 'react'
import { Component } from 'react'
import { Route, Redirect } from 'react-router-dom'
import { ApplicationPaths, QueryParameterNames } from './ApiAuthorizationConstants'
import { AuthorizationContext } from './AuthorizationContext'
export default class AuthorizeRoute extends Component {
    static contextType = AuthorizationContext; 
    constructor(props) {
        super(props);

        this.state = {
            ready: false,
            authenticated: false,
            doesntHaveRole: false
        };
    }

    componentDidMount() {
        this._subscription = this.context.subscribe(() => this.authenticationChanged());
        this.populateAuthenticationState();
    }

    componentWillUnmount() {
        this.context.unsubscribe(this._subscription);
    }

    render() {
        const { ready, authenticated } = this.state;
        var link = document.createElement("a");
        link.href = this.props.path;
        const returnUrl = `${link.protocol}//${link.host}${link.pathname}${link.search}${link.hash}`;
        const redirectUrl = `${ApplicationPaths.Login}?${QueryParameterNames.ReturnUrl}=${encodeURIComponent(returnUrl)}`
        if (!ready) {
            return <div></div>;
        } else {
            const { component: Component,...rest } = this.props;

            return <Route {...rest}
                render={(props) => {
                    if (authenticated && !this.state.doesntHaveRole) {
                        return <Component {...props} />
                    } else if(this.state.doesntHaveRole){
                        return <Redirect to={"/"}/>
                    } else {
                        return <Redirect to={redirectUrl} />
                    }
                }} />
        }
    }

    async populateAuthenticationState() {
        let authenticated= await this.context.isAuthenticated();
        if(!authenticated){
            this.setState({ ready: true, authenticated })
            return;
        }
        let user = await this.context.getUser();
        if(this.props.roles){
            authenticated=this.props.roles.indexOf(user.role)!==-1;
            this.setState({ ready: true, authenticated, doesntHaveRole: !authenticated });
            return;
        }
        this.setState({ ready: true, authenticated });
        return;

    }

    async authenticationChanged() {
        this.setState({ ready: false, authenticated: false });
        await this.populateAuthenticationState();
    }
}

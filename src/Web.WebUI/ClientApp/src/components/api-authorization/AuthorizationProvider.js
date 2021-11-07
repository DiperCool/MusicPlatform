import { UserManager, WebStorageStateStore } from 'oidc-client';
import { ApplicationPaths, ApplicationName } from './ApiAuthorizationConstants';
import React, { useEffect, useState } from "react";
import { AuthorizationContext } from './AuthorizationContext';
import axios from "axios";
export const AuthorizationProvider = ({children})=>{
    let [_callbacks, setCallbacks]= useState([]);
    let[_nextSubscriptionId, setNextSubscriptionId] = useState(0);
    let [_user, setUser]= useState(null);
    let [_isAuthenticated, setIsAuthenticated]=useState(false);
    let [_popUpDisabled] = useState(true);
    let [userManager, setUserManager]= useState(undefined);
    const isAuthenticated=async() =>{
        const user = await getUser();
        setIsAuthenticated(!!user);
        return !!user;
    }
    useEffect(()=>{
        let fetchUserManager = async ()=>{
            let response = await fetch(ApplicationPaths.ApiAuthorizationClientConfigurationUrl);
            if (!response.ok) {
                throw new Error(`Could not load settings for '${ApplicationName}'`);
            }

            let settings = await response.json();
            settings.userStore = new WebStorageStateStore({
                prefix: ApplicationName
            });
            settings.automaticSilentRenew = true;
            settings.includeIdTokenInSilentRenew = true;
            let userManagerInit = new UserManager(settings);
            let userInit = await userManagerInit.getUser();
            if(!!userInit && !window.location.href.includes("authentication"))
            {
                try {
                    let response = await axios.post("https://localhost:5001/connect/userinfo",{
                    
                    },{
                        headers:{
                            Authorization: "Bearer "+userInit.access_token
                        }
                    });
                    userInit.profile = {
                        ...userInit.profile, ...response.data
                    }
                    userManagerInit.storeUser(userInit);
                } catch(e) {

                }
            }
            setUser(userInit);
            setUserManager(userManagerInit);
        }
        fetchUserManager();
    },[]);
    useEffect(()=>{
        if(!userManager|| !_user) return;
        let addAccessTokenExpired = _ => {
            console.log("expired");
            signIn({});
        }
        let addUserSignedOut=async () => {
            await userManager.removeUser();
            updateState(undefined);
        }
        userManager.events.addAccessTokenExpired(addAccessTokenExpired);
        userManager.events.addUserSignedOut(addUserSignedOut);

        return ()=>{
            if(userManager===undefined) return;
            userManager.events.removeAccessTokenExpired(addAccessTokenExpired)
            userManager.events.removeUserSignedOut(addUserSignedOut)
        }
    })
    const getUser=async()=> {
        if (_user && _user.profile) {
            return _user.profile;
        }
        setUser(await userManager.getUser());
        return _user && _user.profile;
    }

    const getAccessToken = async() =>{
        return _user && _user.access_token;
    }

    // We try to authenticate the user in three different ways:
    // 1) We try to see if we can authenticate the user silently. happens
    //    when the user is already logged in on the IdP and is done using a hidden iframe
    //    on the client.
    // 2) We try to authenticate the user using a PopUp Window.  might fail if there is a
    //    Pop-Up blocker or the user has disabled PopUps.
    // 3) If the two methods above fail, we redirect the browser to the IdP to perform a traditional
    //    redirect flow.
    const signIn=async(state)=> {
        try {
            const silentUser = await userManager.signinSilent(createArguments());
            updateState(silentUser);
            return success(state);
        } catch (silentError) {
            // User might not be authenticated, fallback to popup authentication
            console.log("Silent authentication error: ", silentError);

            try {
                if (_popUpDisabled) {
                    throw new Error('Popup disabled. Change \'AuthorizeService.js:AuthorizeService._popupDisabled\' to false to enable it.')
                }

                const popUpUser = await userManager.signinPopup(createArguments());
                updateState(popUpUser);
                return success(state);
            } catch (popUpError) {
                if (popUpError.message === "Popup window closed") {
                    // The user explicitly cancelled the login action by closing an opened popup.
                    return error("The user closed the window.");
                } else if (!_popUpDisabled) {
                    console.log("Popup authentication error: ", popUpError);
                }

                // PopUps might be blocked by the user, fallback to redirect
                try {
                    await userManager.signinRedirect(createArguments(state));
                    return redirect();
                } catch (redirectError) {
                    console.log("Redirect authentication error: ", redirectError);
                    return error(redirectError);
                }
            }
        }
    }

    const completeSignIn =async(url)=> {
        try {
            const user = await userManager.signinCallback(url);
            updateState(user);
            return success(user && user.state);
        } catch (error) {
            console.log('There was an error signing in: ', error);
            return error('There was an error signing in.');
        }
    }

    // We try to sign out the user in two different ways:
    // 1) We try to do a sign-out using a PopUp Window.  might fail if there is a
    //    Pop-Up blocker or the user has disabled PopUps.
    // 2) If the method above fails, we redirect the browser to the IdP to perform a traditional
    //    post logout redirect flow.
    const signOut= async(state) =>{
    
        try {
            if (_popUpDisabled) {
                throw new Error('Popup disabled. Change \'AuthorizeService.js:AuthorizeService._popupDisabled\' to false to enable it.')
            }

            await userManager.signoutPopup(createArguments());
            updateState(undefined);
            return success(state);
        } catch (popupSignOutError) {
            console.log("Popup signout error: ", popupSignOutError);
            try {
                await userManager.signoutRedirect(createArguments(state));
                return redirect();
            } catch (redirectSignOutError) {
                console.log("Redirect signout error: ", redirectSignOutError);
                return error(redirectSignOutError);
            }
        }
    }

    const completeSignOut=async(url) =>{
        
        try {
            const response = await userManager.signoutCallback(url);
            updateState(null);
            return success(response && response.data);
        } catch (error) {
            console.log(`There was an error trying to log out '${error}'.`);
            return error(error);
        }
    }

    const updateState=(user) =>{
        setUser(user)
        setIsAuthenticated(!!user);
        notifySubscribers();
    }
    const subscribe=(callback)=>{
        let callbacks = [..._callbacks];
        callbacks.push({ callback, subscription: _nextSubscriptionId++ });
        setCallbacks(callbacks);
        setNextSubscriptionId(_nextSubscriptionId - 1);
        return ;
    }

    const unsubscribe=(subscriptionId)=> {
        let callbacks = [..._callbacks]
        const subscriptionIndex = callbacks
            .map((element, index) => element.subscription === subscriptionId ? { found: true, index } : { found: false })
            .filter(element => element.found === true);
        if (subscriptionIndex.length !== 1) {
            throw new Error(`Found an invalid number of subscriptions ${subscriptionIndex.length}`);
        }
        callbacks.splice(subscriptionIndex[0].index, 1);
        setCallbacks(callbacks);
    }

    const notifySubscribers=()=> {
        for (let i = 0; i < _callbacks.length; i++) {
            const callback = _callbacks[i].callback;
            callback();
        }
    }

    const createArguments=(state)=> {
        return { useReplaceToNavigate: true, data: state };
    }

    const error=(message)=> {
        return { status: AuthenticationResultStatus.Fail, message };
    }

    const success=(state)=> {
        return { status: AuthenticationResultStatus.Success, state };
    }

    const redirect=()=> {
        return { status: AuthenticationResultStatus.Redirect };
    }
    if(userManager===undefined)
    {
        return (
            <div>loading</div>
        )
    }
    return (
        <AuthorizationContext.Provider value={{getUser,isAuthenticated,signOut,completeSignOut,unsubscribe,subscribe,completeSignIn,signIn, getAccessToken, _callbacks,_nextSubscriptionId,_user,_isAuthenticated,_popUpDisabled,userManager}}>
            {children}
        </AuthorizationContext.Provider>
    )

}



export const AuthenticationResultStatus = {
    Redirect: 'redirect',
    Success: 'success',
    Fail: 'fail'
};

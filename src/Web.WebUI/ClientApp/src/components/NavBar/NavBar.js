import React, {useContext, useEffect, useState} from "react";
import { AuthorizationContext } from "../api-authorization/AuthorizationContext";
import "./NavBar.css";
import { NavBarContentArtist } from "./NavBarContentArtist";
import { NavBarContentListener } from "./NavBarContentListener";
let contents = {
    "Artist":<NavBarContentArtist/>,
    "Listener":<NavBarContentListener/>
}
export const NavBar = ()=>{
    let [content, setContent] = useState(null);
    let {isAuthenticated, getUser } = useContext(AuthorizationContext);
    
    useEffect(()=>{
        const setContentByRole = async() =>{
            if(!(await isAuthenticated())) return;
            let role = (await getUser()).role;
            setContent(contents[role]);
        }
        setContentByRole();
    },[isAuthenticated, getUser])
    return(
        <nav className="NavBar">
            <div className="name-project">
                Music-Platform
            </div>
            {content}
        </nav>
    )
}
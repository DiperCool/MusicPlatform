import React, {useContext, useEffect} from "react";
import { useHistory } from "react-router";
import { AuthorizationContext } from "../api-authorization/AuthorizationContext";

export const useRedirectAlbumNotArtist = (albumId)=>{
    let history = useHistory();
    let {getAccessToken } = useContext(AuthorizationContext);
    useEffect(()=>{
        let redirectAlbumNotUser= async()=>{
            if(!albumId) return
            const response = await fetch(`api/Album/HasAlbumArtist/${albumId}`, {
                method: "GET",
                headers:{
                    Authorization: "Bearer "+ await getAccessToken()
                },
            });
            if(!(await response.json())){
                history.push("/");
            }
        };
        redirectAlbumNotUser();
    },[history,albumId])
}
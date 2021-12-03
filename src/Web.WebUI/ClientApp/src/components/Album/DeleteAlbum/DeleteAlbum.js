import React, { useContext } from 'react'
import { Button } from "../../Components/Button/Button";
import './DeleteAlbum.css';
import { useHistory } from "react-router-dom";
import { AuthorizationContext } from "../../api-authorization/AuthorizationContext";
import { emitCustomEvent } from 'react-custom-events';
import { useRedirectAlbumNotArtist } from '../../Hooks/useRedirectAlbumNotArtist';
export const DeleteAlbum = (props)=>{
    let { getAccessToken}= useContext(AuthorizationContext);
    let history = useHistory();
    useRedirectAlbumNotArtist(props.match.params.id)
    let handlerDeleteAlbum = async()=>{
        const response = await fetch("api/album/", {
            method: "DELETE",
            headers:{
                Authorization: "Bearer "+ await getAccessToken(),
                "Content-Type":"application/json", 
            },
            body:JSON.stringify({
                AlbumId: props.match.params.id
            })
        })
        if(response.ok){
            history.push("/");
            emitCustomEvent("deleteAlbum", { albumId:props.match.params.id });
        }
    }
    return (
        <div className="delete-album-content">
            <div>
                Do you really want to delete this perfect album?
            </div>
            <div className="delete-album-buttons">
                <div>
                    <Button onClick={()=>{handlerDeleteAlbum()}} >Yes</Button>
                </div>
                <div>
                    <Button>No</Button>
                </div>
            </div>
            
        </div>
    )
}
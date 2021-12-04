import React, { useContext } from 'react';
import { Button } from "../../Components/Button/Button";
import './DeleteSong.css';
import { useHistory } from "react-router-dom";
import { AuthorizationContext } from "../../api-authorization/AuthorizationContext";
export const DeleteSong =(props)=>{
    let { getAccessToken}= useContext(AuthorizationContext);
    let history = useHistory();
    let handlerDeleteSong = async()=>{
        const response = await fetch("api/song/", {
            method: "DELETE",
            headers:{
                Authorization: "Bearer "+ await getAccessToken(),
                "Content-Type":"application/json", 
            },
            body:JSON.stringify({
                SongId: props.match.params.id
            })
        })
        if(response.ok){
            history.push("/");
        }
    }
    return (
        <div className="delete-song-content">
            <div>
                Do you really want to delete this perfect song?
            </div>
            <div className="delete-song-buttons">
                <div>
                    <Button onClick={()=>{handlerDeleteSong()}} >Yes</Button>
                </div>
                <div>
                    <Button>No</Button>
                </div>
            </div>
            
        </div>
    )
}
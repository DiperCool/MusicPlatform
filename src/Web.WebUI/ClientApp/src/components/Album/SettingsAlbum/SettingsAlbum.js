import React, { useContext, useState, useEffect } from "react";
import { AuthorizationContext } from "../../api-authorization/AuthorizationContext";
import "./SettingsAlbum.css";
import {Button} from "../../Components/Button/Button"
import {Link} from "react-router-dom"
export const SettingsAlbum=(props)=>{
    let [album, setAlbum] = useState(null);
    let { getAccessToken}= useContext(AuthorizationContext);
    useEffect(()=>{
        let loadAlbum = async ()=>{
            setAlbum(null);
            const response = await fetch(`api/album/${props.match.params.id}`, {
                method: "GET",
                headers:{
                    Authorization: "Bearer "+ await getAccessToken()
                },
            });
            setAlbum(await response.json());
            
        };
        loadAlbum();
    },[props.match.params.id,getAccessToken])
    if(!album){
        return(
            <div>
                Loading...
            </div>
        )
    }
    return (
        <div class="settings-album">
            <div class="settings-album-information">
                <div>
                    <img class="settings-album-picture" src={album.picture} alt="Album"/>
                </div>
                <div class="settings-album-title-buttons">
                    <div class="settings-album-title">
                        {album.title}
                    </div>
                    <div clas="settings-album-buttons">
                        <Button>Add Song</Button>
                        <Link to={`/settings/artist/album/change/${album.id}`}>
                            <Button>
                                Change Album
                            </Button>
                        </Link>
                        <Button>Delete Album</Button>
                    </div>
                </div>
            </div>


        </div>
    )
}
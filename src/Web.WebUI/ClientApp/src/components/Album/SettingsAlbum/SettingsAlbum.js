import React, { useContext, useState, useEffect } from "react";
import { AuthorizationContext } from "../../api-authorization/AuthorizationContext";
import "./SettingsAlbum.css";
import { useHistory } from "react-router";
import {Button} from "../../Components/Button/Button"
import {Link} from "react-router-dom"
import { useRedirectAlbumNotArtist } from "../../Hooks/useRedirectAlbumNotArtist";
import { SettingsAlbumSongs } from "./SettingsAlbumSongs/SettingsAlbumSongs";
import { LoadMoreButton } from "../../Components/LoadMoreButton/LoadMoreButton";
export const SettingsAlbum=(props)=>{
    let [album, setAlbum] = useState(null);
    let { getAccessToken, getUser}= useContext(AuthorizationContext);
    let [songs, setSongs]= useState([]);
    let [songsPagination, setSongsPagination]= useState({
        hasNextPage:false,
        id:0,
        pageSize:1
    });
    let history = useHistory();
    let getSongs = async (id=0, pageSize=1)=>{
        const response = await fetch(`api/song?albumId=${props.match.params.id}&songId=${id}&pageSize=${pageSize}`, {
            method: "GET",
            headers:{
                Authorization: "Bearer "+ await getAccessToken()
            },
        });
        let songsRes= await response.json();
        setSongs([...songs, ...songsRes.items]);
        delete songsRes.items;
        setSongsPagination(songsRes);
    }
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
    },[props.match.params.id,getAccessToken]);
    useEffect(()=>{
        let loadSongs = async ()=>{
            setSongs([]);
            setSongsPagination({
                hasNextPage:false,
                id:0,
                pageSize:1
            });
            let songsRes = await getSongs();

        };
        loadSongs();
    },[props.match.params.id,getAccessToken])
    useRedirectAlbumNotArtist(album?.id?? null);
    if(!album || songs.length===0){
        return(
            <div>
                Loading...
            </div>
        )
    }
    return (
        <div className="settings-album">
            <div className="settings-album-information">
                <div>
                    <img className="settings-album-picture" src={album.picture} alt="Album"/>
                </div>
                <div className="settings-album-title-buttons">
                    <div className="settings-album-title">
                        {album.title}
                    </div>
                    <div className="settings-album-buttons">
                        <Link to={`/artist/album/createSong/${album.id}`}>
                            <Button className="setting-album-button">Add Song</Button>
                        </Link>
                        <Link to={`/settings/artist/album/change/${album.id}`}>
                            <Button className="setting-album-button">
                                Change Album
                            </Button>
                        </Link>
                        <Link to={`/settings/artist/album/delete/${album.id}`}>
                            <Button className="setting-album-button">Delete Album</Button>
                        </Link>
                    </div>
                </div>
            </div>
            <div>
                <SettingsAlbumSongs songs={songs}/>
                {songsPagination.hasNextPage?
                    <LoadMoreButton size={45} onClick={async()=>{getSongs(songsPagination.id, songsPagination.pageSize)}} />:null}
            </div>

        </div>
    )
}
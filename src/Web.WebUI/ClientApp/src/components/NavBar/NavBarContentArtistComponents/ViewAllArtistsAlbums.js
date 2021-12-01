import React, {  useContext, useEffect, useState } from "react";
import { AuthorizationContext } from "../../api-authorization/AuthorizationContext";
import "./ViewAllArtistsAlbums.css";
import {CgArrowDownO} from "react-icons/cg";
import { useCustomEventListener } from 'react-custom-events';
import { AlbumItem } from "./AlbumItem";
export const ViewAllArtistsAlbums = ()=>{
    let [albums, setAlbums]= useState([]);
    let [pagination, setPagination]= useState({
        hasNextPage:false,
        id:0,
        pageSize:1
    });
    let {getUser, getAccessToken} = useContext(AuthorizationContext);
    let loadAlbums = async(id=0, pageSize=1)=>{
        let user = await getUser();
        let response = await fetch(`api/album?artistId=${user.AccountId}&pageSize=${pageSize}&albumId=${id}`,{
            method: "GET",
            headers:{
                Authorization: "Bearer " + await getAccessToken()
            }
        });
        let data = await response.json();
        setAlbums([...albums, ...data.items]);
        delete data.items;
        setPagination(data);
    };
    useCustomEventListener("addAlbum",(data)=>{
        setAlbums([...albums, data]);
    }); 
    useCustomEventListener("changeAlbum",(data)=>{
        let albumsCopy = [...albums];
        albumsCopy.forEach(album=>{
            if(album.id===Number(data.albumId)){
                album.title = data.title
            }
        });
        setAlbums(albumsCopy);
    })
    useCustomEventListener("deleteAlbum", (data)=>{
        let albumsCopy = [...albums];
        
        albumsCopy=albumsCopy.filter(x=> x.id!==Number(data.albumId));
        setAlbums(albumsCopy);
    });
    let clickLoadMoreButton = async()=>{
        await loadAlbums(pagination.id, pagination.pageSize)
    }
    useEffect(()=>{
        
        loadAlbums();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    },[]);

    return(
        <div className="content-nav-albums">
            <div className="title-nav-albums">
                Your Albums
            </div>
            <div>
                {albums.map(el=><AlbumItem key={el.id} album={el}/>)}
            </div> 
            {pagination.hasNextPage?
            <div  className="load-more-button-wrapper">
                <CgArrowDownO onClick={clickLoadMoreButton} size={30}  className="load-more-button "/>
            </div>:null}
        </div>
    )
}
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
        pageNumber:1,
        pageSize:1
    });
    let {getUser, getAccessToken} = useContext(AuthorizationContext);
    let loadAlbums = async(pageNumber=1, pageSize=1)=>{
        let user = await getUser();
        let response = await fetch(`api/album?artistId=${user.AccountId}&pageSize=${pageSize}&pageNumber=${pageNumber}`,{
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
    })
    let clickLoadMoreButton = async()=>{
        await loadAlbums(pagination.pageNumber+1, pagination.pageSize)
    }
    useEffect(()=>{
        
        loadAlbums();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    },[]);

    return(
        <div class="content-nav-albums">
            <div class="title-nav-albums">
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
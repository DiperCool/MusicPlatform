import React from "react";
import { ButtonCreateAlbum } from "./NavBarContentArtistComponents/ButtonCreateAlbum";
import { ViewAllArtistsAlbums } from "./NavBarContentArtistComponents/ViewAllArtistsAlbums";
import "./NavBarContentArtist.css";
export const NavBarContentArtist=()=>{

    return (
        <div>
            <ViewAllArtistsAlbums/>
            <hr className="hr"/>
            <ButtonCreateAlbum/>
        </div>
    )
}
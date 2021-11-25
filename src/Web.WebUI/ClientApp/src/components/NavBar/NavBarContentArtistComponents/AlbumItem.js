import React from "react";
import { Link } from "react-router-dom";
import "./AlbumItem.css";
export const AlbumItem = ({album})=>{
    return(
        <div className="album-item">
            <Link className="album-item-link" to={`/settings/artist/album/${album.id}`}>{album.title}</Link>
        </div>
    );
}
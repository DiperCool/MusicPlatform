import React from "react";
import {AiOutlinePlusCircle} from "react-icons/ai";
import { NavLink } from "react-router-dom";
import "./ButtonCreateAlbum.css";
export const ButtonCreateAlbum = ()=>{
    return (
        <NavLink to="/artist/album/create" style={{textDecoration:"none"}}>
            <div className="button-create-album">
                <div>
                    <AiOutlinePlusCircle size={30} className="plus-icon"/>
                </div>
                <div className="text-create-album">
                    New Album
                </div>
            </div>
        </NavLink>
    )
}
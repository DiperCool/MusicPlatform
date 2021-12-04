import React from 'react'
import "./SettingsAlbumSong.css";
import { IoIosCreate } from "react-icons/io";
import {AiFillDelete} from "react-icons/ai";
import { Link } from 'react-router-dom';
export const SettingsAlbumSong = ({number, title,id})=>{
    return (
        <tr className="settings-album-song-content">
            <td>{number}</td>
            <td>{title}</td>
            <td>
                <Link to={`/settings/artist/song/change/${id}`}  className={"settings-album-song-icon"}>
                    <IoIosCreate size={45}  />
                </Link>
            </td>
            <td>
                <Link to={`/settings/artist/song/delete/${id}`} className={"settings-album-song-icon"}>
                    <AiFillDelete size={45}  /> 
                </Link>
            </td>
        </tr>
    )
}
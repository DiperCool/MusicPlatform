import React from 'react'
import { LoadMoreButton } from '../../../Components/LoadMoreButton/LoadMoreButton'
import { SettingsAlbumSong } from './SettingsAlbumSong'
import "./SettingsAlbumSongs.css"
export const SettingsAlbumSongs = ({songs})=>{
    return (
        <div className="settings-album-songs-content">
           <table className="settings-album-songs-table">
               <tbody>
                    <tr>
                        <th>#</th>
                        <th>Title</th>
                        <th>Change</th>
                        <th>Delete</th>
                    </tr>
                    {songs.map((el,i)=><SettingsAlbumSong key={el.id} id={el.id} title={el.title} number={i+1} />)}
               </tbody>
           </table>
        </div>
    )
}
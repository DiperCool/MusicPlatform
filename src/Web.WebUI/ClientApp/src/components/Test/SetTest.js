import axios from "axios";
import React, { useContext, useRef } from "react";
import { AuthorizationContext } from "../api-authorization/AuthorizationContext";
import AudioPlayer from 'react-h5-audio-player';
import 'react-h5-audio-player/lib/styles.css';
export const SetTest = ()=>{
    let inputRef = useRef("")
    let {getAccessToken}= useContext(AuthorizationContext);
    let click =async ()=>{
        await axios.post("https://localhost:5001/api/test/",{
            test : inputRef.current.value
        },{
            headers:{
                Authorization: "Bearer "+(await getAccessToken())
            }
        })
    }
    return (
        <AudioPlayer
    autoPlay
    src="https://localhost:5001/api/playsong?songId=1"
    onPlay={e => console.log("onPlay")}
    // other props here
  />
    )
}

import axios from "axios";
import React, { useRef } from "react";
import authService from "../api-authorization/AuthorizeService";
export const SetTest = ()=>{
    let inputRef = useRef("")
    let click =async ()=>{
        await axios.post("https://localhost:5001/api/test/",{
            test : inputRef.current.value
        },{
            headers:{
                Authorization: "Bearer "+(await authService.getAccessToken())
            }
        })
    }
    return (
        <div>
            <input ref={inputRef} type="text"/>
        <button onClick={click}>Click!</button>
        </div>
    )
}

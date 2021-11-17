import React from "react";
import "./TextField.css";
export const TextField = (props)=>{
    return(
        <input type="text" {...props} className={"text-field "+props.className}/>
    )
}
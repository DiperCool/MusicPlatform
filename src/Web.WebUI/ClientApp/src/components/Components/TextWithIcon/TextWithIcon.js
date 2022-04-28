import React from "react";
import "./TextWithIcon.css"
export const TextWithIcon= (props)=>{

    return (
        <div className="text-with-icon-wrapper">
            <div className={props.classIcon??"icon-default"}>
                {props.icon}
            </div>
            <div className={props.classIcon??"text-default"}>
                {props.text}
            </div>
        </div>
    )
}
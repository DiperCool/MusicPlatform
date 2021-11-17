import React from 'react';
import "./Button.css"
export const Button = (props)=>{
    return(
        <button {...props} className={"base-button "+props.className}>
            {props.children}
        </button>
    )
}
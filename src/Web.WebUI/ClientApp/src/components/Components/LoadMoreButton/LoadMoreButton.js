import React from 'react'
import {CgArrowDownO} from "react-icons/cg";
import "./LoadMoreButton.css";
export const LoadMoreButton = (props)=>{
    return (
        <div  className="load-more-button-wrapper">
            <CgArrowDownO  size={30} {...props} className={"load-more-button "+props.className }/>
        </div>
    )
}
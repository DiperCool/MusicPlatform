import React, { useRef } from "react";
import { Button } from "../Button/Button";
import {FaFileDownload} from "react-icons/fa";
import "./FileInput.css";
export const FileInput = (props)=>{
    const inputFile = useRef(null);
    const onButtonClick = () => {
        inputFile.current.click();
        
    };
    return(
        <div>
            <label htmlFor="file-upload" className={"file-input-label "+props.className}>
                <Button onClick={onButtonClick} >
                    <FaFileDownload size={30}/>
                    {props.text}
                </Button>
            </label>
        <input {...props} className={"file-input"} type="file" id="file-upload" ref={inputFile}/>
        </div>
    )
}
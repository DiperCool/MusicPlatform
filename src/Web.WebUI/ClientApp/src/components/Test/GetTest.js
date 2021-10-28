import React, { useContext, useEffect, useState } from "react";
import axios from "axios";
import { AuthorizationContext } from "../api-authorization/AuthorizationContext";

export const GetTest = ()=>{
    let [test, setTest]= useState("");
    let {getAccessToken}= useContext(AuthorizationContext)
    useEffect(()=>{
        let loadTest = async()=>{
            let test = await axios.get("https://localhost:5001/api/test/", {
                headers:{
                    Authorization: "Bearer "+(await getAccessToken())
                }
            });
            setTest(test.data);
        }
        loadTest();
    },[getAccessToken])

    return  (
        <div>{test}</div>
    )
}  
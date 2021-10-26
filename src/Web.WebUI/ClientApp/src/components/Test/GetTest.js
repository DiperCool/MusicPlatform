import React, { useEffect, useState } from "react";
import axios from "axios";

export const GetTest = ()=>{
    let [test, setTest]= useState("");

    useEffect(()=>{
        let loadTest = async()=>{

            let test = await axios.get("https://localhost:5001/api/test/");
            setTest(test.data);
        }
        loadTest();
    },[])

    return  (
        <div>{test}</div>
    )
}  
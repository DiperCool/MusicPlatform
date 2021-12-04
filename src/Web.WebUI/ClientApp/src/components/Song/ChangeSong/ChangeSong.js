import React, { useContext } from "react";
import { Button } from "../../Components/Button/Button";
import { IoIosCreate } from "react-icons/io";
import "./ChangeSong.css";
import { TextField } from "../../Components/TextField/TextField";
import { FileInput } from "../../Components/FileInput/FileInput";
import { Formik, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { useHistory } from "react-router-dom";
import { AuthorizationContext } from "../../api-authorization/AuthorizationContext";
export const ChangeSong = (props)=>{
    let { getAccessToken}= useContext(AuthorizationContext);
    let history = useHistory();
    return (
        <Formik
            initialValues={{ title: '', file: null }}
            validationSchema={Yup.object({
                title: Yup.string()
                    .max(20, 'Must be 20 characters or less'), 
            })}
            onSubmit={async(values) => {
                let songId = props.match.params.id;
                let fd = new FormData();
                fd.append("title", values.title);
                fd.append("file", values.file);
                fd.append("songId",songId);
                const response = await fetch("api/song/", {
                    method: "PUT",
                    headers:{
                        Authorization: "Bearer "+ await getAccessToken()
                    },
                    body:fd
                })
                if(response.ok){
                    history.push("/");
                }

            }}
        >
            {({ setFieldValue, values,handleChange }) => (
                <Form>
                    <div className="song-change-content">
                        <div>
                            <TextField name="title" onChange={handleChange}  placeholder="Enter a new title for the song (optional)" className="text-field-change-song"/>
                            <ErrorMessage name="title" component="div" />
                        </div>
                        <div>
                            <FileInput  name="file"text={!!values.file ? "The song successfuly loaded" : "Choose a new song for the album (optional)"} onChange={(e) => { setFieldValue("file", e.currentTarget.files[0]); }} />                        </div>
                            <ErrorMessage name="file" component="div" />
                        <div>
                            <Button type="onSubmit">
                                <div className="song-change-button">
                                    <div>
                                        <IoIosCreate size={30} />
                                    </div>
                                    <div>
                                        Change Song
                                    </div>
                                </div>
                            </Button>
                        </div>


                    </div>
                </Form>
            )}
        </Formik>


    )
}
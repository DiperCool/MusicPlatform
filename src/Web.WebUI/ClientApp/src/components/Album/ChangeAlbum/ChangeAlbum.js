import React, { useContext } from "react";
import { Button } from "../../Components/Button/Button";
import { IoIosCreate } from "react-icons/io";
import "./ChangeAlbum.css";
import { TextField } from "../../Components/TextField/TextField";
import { FileInput } from "../../Components/FileInput/FileInput";
import { Formik, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { useHistory } from "react-router-dom";
import { AuthorizationContext } from "../../api-authorization/AuthorizationContext";
import { emitCustomEvent } from 'react-custom-events';
export const ChangeAlbum = (props)=>{
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
                let albumId = props.match.params.id;
                let fd = new FormData();
                fd.append("title", values.title);
                fd.append("file", values.file);
                fd.append("albumId",albumId);
                const response = await fetch("api/album/", {
                    method: "PUT",
                    headers:{
                        Authorization: "Bearer "+ await getAccessToken()
                    },
                    body:fd
                })
                if(response.ok){
                    history.push("/");
                    emitCustomEvent("changeAlbum", {...await response.json(),...{ albumId: albumId}});
                }

            }}
        >
            {({ setFieldValue, values,handleChange }) => (
                <Form>
                    <div className="album-change-content">
                        <div>
                            <TextField name="title" onChange={handleChange}  placeholder="Enter a new title for the album (optional)" className="text-field-change-album"/>
                            <ErrorMessage name="title" component="div" />
                        </div>
                        <div>
                            <FileInput  name="file"text={!!values.file ? "The photo successfuly loaded" : "Choose a new photo for the album (optional)"} onChange={(e) => { setFieldValue("file", e.currentTarget.files[0]); }} />                        </div>
                            <ErrorMessage name="file" component="div" />
                        <div>
                            <Button type="onSubmit">
                                <div className="album-change-button">
                                    <div>
                                        <IoIosCreate size={30} />
                                    </div>
                                    <div>
                                        Change Album
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
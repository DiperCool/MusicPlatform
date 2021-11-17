import React, { useContext } from "react";
import { Button } from "../../Components/Button/Button";
import { IoIosCreate } from "react-icons/io";
import "./AlbumCreate.css";
import { TextField } from "../../Components/TextField/TextField";
import { FileInput } from "../../Components/FileInput/FileInput";
import { Formik, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { useHistory } from "react-router-dom";
import { AuthorizationContext } from "../../api-authorization/AuthorizationContext";
export const AlbumCreate = () => {
    let { getAccessToken}= useContext(AuthorizationContext);
    let history = useHistory();
    return (
        <Formik
            initialValues={{ title: '', file: null }}
            validationSchema={Yup.object({
                title: Yup.string()
                    .max(20, 'Must be 20 characters or less')
                    .required('A titile is required'),
                file: Yup.mixed().required('A file is required')
       
            })}
            onSubmit={async(values) => {
                let fd = new FormData();
                fd.append("title", values.title);
                fd.append("file", values.file);
                const response = await fetch("api/album/", {
                    method: "POST",
                    headers:{
                        Authorization: "Bearer "+ await getAccessToken()
                    },
                    body:fd
                })
                if(response.ok) history.push("/");

            }}
        >
            {({ setFieldValue, values,handleChange }) => (
                <Form>
                    <div className="album-create-content">
                        <div>
                            <TextField name="title" onChange={handleChange} text="hello" placeholder="Enter a title for a new album" className="oleg" />
                            <ErrorMessage name="title" component="div" />
                        </div>
                        <div>
                            <FileInput  name="file"text={!!values.file ? "The photo successfuly loaded" : "Choose a photo for a new album"} onChange={(e) => { setFieldValue("file", e.currentTarget.files[0]); }} />                        </div>
                            <ErrorMessage name="file" component="div" />
                        <div>
                            <Button type="onSubmit">
                                <div className="album-create-button">
                                    <div>
                                        <IoIosCreate size={30} />
                                    </div>
                                    <div>
                                        Create Album
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


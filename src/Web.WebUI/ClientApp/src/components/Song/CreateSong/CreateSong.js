import React, { useContext } from 'react'
import { useRedirectAlbumNotArtist } from '../../Hooks/useRedirectAlbumNotArtist'
import { Button } from "../../Components/Button/Button";
import { IoIosCreate } from "react-icons/io";
import "./CreateSong.css";
import { TextField } from "../../Components/TextField/TextField";
import { FileInput } from "../../Components/FileInput/FileInput";
import { Formik, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { useHistory } from "react-router-dom";
import { AuthorizationContext } from "../../api-authorization/AuthorizationContext";
export const CreateSong =(props)=>{
    let history = useHistory();
    let {getAccessToken}= useContext(AuthorizationContext);
    useRedirectAlbumNotArtist(props.match.params.id);
    return(
        <Formik
            initialValues={{ title: '', file: null }}
            validationSchema={Yup.object({
                title: Yup.string()
                    .max(20, 'Must be 20 characters or less'),
                file: Yup.mixed().required('A file is required')
            })}
            onSubmit={async(values) => {
                let albumId = props.match.params.id;
                let fd = new FormData();
                fd.append("Title", values.title);
                fd.append("File", values.file);
                fd.append("AlbumId",albumId);
                const response = await fetch("api/song/", {
                    method: "POST",
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
                    <div className="song-create-content">
                        <div>
                            <TextField name="title" onChange={handleChange}  placeholder="Enter a title for a new song" className="text-field-song-create"/>
                            <ErrorMessage name="title" component="div" />
                        </div>
                        <div>
                            <FileInput  name="file"text={!!values.file ? "The song successfuly loaded" : "Choose a new song for the album"} onChange={(e) => { setFieldValue("file", e.currentTarget.files[0]); }} />                        </div>
                            <ErrorMessage name="file" component="div" />
                        <div>
                            <Button type="onSubmit">
                                <div className="song-create-button">
                                    <div>
                                        <IoIosCreate size={30} />
                                    </div>
                                    <div>
                                        Create Song
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
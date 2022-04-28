import React from "react"
import {Link} from "react-router-dom"
import { TextWithIcon } from "../../Components/TextWithIcon/TextWithIcon"
import {BsSearch} from "react-icons/bs";

export const SearchButton = ()=>{


    return (
        <div>
            <Link to="/search">
                <TextWithIcon text="Search" icon={<BsSearch size={30}/>} className="search-content"/>
            </Link>
        </div>
    )
}
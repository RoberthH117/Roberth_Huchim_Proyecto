import { ForgetI } from "./forget.interface";

export interface DataI{
    //dataResponse:ForgetI;

     dataResponse : Array<{ id: string; email: string }>;
    }


    export interface RolI{
        dataResponse:Array<{id:string;name:string;descripcion:string}>;
    }
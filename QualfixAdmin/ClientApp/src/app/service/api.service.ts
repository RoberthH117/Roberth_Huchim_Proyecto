import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {LoginI} from '../Modelos/login.interface';
import { ResponseI } from '../Modelos/response.interface';
import { ForgetI } from '../Modelos/forget.interface';
import { DataI } from '../Modelos/Data.interface';



@Injectable({
  providedIn: 'root'
})
export class ApiService {


private uriApi='https://roberth-huchim-proyecto.onrender.com/Login/';


  constructor(private https:HttpClient) { }


  loginByEmail(form:LoginI):Observable<ResponseI>{

let direccion=this.uriApi+'Validar';
debugger
return this.https.post<ResponseI>(direccion,form);

  }

  GetComprobation(): Observable<boolean> {
    debugger
    const direccion = `${this.uriApi}Comprobar`;

    return this.https.get<boolean>(direccion);
  }


forgetByEmail(form:ForgetI){

let direccion=this.uriApi+'Forget';

return this.https.post<DataI>(direccion,form);

//return this.https.post(direccion, form).subscribe();


}







}

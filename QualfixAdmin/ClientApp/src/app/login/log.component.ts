import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder }  from '@angular/forms';
import {ApiService} from '../service/api.service'
import {LoginI} from '../Modelos/login.interface'
import { Observable } from 'rxjs';
import { ResponseI } from '../Modelos/response.interface';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.css']
})
export class LogComponent implements OnInit {
  mostrarContrasena = false;
  loginForm= new FormGroup({
correo: new FormControl('',[
  Validators.required,
  Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
  passwordHash : new FormControl('',[Validators.required, 
Validators.minLength(5)
])
  }
  )

  submitted: boolean = false;

constructor(private api:ApiService, private router:Router, private cookieService: CookieService,private fb: FormBuilder ) {

  
}

toggleMostrarContrasena() {
  this.mostrarContrasena = !this.mostrarContrasena;
}

ngOnInit(): void{

}
navigateToDetails() {
  this.router.navigate(['/soli']);
}


mdlSampleIsOpen : boolean = false;
ErrorIsOpen : boolean= false;
openModal(open : boolean) : void {
this.mdlSampleIsOpen = open;
}

ErrorModal(Error : boolean): void{
this.ErrorIsOpen=Error;

}

Redireccionar():void{

  this.router.navigate(['home']);
}


onLogin(form:any){

console.log(form);



 this.api.loginByEmail(form).subscribe(data=>{

//let dataResponse:ResponseI = data;



if(data.status=="200"){
// Después de una autenticación exitosa, recibe el token del servidor.
const token = data.token; // Reemplaza con tu token

// Establece la cookie en el navegador.
//this.cookieService.set('Authorization', token, undefined, '/', undefined, true, 'Strict');
localStorage.setItem('Authorization', token);
 this.mdlSampleIsOpen=true;
this.ErrorIsOpen=false;


}
else{
this.mdlSampleIsOpen=false;
this.ErrorIsOpen=true;

}

 })
 
} 


  
}

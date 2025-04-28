import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { Router } from '@angular/router';

import { ForgetI } from 'src/app/Modelos/forget.interface';
import { ResponseI } from 'src/app/Modelos/response.interface';
import { ApiService } from 'src/app/service/api.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent implements OnInit{




  loginForm= new FormGroup({
    correo: new FormControl('',[
      Validators.required,
      Validators.email]),
    
    id: new FormControl('',[Validators.required,
      
    
    ])
      }
      )
    
      submitted: boolean = false;
    
    constructor(private api:ApiService, private router:Router ) {}
    

    
    
    ngOnInit(): void{

      
    
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
     this.router.navigate(['']);
}

    onLogin(form:any){
    
    
      console.log(form);    
    




     this.api.forgetByEmail(form).subscribe(data=>{
    
 
    


    if(data.dataResponse!=null){
      this.mdlSampleIsOpen=true;
      this.ErrorIsOpen=false;
    
   



    }
    else{
    
      console.log("No llega");
      this.mdlSampleIsOpen=false;
      this.ErrorIsOpen=true;
      
 
    }
    
     })
     
    } 







}

import { Component, OnInit } from '@angular/core';
import { DatosUsuario } from 'src/app/Modelos/Licencia.interface';
import { LicenciaService } from 'src/app/service/licencia.service';

@Component({
  selector: 'app-perfil-usuario',
  templateUrl: './perfil-usuario.component.html',
  styleUrls: ['./perfil-usuario.component.css']
})
export class PerfilUsuarioComponent implements OnInit{
  datos:DatosUsuario | undefined;
  constructor(private licencia:LicenciaService){

    

  }

  ngOnInit(){
    

this.obtener();

  }


obtener(){
  this.licencia.getLicenciaActual().subscribe(
    (data)=>{
      console.log(data);
      debugger
      this.datos=data;
    }
  )
}





}


import { MatDialog } from '@angular/material/dialog';
import { ModalRegUsuariosComponent } from '../modal-reg-usuarios/modal-reg-usuarios.component';
import { Usuariomodel } from './../../Models/Usuario-interface';
import { UsuarioService } from './../../Services/usuario.service';
import { Component, OnInit } from '@angular/core';
import { ModalEditUsuariosComponent } from '../modal-edit-usuarios/modal-edit-usuarios.component';
import { ModalEliminarUsuariosComponent } from '../modal-eliminar-usuarios/modal-eliminar-usuarios.component';
import { PerfilService } from 'src/app/service/perfil.service';
import { RolService } from 'src/app/service/rol.service';


@Component({
  selector: 'app-registro-usuarios',
  templateUrl: './registro-usuarios.component.html',
  styleUrls: ['./registro-usuarios.component.css']
})
export class RegistroUsuariosComponent implements OnInit{
  idUsuarioSeleccionado: string = '';
  usuarios: Usuariomodel[] = []; 
  
   
  constructor(private  usuarioService:UsuarioService, public dialog: MatDialog,private perfil:PerfilService,private rol:RolService) {
    
  }
  ngOnInit(): void {
      this.cargartodosusuarios();
    
  }
 



  openDialog() {
    const dialogRef = this.dialog.open(ModalRegUsuariosComponent);
 
  }
  openDialogeditar(id: string) {
   
    const dialogRef = this.dialog.open(ModalEditUsuariosComponent, {
      data: { id }
    });
  }
  openDialogeliminar(id: string) {
    
    const dialogRef = this.dialog.open(ModalEliminarUsuariosComponent, {
      data: { id }
    });
  }
  public cargartodosusuarios (){
debugger
this.usuarioService.getAllUsersWithProfiles().subscribe(data=>{
  
  console.log(data);
  this.usuarios=data;
})


  }




  editarUsuario(Id: string) {
    this.idUsuarioSeleccionado = Id;
    
  }
}

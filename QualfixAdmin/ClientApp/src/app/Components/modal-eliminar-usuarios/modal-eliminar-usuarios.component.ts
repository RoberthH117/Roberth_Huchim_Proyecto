import { Component, Inject, OnInit } from '@angular/core';
import { UsuarioService } from 'src/app/Services/usuario.service';

import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Usuariomodel } from 'src/app/Models/Usuario-interface';
@Component({
  selector: 'app-modal-eliminar-usuarios',
  templateUrl: './modal-eliminar-usuarios.component.html',
  styleUrls: ['./modal-eliminar-usuarios.component.css']
})
export class ModalEliminarUsuariosComponent implements OnInit{
  usuario: Usuariomodel = {
    id: '', 
    firstName: '',
    lastName: '',
    status: 1,
    activo: false,
    userName: '',
    normalizedUserName: '',
    email: '',
    normalizedEmail: '',
    emailConfirmed: false,
    passwordHash: '',
    securityStamp: '',
    concurrencyStamp: '',
    phoneNumber: '',
    phoneNumberConfirmed: false,
    twoFactorEnabled: false,
    lockoutEnabled: false,
    accessFailedCount: 0
};

  constructor(private usuarioService:UsuarioService,@Inject(MAT_DIALOG_DATA) public data: Usuariomodel,){
    this.usuario.id = data.id;
  }
  ngOnInit(): void {
    
  }

  Eliminar(id:string){
    this.usuarioService.EliminarUsuario(`https://roberth-huchim-proyecto.onrender.com/api/usuario/${this.usuario.id}`);
    
  }
}

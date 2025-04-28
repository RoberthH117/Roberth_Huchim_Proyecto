import { Component, OnInit } from '@angular/core';
import { RolService } from '../service/rol.service';
import { ModeloPermisos } from '../Modelos/Permisos.interface';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{

 
  constructor(private rolService: RolService, private authService: AuthService) {}

  
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    console.log('presionado')
    this.isExpanded = !this.isExpanded;
  }

  ModuloRol:boolean=false;
  ModuloTipoLicencia:boolean=false;
  ModuloPerfil:boolean=false;
  ModuloLicencia:boolean=false;
  ModuloHistorialLicencia:boolean=false;
  ModuloConfigCfdi:boolean=false;
  ModuloClientes:boolean=false;
  ModuloProductos:boolean=false;
  ModuloUsuarios:boolean=false;
  ModuloPerfilUsuario:boolean=false;
  ModuloTicketRegistro:boolean=false;
  ModuloComprarLicencia:boolean=false;



  cerrarSesion() {
    // Llama al método del servicio de autenticación para cerrar la sesión.
    this.authService.cerrarSesion();
  }
  ngOnInit() {

    // Llama al servicio para obtener los permisos y actualiza VerInventario en función de la respuesta.
    this.rolService.GetPermisos().subscribe((permisos: ModeloPermisos) => {
      debugger
      // VerInventario será true si permisos.VerInventario es true, de lo contrario, será false.
      
      this.ModuloRol = permisos.moduloRol;
      this.ModuloTipoLicencia=permisos.moduloTipoLicencia;
      this.ModuloPerfil=permisos.moduloPerfil;
      this.ModuloLicencia=permisos.moduloLicencia;
      this.ModuloHistorialLicencia=permisos.moduloHistorialLicencia;
      this.ModuloClientes=permisos.moduloClientes;
      this.ModuloProductos=permisos.moduloProductos;
      this.ModuloUsuarios=permisos.moduloUsuario;
      this.ModuloConfigCfdi=permisos.moduloConfigCfdi;
      this.ModuloPerfilUsuario=permisos.moduloPerfilUsuario;
      this.ModuloTicketRegistro=permisos.moduloTicketRegistro;  
      this.ModuloComprarLicencia=permisos.moduloComprarLicencia;
         
    
    });
  }





}

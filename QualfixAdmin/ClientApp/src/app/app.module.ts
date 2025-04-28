import { ModalEditProductosComponent } from './Components/modal-edit-productos/modal-edit-productos.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { RegistroUsuariosComponent } from './Components/registro-usuarios/registro-usuarios.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialsModule } from './Shared/angular-materials/angular-materials.module';
import { ForgetPasswordComponent } from './login/forget-password/forget-password.component';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ModalRegUsuariosComponent } from './Components/modal-reg-usuarios/modal-reg-usuarios.component';
import { ModalEditUsuariosComponent } from './Components/modal-edit-usuarios/modal-edit-usuarios.component';
import { LogComponent } from './login/log.component';
import { NuevoRolComponent } from './Components/nuevorol/nuevo-rol/nuevo-rol.component';
import { MatButtonModule } from '@angular/material/button';
import { ModalregistrorolComponent } from './Components/ModalRol/modalregistrorol/modalregistrorol.component';
import { ModaleditarrolComponent } from './Components/ModalRol/modaleditarrol/modaleditarrol.component';
import { AuthInterceptor } from './auth.interceptor';
import { MatDialogModule } from '@angular/material/dialog';
import { RolService } from './service/rol.service';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatPaginatorModule } from '@angular/material/paginator';
import { AuthService } from './auth.service';
import { TipoLicenciaComponent } from './Components/tipo-licencia/tipo-licencia.component';
import { ModalregistrotipolicenciaComponent } from './Components/ModalTipoLicencia/modalregistrotipolicencia/modalregistrotipolicencia.component';
import { ModaleditartipolicenciaComponent } from './Components/ModalTipoLicencia/modaleditartipolicencia/modaleditartipolicencia.component';
import { LoadingSpinnerComponent } from './Components/loading-spinner/loading-spinner.component';
import { PerfilComponent } from './Components/perfil/perfil.component';
import { PerfilRegistroComponent } from './Components/modalPerfil/perfil-registro/perfil-registro.component';
import { PerfilService } from './service/perfil.service';
import { PerfilEditarComponent } from './Components/modalPerfil/perfil-editar/perfil-editar.component';
import { ModalEliminarUsuariosComponent } from './Components/modal-eliminar-usuarios/modal-eliminar-usuarios.component';
import { AsignarLicenciaComponent } from './Components/asignar-licencia/asignar-licencia.component';
import { HistorialLicenciaComponent } from './Components/historial-licencia/historial-licencia.component';
import { PerfilUsuarioComponent } from './Components/perfil-usuario/perfil-usuario.component';

import { SolicitudRegistroUsuarioComponent } from './Components/solicitud-registro-usuario/solicitud-registro-usuario.component';

import { ProductosComponent } from './Components/productos/productos.component';
import { ModalRegProductosComponent } from './Components/modal-reg-productos/modal-reg-productos.component';
import { ModalEliminarProductosComponent } from './Components/modal-eliminar-productos/modal-eliminar-productos.component';
import { ModuloClientesComponent } from './Components/modulo-clientes/modulo-clientes.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { ModalRegClienteComponent } from './Components/modal-reg-cliente/modal-reg-cliente.component';
import { ModalRegConfigCFDIComponent } from './Components/modal-reg-config-cfdi/modal-reg-config-cfdi.component';
import { ConfigCFDIComponent } from './Components/config-cfdi/config-cfdi.component';
import { CommonModule, DatePipe } from '@angular/common';
import { ModalEditConfigCFDIComponent } from './Components/modal-edit-config-cfdi/modal-edit-config-cfdi.component';
import { ModalEliminarConfigCFDIComponent } from './Components/modal-eliminar-config-cfdi/modal-eliminar-config-cfdi.component';
import { ModalInfoClienteComponent } from './Components/modal-info-cliente/modal-info-cliente.component';
import { ModalEliminarClienteComponent } from './Components/modal-eliminar-cliente/modal-eliminar-cliente.component';
import { ModalEditClienteComponent } from './Components/modal-edit-cliente/modal-edit-cliente.component';
import { TicketRegistroComponent } from './Components/ticket-registro/ticket-registro.component';
import { RevisionTicketComponent } from './Components/revision-ticket/revision-ticket.component';
import { TicketVerMasComponent } from './Components/ticket-ver-mas/ticket-ver-mas.component';
import { TicketUserHistorialMasComponent } from './Components/ticket-user-historial-mas/ticket-user-historial-mas.component';
import { TicketUserHistorialComponent } from './Components/ticket-user-historial/ticket-user-historial.component';
import { TicketService } from './service/ticket.service';
import { ComprarLicenciaComponent } from './Components/comprar-licencia/comprar-licencia.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegistroUsuariosComponent,
    ModalRegUsuariosComponent,
    ModalEditUsuariosComponent,
    ModalEliminarUsuariosComponent,
    LogComponent,
    ForgetPasswordComponent,
    NuevoRolComponent,
    ModalregistrorolComponent,
    ModaleditarrolComponent,
    TipoLicenciaComponent,
    ModalregistrotipolicenciaComponent,
    ModaleditartipolicenciaComponent,
    LoadingSpinnerComponent,
    PerfilComponent,
    PerfilRegistroComponent,
    PerfilEditarComponent,
    AsignarLicenciaComponent,
    HistorialLicenciaComponent,
    PerfilUsuarioComponent,
    TicketRegistroComponent,
    RevisionTicketComponent,
    TicketVerMasComponent,
    TicketUserHistorialMasComponent,
    TicketUserHistorialComponent, ComprarLicenciaComponent,
    TicketRegistroComponent,
    SolicitudRegistroUsuarioComponent,
    ProductosComponent,
    ModalEditProductosComponent,
    ModalRegProductosComponent,
    ModalEliminarProductosComponent,
    ModuloClientesComponent,
    ModalRegClienteComponent,
    ModalRegConfigCFDIComponent,
    ConfigCFDIComponent,
    ModalEditConfigCFDIComponent,
    ModalEliminarConfigCFDIComponent,
    ModalInfoClienteComponent,
    ModalEliminarClienteComponent,
    ModalEditClienteComponent,

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialsModule,
    MatIconModule,
    HttpClientModule,
    MatButtonModule,
    MatDialogModule,
    DragDropModule,
    MatPaginatorModule,


    NgxDropzoneModule,

    RouterModule.forRoot([
      { path: '', component: LogComponent, pathMatch: 'full',  },
      { path: 'login', component: LogComponent },
      {
        path: '',
        component: NavMenuComponent,
        canActivate: [AuthService],
        children: [
      
          { path: 'home', component: HomeComponent, pathMatch: 'full', canActivate: [AuthService] },
          { path: 'registrousuarios', component: RegistroUsuariosComponent, pathMatch: 'full', canActivate: [AuthService] },
          { path: 'reg', component: ModalRegUsuariosComponent, pathMatch: 'full', canActivate: [AuthService] },
          { path: 'edit', component: ModalEditUsuariosComponent, pathMatch: 'full', canActivate: [AuthService] },
          { path: 'soli', component: SolicitudRegistroUsuarioComponent, pathMatch: 'full' },
          { path: 'modclientes', component: ModuloClientesComponent, pathMatch: 'full' },
          { path: 'productos', component: ProductosComponent, pathMatch: 'full' },
          { path: 'configCFDI', component: ConfigCFDIComponent, pathMatch: 'full' },
          { path: 'perfil', component: PerfilComponent, canActivate: [AuthService] },
          { path: 'perfil-registro', component: PerfilRegistroComponent },
          { path: 'asignar-licencia', component: AsignarLicenciaComponent },
          { path: 'licencias', component: HistorialLicenciaComponent },
          { path: 'TipoLicencia', component: TipoLicenciaComponent },
          { path: 'PerfilUsuario', component: PerfilUsuarioComponent },
          { path: 'Rol', component: NuevoRolComponent, canActivate: [AuthService] },
        

          { path: 'perfil', component: PerfilComponent, canActivate: [AuthService] },
          { path: 'perfil-registro', component: PerfilRegistroComponent },
          { path: 'asignar-licencia', component: AsignarLicenciaComponent },
          { path: 'licencias', component: HistorialLicenciaComponent },
          { path: 'TipoLicencia', component: TipoLicenciaComponent },
          { path: 'PerfilUsuario', component: PerfilUsuarioComponent },
          { path: 'Ticket', component: TicketRegistroComponent, pathMatch: 'full', canActivate: [AuthService] },
          { path: 'Revision', component: RevisionTicketComponent },
          { path: 'HistorialTicket', component: TicketUserHistorialComponent, pathMatch: 'full' },
          { path: 'UsuarioComprar', component: ComprarLicenciaComponent },



          { path: 'Rol', component: NuevoRolComponent, canActivate: [AuthService] },


          { path: 'forget', component: ForgetPasswordComponent },
        ]
      },


    ]),




  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,

    }, RolService, PerfilService, TicketService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
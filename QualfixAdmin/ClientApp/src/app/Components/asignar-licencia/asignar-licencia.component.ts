import { Component } from '@angular/core';
import { LicenciaService } from 'src/app/service/licencia.service';
import { TipoLicenciaComponent } from '../tipo-licencia/tipo-licencia.component';
import { TipoLicenciaService } from 'src/app/service/tipo-licencia.service';
import { LicenciaModel } from 'src/app/Modelos/Licencia.interface';
import { NgForm } from '@angular/forms';
import { Usuariomodel } from 'src/app/Models/Usuario-interface';


@Component({
  selector: 'app-asignar-licencia',
  templateUrl: './asignar-licencia.component.html',
  styleUrls: ['./asignar-licencia.component.css']
})
export class AsignarLicenciaComponent {
  usuarios: Usuariomodel[] = []; // Debes ajustar el tipo de datos según tu modelo de usuario
  licencias: any[]=[];
  selectedUsuarioId: string = ''; // Almacena el ID del usuario seleccionado
  selectedLicenciaId: number |undefined; // Almacena el ID de la licencia seleccionada
  mensaje: string = ''; // Variable para almacenar el mensaje
  formSubmitted: boolean = false; // Variable para rastrear si se ha enviado el formulario

  constructor(private licenciaService: LicenciaService, private tipoLicencia: TipoLicenciaService) {}
  ngOnInit() {
    this.getUsuarios();
    this.getLicenias();
  }


  getUsuarios() {
    
    this.licenciaService.getUsuariosEmpresa().subscribe(
      data => {
        
        this.usuarios = data; // Asigna los usuarios obtenidos al arreglo
      },
      error => {
        console.error('Error al obtener usuarios', error);
      }
    );
  }

  getLicenias(){
    this.tipoLicencia.GetAllTipoLicencia().subscribe(
      data=>{
        this.licencias=data;
      },
      error=>{
        console.error('Error al obtener licencias', error);
      }
    );
  }

  // En tu componente TypeScript
obtenerEmailUsuarioSeleccionado(): string {
  const usuarioSeleccionado = this.usuarios.find(user => user.id === this.selectedUsuarioId);
  return usuarioSeleccionado ? usuarioSeleccionado.email : '';
}

obtenerNombreUsuarioSeleccionado(): string {
  const usuarioSeleccionado = this.usuarios.find(user => user.id === this.selectedUsuarioId);
  return usuarioSeleccionado ? usuarioSeleccionado.firstName : '';
}

  mdlSampleIsOpen : boolean = false;
  ErrorIsOpen : boolean= false;
  openModal(open : boolean) : void {
  this.mdlSampleIsOpen = open;
  }
  
  ErrorModal(Error : boolean): void{
  this.ErrorIsOpen=Error;
  
  }
  onSubmit() {

    this.formSubmitted = true; // Marcar que se ha enviado el formulario

    // Verifica que se haya seleccionado un usuario y una licencia
    if (this.selectedUsuarioId && this.selectedLicenciaId) {
      // Crea un objeto LicenciaModel con los datos seleccionados
      const request: LicenciaModel = {
        Id_Usuario: this.selectedUsuarioId,
        TipoId: this.selectedLicenciaId
      };

      // Llama al método generarLicencia con el objeto request
      this.licenciaService.generarLicencia(request).subscribe(
        response => {
          // Maneja la respuesta del servicio, si es necesario
          this.mensaje = 'Licencia generada con éxito';
          this.selectedUsuarioId = '';
        this.selectedLicenciaId = undefined;
        this.formSubmitted = false;
        this.mdlSampleIsOpen=true;
this.ErrorIsOpen=false;
        },
        error => {
          console.error('Error al generar licencia', error);
         
          this.mensaje = 'Error al generar licencia';
          this.mdlSampleIsOpen=false;
          this.ErrorIsOpen=true;
        }
      );
    } else {
      console.error('Por favor, selecciona un usuario y una licencia antes de enviar el formulario.');
    }
  }





}

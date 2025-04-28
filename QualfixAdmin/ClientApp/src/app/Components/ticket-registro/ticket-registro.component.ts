import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TicketService } from 'src/app/service/ticket.service';
import { NgxDropzoneModule } from 'ngx-dropzone';


@Component({
  selector: 'app-ticket-registro',
  templateUrl: './ticket-registro.component.html',
  styleUrls: ['./ticket-registro.component.css']
})
export class TicketRegistroComponent {
  
  @ViewChild('formulario') formulario!: NgForm;
  
  imagenesSeleccionadas: FileList | undefined;

  constructor(private ticketService: TicketService) {}

  ngOnInit(): void {
    
  }
  files: File[] = [];

  agregarElemento(){}
  onSelect(event: { addedFiles: any; }) {
    if (this.files.length + event.addedFiles.length <= 3) {
      this.files.push(...event.addedFiles);
    } else {
      // Mostrar mensaje de error o tomar alguna otra acción cuando se excede el límite de imágenes
      console.error('Se ha excedido el límite de imágenes permitidas.');
    }
  }
  
  onRemove(event: File) {
    console.log(event);
    this.files.splice(this.files.indexOf(event), 1);
  }

  
  enviarTicket() {
    debugger
    if (this.formulario.valid) {
      const titulo = this.formulario.value.titulo;
      const descripcion = this.formulario.value.descripcion;

      // Verifica si imagenesSeleccionadas no es undefined antes de llamar al servicio
      if (this.files) {
        this.ticketService.enviarTicket(titulo, descripcion, this.files).subscribe(
          response => {
            // Manejar la respuesta exitosa aquí
            console.log('Respuesta exitosa:', response);
          },
          error => {
            // Manejar el error aquí
            console.error('Error en la solicitud:', error);
          }
        );
      } else {
        // Manejo de errores o mensajes para el usuario cuando no hay imágenes seleccionadas
      }
    } else {
      // Manejo de errores o mensajes para el usuario si el formulario no es válido
    }
  }

  
  seleccionarImagenes(event: any) {
    debugger
    try {
      this.imagenesSeleccionadas = event.target.files;
    } catch (error) {
      console.error('Error al seleccionar imágenes:', error);
      // Manejo de errores o mensajes para el usuario
    }
  }

}
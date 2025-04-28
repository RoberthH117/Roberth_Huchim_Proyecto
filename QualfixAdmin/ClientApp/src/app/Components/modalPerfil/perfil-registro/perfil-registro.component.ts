import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { GetAllAccesosRequest, NuevoPerfilModel } from 'src/app/Modelos/Perfil.interface';
import { PerfilService } from 'src/app/service/perfil.service';

@Component({
  selector: 'app-perfil-registro',
  templateUrl: './perfil-registro.component.html',
  styleUrls: ['./perfil-registro.component.css']
})
export class PerfilRegistroComponent implements OnInit{
  

  permisos: string[] = [];
  asignados: string[] = [];
  
  CrearPerfil: FormGroup;
  elementosasi!: string;
  
      submitted: boolean = false;
      asignadosNotEmpty = false; // Agrega esta propiedad
  constructor(private  rolService:PerfilService,private router: Router,public dialogRef: MatDialogRef<PerfilRegistroComponent>,private formBuilder: FormBuilder) {
  

  this.CrearPerfil = this.formBuilder.group({
    name: ['', Validators.required],
    descripcion: ['', Validators.required]
  });



    const elementosAsignados = this.asignados;
    const elementos=this.permisos;





  }
  ngOnInit(): void {

     this.rolService.getListaDeAccesos().subscribe(
      (resultado) => {
debugger
        resultado.forEach((perfil: GetAllAccesosRequest) => {

          //this.permisos = this.permisosDisponibles.map((valor) => ({ valor }));
          this.permisos.push(perfil.nombre); 
        // Reemplaza "algunCampo" por el campo que deseas agregar al arreglo
        });

      
        // Realiza acciones después de agregar el rol, por ejemplo, redireccionar o cerrar el modal.
        console.log("Exito");
        
       
        // Notifica a los observadores que se ha agregado un nuevo rol
     
        
      },
      (error) => {
        console.error('Error al obtener rol', error);
      }

     );

    
  }

  

  
  drop(event: CdkDragDrop<string[]>): void {
    if (event.previousContainer === event.container) {
      // Mover dentro del mismo contenedor
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      // Mover a un contenedor diferente
      transferArrayItem<string>(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
    }
    this.asignadosNotEmpty = this.asignados.length > 0;
  }

 
  onSubmit(form:any) {
   debugger
    if (form.valid) { // Asegúrate de que el formulario sea válido
      // Obtén los datos del formulario
      const elementosAsignadosString = this.asignados.join(',');
       // Convierte el arreglo en una cadena separada por comas
      const nuevoRol: NuevoPerfilModel = {
        // Puedes dejarlo vacío si el servidor genera el ID automáticamente
      
        Perfil: form.value.name, // Reemplaza 'name' con el nombre del campo en tu formulario
        Descripcion: form.value.descripcion, // Reemplaza 'descripcion' con el nombre del campo en tu formulario
        Accesos: this.asignados
     
      };

 console.log(nuevoRol);
 if (!this.asignadosNotEmpty) {
  return;
}
debugger
        this.rolService.insertarNuevoPerfil(nuevoRol).subscribe(
          (resultado) => {

           debugger
            // Realiza acciones después de agregar el rol, por ejemplo, redireccionar o cerrar el modal.
            console.log("Exito");
            this.dialogRef.close(); // Cierra el modal
           
            // Notifica a los observadores que se ha agregado un nuevo rol
            this.rolService.notifyNuevoPerfilModificado(resultado);
            
          },
          (error) => {
            debugger
            console.error('Error al agregar el perfil', error);
          }
        );

      

    }


   
  }
  cancelar() {
    this.dialogRef.close();
 }
}
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators,FormBuilder } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { RolI } from 'src/app/Modelos/Data.interface';
import { PermisosRequest, PermissionsRolRequest, RolInterface, RolInterfacePost } from 'src/app/Modelos/rol.interface';
import { RolService } from 'src/app/service/rol.service';
import {NgFor} from '@angular/common';
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
  CdkDrag,
  CdkDropList,
} from '@angular/cdk/drag-drop';


@Component({
  selector: 'app-modalregistrorol',
  templateUrl: './modalregistrorol.component.html',
  styleUrls: ['./modalregistrorol.component.css'],
})
export class ModalregistrorolComponent implements OnInit{
  

  permisos: string[] = [];
  asignados: string[] = [];
  
  CrearRol: FormGroup;
  elementosasi!: string;
  
      submitted: boolean = false;
      asignadosNotEmpty = false; // Agrega esta propiedad
  constructor(private  rolService:RolService,private router: Router,public dialogRef: MatDialogRef<ModalregistrorolComponent>,private formBuilder: FormBuilder) {
  

  this.CrearRol = this.formBuilder.group({
    name: ['', Validators.required],
    descripcion: ['', Validators.required]
  });



    const elementosAsignados = this.asignados;
    const elementos=this.permisos;





  }
  ngOnInit(): void {

     this.rolService.GetAllPermissionsList().subscribe(
      (resultado) => {
debugger
        resultado.forEach((rol: PermisosRequest) => {

          //this.permisos = this.permisosDisponibles.map((valor) => ({ valor }));
          this.permisos.push(rol.name); 
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
    const valorDelElemento = event.item.element.nativeElement.textContent;
    console.log('Valor del elemento arrastrable:', valorDelElemento);
    console.log('Event.previousContainer.id:', event.previousContainer.id);
    console.log('Event.container.id:', event.container.id);
    debugger;
  
    if (event.previousContainer === event.container) {
     // Mueve el elemento dentro del mismo contenedor.
  moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
} else {
      if (event.previousContainer.id === 'asignadosList' && event.container.id === 'permisosList' && valorDelElemento === 'Ver Rol') {
        // Verifica si "Editar Rol", "Eliminar Rol" o "Crear Rol" están presentes en el contenedor de origen (asignadosList).
        const contieneEditarRol = event.previousContainer.data.includes('Editar Rol');
        const contieneEliminarRol = event.previousContainer.data.includes('Eliminar Rol');
        const contieneCrearRol = event.previousContainer.data.includes('Crear Rol');
  
        // Elimina "Ver Rol" del contenedor anterior (asignadosList).
        transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
  
        // Transfiere "Editar Rol", "Eliminar Rol" y/o "Crear Rol" si están presentes en asignadosList y no están en permisosList.
        if (contieneEditarRol && !event.container.data.includes('Editar Rol')) {
          transferArrayItem(['Editar Rol'], event.container.data, 0, 1);
        }
        if (contieneEliminarRol && !event.container.data.includes('Eliminar Rol')) {
          transferArrayItem(['Eliminar Rol'], event.container.data, 0, 1);
        }
        if (contieneCrearRol && !event.container.data.includes('Crear Rol')) {
          transferArrayItem(['Crear Rol'], event.container.data, 0, 1);
        }
        
        // Elimina "Editar Rol", "Eliminar Rol" y "Crear Rol" del contenedor anterior si están presentes.
        if (contieneEditarRol) {
          const indexToRemove = event.previousContainer.data.indexOf('Editar Rol');
          if (indexToRemove !== -1) {
            event.previousContainer.data.splice(indexToRemove, 1);
          }
        }
        if (contieneEliminarRol) {
          const indexToRemove = event.previousContainer.data.indexOf('Eliminar Rol');
          if (indexToRemove !== -1) {
            event.previousContainer.data.splice(indexToRemove, 1);
          }
        }
        if (contieneCrearRol) {
          const indexToRemove = event.previousContainer.data.indexOf('Crear Rol');
          if (indexToRemove !== -1) {
            event.previousContainer.data.splice(indexToRemove, 1);
          }
        }
      } else {



        if (event.previousContainer.id === 'asignadosList' && event.container.id === 'permisosList' && valorDelElemento === 'Ver Tipo Licencia') {
          // Verifica si "Editar Tipo Licencia", "Eliminar Tipo Licencia" o "Crear Tipo Licencia" están presentes en el contenedor de origen (asignadosList).
          const contieneEditarTipoLicencia = event.previousContainer.data.includes('Editar Tipo Licencia');
          const contieneEliminarTipoLicencia = event.previousContainer.data.includes('Eliminar Tipo Licencia');
          const contieneCrearTipoLicencia = event.previousContainer.data.includes('Crear Tipo Licencia');
    
          // Elimina "Ver Tipo Licencia" del contenedor anterior (asignadosList).
          transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
    
          // Transfiere "Editar Tipo Licencia", "Eliminar Tipo Licencia" y/o "Crear Tipo Licencia" si están presentes en asignadosList y no están en permisosList.
          if (contieneEditarTipoLicencia && !event.container.data.includes('Editar Tipo Licencia')) {
            transferArrayItem(['Editar Tipo Licencia'], event.container.data, 0, 1);
          }
          if (contieneEliminarTipoLicencia && !event.container.data.includes('Eliminar Tipo Licencia')) {
            transferArrayItem(['Eliminar Tipo Licencia'], event.container.data, 0, 1);
          }
          if (contieneCrearTipoLicencia && !event.container.data.includes('Crear Tipo Licencia')) {
            transferArrayItem(['Crear Tipo Licencia'], event.container.data, 0, 1);
          }
          
          // Elimina "Editar Tipo Licencia", "Eliminar Tipo Licencia" y "Crear Tipo Licencia" del contenedor anterior si están presentes.
          if (contieneEditarTipoLicencia) {
            const indexToRemove = event.previousContainer.data.indexOf('Editar Tipo Licencia');
            if (indexToRemove !== -1) {
              event.previousContainer.data.splice(indexToRemove, 1);
            }
          }
          if (contieneEliminarTipoLicencia) {
            const indexToRemove = event.previousContainer.data.indexOf('Eliminar Tipo Licencia');
            if (indexToRemove !== -1) {
              event.previousContainer.data.splice(indexToRemove, 1);
            }
          }
          if (contieneCrearTipoLicencia) {
            const indexToRemove = event.previousContainer.data.indexOf('Crear Tipo Licencia');
            if (indexToRemove !== -1) {
              event.previousContainer.data.splice(indexToRemove, 1);
            }
          }
        } else {
          transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
        }





       
      }







    }
  
    this.asignadosNotEmpty = this.asignados.length > 0;
  }
  
  
  

 
  onSubmit(form:any) {
   debugger
    if (form.valid) { // Asegúrate de que el formulario sea válido
      // Obtén los datos del formulario
      const elementosAsignadosString = this.asignados.join(',');
       // Convierte el arreglo en una cadena separada por comas
      const nuevoRol: RolInterfacePost = {
        // Puedes dejarlo vacío si el servidor genera el ID automáticamente
        Id:"",
        Rolname: form.value.name, // Reemplaza 'name' con el nombre del campo en tu formulario
        Descripcion: form.value.descripcion, // Reemplaza 'descripcion' con el nombre del campo en tu formulario
        Permisos: elementosAsignadosString
     
      };

 console.log(nuevoRol);
 if (!this.asignadosNotEmpty) {
  return;
}

        this.rolService.AddRolModificado(nuevoRol).subscribe(
          (resultado) => {

           
            // Realiza acciones después de agregar el rol, por ejemplo, redireccionar o cerrar el modal.
            console.log("Exito");
            this.dialogRef.close(); // Cierra el modal
           
            // Notifica a los observadores que se ha agregado un nuevo rol
            this.rolService.notifyNuevoRolModificado(resultado);
            
          },
          (error) => {
            console.error('Error al agregar el rol', error);
          }
        );

      

    }


   
  }
  cancelar() {
    this.dialogRef.close();
 }
}

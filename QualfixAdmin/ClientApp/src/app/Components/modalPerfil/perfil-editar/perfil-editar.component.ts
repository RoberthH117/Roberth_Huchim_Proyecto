import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { PerfilService } from 'src/app/service/perfil.service';
import { PerfilRegistroComponent } from '../perfil-registro/perfil-registro.component';
import { GetAllAccesosRequest, actualizarPerfil } from 'src/app/Modelos/Perfil.interface';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-perfil-editar',
  templateUrl: './perfil-editar.component.html',
  styleUrls: ['./perfil-editar.component.css']
})
export class PerfilEditarComponent implements OnInit{

  CrearPerfil: FormGroup;
  id:string;
      submitted: boolean = false;


      permisos: string[] = [];
      asignados: string[] = [];
     
      elementosasi!: string;
      asignadosNotEmpty = true; // Agrega esta propiedad
  constructor(private  rolService:PerfilService,@Inject(MAT_DIALOG_DATA) public data: any,private router: Router,public dialogRef: MatDialogRef<PerfilRegistroComponent>,private formBuilder: FormBuilder) {
    
this.id=data.id;


  this.CrearPerfil = this.formBuilder.group({
    name: ['', Validators.required],
    descripcion: ['', Validators.required]
  });

  }
  ngOnInit() {


    
    this.asignadosNotEmpty = true;



     
    const token = localStorage.getItem('token');

    const ObtenerId: actualizarPerfil = {
      // Puedes dejarlo vacío si el servidor genera el ID automáticamente
    PerfilId:parseInt(this.id),
    Perfil:'',
    Descripcion:'',
    Accesos:[]

    };

    this.rolService.ObtenerPermisos(ObtenerId).subscribe(
      (resultado) => {
        this.asignados = resultado.accesosAsignados.map(acceso => acceso.nombre);
        this.permisos = resultado.accesosDisponibles.map(acceso => acceso.nombre);
        debugger
        // Realiza acciones después de agregar los elementos a 'done' y 'permisos'
        console.log("Éxito");
      },
      (error) => {
        console.error('Error al obtener los permisos', error);
      }
    );


    this.rolService.buscarPerfilPorId(ObtenerId).subscribe(
      (data) => {


        console.log(data);
        this.CrearPerfil.patchValue({
          name: data.perfil,
          descripcion: data.descripcion,
          
          
        });

       
        // Aquí puedes manejar los datos, como mostrarlos en tu tabla HTML.
      },

      (error) => {
        console.error('Error al obtener los roles', error);
      }
    );
    
   
  }




  drop(event: CdkDragDrop<string[]>): void {

    debugger

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
   
    if (form.valid) { // Asegúrate de que el formulario sea válido
      // Obtén los datos del formulario
      const elementosAsignadosString = this.asignados.join(',');
      const nuevoRol: actualizarPerfil = {
        // Puedes dejarlo vacío si el servidor genera el ID automáticamente
        PerfilId:parseInt(this.id),
        Perfil: form.value.name, // Reemplaza 'name' con el nombre del campo en tu formulario
        Descripcion: form.value.descripcion, // Reemplaza 'descripcion' con el nombre del campo en tu formulario
        Accesos: this.asignados
      };

    
     
      if (!this.asignadosNotEmpty) {
        return;
      }
    
debugger
        this.rolService.actualizarPerfil(nuevoRol).subscribe(
          (resultado) => {
            // Realiza acciones después de agregar el rol, por ejemplo, redireccionar o cerrar el modal.
            console.log("Exito");
            this.dialogRef.close(); // Cierra el modal
            
           
            // Notifica a los observadores que se ha agregado un nuevo rol
            this.rolService.notifyNuevoPerfilModificado(resultado);
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

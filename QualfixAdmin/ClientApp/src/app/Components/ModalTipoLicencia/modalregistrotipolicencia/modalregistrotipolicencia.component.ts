import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { PermisosRequest, RolInterfacePost } from 'src/app/Modelos/rol.interface';
import { RolService } from 'src/app/service/rol.service';
import { ModalregistrorolComponent } from '../../ModalRol/modalregistrorol/modalregistrorol.component';
import { TipoLicenciaService } from 'src/app/service/tipo-licencia.service';
import { TipoLicenciaInsertI } from 'src/app/Modelos/TipoLicencia.interface';

@Component({
  selector: 'app-modalregistrotipolicencia',
  templateUrl: './modalregistrotipolicencia.component.html',
  styleUrls: ['./modalregistrotipolicencia.component.css']
})



export class ModalregistrotipolicenciaComponent implements OnInit{
  

 
  CrearTipoLicencia: FormGroup;
  elementosasi!: string;
  
      submitted: boolean = false;
  constructor(private  licenciaService:TipoLicenciaService,private router: Router,public dialogRef: MatDialogRef<ModalregistrotipolicenciaComponent>,private formBuilder: FormBuilder) {
  

  this.CrearTipoLicencia = this.formBuilder.group({
    tiempo: ['', Validators.required],
    tipo: ['', Validators.required],
    costo:['',Validators.required]
  });



 





  }
  ngOnInit(): void {

   

    
  }

  

 
 
  onSubmit(form:any) {
   debugger
    if (form.valid) { // Asegúrate de que el formulario sea válido
      // Obtén los datos del formulario
    
       // Convierte el arreglo en una cadena separada por comas
      const TipoLicencia: TipoLicenciaInsertI = {
        // Puedes dejarlo vacío si el servidor genera el ID automáticamente
        
        Tipo: form.value.tipo, // Reemplaza 'name' con el nombre del campo en tu formulario
        Tiempo: form.value.tiempo, // Reemplaza 'descripcion' con el nombre del campo en tu formulario
       Costo:form.value.costo
     
      };



        this.licenciaService.InsertTipoLicencia(TipoLicencia).subscribe(
          (resultado) => {

           
            // Realiza acciones después de agregar el rol, por ejemplo, redireccionar o cerrar el modal.
            console.log("Exito");
            this.dialogRef.close(); // Cierra el modal
           
            // Notifica a los observadores que se ha agregado un nuevo rol
            this.licenciaService.notifyNuevoRolModificado(resultado);
            
          },
          (error) => {
            console.error('Error al agregar el tipo de licencia', error);
          }
        );

      

    }


   
  }
  cancelar() {
    this.dialogRef.close();
 }
}

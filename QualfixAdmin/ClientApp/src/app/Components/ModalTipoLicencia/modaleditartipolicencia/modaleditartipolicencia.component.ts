import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { RolInterface, PermisosRequest, RolInterfacePost } from 'src/app/Modelos/rol.interface';
import { RolService } from 'src/app/service/rol.service';
import { ModalregistrorolComponent } from '../../ModalRol/modalregistrorol/modalregistrorol.component';
import { TipoLicenciaService } from 'src/app/service/tipo-licencia.service';
import { TipoLicenciaFindIdI, TipoLicenciaUpdateI } from 'src/app/Modelos/TipoLicencia.interface';

@Component({
  selector: 'app-modaleditartipolicencia',
  templateUrl: './modaleditartipolicencia.component.html',
  styleUrls: ['./modaleditartipolicencia.component.css']
})
export class ModaleditartipolicenciaComponent implements OnInit {

  CrearTipoLicencia: FormGroup;
  id:number;
      submitted: boolean = false;


      
     
      elementosasi!: string;
  constructor(private  licenciaService:TipoLicenciaService,@Inject(MAT_DIALOG_DATA) public data: any,private router: Router,public dialogRef: MatDialogRef<ModaleditartipolicenciaComponent>,private formBuilder: FormBuilder) {
    
this.id=data.id;


this.CrearTipoLicencia = this.formBuilder.group({
  tiempo: ['', Validators.required],
  tipo: ['', Validators.required],
  costo:['',Validators.required]
});


  }
  ngOnInit() {


    
 



     


    const ObtenerId: TipoLicenciaFindIdI = {
      // Puedes dejarlo vacío si el servidor genera el ID automáticamente
    TipoLicenciaId:this.id,


    };

  


    this.licenciaService.FindTipoLicencia(ObtenerId).subscribe(
      (data) => {


        console.log(data);
        this.CrearTipoLicencia.patchValue({
          tipo: data.tipo,
          tiempo: data.tiempo,
          costo:data.costo
          
          
          
        });

       
        // Aquí puedes manejar los datos, como mostrarlos en tu tabla HTML.
      },

      (error) => {
        console.error('Error al obtener los roles', error);
      }
    );
    
   
  }




  
 
  onSubmit(form:any) {
   debugger
    if (form.valid) { // Asegúrate de que el formulario sea válido
      // Obtén los datos del formulario
     
      const TipoLicencia: TipoLicenciaUpdateI = {
        // Puedes dejarlo vacío si el servidor genera el ID automáticamente
        TipoLicenciaId:this.id,
        Tipo: form.value.tipo, // Reemplaza 'name' con el nombre del campo en tu formulario
        Tiempo: form.value.tiempo, // Reemplaza 'descripcion' con el nombre del campo en tu formulario
        Costo:form.value.costo
        
      };

 
        this.licenciaService.UpdateTipoLicencia(TipoLicencia).subscribe(
          
          (resultado) => {
            // Realiza acciones después de agregar el rol, por ejemplo, redireccionar o cerrar el modal.
            console.log("Exito");
            this.dialogRef.close(); // Cierra el modal
            
           debugger
            // Notifica a los observadores que se ha agregado un nuevo rol
            this.licenciaService.notifyNuevoRolModificado(resultado);
          },
          (error) => {
            console.error('Error al agregar el rol', error);
          }
        );

      

    }


   
  }
  cancelar() {
    this.CrearTipoLicencia.reset(); // Restablece los valores del formulario
    this.dialogRef.close(); // Cierra el diálogo
  }
}

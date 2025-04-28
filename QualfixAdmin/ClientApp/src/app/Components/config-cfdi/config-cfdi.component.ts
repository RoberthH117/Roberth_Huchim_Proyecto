import { ConfigCFDIService } from 'src/app/Services/config-cfdi.service';
import { ConfigCFDIModel } from './../../Models/ConfiguracionCFDI-interface';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ModalRegConfigCFDIComponent } from '../modal-reg-config-cfdi/modal-reg-config-cfdi.component';
import { ModalEditConfigCFDIComponent } from '../modal-edit-config-cfdi/modal-edit-config-cfdi.component';
import { ModalEliminarConfigCFDIComponent } from '../modal-eliminar-config-cfdi/modal-eliminar-config-cfdi.component';

@Component({
  selector: 'app-config-cfdi',
  templateUrl: './config-cfdi.component.html',
  styleUrls: ['./config-cfdi.component.css']
})
export class ConfigCFDIComponent implements OnInit {
  dataSource: ConfigCFDIModel[] = [];
  displayedColumns = [
     
 
    'cuenta',
    'fecha',
    'action',
  ];

  constructor(private configservice:ConfigCFDIService, private dialogref:MatDialog){}
  ngOnInit(): void {
     this.obtenerConfigCFDI();
  }

  obtenerConfigCFDI(){
    this.configservice.getAll('https://localhost:7141/api/ConfiguracionCDFI').subscribe(Response =>{
      console.log(Response);
      this.dataSource = Response;
    })
  }

  onInsertConfig_CFDI(){
    const dialogRef = this.dialogref.open(ModalRegConfigCFDIComponent);
  }
  onUpdateConfig_CFDI(id:number){
    const dialogref= this.dialogref.open(ModalEditConfigCFDIComponent,{data:{id}});
  }

  OnDeletConfig_CFDI(id:number){
    const dialogref= this.dialogref.open(ModalEliminarConfigCFDIComponent,{data:{id}});
  }
}

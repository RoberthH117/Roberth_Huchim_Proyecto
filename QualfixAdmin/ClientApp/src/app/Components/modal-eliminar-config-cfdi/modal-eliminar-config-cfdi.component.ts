import { ConfigCFDIService } from 'src/app/Services/config-cfdi.service';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ConfigCFDIModel } from 'src/app/Models/ConfiguracionCFDI-interface';

@Component({
  selector: 'app-modal-eliminar-config-cfdi',
  templateUrl: './modal-eliminar-config-cfdi.component.html',
  styleUrls: ['./modal-eliminar-config-cfdi.component.css']
})
export class ModalEliminarConfigCFDIComponent implements OnInit {
  configCFDI:ConfigCFDIModel ={
    id:0,
  }
  constructor(private serviceconfig:ConfigCFDIService,@Inject(MAT_DIALOG_DATA) public data: ConfigCFDIModel,){
this.configCFDI.id = data.id
  }

  ngOnInit(): void {
     
  }
  Eliminar(id:number){
    this.serviceconfig.EliminarConfigcfdi(`https://localhost:7141/api/ConfiguracionCDFI/${this.configCFDI.id}`);
    
  }
}

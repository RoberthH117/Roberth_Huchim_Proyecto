import { Paismodel } from './../../Models/Pais-Interface';
import { InformacionFiscalService } from './../../Services/informacion-fiscal.service';
 
import { ClientesService } from './../../Services/clientes.service';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Clientemodel } from 'src/app/Models/Cliente-Interface';
import { InformacionFiscalmodel } from 'src/app/Models/InformacionFiscal-Interface';
 
 
 

@Component({
  selector: 'app-modal-info-cliente',
  templateUrl: './modal-info-cliente.component.html',
  styleUrls: ['./modal-info-cliente.component.css']
})
export class ModalInfoClienteComponent implements OnInit {
  cliente:Clientemodel={
    clienteId:0,
    
  }
  informacionFiscal:InformacionFiscalmodel={
    
  }
  constructor(private clienteservice:ClientesService,private infofiscalService:InformacionFiscalService , @Inject(MAT_DIALOG_DATA) public data: Clientemodel,) {
     this.cliente.clienteId = data.clienteId;
  }
  ngOnInit(): void { 
  this.obtenerCliente();
  
  }

  obtenerCliente(){
  
    this.clienteservice.getCliente(`https://localhost:7141/api/cliente/${this.cliente.clienteId}`).subscribe(response=>{
       console.log(response.informacionFiscalId)
      this.cliente = response;
      this.obtenerInformacionFiscal();
    })
    
  }
  obtenerInformacionFiscal(){
  
    this.infofiscalService.getInformacionFiscal(`https://localhost:7141/api/informacionfiscal/${this.cliente.informacionFiscalId}`).subscribe(response=>{
    console.log(response.pais?.nombre)  
    this.informacionFiscal = response;
    })
  }
}
//@Inject(MAT_DIALOG_DATA) public data: Clientemodel,
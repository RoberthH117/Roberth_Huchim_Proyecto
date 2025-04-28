import { ClientesService } from 'src/app/Services/clientes.service';
import { Component, Inject, OnInit } from '@angular/core';
import { Clientemodel } from 'src/app/Models/Cliente-Interface';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-eliminar-cliente',
  templateUrl: './modal-eliminar-cliente.component.html',
  styleUrls: ['./modal-eliminar-cliente.component.css']
})
export class ModalEliminarClienteComponent implements OnInit {

  cliente:Clientemodel={
    clienteId:0,
    nombreComercial:'Defecto',
  } 

  constructor(private clienteService:ClientesService,@Inject(MAT_DIALOG_DATA) public data: Clientemodel){
    this.cliente.clienteId = data.clienteId;
    this.cliente.nombreComercial = data.nombreComercial;
  }
  ngOnInit(): void {
     
   
  }

  Eliminar(clienteId:number){
    this.clienteService.EliminarCliente(`https://localhost:7141/api/cliente/${clienteId}`);
    
  }
}

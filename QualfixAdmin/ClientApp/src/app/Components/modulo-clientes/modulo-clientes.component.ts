
import { PaisService } from './../../Services/pais.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Ciudadmodel } from 'src/app/Models/Ciudad-interface';
import { Clientemodel } from 'src/app/Models/Cliente-Interface';
import { Estadomodel } from 'src/app/Models/Estado-interface';
import { Paismodel } from 'src/app/Models/Pais-Interface';
import { CiudadService } from 'src/app/Services/ciudad.service';
import { ClientesService } from 'src/app/Services/clientes.service';
import { EstadoService } from 'src/app/Services/estado.service';
import { ModalRegClienteComponent } from '../modal-reg-cliente/modal-reg-cliente.component';
import { ModalInfoClienteComponent } from '../modal-info-cliente/modal-info-cliente.component';
import { ModalEliminarClienteComponent } from '../modal-eliminar-cliente/modal-eliminar-cliente.component';
import { ModalEditClienteComponent } from '../modal-edit-cliente/modal-edit-cliente.component';

@Component({
  selector: 'app-modulo-clientes',
  templateUrl: './modulo-clientes.component.html',
  styleUrls: ['./modulo-clientes.component.css']
})
export class ModuloClientesComponent implements OnInit {

  clientes: Clientemodel[] = [];
  displayedColumns = [

    'razonSocial',
    'nameComercio',
    'rfc',
    'action',
  ];

  constructor(private clienteService: ClientesService, private dialog: MatDialog,) {
  }

  ngOnInit(): void {
    this.obtenerClientes();
  }


  obtenerClientes() {
    // Reemplaza 'url_de_tu_api' con la URL real de tu API que devuelve la lista de clientes
    this.clienteService.getAll('https://localhost:7141/api/cliente').subscribe(Response => {
      this.clientes = Response;
      console.log(Response);
    })
  }

  verDetalles(clienteId: number) {
    console.log(clienteId)
    const dialogref = this.dialog.open(ModalInfoClienteComponent, { data: { clienteId } });
  }

  editarCliente(clienteId: number, informacionFiscalId:number) {
    const dialogref = this.dialog.open(ModalEditClienteComponent, { data: { clienteId,informacionFiscalId } });
  }

  eliminarCliente(clienteId: number,nombreComercial:string) {
    const dialogref = this.dialog.open(ModalEliminarClienteComponent, { data: { clienteId,nombreComercial } });
  }
  agregarCliente() {
    const dialogRef = this.dialog.open(ModalRegClienteComponent);
  }
}
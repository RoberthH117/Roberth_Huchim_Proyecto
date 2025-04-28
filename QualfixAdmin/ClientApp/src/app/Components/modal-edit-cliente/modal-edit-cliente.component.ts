import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Ciudadmodel } from 'src/app/Models/Ciudad-interface';
import { Clientemodel } from 'src/app/Models/Cliente-Interface';
import { Estadomodel } from 'src/app/Models/Estado-interface';
import { InformacionFiscalmodel } from 'src/app/Models/InformacionFiscal-Interface';
import { Paismodel } from 'src/app/Models/Pais-Interface';
import { RegimenFiscalModel } from 'src/app/Models/RegimenFiscal-interface';
import { CiudadService } from 'src/app/Services/ciudad.service';
import { ClientesService } from 'src/app/Services/clientes.service';
import { EstadoService } from 'src/app/Services/estado.service';
import { InformacionFiscalService } from 'src/app/Services/informacion-fiscal.service';
import { PaisService } from 'src/app/Services/pais.service';
import { RegimenFiscalService } from 'src/app/Services/regimen-fiscal.service';

@Component({
  selector: 'app-modal-edit-cliente',
  templateUrl: './modal-edit-cliente.component.html',
  styleUrls: ['./modal-edit-cliente.component.css']
})
export class ModalEditClienteComponent implements OnInit {
  
  isLinear = false;
  cliente: Clientemodel = {
    clienteId: 0,
    direccion: '',
    nombreComercial: '',
    cp: '',
    telefonos: '',
    fax: '',
    correoElectronico: '',
    informacionFiscalId: 0,
    ciudadId: 0,
    estadoId: 0,
    paisId: 0,
    calle: '',
    numeroExterior: '',
    numeroInterior: '',
    cruzamientos: '',
    colonia: '',
  };
  infofiscal: InformacionFiscalmodel = {
    informacionFiscalId: 0,
  };

  formcliente: FormGroup;
  formInformacionFiscal: FormGroup;

  regimenfiscal: RegimenFiscalModel[] = [];
  paises: Paismodel[] = [];
  estados: Estadomodel[] = [];
  ciudades: Ciudadmodel[] = [];
  
  constructor(@Inject(MAT_DIALOG_DATA)public data: Clientemodel,private _formBuilder: FormBuilder, private clienteservice: ClientesService, private InformacionFiscalservice: InformacionFiscalService, private regimenFservice: RegimenFiscalService, private paisService: PaisService, private estadoService: EstadoService, private ciudadService: CiudadService) {
    this.cliente.clienteId = data.clienteId;
    this.cliente.informacionFiscalId = data.informacionFiscalId;
    this.formcliente = this._formBuilder.group({
      direccion: ['', Validators.required],
      nombreComercial: ['', Validators.required],
      cp: ['', Validators.required],
      telefonos: ['', Validators.required],
      fax: ['', Validators.required],
      correoElectronico: ['', Validators.required], // Agregado para correoElectronico
      informacionFiscalId: [0],
      ciudadId: ['', Validators.required],
      estadoId: ['', Validators.required],
      paisId: ['', Validators.required],
      calle: ['', Validators.required],
      numeroExterior: ['', Validators.required],
      numeroInterior: ['', Validators.required],
      cruzamientos: ['', Validators.required],
      colonia: ['', Validators.required],
    });
    this.formInformacionFiscal = this._formBuilder.group({
      informacionFiscalId: [0],
      razonSocial:['', Validators.required],
      rfc:['', Validators.required],
      direccion:['', Validators.required],
      cp: ['', Validators.required],
      ciudadId: 0,
      estadoId: 0,
      paisId: 0,
      numeroInterior: ['', Validators.required],
      numeroExterior:['', Validators.required],
      colonia: ['', Validators.required],
      localidad:['', Validators.required],
      referencia:['', Validators.required],
      municipio:['', Validators.required],
      regFiscalId: 0
    });

  }

  ngOnInit(): void {
    this.getPaises();
    this.getEstado();
    this.getCiudad();
    this.getRegimenFiscal();
    this.getInformacionFiscal();
    this.getInformacionCliente();
  }
  public getPaises() {
    this.paisService.getall('https://localhost:7141/api/pais').subscribe(Response => {
      this.paises = Response;
    });
  }
  public getEstado() {
    this.estadoService.getall('https://localhost:7141/api/estado').subscribe(Response => {
      this.estados = Response;
    });
  }
  public getCiudad() {
    this.ciudadService.getall('https://localhost:7141/api/ciudad').subscribe(Response => {
      this.ciudades = Response;
    });
  }
  public getRegimenFiscal() {
    this.regimenFservice.getAll('https://localhost:7141/api/RegimenFiscal').subscribe(Response => {
      this.regimenfiscal = Response;
    });
  }
  public getInformacionFiscal() {
    this.InformacionFiscalservice.getInformacionFiscal(`https://localhost:7141/api/informacionfiscal/${this.cliente.informacionFiscalId}`).subscribe(Response => {
      this.formInformacionFiscal.patchValue(Response);
    });
  }
  public getInformacionCliente() {
    this.clienteservice.getCliente(`https://localhost:7141/api/cliente/${this.cliente.clienteId}`).subscribe(Response => {
      this.formcliente.patchValue(Response);
    });
  }

  EditarInformacionFiscal(){
    this.InformacionFiscalservice.EditarInformacionFiscal(`https://localhost:7141/api/informacionfiscal/${this.cliente.informacionFiscalId}`,this.formInformacionFiscal.value)
  }
  EditarCliente(){
    this.clienteservice.EditarCliente(`https://localhost:7141/api/cliente/${this.cliente.clienteId}`,this.formcliente.value);
  }
}

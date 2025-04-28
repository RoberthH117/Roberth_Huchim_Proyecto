import { ClientesService } from 'src/app/Services/clientes.service';
import { Clientemodel } from 'src/app/Models/Cliente-Interface';
import { InformacionFiscalmodel } from './../../Models/InformacionFiscal-Interface';
import { ResponseI } from 'src/app/Modelos/response.interface';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Ciudadmodel } from 'src/app/Models/Ciudad-interface';
import { Estadomodel } from 'src/app/Models/Estado-interface';
import { Paismodel } from 'src/app/Models/Pais-Interface';
import { RegimenFiscalModel } from 'src/app/Models/RegimenFiscal-interface';
import { CiudadService } from 'src/app/Services/ciudad.service';
import { EstadoService } from 'src/app/Services/estado.service';
import { InformacionFiscalService } from 'src/app/Services/informacion-fiscal.service';
import { PaisService } from 'src/app/Services/pais.service';
import { RegimenFiscalService } from 'src/app/Services/regimen-fiscal.service';


@Component({
  selector: 'app-modal-reg-cliente',
  templateUrl: './modal-reg-cliente.component.html',
  styleUrls: ['./modal-reg-cliente.component.css']
})
export class ModalRegClienteComponent implements OnInit {
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

  constructor(private _formBuilder: FormBuilder, private clienteservice: ClientesService, private InformacionFiscalservice: InformacionFiscalService, private regimenFservice: RegimenFiscalService, private paisService: PaisService, private estadoService: EstadoService, private ciudadService: CiudadService) {
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
  CrearInformacionFiscal() {
    console.log(this.formInformacionFiscal.value);
    if (this.formInformacionFiscal.valid) {
      this.InformacionFiscalservice.CrearInformacionFiscal('https://localhost:7141/api/informacionfiscal', this.formInformacionFiscal.value).subscribe(
        (res: any) => {
          console.log(res);
  
          // Extraer el número de nuevoId
          const nuevoId = res.nuevoId;
  
          // Asignar el número directamente a informacionFiscalId
          this.formcliente.patchValue({
            informacionFiscalId: nuevoId
          });
        },
        error => {
          console.error("Error al crear información fiscal:", error);
          // Manejar el error según sea necesario
        }
      );
    }
  }
  
  CrearCliente() {
    console.log(this.formcliente.value);
    if (this.formcliente.valid) {
      this.clienteservice.CrearCliente('https://localhost:7141/api/cliente', this.formcliente.value);
    }
  }
}

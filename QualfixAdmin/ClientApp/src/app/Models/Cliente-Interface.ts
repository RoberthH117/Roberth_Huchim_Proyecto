import { Ciudadmodel } from "./Ciudad-interface";
import { Estadomodel } from "./Estado-interface";
import { Paismodel } from "./Pais-Interface";

export interface Clientemodel{
    clienteId: number,
    direccion?: string,
    nombreComercial?: string,
    cp?: string,
    telefonos?: string,
    fax?: string,
    correoElectronico?: string,
    informacionFiscalId?: number,
    ciudadId?: number,
    ciudad?:Ciudadmodel,
    estadoId?: number,
    estado?: Estadomodel,
    paisId?: number,
    pais?:Paismodel,
    calle?: string,
    numeroExterior?: string,
    numeroInterior?: string,
    cruzamientos?: string,
    colonia?: string,
   
}

export interface Clientesrray extends Array<Clientemodel>{}
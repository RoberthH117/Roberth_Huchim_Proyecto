import { Ciudadmodel } from "./Ciudad-interface";
import { Estadomodel } from "./Estado-interface";
import { Paismodel } from "./Pais-Interface";
import { RegimenFiscalModel } from "./RegimenFiscal-interface";

export interface InformacionFiscalmodel{
    informacionFiscalId?: number,
    razonSocial?: string,
    rfc?: string,
    direccion?: string,
    cp?: string,
    ciudadId?: number,
    estadoId?: number,
    paisId?: number,
    pais?:Paismodel,
    estado?:Estadomodel,
    ciudad?: Ciudadmodel,
    numeroInterior?: string,
    numeroExterior?: string,
    colonia?: string,
    localidad?: string,
    referencia?: string,
    municipio?: string,
    regFiscalId?: number,
    regFiscal?: RegimenFiscalModel,
}
export interface InformacioFiscalarray extends Array<InformacionFiscalmodel>{}
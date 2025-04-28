export interface TipoLicenciaI{
 
        tipoLicenciaId:number;
        tipo:string;
        tiempo:number;
        costo:number;
}

export interface TipoLicenciaUpdateI{
    TipoLicenciaId:number;
    Tipo:string;
        Tiempo:number;
        Costo:number;
}

export interface TipoLicenciaFindIdI{
    TipoLicenciaId:number;
}

export interface TipoLicenciaInsertI{
    Tipo:string;
        Tiempo:number;
        Costo:number;
}
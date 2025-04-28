export interface LicenciaModel{
 
 
    TipoId?:number;
    Id_Usuario?:string;
}


export interface LicenciaRequest {
    [x: string]: any;

    licenciaId:number;
    licencia:string;
    fecha_Inicio:Date;
    fecha_Expiracion:Date;
    


}

export interface DatosUsuario {
    userId: string;
    userName: string;
    licenseId: number;
    licenseName: string;
    last5Characters: string;
    monthOfLicense: string;
    // Agrega otras propiedades según sea necesario
  }
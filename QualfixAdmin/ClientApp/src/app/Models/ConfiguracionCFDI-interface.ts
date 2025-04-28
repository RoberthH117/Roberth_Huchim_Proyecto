export interface ConfigCFDIModel{
    id:number,
    nameFileCer?:string,
    nameFileKey?:string,
    fileCer?:any,
    fileKey?:any,
    filePasswordKey?:string,
    lastModifiedOn?:Date,
    cuenta?:string,
    passwordUsuario?:string,
    usuario?:string
}
export interface ConfigCFDIrray extends Array<ConfigCFDIModel>{}
export interface ConfigCFDIcodeModel{
    id:number,
    nameFileCer:string,
    nameFileKey:string,
    fileCer?:any,
    fileKey?:any,
    filePasswordKey?:string,
    lastModifiedOn:Date,
    cuenta:string,
    passwordUsuario:string,
    usuario:string
}
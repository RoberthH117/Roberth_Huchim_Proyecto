export interface RegimenFiscalModel{
    id: number,
    codeRegimenFiscal: number,
    descripcion:string,
    fisica: boolean,
    moral: boolean,
    status: boolean,
     
}
export interface RegimenFiscalsarray extends Array<RegimenFiscalModel>{}
export interface Paismodel{
    paisId?: number,
    nombre?: string,
    tipoMoneda?: string,
    simboloMoneda?: String,
}

export interface Paisarray extends Array<Paismodel>{}
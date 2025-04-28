export interface Estadomodel{
    estadoId?: number,
    nombre?: string,
    paisId?: number,
}

export interface Estadoarray extends Array<Estadomodel>{}
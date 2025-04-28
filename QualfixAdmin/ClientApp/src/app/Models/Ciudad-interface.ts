export interface Ciudadmodel{
    ciudadId?: number,
    nombre?: string,
    estadoId?: number,
}

export interface Ciudadarray extends Array<Ciudadmodel>{}
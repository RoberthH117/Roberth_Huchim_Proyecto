export interface ReporteModelo {
    result: Reporte[];
  }
  
  export interface Reporte {
    id?: number;
    titulo?: string;
    descripcion?: string;
    estado?: string;
    fecha_Solicitud?: string;
    imagenes?: ImagenModelo[];
  }
  
  export interface ImagenModelo {
    base64: string;
    ruta: string;
    nombre: string;
    tamaño?: number;
    reporteId?: number;
    reporte?: any;
  }

  export interface IdRequest
  {
   Id:string;
  }
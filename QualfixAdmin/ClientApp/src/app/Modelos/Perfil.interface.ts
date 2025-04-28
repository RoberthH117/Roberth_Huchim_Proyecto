export interface PerfilAccesosModel {
    perfilId:number;
    perfil: string;
    id_Company: number | null;
    descripcion: string | null;
    accesos: string[];
  }



  export interface NuevoPerfilModel {
    Perfil: string;
    Descripcion: string;
    Accesos: string[];
 
  }

  export interface actualizarPerfil {
    PerfilId:number;
    Perfil: string;
    Descripcion: string;
    Accesos: string[];
  
  }

  export interface GetAllAccesosRequest {
    accesosId:number;
    nombre: string;
    descripcion: string;
  
  }



  export interface GetAllAcce {
    accesosAsignados: GetAllAccesosRequest[]; // Reemplaza 'permisosAsignados' con el nombre de tu propiedad
    accesosDisponibles: GetAllAccesosRequest[]; // Reemplaza 'permisosDisponibles' con el nombre de tu propiedad
}
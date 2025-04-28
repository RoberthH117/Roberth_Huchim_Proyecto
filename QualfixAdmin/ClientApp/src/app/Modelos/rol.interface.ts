export interface RolInterface {


    id:string;
    name:string;
    descripcion:string;
    permisos:string;


}


export interface RolInterfacePost {


   Id:string;
    Rolname:string;
    Descripcion:string;
    Permisos:string;

}


export interface PermissionsRolRequest {

    rolId:string;
    rol:string;
    permisos:string;
    descripcion:string;
    


}

export interface PermissionDisp {

    rolId:string;
    rol:string;
    permisos:string;
    descripcion:string;
    


}

export interface RolInterfacePostEnviar {


   Id:string;
    Rolname:string;
    Descripcion:string;
    Permisos:string;

}

export interface RolConPermisos {
    nombre: string;  // Reemplaza 'nombre' con el nombre del campo en RolInterface
    descripcion: string;  // Reemplaza 'descripcion' con el nombre del campo en RolInterface
    permisos: string;  // Reemplaza 'permisos' con el nombre del campo en PermissionsRolRequest
  }

  export interface PermisosRequest {
    permissionID: string;  // Reemplaza 'nombre' con el nombre del campo en RolInterface
    name: string;  // Reemplaza 'descripcion' con el nombre del campo en RolInterface
    description: string;  // Reemplaza 'permisos' con el nombre del campo en PermissionsRolRequest
  }

  export interface PermissionsRolRequest {
    permisosAsignados: PermisosRequest[]; // Reemplaza 'permisosAsignados' con el nombre de tu propiedad
    permisosDisponibles: PermisosRequest[]; // Reemplaza 'permisosDisponibles' con el nombre de tu propiedad
}
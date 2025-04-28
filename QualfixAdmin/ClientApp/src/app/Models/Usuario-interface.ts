export interface Usuariomodel {
    id: string;
    firstName: string;
    lastName: string;
    status: number;
    activo: boolean;
    userName: string;
    normalizedUserName: string;
    email:string;
    normalizedEmail:string;
    emailConfirmed:boolean;
    passwordHash:string;
    securityStamp:string;
    concurrencyStamp:string;
    phoneNumber:string;
    phoneNumberConfirmed:boolean;
    twoFactorEnabled:boolean;
    lockoutEnabled:boolean;
    accessFailedCount:number;
    profileName?:string;
    rolName?:string;
  }
  export interface EditarUsuariomodel {
    id: string;
    firstName: string;
    lastName: string;
    status: number;
    activo: boolean;
    email:string;
    passwordHash:string;
  }
  export interface RegistroUsuariomodel {
    firstName: string;
    lastName: string;
    status: number;
    activo: boolean;
    email:string;
    passwordHash:string;
  }
  export interface SolicitudRegistroUsuariomodel {
    firstName: string;
    lastName: string;
    email:string;
    passwordHash:string;
  }
export interface UsuariomodelArray extends Array<Usuariomodel>{}
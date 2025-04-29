import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { RolI } from '../Modelos/Data.interface';
import { PermisosRequest, PermissionsRolRequest, RolInterface, RolInterfacePost, RolInterfacePostEnviar } from '../Modelos/rol.interface';
import { KeyValue } from '@angular/common';
import { CookieService } from 'ngx-cookie-service';
import { Token } from '@angular/compiler';
import { BehaviorSubject } from 'rxjs';
import { ModeloPermisos } from '../Modelos/Permisos.interface';
@Injectable({
  providedIn: 'root'
})
export class RolService {


private uriApi='https://roberth-huchim-proyecto.onrender.com/CrearRol/';

private nuevoRolSubjectModificado = new Subject<PermissionsRolRequest>();
private elementoEliminadoSubject = new Subject<string>();
private token:string | undefined;

constructor(private https:HttpClient, private cookieService: CookieService) { 

  this.token = this.cookieService.get('Authorization');
  
}





getRol(): Observable<any> {
  // Realiza una solicitud HTTP GET y tipa la respuesta.
  return this.https.get<any>(`${this.uriApi}ObtenerRol`);
}

getNombre(): Observable<any> {
  // Realiza una solicitud HTTP GET y tipa la respuesta.
  return this.https.get<any>(`${this.uriApi}ObtenerNombre`);
}

GetAllPermissions(): Observable<PermissionsRolRequest[]> {
  const direccion = `${this.uriApi}FindAllUserPermissions`; // Asegúrate de ajustar la URL de acuerdo a tu API.

  // No necesitas agregar el encabezado de autorización manualmente aquí.
  // El interceptor se encargará de ello si el token está disponible en localStorage.

  return this.https.get<PermissionsRolRequest[]>(direccion);
}



GetPermisos(): Observable<ModeloPermisos> {
  const direccion = `${this.uriApi}Permisos`; // Asegúrate de ajustar la URL de acuerdo a tu API.

  // No necesitas agregar el encabezado de autorización manualmente aquí.
  // El interceptor se encargará de ello si el token está disponible en localStorage.

  return this.https.get<ModeloPermisos>(direccion);
}


GetAllRol(): Observable<RolInterface[]> {
  const direccion = `${this.uriApi}FindAllRol`; // Asegúrate de ajustar la URL de acuerdo a tu API.

  // No necesitas agregar el encabezado de autorización manualmente aquí.
  // El interceptor se encargará de ello si el token está disponible en localStorage.

  return this.https.get<RolInterface[]>(direccion);
}


GetAllPermissionsList(): Observable<PermisosRequest[]> {
  const direccion = `${this.uriApi}FindAllPermissions`; // Asegúrate de ajustar la URL de acuerdo a tu API.

  // No necesitas agregar el encabezado de autorización manualmente aquí.
  // El interceptor se encargará de ello si el token está disponible en localStorage.

  return this.https.get<PermisosRequest[]>(direccion);
}


AddRolModificado(rol: RolInterfacePostEnviar): Observable<PermissionsRolRequest> {
  const direccion = `${this.uriApi}NuevoRol`; // Ajusta la URL según tu API.

  // Agrega un encabezado de autorización con el token de autenticación en la solicitud.
  const headers = { Authorization: `Bearer ${this.token}` };

  // Realiza la solicitud POST a la API con los datos del nuevo rol y los encabezados de autorización.
  return this.https.post<PermissionsRolRequest>(direccion, rol, { headers });
}








UpdateRolModificado(rol: RolInterfacePost): Observable<PermissionsRolRequest> {
  const direccion = `${this.uriApi}UpdateRolPermisos`; // Ajusta la URL según tu API.

  // Agrega un encabezado de autorización con el token de autenticación en la solicitud.
  const headers = { Authorization: `Bearer ${this.token}` };

  // Realiza la solicitud POST a la API con los datos del nuevo rol y los encabezados de autorización.
  return this.https.post<PermissionsRolRequest>(direccion, rol, { headers });
}





DeleteRolModificado(rol: RolInterface): Observable<PermissionsRolRequest> {
  const direccion = `${this.uriApi}EliminarRolConPermisos`; // Ajusta la URL según tu API.

  // Agrega un encabezado de autorización con el token de autenticación en la solicitud.
  const headers = { Authorization: `Bearer ${this.token}` };

  // Realiza la solicitud POST a la API con los datos del nuevo rol y los encabezados de autorización.
  return this.https.post<PermissionsRolRequest>(direccion, rol, { headers });
}



ObtenerPermisos(rol: RolInterface): Observable<PermissionsRolRequest> {
  const direccion = `${this.uriApi}RolDisponible`; // Ajusta la URL según tu API.

  // Agrega un encabezado de autorización con el token de autenticación en la solicitud.
  const headers = { Authorization: `Bearer ${this.token}` };

  // Realiza la solicitud POST a la API con los datos del nuevo rol y los encabezados de autorización.
  return this.https.post<PermissionsRolRequest>(direccion, rol, { headers });
}



GetIdRolModificado(rol: RolInterface): Observable<PermissionsRolRequest> {
  const direccion = `${this.uriApi}FindRolPermissions`; // Ajusta la URL según tu API.

  // Agrega un encabezado de autorización con el token de autenticación en la solicitud.
const headers = { Authorization: `Bearer ${this.token}` };

  // Realiza la solicitud POST a la API con los datos del nuevo rol y los encabezados de autorización.
  return this.https.post<PermissionsRolRequest>(direccion, rol, { headers });
}




notifyNuevoRolModificado(nuevoRol: PermissionsRolRequest)  {
  console.log("notity");
  this.nuevoRolSubjectModificado.next(nuevoRol);
}



getUsuarioActualizadoObservableModificado(): Observable<PermissionsRolRequest> {
 
  return this.nuevoRolSubjectModificado.asObservable();
}

notifyElementoEliminado(id: string) {
  this.elementoEliminadoSubject.next(id);
}





getNuevoRolObservableModficado(): Observable<PermissionsRolRequest> {
  
 
    return this.nuevoRolSubjectModificado.asObservable();
}


getElementoEliminadoObservable(): Observable<string> {
  return this.elementoEliminadoSubject.asObservable();
}

}

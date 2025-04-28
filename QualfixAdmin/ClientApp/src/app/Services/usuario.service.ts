
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UsuariomodelArray, Usuariomodel, RegistroUsuariomodel, SolicitudRegistroUsuariomodel } from '../Models/Usuario-interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private http: HttpClient) {

  }

  // getAllUsersWithProfiles(): Observable<any> {
  //   const url = `https://localhost:7141/api/usuario/ObtenerUsuario`;
  //   return this.http.get<any>(url);
  // }

  getAllUsersWithProfiles(): Observable<any> {
    // Realiza una solicitud HTTP GET y tipa la respuesta.
    const url = `https://localhost:7141/api/usuario/ObtenerUsuario`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json',
  });
    return this.http.get<any>(url, {headers});
  }



  public getusuario(url: string) {
    return this.http.get<Usuariomodel>(url);
  }
  
// usuario.service.ts
CrearUsuario(url: string, usuario: RegistroUsuariomodel, nombreCompany?: string, nombrePerfil?: string,nombreRol?: string ): Observable<any> {
  nombreCompany = nombreCompany || '';
  nombrePerfil = nombrePerfil || '';
  nombreRol = nombreRol || '';
  const body = {
    usuario: usuario,
    nombreCompany: nombreCompany,
    nombrePerfil: nombrePerfil,
    nombreRol: nombreRol,
  };

  return this.http.post(url, body);
}



  SolicitudCrearUsuario(url: string, usuario: SolicitudRegistroUsuariomodel) {

    return this.http.post(url, usuario).subscribe(response => {
      window.location.reload();
      console.log('Usuario creado:', response);
    },
      error => {
        console.error('Error al crear usuario:', error);
        // Manejar el error si es necesario
      });
  }
  EditarUsuario(url: string, usuario: Usuariomodel) {
    return this.http.put(url, usuario).subscribe(response => {
      console.log('Usuario editado:', response);
      window.location.reload();
    },
      error => {
        console.error('Error al editar usuario:', error);
        // Manejar el error si es necesario
      });
  }
  EliminarUsuario(url: string) {
    return this.http.delete(url).subscribe(response => {
      console.log('Usuario Eliminado:', response);
      window.location.reload();
    },
      error => {
        console.error('Error al Eliminar usuario:', error);
        // Manejar el error si es necesario
      });
  }
}

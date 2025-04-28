import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { GetAllAcce, GetAllAccesosRequest, NuevoPerfilModel, PerfilAccesosModel, actualizarPerfil } from '../Modelos/Perfil.interface';

@Injectable({
  providedIn: 'root'
})
export class PerfilService {


  private nuevoRolSubjectModificado = new Subject<PerfilAccesosModel>();
  private elementoEliminadoSubject = new Subject<string>();

private uriApi='https://localhost:7141/Perfil/'

  constructor(private https:HttpClient) { }



  getPerfilesConAccesos(): Observable<PerfilAccesosModel[]> {
    // Realiza una solicitud HTTP GET y tipa la respuesta.
    return this.https.get<PerfilAccesosModel[]>(`${this.uriApi}perfiles-accesos`);
  }
  getPerfil(): Observable<any> {
    // Realiza una solicitud HTTP GET y tipa la respuesta.
    return this.https.get<any>(`${this.uriApi}ObtenerPerfil`);
  }

  insertarNuevoPerfil(nuevoPerfil: NuevoPerfilModel): Observable<any> {
    // Realiza una solicitud HTTP POST y envía el modelo de datos.
    return this.https.post(`${this.uriApi}Perfil-create`, nuevoPerfil);
  }
  getListaDeAccesos(): Observable<GetAllAccesosRequest[]> {
    return this.https.get<GetAllAccesosRequest[]>(`${this.uriApi}Accesos`);
  }
  
  actualizarPerfil(perfilActualizado: actualizarPerfil): Observable<any> {
    return this.https.post(`${this.uriApi}Perfil-update`, perfilActualizado);
  }

  eliminarPerfil(Perfil: actualizarPerfil): Observable<PerfilAccesosModel> {
    return this.https.post<PerfilAccesosModel>(`${this.uriApi}Perfil-delete`, Perfil);
  }

  buscarPerfilPorId(perfilId: actualizarPerfil): Observable<any> {
    return this.https.post<any>(`${this.uriApi}PerfilId-accesos`, perfilId);
  }


  ObtenerPermisos(Perfil: actualizarPerfil): Observable<GetAllAcce> {
    const direccion = `${this.uriApi}AccesosDisponibles`; // Ajusta la URL según tu API.
  

  
    // Realiza la solicitud POST a la API con los datos del nuevo rol y los encabezados de autorización.
    return this.https.post<GetAllAcce>(direccion, Perfil);
  }


  notifyNuevoPerfilModificado(nuevoPerfil: PerfilAccesosModel) {
    this.nuevoRolSubjectModificado.next(nuevoPerfil);
  }

  getNuevoPerfilObservableModificado(): Observable<PerfilAccesosModel> {
    return this.nuevoRolSubjectModificado.asObservable();
  }

  notifyElementoEliminado(id: string) {
    this.elementoEliminadoSubject.next(id);
  }

  getElementoEliminadoObservable(): Observable<string> {
    return this.elementoEliminadoSubject.asObservable();
  }

  getPerfilActualizadoObservableModificado(): Observable<PerfilAccesosModel> {
 
    return this.nuevoRolSubjectModificado.asObservable();
  }

}

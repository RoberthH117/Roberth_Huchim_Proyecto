import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { TipoLicenciaFindIdI, TipoLicenciaI, TipoLicenciaInsertI, TipoLicenciaUpdateI } from '../Modelos/TipoLicencia.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TipoLicenciaService {
private uriApi='https://localhost:7141/TipoLicencia/';

private nuevalicenciaSubjectMoficado = new Subject<TipoLicenciaI>
private elementoEliminadoSubject= new Subject<number>();


  constructor(private https:HttpClient) { }



  GetAllTipoLicencia(): Observable<TipoLicenciaI[]> {
    const direccion = `${this.uriApi}GetTipoLicencia`; // Asegúrate de ajustar la URL de acuerdo a tu API.
  
    // No necesitas agregar el encabezado de autorización manualmente aquí.
    // El interceptor se encargará de ello si el token está disponible en localStorage.
  
    return this.https.get<TipoLicenciaI[]>(direccion);
  }


  InsertTipoLicencia(licencia: TipoLicenciaInsertI): Observable<TipoLicenciaI> {
    const direccion = `${this.uriApi}InsertTipoLicencia`; // Ajusta la URL según tu API.
  
    // Agrega un encabezado de autorización con el token de autenticación en la solicitud.
    
  
    // Realiza la solicitud POST a la API con los datos del nuevo licencia y los encabezados de autorización.
    return this.https.post<TipoLicenciaI>(direccion, licencia);
  }

  UpdateTipoLicencia(licencia: TipoLicenciaUpdateI): Observable<TipoLicenciaI> {
    const direccion = `${this.uriApi}UpdateTipoLicencia`; // Ajusta la URL según tu API.
  
    // Agrega un encabezado de autorización con el token de autenticación en la solicitud.
    
  
    // Realiza la solicitud POST a la API con los datos del nuevo licencia y los encabezados de autorización.
    return this.https.post<TipoLicenciaI>(direccion, licencia);
  }

  FindTipoLicencia(licencia: TipoLicenciaFindIdI): Observable<TipoLicenciaI> {
    const direccion = `${this.uriApi}GetIdTipoLicencia`; // Ajusta la URL según tu API.
  
    // Agrega un encabezado de autorización con el token de autenticación en la solicitud.
    
  
    // Realiza la solicitud POST a la API con los datos del nuevo licencia y los encabezados de autorización.
    return this.https.post<TipoLicenciaI>(direccion, licencia);
  }

  DeleteTipoLicencia(licencia: TipoLicenciaFindIdI): Observable<TipoLicenciaI> {
    const direccion = `${this.uriApi}DeleteTipoLicencia`; // Ajusta la URL según tu API.
  
    // Agrega un encabezado de autorización con el token de autenticación en la solicitud.
   
  
    // Realiza la solicitud POST a la API con los datos del nuevo rol y los encabezados de autorización.
    return this.https.post<TipoLicenciaI>(direccion,licencia);
  }




notifyNuevoRolModificado(licencia: TipoLicenciaI)  {
  console.log("notity");
  this.nuevalicenciaSubjectMoficado.next(licencia);
}



getUsuarioActualizadoObservableModificado(): Observable<TipoLicenciaI> {
 
  return this.nuevalicenciaSubjectMoficado.asObservable();
}

notifyElementoEliminado(id: number) {
  this.elementoEliminadoSubject.next(id);
}





getNuevoRolObservableModficado(): Observable<TipoLicenciaI> {
  
 
    return this.nuevalicenciaSubjectMoficado.asObservable();
}


getElementoEliminadoObservable(): Observable<number> {
  return this.elementoEliminadoSubject.asObservable();
}













}

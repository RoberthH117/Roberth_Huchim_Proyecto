import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LicenciaModel } from '../Modelos/Licencia.interface';

@Injectable({
  providedIn: 'root'
})
export class LicenciaService {
  private baseUrl: string = 'https://roberth-huchim-proyecto.onrender.com/Licencia'; // Reemplaza 'tu-api-base-url' con la URL de tu API

  constructor(private http: HttpClient) { }

  generarLicencia(request: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/GenerarLicencia`, request);
  }

  obtenerLicencias(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/ObtenerLicencias`);
  }

  getLicenciaUsuario(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/GetLicenciaUsuario`);
  }

  getUsuariosEmpresa(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/GetUsuariosEmpresa`);
  }

  getLicenciaActual(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/GetUsuario`);
  }


  getEstadoLicenciaBool(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/EstadoLicencia`);

  }





}
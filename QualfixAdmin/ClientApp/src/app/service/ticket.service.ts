import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { IdRequest, Reporte, ReporteModelo } from '../Modelos/Ticket.interface';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private uriApi='https://localhost:7141/Ticket/';


  constructor(private https:HttpClient) { }


  enviarTicket(titulo: string, descripcion: string, imagenes: File[]): Observable<any> {
    debugger
    const formData: FormData = new FormData();
    formData.append('Titulo', titulo);
    formData.append('Descripcion', descripcion);
  
    // Agrega cada imagen al formulario
    for (let i = 0; i < imagenes.length; i++) {
      formData.append('Imagenes', imagenes[i]);
    }
  
    // Realiza la solicitud POST al endpoint correspondiente
    return this.https.post<any>(`${this.uriApi}CreateTicket`, formData);
  }

  
  
  getTicketsPendientes(): Observable<any> {
    // Realiza una solicitud HTTP GET y tipa la respuesta.
    return this.https.get<any>(`${this.uriApi}GetTicketPendiente`);
  }

  getTicketUser(): Observable<any> {
    // Realiza una solicitud HTTP GET y tipa la respuesta.
    return this.https.get<any>(`${this.uriApi}FindTicket`);
  }

  getIdTicket(id: string): Observable<any> {
    const requestBody = { id: id };
  
    const headers = { 'Content-Type': 'application/json' };
  
 debugger
  
    return this.https.post<any>(`${this.uriApi}GetIdTicket`, requestBody, { headers });
  }




UpdateTicket(id:number,estado:string): Observable<any> {
  const requestBody = { id:id,
  estado:estado};

  return this.https.post<any>(`${this.uriApi}UpdateTicket`, requestBody);
}


}

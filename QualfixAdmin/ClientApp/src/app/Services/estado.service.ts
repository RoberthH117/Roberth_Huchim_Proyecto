import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Estadoarray } from '../Models/Estado-interface';

@Injectable({
  providedIn: 'root'
})
export class EstadoService {

  constructor(private http:HttpClient) { }
  public getall(url: string) {
    return this.http.get<Estadoarray>(url);
  }
}

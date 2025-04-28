import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Ciudadarray } from '../Models/Ciudad-interface';

@Injectable({
  providedIn: 'root'
})
export class CiudadService {

  constructor(private http:HttpClient) { }

  public getall(url: string) {
    return this.http.get<Ciudadarray>(url);
  }
}

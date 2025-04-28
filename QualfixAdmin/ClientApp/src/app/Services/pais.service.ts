import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Paisarray } from '../Models/Pais-Interface';

@Injectable({
  providedIn: 'root'
})
export class PaisService {

  constructor(private http:HttpClient) { }

  public getall(url: string) {
    return this.http.get<Paisarray>(url);
  }
}

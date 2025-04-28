import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegimenFiscalsarray } from '../Models/RegimenFiscal-interface';

@Injectable({
  providedIn: 'root'
})
export class RegimenFiscalService {

  constructor( private http:HttpClient) { }

  public getAll (url:string){
    return this.http.get<RegimenFiscalsarray>(url);
  }
}

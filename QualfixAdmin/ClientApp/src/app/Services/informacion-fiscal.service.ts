 
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { InformacioFiscalarray, InformacionFiscalmodel } from '../Models/InformacionFiscal-Interface';

@Injectable({
  providedIn: 'root'
})
export class InformacionFiscalService {

  constructor(private http:HttpClient) { }

  public getAllInformacionFiscal(url:string){
    return this.http.get<InformacioFiscalarray>(url);
  }
  public getInformacionFiscal(url: string) {
    return this.http.get<InformacionFiscalmodel>(url);
  }

  CrearInformacionFiscal(url: string, InfoFsical: InformacionFiscalmodel) {

    return this.http.post(url, InfoFsical);
  }

  EditarInformacionFiscal(url: string, infofiscal: InformacionFiscalmodel) {
    return this.http.put(url, infofiscal).subscribe(response => {
      console.log('producto editado:', response);
      
    },
      error => {
        console.error('Error al editar producto:', error);
        // Manejar el error si es necesario
      });
  }
  EliminarInformacionFiscal(url: string) {
    return this.http.delete(url).subscribe(response => {
      console.log('producto Eliminado:', response);
      
    },
      error => {
        console.error('Error al Eliminar producto:', error);
        // Manejar el error si es necesario
      });
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigCFDIModel, ConfigCFDIcodeModel, ConfigCFDIrray } from '../Models/ConfiguracionCFDI-interface';

@Injectable({
  providedIn: 'root'
})
export class ConfigCFDIService {

  constructor(private http: HttpClient) { }

  public getAll(url: string) {
    return this.http.get<ConfigCFDIrray>(url);
  }
  public getConfigCFDI(url: string) {
    return this.http.get<ConfigCFDIcodeModel>(url);
  }

  CrearConfigcfdi(url: string, configcfdi: ConfigCFDIModel) {

    return this.http.post(url, configcfdi).subscribe(response => {
      console.log('Producto creado:', response);
      window.location.reload();
    },
      error => {
        console.error('Error al crear usuario:', error);
        // Manejar el error si es necesario
      }
    );
  }
  EditarConfigcfdi(url: string, configcfdi: ConfigCFDIModel) {
    return this.http.put(url, configcfdi).subscribe(response => {
      console.log('producto editado:', response);
      window.location.reload();
    },
      error => {
        console.error('Error al editar producto:', error);
        // Manejar el error si es necesario
      });
  }

  EliminarConfigcfdi(url: string) {
    return this.http.delete(url).subscribe(response => {
      console.log('producto Eliminado:', response);
      window.location.reload();
    },
      error => {
        console.error('Error al Eliminar producto:', error);
        // Manejar el error si es necesario
      });
  }
}

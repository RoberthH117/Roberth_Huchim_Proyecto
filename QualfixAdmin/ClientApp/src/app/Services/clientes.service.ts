import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Clientemodel, Clientesrray } from '../Models/Cliente-Interface';

@Injectable({
  providedIn: 'root'
})
export class ClientesService {

  constructor(private http:HttpClient) { }

  public getAll (url:string){
    return this.http.get<Clientesrray>(url);
  }
  public getCliente(url: string) {
    return this.http.get<Clientemodel>(url);
  }

  CrearCliente(url: string, cliente: Clientemodel) {

    return this.http.post(url, cliente).subscribe(response => {
      console.log('Producto creado:', response);
      window.location.reload();
    },
      error => {
        console.error('Error al crear usuario:', error);
        // Manejar el error si es necesario
      }
    );
  }

  EditarCliente(url: string, cliente: Clientemodel) {
    return this.http.put(url, cliente).subscribe(response => {
      console.log('producto editado:', response);
      window.location.reload();
    },
      error => {
        console.error('Error al editar producto:', error);
        // Manejar el error si es necesario
      });
  }
  EliminarCliente(url: string) {
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

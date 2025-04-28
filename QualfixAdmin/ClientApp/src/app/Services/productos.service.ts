import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { productomodel, productosrray } from '../Models/Producto-Interface';

@Injectable({
  providedIn: 'root'
})
export class ProductosService {

  constructor( private http:HttpClient) { }

  public getAll (url:string){
    return this.http.get<productosrray>(url);
  }
  public getProducto(url: string) {
    return this.http.get<productomodel>(url);
  }

  CrearUsuario(url: string, producto: productomodel) {

    return this.http.post(url, producto).subscribe(response => {
      console.log('Producto creado:', response);
      window.location.reload();
    },
      error => {
        console.error('Error al crear usuario:', error);
        // Manejar el error si es necesario
      }
    );
  }

  Editarproducto(url: string, producto: productomodel) {
    return this.http.put(url, producto).subscribe(response => {
      console.log('producto editado:', response);
      window.location.reload();
    },
      error => {
        console.error('Error al editar producto:', error);
        // Manejar el error si es necesario
      });
  }
  Eliminarproducto(url: string) {
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

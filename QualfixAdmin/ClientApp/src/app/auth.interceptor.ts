import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Obtén el token desde donde lo tengas almacenado (en este caso, localStorage)
    const token = localStorage.getItem('Authorization');
let req = request;
debugger
    // Clona la solicitud original y agrega el encabezado de autorización con el token
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,// No es necesario agregar "Bearer" aquí
        },
      });

      console.log(request);



    }

    return next.handle(request);
  }
}
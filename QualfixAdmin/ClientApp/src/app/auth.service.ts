import { Injectable } from '@angular/core';
import { CanActivate, CanActivateFn, Router } from '@angular/router';
import { ApiService } from './service/api.service';
import { Observable, catchError, map, of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService implements CanActivate{

  constructor(private apiservice: ApiService, private router: Router) {}


  canActivate(): Observable<boolean> {
    debugger
    return this.apiservice.GetComprobation().pipe(
      catchError((error) => {
        // En caso de error al comprobar la autenticación, redirige al inicio de sesión.
        // this.router.navigate(['/login']);
        console.log(error);
        return of(true);
      })
    );
  }


  cerrarSesion() {
    // Llama al servicio de autenticación para cerrar la sesión.
    localStorage.clear();

    // Redirige al usuario a la página de inicio de sesión o a la página de inicio de la aplicación.
    // Puedes usar una función de enrutamiento o redirigir manualmente.
  }
}

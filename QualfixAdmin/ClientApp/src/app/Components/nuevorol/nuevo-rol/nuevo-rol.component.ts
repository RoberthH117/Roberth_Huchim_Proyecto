import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { MatTableDataSource } from '@angular/material/table';
import { RolI } from 'src/app/Modelos/Data.interface';
import { PermissionsRolRequest, RolInterface, RolInterfacePost, RolInterfacePostEnviar } from 'src/app/Modelos/rol.interface';
import { RolService } from 'src/app/service/rol.service';
import { ModalregistrorolComponent } from '../../ModalRol/modalregistrorol/modalregistrorol.component';
import { Observable, Subscription } from 'rxjs';
import { ModaleditarrolComponent } from '../../ModalRol/modaleditarrol/modaleditarrol.component';
import { CookieService } from 'ngx-cookie-service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ModeloPermisos } from 'src/app/Modelos/Permisos.interface';



@Component({
  selector: 'app-nuevo-rol',
  templateUrl: './nuevo-rol.component.html',
  styleUrls: ['./nuevo-rol.component.css']
})
export class NuevoRolComponent implements OnInit{

   
  constructor(private  rolService:RolService, public dialog: MatDialog,private cookieService: CookieService) {  }
  isLoading:boolean | undefined;
  permisos = new MatTableDataSource<PermissionsRolRequest>([]);
  //permisos: Observable<PermissionsRolRequest[]> = new Observable();
PermisosRoles: PermissionsRolRequest[]=[];
  displayedColumns: string[] = ['nombre', 'descripcion','permisos','acciones'];
  private nuevoRolSubscription: Subscription | undefined;
  totalElements: number | undefined; // Cantidad total de elementos
  pageSize: number = 10; // Número de elementos por página
  private elementoEliminadoSubscription: Subscription | undefined;
private nuevoRolSubject : Subscription|undefined;
  private rolActualizadoSubscription: Subscription | undefined;


  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;


  // Evento de paginación
  Editar:boolean=false;
  Crear:boolean=false;
  Eliminar:boolean=false;
  

  ngOnInit() {

  
    this.isLoading=true;
          


    this.rolService.GetPermisos().subscribe((permisos: ModeloPermisos) => {
      debugger
      // VerInventario será true si permisos.VerInventario es true, de lo contrario, será false.
      
      this.Editar = permisos.editar;
      this.Eliminar=permisos.eliminar;
      this.Crear=permisos.crear;
    });
   
    this.rolService.GetAllPermissions().subscribe(
      (data) => {
        this.PermisosRoles = data;
        this.totalElements = data.length; // Establece la cantidad total de elementos
        this.permisos = new MatTableDataSource<PermissionsRolRequest>(data);
        this.isLoading = false; // Oculta la pantalla de carga después de obtener los datos
       
       
        // Aquí puedes manejar los datos, como mostrarlos en tu tabla HTML.

        if (this.paginator) {
          this.paginator.pageIndex = 0;
        }
        this.onPageChange({ pageIndex: 0, pageSize: this.pageSize, length: this.totalElements });
      },

      (error) => {
        console.error('Error al obtener los roles', error);
      }

      
    );
    this.nuevoRolSubscription = this.rolService.getNuevoRolObservableModficado().subscribe(() => {
      if (this.paginator) {
        const currentPage = this.paginator.pageIndex; // Almacena la página actual
        this.rolService.GetAllPermissions().subscribe(
          (data) => {
            this.PermisosRoles = data;
            this.totalElements = data.length; // Establece la cantidad total de elementos
            this.permisos = new MatTableDataSource<PermissionsRolRequest>(data);
            
            // Restablece la página a la que estaba antes de cargar los nuevos datos
            if(this.paginator){
            this.paginator.pageIndex = currentPage;
            
            this.onPageChange({ pageIndex: currentPage, pageSize: this.pageSize, length: this.totalElements });
            // Aquí puedes manejar los datos, como mostrarlos en tu tabla HTML.
            }
          
          },
          (error) => {
            console.error('Error al obtener los roles', error);
          }
        );
      }
    });

    
   
    

    this.rolActualizadoSubscription = this.rolService.getUsuarioActualizadoObservableModificado().subscribe((rolActualizado) => {
      // Encuentra y actualiza el rol en tu lista de roles
      const index = this.PermisosRoles.findIndex((rol) => rol.rolId === rolActualizado.rolId);
      
      if (index !== -1) {
        this.PermisosRoles[index] = rolActualizado;
        this.PermisosRoles.splice(index, 1);
      }

     
    
      // Actualiza la tabla
      this.permisos = new MatTableDataSource<PermissionsRolRequest>(this.PermisosRoles);
    });

    this.elementoEliminadoSubscription = this.rolService.getElementoEliminadoObservable().subscribe((id) => {
      // Encuentra y elimina el elemento en tu lista de roles utilizando el ID
      const index = this.PermisosRoles.findIndex((rol) => rol.rolId === id);
      if (index !== -1) {
        this.PermisosRoles.splice(index, 1);
        this.totalElements = this.PermisosRoles.length;

        if (this.paginator) {
          // Calcula la última página después de eliminar el elemento
          const lastPageIndex = this.calculateLastPageIndex(this.totalElements);
          
          // Verifica si la página actual es mayor o igual a la última página
          if (this.paginator.pageIndex >= lastPageIndex) {
            // Establece la página actual en la último página
            this.paginator.pageIndex = lastPageIndex;
          }
        
    
        // Crea una nueva instancia de MatTableDataSource con el arreglo actualizado
        this.permisos = new MatTableDataSource<PermissionsRolRequest>(this.PermisosRoles);
        
        // Asigna el paginador actual a la nueva instancia de MatTableDataSource
        this.permisos.paginator = this.paginator;
        }
      }
    });

    
  }





  onPageChange(event: PageEvent): void {

  if(this.paginator){
    this.paginator._intl.itemsPerPageLabel = 'Registros por pagina';
  }
    const startIndex = event.pageIndex * event.pageSize;
    const endIndex = startIndex + event.pageSize;
    this.permisos = new MatTableDataSource<PermissionsRolRequest>(this.PermisosRoles.slice(startIndex, endIndex));
  }
  private calculateLastPageIndex(totalElements: number): number {
if(this.paginator){

    return Math.floor((totalElements - 1) / this.paginator.pageSize);
}
return 0;
  
  }
  agregarElemento() {
    const dialogRef = this.dialog.open(ModalregistrorolComponent);
  }

  verElemento(elemento: PermissionsRolRequest) {
    // Implementa la lógica para ver un elemento
  }

  editarElemento(permisos: PermissionsRolRequest) {
    const dialogRef = this.dialog.open(ModaleditarrolComponent, {
      data: { id: permisos.rolId }
    });
  }

  eliminarElemento(elemento: PermissionsRolRequest) {
    // Implementa la lógica para eliminar un elemento


    const nuevoRol: RolInterface = {
      // Puedes dejarlo vacío si el servidor genera el ID automáticamente
      id:elemento.rolId,
      name: "", // Reemplaza 'name' con el nombre del campo en tu formulario
      descripcion:"", // Reemplaza 'descripcion' con el nombre del campo en tu formulario
      permisos:""
    };
 
    
            this.rolService.DeleteRolModificado(nuevoRol).subscribe(
              (resultado) => {
                // Realiza acciones después de agregar el rol, por ejemplo, redireccionar o cerrar el modal.
                console.log("Exito");
            
                
                // Notifica a los observadores que se ha agregado un nuevo rol
                this.rolService.notifyElementoEliminado(nuevoRol.id);
              },
              (error) => {
                console.error('Error al agregar el rol', error);
              }
            );
    
          
  }


  ngOnDestroy() {
    if (this.elementoEliminadoSubscription) {
      this.elementoEliminadoSubscription.unsubscribe();
    }
    if (this.nuevoRolSubscription) {
      this.nuevoRolSubscription.unsubscribe();
    }
    if (this.rolActualizadoSubscription) {
      this.rolActualizadoSubscription.unsubscribe();
    }

  }




 





}







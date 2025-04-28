import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { CookieService } from 'ngx-cookie-service';
import { Subscription } from 'rxjs';
import { PermissionsRolRequest, RolInterface } from 'src/app/Modelos/rol.interface';
import { RolService } from 'src/app/service/rol.service';
import { ModaleditarrolComponent } from '../ModalRol/modaleditarrol/modaleditarrol.component';
import { ModalregistrorolComponent } from '../ModalRol/modalregistrorol/modalregistrorol.component';
import { TipoLicenciaService } from 'src/app/service/tipo-licencia.service';
import { TipoLicenciaFindIdI, TipoLicenciaI } from 'src/app/Modelos/TipoLicencia.interface';
import { ModalregistrotipolicenciaComponent } from '../ModalTipoLicencia/modalregistrotipolicencia/modalregistrotipolicencia.component';
import { ModaleditartipolicenciaComponent } from '../ModalTipoLicencia/modaleditartipolicencia/modaleditartipolicencia.component';
import { ModeloPermisos } from 'src/app/Modelos/Permisos.interface';

@Component({
  selector: 'app-tipo-licencia',
  templateUrl: './tipo-licencia.component.html',
  styleUrls: ['./tipo-licencia.component.css']
})
export class TipoLicenciaComponent implements OnInit{

   
  constructor(private  licenciaService:TipoLicenciaService, public dialog: MatDialog, private rolService:RolService) {  }
  isLoading:boolean | undefined;
  permisos = new MatTableDataSource<TipoLicenciaI>([]);
  //permisos: Observable<PermissionsRolRequest[]> = new Observable();
PermisosRoles: TipoLicenciaI[]=[];
  displayedColumns: string[] = [ 'tipo','tiempo','costo','acciones'];
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

    this.rolService.GetPermisos().subscribe((permisos: ModeloPermisos) => {
      debugger
      // VerInventario será true si permisos.VerInventario es true, de lo contrario, será false.
      
      this.Editar = permisos.editar;
      this.Eliminar=permisos.eliminar;
      this.Crear=permisos.crear;
    });
    
  this.isLoading=true;
   
          
   
    this.licenciaService.GetAllTipoLicencia().subscribe(
      (data) => {
        debugger
        this.PermisosRoles = data;
        
        this.totalElements = data.length; // Establece la cantidad total de elementos
        this.permisos = new MatTableDataSource<TipoLicenciaI>(data);
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
    this.nuevoRolSubscription = this.licenciaService.getNuevoRolObservableModficado().subscribe(() => {
      if (this.paginator) {
        const currentPage = this.paginator.pageIndex; // Almacena la página actual
        this.licenciaService.GetAllTipoLicencia().subscribe(
          (data) => {
            this.PermisosRoles = data;
            this.totalElements = data.length; // Establece la cantidad total de elementos
            this.permisos = new MatTableDataSource<TipoLicenciaI>(data);
            
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

    
   
    

    this.rolActualizadoSubscription = this.licenciaService.getUsuarioActualizadoObservableModificado().subscribe((rolActualizado) => {
      // Encuentra y actualiza el rol en tu lista de roles
      const index = this.PermisosRoles.findIndex((rol) => rol.tipoLicenciaId === rolActualizado.tipoLicenciaId);
      
      if (index !== -1) {
        this.PermisosRoles[index] = rolActualizado;
        this.PermisosRoles.splice(index, 1);
      }

     
    
      // Actualiza la tabla
      this.permisos = new MatTableDataSource<TipoLicenciaI>(this.PermisosRoles);
    });

    this.elementoEliminadoSubscription = this.licenciaService.getElementoEliminadoObservable().subscribe((id) => {
      // Encuentra y elimina el elemento en tu lista de roles utilizando el ID
      const index = this.PermisosRoles.findIndex((rol) => rol.tipoLicenciaId === id);
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
        this.permisos = new MatTableDataSource<TipoLicenciaI>(this.PermisosRoles);
        
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
    this.permisos = new MatTableDataSource<TipoLicenciaI>(this.PermisosRoles.slice(startIndex, endIndex));
  }
  private calculateLastPageIndex(totalElements: number): number {
if(this.paginator){

    return Math.floor((totalElements - 1) / this.paginator.pageSize);
}
return 0;
  
  }
  agregarElemento() {
    const dialogRef = this.dialog.open(ModalregistrotipolicenciaComponent);
  }

  verElemento(elemento: TipoLicenciaI) {
    // Implementa la lógica para ver un elemento
  }

  editarElemento(permisos: TipoLicenciaI) {
    const dialogRef = this.dialog.open(ModaleditartipolicenciaComponent, {
      data: { id: permisos.tipoLicenciaId }
    });
  }

  eliminarElemento(elemento: TipoLicenciaI) {
    // Implementa la lógica para eliminar un elemento


    const TipoLicencia: TipoLicenciaFindIdI = {
      // Puedes dejarlo vacío si el servidor genera el ID automáticamente
      TipoLicenciaId:elemento.tipoLicenciaId
     
    };
 
    
            this.licenciaService.DeleteTipoLicencia(TipoLicencia).subscribe(
              (resultado) => {
                // Realiza acciones después de agregar el rol, por ejemplo, redireccionar o cerrar el modal.
                console.log("Exito");
            
                
                // Notifica a los observadores que se ha agregado un nuevo rol
                this.licenciaService.notifyElementoEliminado(TipoLicencia.TipoLicenciaId);
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

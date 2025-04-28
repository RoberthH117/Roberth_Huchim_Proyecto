import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { ModeloPermisos } from 'src/app/Modelos/Permisos.interface';
import { PermissionsRolRequest, RolInterface } from 'src/app/Modelos/rol.interface';
import { RolService } from 'src/app/service/rol.service';
import { ModaleditarrolComponent } from '../ModalRol/modaleditarrol/modaleditarrol.component';
import { ModalregistrorolComponent } from '../ModalRol/modalregistrorol/modalregistrorol.component';
import { LicenciaService } from 'src/app/service/licencia.service';
import { LicenciaRequest } from 'src/app/Modelos/Licencia.interface';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-historial-licencia',
  templateUrl: './historial-licencia.component.html',
  providers: [DatePipe],
  styleUrls: ['./historial-licencia.component.css']
})



export class HistorialLicenciaComponent implements OnInit {
  constructor(private licenciaService: LicenciaService, private datePipe: DatePipe) {}

  isLoading: boolean | undefined;
  licencias: MatTableDataSource<LicenciaRequest> = new MatTableDataSource<LicenciaRequest>([]);
  licenciaslocal: LicenciaRequest[]=[];
  displayedColumns: string[] = ['licencia', 'fecha_inicio', 'fecha_expiracion','tiempo'];
  totalElements: number | undefined; // Cantidad total de elementos
  pageSize: number = 10; // Número de elementos por página
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;

  ngOnInit() {
    this.isLoading = true;

    // Reemplaza el siguiente bloque con tu método getlicenciaUsuario
    this.licenciaService.getLicenciaUsuario().subscribe(
      (data) => {
        
        this.licenciaslocal = data;
        this.licencias.data = data;
        this.totalElements = data.length;
        this.isLoading = false;
        this.licencias = new MatTableDataSource<LicenciaRequest>(data);
        if (this.paginator) {
          this.paginator.pageIndex = 0;
        }
        this.onPageChange({ pageIndex: 0, pageSize: this.pageSize, length: this.totalElements || 0 });
      },
      (error) => {
        console.error('Error al obtener las licencias', error);
      }
    );
  }
  formatFecha(fecha: string): string {
    const parsedFecha = new Date(fecha);
    return this.datePipe.transform(parsedFecha, 'dd/MM/yyyy') || '';
  }
  onPageChange(event: PageEvent): void {
    debugger
    if (this.paginator) {
      this.paginator._intl.itemsPerPageLabel = 'Registros por página';
    }
  
    const startIndex = event.pageIndex * event.pageSize;
    const endIndex = startIndex + event.pageSize;
  
    // Ajustar los datos existentes en lugar de crear una nueva instancia de MatTableDataSource
    this.licencias = new MatTableDataSource<LicenciaRequest>(this.licenciaslocal.slice(startIndex, endIndex));
  }
    private calculateLastPageIndex(totalElements: number): number {
  if(this.paginator){
  
      return Math.floor((totalElements - 1) / this.paginator.pageSize);
  }
  return 0;
    
    }
}
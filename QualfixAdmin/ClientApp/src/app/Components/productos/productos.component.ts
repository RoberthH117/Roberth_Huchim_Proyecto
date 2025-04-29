 
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { productomodel } from 'src/app/Models/Producto-Interface';
import { ProductosService } from 'src/app/Services/productos.service';
import { ModalRegProductosComponent } from '../modal-reg-productos/modal-reg-productos.component';
import { ModalEditProductosComponent } from '../modal-edit-productos/modal-edit-productos.component';
import { ModalEliminarProductosComponent } from '../modal-eliminar-productos/modal-eliminar-productos.component';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {

  pagina = 1;
  constructor(private productoservice:ProductosService, public dialog: MatDialog,){

  }
  ngOnInit(): void {
    this.cargarProductos();
  }
 
  productos: productomodel[]=[];

  public openDialogCrear(){
    const dialogRef = this.dialog.open(ModalRegProductosComponent);
  }
  
 
  public openDialogEditar(id: number) {
    const dialogRef = this.dialog.open(ModalEditProductosComponent, {
      data: { id }
    });
  }
  
  
  public openDialogEliminar(id: number){
    const dialogRef = this.dialog.open(ModalEliminarProductosComponent, {
      data: { id }
    });
  }

  public cargarProductos(){
    this.productoservice.getAll(`https://roberth-huchim-proyecto.onrender.com/api/producto?pagina=${this.pagina}`).subscribe(Response =>{
      this.productos = Response;
      console.log(Response);
    })
  }
  public paginaprev(){
    this.pagina--;
    if(this.pagina==0){
      this.pagina=1;
      this.cargarProductos();
    }
    this.cargarProductos();
  }
  public paginanext(){
    this.pagina++;
    this.cargarProductos();
  }
}

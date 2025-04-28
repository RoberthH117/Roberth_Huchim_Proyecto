import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { productomodel } from 'src/app/Models/Producto-Interface';
import { ProductosService } from 'src/app/Services/productos.service';

@Component({
  selector: 'app-modal-eliminar-productos',
  templateUrl: './modal-eliminar-productos.component.html',
  styleUrls: ['./modal-eliminar-productos.component.css']
})
export class ModalEliminarProductosComponent implements OnInit{
  producto:productomodel={
    id:0,
    nombre:'',
    descripcion:''
  }
  
  constructor(private productoService:ProductosService,@Inject(MAT_DIALOG_DATA) public data: productomodel,){
    this.producto.id = data.id;
  }
  
  
  
  ngOnInit(): void {
     
  }


  Eliminar(id:number){
    this.productoService.Eliminarproducto(`https://localhost:7141/api/producto/${this.producto.id}`);
    
  }
}

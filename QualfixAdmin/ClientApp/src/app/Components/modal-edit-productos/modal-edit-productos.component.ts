import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
 
import { productomodel } from 'src/app/Models/Producto-Interface';
import { ProductosService } from 'src/app/Services/productos.service';
import { ModalRegProductosComponent } from '../modal-reg-productos/modal-reg-productos.component';

@Component({
  selector: 'app-modal-edit-productos',
  templateUrl: './modal-edit-productos.component.html',
  styleUrls: ['./modal-edit-productos.component.css']
})
export class ModalEditProductosComponent implements OnInit {
  producto:productomodel = {
    id:0,
    nombre:'',
    descripcion:''
  }
  form: FormGroup;
  constructor( 
    private formBuilder: FormBuilder, 
    private productoService: ProductosService,
    @Inject(MAT_DIALOG_DATA) public data: productomodel,
    public dialogRef: MatDialogRef<ModalEditProductosComponent>,
    ){
      this.producto.id = data.id;
    this.form = this.formBuilder.group(
      {
        nombre:['',Validators.required],
        descripcion: ['', [Validators.required, Validators.maxLength(200)]]
      });
      this.obtenerProducto();
  }
  
  get nombre() {
    return this.form.get('nombre')!;
  }

  get descripcion() {
    return this.form.get('descripcion')!;
  }
  ngOnInit(): void {
   
  }
  obtenerProducto() {
    this.productoService.getProducto(`https://localhost:7141/api/producto/${this.producto.id}`).subscribe(Response => {
 
    this.form.patchValue(Response);
      
    });
}
  public onSubmit(){
    if (this.form.valid) {
      const productoData = this.form.value;
      this.productoService.Editarproducto(`https://localhost:7141/api/producto/${this.producto.id}`, productoData);
      this.dialogRef.close();
       
      console.log(productoData);
    } else {
      console.log('El formulario no es válido');
    }
  }
  public cancelar() {
    this.dialogRef.close();
  }
}
function obtenerusuario() {
  throw new Error('Function not implemented.');
}


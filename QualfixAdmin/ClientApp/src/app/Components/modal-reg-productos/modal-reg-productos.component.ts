import { productomodel } from 'src/app/Models/Producto-Interface';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ProductosService } from 'src/app/Services/productos.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-modal-reg-productos',
  templateUrl: './modal-reg-productos.component.html',
  styleUrls: ['./modal-reg-productos.component.css']
})
export class ModalRegProductosComponent implements OnInit{
  producto:productomodel = {
    id:0,
    nombre:'',
    descripcion:''
  }
  form: FormGroup;
  constructor(private _snackBar: MatSnackBar,private formBuilder: FormBuilder, private productoService: ProductosService,public dialogRef: MatDialogRef<ModalRegProductosComponent>){
    this.form = this.formBuilder.group(
      {
        nombre:['',Validators.required],
        descripcion: ['', [Validators.required, Validators.maxLength(200)]]
      });
  }
  
  get nombre() {
    return this.form.get('nombre')!;
  }

  get descripcion() {
    return this.form.get('descripcion')!;
  }
  ngOnInit(): void {
    
  }

  public  onSubmit(){
    if (this.form.valid) {
     this.productoService.CrearUsuario('https://roberth-huchim-proyecto.onrender.com/api/producto',this.form.value);
    } else {
       
    }  
  
  }
  public cancelar() {
    this.dialogRef.close();
  }
}

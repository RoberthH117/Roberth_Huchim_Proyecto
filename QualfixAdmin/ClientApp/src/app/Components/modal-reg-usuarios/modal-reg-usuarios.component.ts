import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { concatMap } from 'rxjs';
import {RegistroUsuariomodel } 'src/app/Models/Usuario-interface.ts'
import { UsuarioService } from 'src/app/Services/usuario.service';
import { PerfilService } from 'src/app/service/perfil.service';
import { RolService } from 'src/app/service/rol.service';

@Component({
  selector: 'app-modal-reg-usuarios',
  templateUrl: './modal-reg-usuarios.component.html',
  styleUrls: ['./modal-reg-usuarios.component.css']
})

export class ModalRegUsuariosComponent implements OnInit {
  usuario: RegistroUsuariomodel = {
    firstName: '',
    lastName: '',
    status: 1,
    activo: true,
    email: '',
    passwordHash: '',
  };
  form: FormGroup;
  mostrarContrasenia: boolean = false;
  mostrarconfirmarContrasenia: boolean = false;
  estados: string[] = ['Activo', 'Inactivo'];
  ArrayPerfil: string[] = [];
ArrayRol: string[] = [];
ArrayNombre:string[]=[];
selectedPerfil: string | null = null;
selectedRol:string|null=null;
selectedNombre:string|null=null;

  constructor(private formBuilder: FormBuilder, private usuarioService: UsuarioService, public dialogRef: MatDialogRef<ModalRegUsuariosComponent>, private rol:RolService,private perfil:PerfilService) {
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.pattern(/^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/)]],
      passwordHash: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required,Validators.minLength(6)]],
      activo: [true, Validators.required],
      perfil: ['', Validators.required],
      rol: ['', Validators.required],
      nombre:['', Validators.required]
    });
  }

  ngOnInit(): void {

    this.obtenerPefil();
    this.obtenerRol();
    this.obtenerNombre();
  }

  compararContrasenas() {
    const passwordValue = this.form.get('passwordHash')?.value;
    const confirmPasswordValue = this.form.get('confirmPassword')?.value;

    if (passwordValue === confirmPasswordValue) {
      this.form.get('confirmPassword')?.setErrors(null);
    } else {
      this.form.get('confirmPassword')?.setErrors({ contraseñasDiferentes: true });
    }

    // Actualizamos el estado de validez del formulario
    this.form.markAllAsTouched();
    this.form.updateValueAndValidity();
  }

  obtenerPefil() {
    this.perfil.getPerfil().subscribe(data => {
      this.ArrayPerfil = data;
    });
  }
  
  obtenerRol() {
    this.rol.getRol().subscribe(data => {
      this.ArrayRol = data;
    });
  }

  obtenerNombre() {
    this.rol.getNombre().subscribe(data => {
      this.ArrayNombre = data;
    });
  }



  // solicitud-registro-usuario.component.ts
  onSubmit() {
    if (this.form.valid) {
      const selectedPerfil = this.form.get('perfil')?.value;
  const selectedRol = this.form.get('rol')?.value;
  const selectedNombre=this.form.get('nombre')?.value;

  this.usuarioService.CrearUsuario(
    `https://roberth-huchim-proyecto.onrender.com/api/usuario/AgregarUsuario`,
    this.form.value,
    this.selectedNombre || undefined,
    this.selectedPerfil ||undefined,
    this.selectedRol || undefined
    
  ).subscribe(
    () => {
      console.log('Todas las solicitudes se completaron con éxito');
      window.location.reload();
    },
    error => {
      console.error('Error al realizar las solicitudes:', error);
      // Manejar el error si es necesario
    }
  );
        this.dialogRef.close();
    } else {
        // Muestra los errores de validación
        this.form.markAllAsTouched();
    }
  }

  cancelar() {
    this.dialogRef.close();
  }
}

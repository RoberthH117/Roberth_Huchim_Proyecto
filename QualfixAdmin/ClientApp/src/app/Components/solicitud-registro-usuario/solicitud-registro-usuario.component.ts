import { SolicitudRegistroUsuariomodel } from './../../Models/Usuario-interface';
import { UsuarioService } from 'src/app/Services/usuario.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { RegistroUsuariomodel } from 'src/app/Models/Usuario-interface';

@Component({
  selector: 'app-solicitud-registro-usuario',
  templateUrl: './solicitud-registro-usuario.component.html',
  styleUrls: ['./solicitud-registro-usuario.component.css']
})
export class SolicitudRegistroUsuarioComponent implements OnInit {

  usuario: RegistroUsuariomodel = {
    firstName: '',
    lastName: '',
    email: '',
    activo: false,
    status: 1,
    passwordHash: '',

  };
  form: FormGroup;
  mostrarContrasenia: boolean = false;
  mostrarconfirmarContrasenia: boolean = false;
  constructor(private formBuilder: FormBuilder, private usuarioService: UsuarioService) {
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.pattern(/^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/)]],
      passwordHash: ['', [Validators.required,Validators.minLength(6)]],
      confirmPassword: ['',[ Validators.required,Validators.minLength(6)]],

    });
  }
  ngOnInit(): void {

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

  onSubmit() {
    if (this.form.valid) {
      console.log(this.form.value);
      this.usuarioService.CrearUsuario(`https://localhost:7141/api/usuario`, this.form.value);
      // Aquí puedes enviar los datos del formulario
    }
    else {
      // Muestra los errores de validación
      this.form.markAllAsTouched();
    }


  }

}

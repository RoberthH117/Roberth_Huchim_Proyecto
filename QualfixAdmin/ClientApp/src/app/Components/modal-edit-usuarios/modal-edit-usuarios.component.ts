import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UsuarioService } from 'src/app/Services/usuario.service';
import { Usuariomodel } from 'src/app/Models/Usuario-interface';

@Component({
  selector: 'app-modal-edit-usuarios',
  templateUrl: './modal-edit-usuarios.component.html',
  styleUrls: ['./modal-edit-usuarios.component.css']
})
export class ModalEditUsuariosComponent implements OnInit {
  usuarioForm: FormGroup;
  usuario: Usuariomodel = {
    id: '',
    firstName: '',
    lastName: '',
    status: 1,
    activo: false,
    userName: '',
    normalizedUserName: '',
    email: '',
    normalizedEmail: '',
    emailConfirmed: false,
    passwordHash: '',
    securityStamp: '',
    concurrencyStamp: '',
    phoneNumber: '',
    phoneNumberConfirmed: false,
    twoFactorEnabled: false,
    lockoutEnabled: false,
    accessFailedCount: 0
  };
  mostrarContrasenia: boolean = false;
  mostrarconfirmarContrasenia: boolean = false;
  perfiles: string[] = ['Usuario basico', 'Administrador'];
  estados: string[] = ['Activo', 'Inactivo'];

  constructor(
    public dialogRef: MatDialogRef<ModalEditUsuariosComponent>,
    private usuarioService: UsuarioService,
    @Inject(MAT_DIALOG_DATA) public data: Usuariomodel,
    private formBuilder: FormBuilder
  ) {
    this.usuario.id = data.id;
    this.usuarioForm = this.formBuilder.group({
      id: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.pattern(/^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/)]],
      passwordHash: ['', [ Validators.minLength(6)]],
      confirmPassword: ['', [ Validators.minLength(6)]],
      activo: [true, Validators.required],
      status: [1],
      userName: [''],
      normalizedUserName: [''],
      normalizedEmail: [''],
      emailConfirmed: [false],
      securityStamp: [''],
      concurrencyStamp: [''],
      phoneNumber: [''],
      phoneNumberConfirmed: [false],
      twoFactorEnabled: [false],
      lockoutEnabled: [false],
      accessFailedCount: [0]
    });

    this.obtenerusuario();
  }

  ngOnInit(): void {}

  obtenerusuario() {
    this.usuarioService.getusuario(`https://roberth-huchim-proyecto.onrender.com/api/usuario/${this.usuario.id}`).subscribe(Response => {
      this.usuarioForm.patchValue(Response);
    });
  }


  compararContrasenas() {
    const passwordValue = this.usuarioForm.get('passwordHash')?.value;
    const confirmPasswordValue = this.usuarioForm.get('confirmPassword')?.value;

    if (passwordValue === confirmPasswordValue) {
      this.usuarioForm.get('confirmPassword')?.setErrors(null);
    } else {
      this.usuarioForm.get('confirmPassword')?.setErrors({ contraseñasDiferentes: true });
    }

    // Actualizamos el estado de validez del formulario
    this.usuarioForm.markAllAsTouched();
    this.usuarioForm.updateValueAndValidity();
  }


  onSubmit() {
    if (this.usuarioForm.valid) {
      const usuarioData = this.usuarioForm.value;
      this.usuarioService.EditarUsuario(`https://roberth-huchim-proyecto.onrender.com/api/usuario/${usuarioData.id}`, usuarioData);
      this.dialogRef.close();
      console.log(usuarioData);
    } else {
      console.log('El formulario no es válido');
    }
  }

  cancelar() {
    this.dialogRef.close();
  }
}

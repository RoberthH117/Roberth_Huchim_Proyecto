import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfigCFDIModel } from 'src/app/Models/ConfiguracionCFDI-interface';
import { ConfigCFDIService } from 'src/app/Services/config-cfdi.service';

@Component({
  selector: 'app-modal-edit-config-cfdi',
  templateUrl: './modal-edit-config-cfdi.component.html',
  styleUrls: ['./modal-edit-config-cfdi.component.css']
})
export class ModalEditConfigCFDIComponent implements OnInit {
  configCFDI: ConfigCFDIModel = {
    id: 0,
    nameFileCer: '',
    nameFileKey: '',
    lastModifiedOn: new Date(),
    cuenta: '',
    filePasswordKey: '',
    passwordUsuario: '',
    usuario: ''
  };
  configForm: FormGroup;
  files: ConfigCFDIModel[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: ConfigCFDIModel,
    private httpClient: HttpClient,
    private configcfdiService: ConfigCFDIService,
    private _formBuilder: FormBuilder,
    private configCFDIService: ConfigCFDIService,
    private _snackBar: MatSnackBar,
  ) {
    this.configCFDI.id = data.id;
    this.configForm = this._formBuilder.group({
      cuenta: ['', [Validators.required]],
      usuario: ['', [Validators.required]],
      passwordUsuario: ['', [Validators.required]],
      lastModifiedOn: [null],
      fileCer: [null],
      fileKey: [null],
      filePasswordKey: [''],
      nameFileCer: [''],
      nameFileKey: [''],
       
    })
  }

  ngOnInit(): void {
    this.configcfdiService.getConfigCFDI(`https://localhost:7141/api/ConfiguracionCDFI/${this.configCFDI.id}`)
      .subscribe((response: any) => {
        this.handleFileResponse(response);
         
      });
  }

  private handleFileResponse(response: any): void {
    const fileCerBlob = this.base64ToBlob(response.fileCer, 'application/x-x509-ca-cert',response.nameFileCer);
    const fileKeyBlob = this.base64ToBlob(response.fileKey, 'application/octet-stream', response.nameFileKey);
  
    // Ahora puedes usar fileCerBlob y fileKeyBlob según tus necesidades
    // Por ejemplo, podrías asignarlos a propiedades de tu componente o servicio.
    this.configForm.patchValue(response);
    this.fileCer.push(fileCerBlob);
    this.fileKey.push(fileKeyBlob);
  }
  private base64ToBlob(base64: string, mimeType: string, fileName: string): Blob | null {
    try {
      // Obtener la parte después de la coma
      const base64Part = base64.split(',')[1];
      
      // Intentar decodificar la cadena Base64
      const decodedBase64 = atob(base64Part);
    
      // Resto del código para convertir en Blob
      const byteCharacters = decodedBase64;
      const byteNumbers = new Array(byteCharacters.length);
    
      for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
      }
    
      const byteArray = new Uint8Array(byteNumbers);
    
      // Crear un Blob y asignar el nombre
      const blob = new Blob([byteArray], { type: mimeType });
      (blob as any).name = fileName;
    
      return blob;
    } catch (error) {
      console.error('Error al decodificar base64:', error);
      return null;
    }
  }
  
  
  

   

  onSubmit() {
    
      this.configForm.patchValue({
        lastModifiedOn: new Date(),
      });

      this.configCFDIService.EditarConfigcfdi(`https://localhost:7141/api/ConfiguracionCDFI/${this,this.configCFDI.id}`, this.configForm.value);
      console.log(this.configForm.value)
  }

  fileCer: any[] = [];
  fileKey: any[] = [];

 

  convertToBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onloadend = () => resolve(reader.result as string);
      reader.onerror = reject;
      reader.readAsDataURL(file);
    });
  }

  onSelectCert(event: any) {
    if (this.fileCer.length < 1) {
      const file = event.addedFiles[0];
      this.convertToBase64(file).then(base64Data => {
        console.log('Certificado en base64:', base64Data);
        this.configForm.patchValue({ fileCer: base64Data, nameFileCer: file.name });
      }).catch(error => {
        console.error('Error al convertir a base64:', error);
      });
      this.fileCer.push(file);
      this._snackBar.open('Archivo agregado.', '');
    } else {
      this._snackBar.open('Ya agregó un archivo.', '', {
        panelClass: ['redNoMatch'],
      });
    }
  }

  onRemoveCert(event: any) {
    this.fileCer.splice(this.fileCer.indexOf(event), 1);
    this._snackBar.open('Archivo eliminado.', '');
  }

  onSelectKey(event: any) {
   
    if (this.fileKey.length < 1) {
      const file = event.addedFiles[0];
      this.convertToBase64(file).then(base64Data => {
        console.log('Llave en base64:', base64Data);
        this.configForm.patchValue({ fileKey: base64Data, nameFileKey: file.name });
      }).catch(error => {
        console.error('Error al convertir a base64:', error);
      });
      this.fileKey.push(file);
      this._snackBar.open('Archivo agregado.', '');
    } else {
      this._snackBar.open('Ya agregó un archivo.', '', {
        panelClass: ['redNoMatch'],
      });
    }
  }

  onRemoveKey(event: any) {
    this.fileKey.splice(this.fileKey.indexOf(event), 1);
    this._snackBar.open('Archivo eliminado.', '');
  }
}
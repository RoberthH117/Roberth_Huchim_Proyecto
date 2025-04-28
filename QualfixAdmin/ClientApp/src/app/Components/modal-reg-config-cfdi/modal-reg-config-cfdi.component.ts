import { EventEmitter, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfigCFDIModel } from 'src/app/Models/ConfiguracionCFDI-interface';
import { ConfigCFDIService } from 'src/app/Services/config-cfdi.service';

@Component({
  selector: 'app-modal-reg-config-cfdi',
  templateUrl: './modal-reg-config-cfdi.component.html',
  styleUrls: ['./modal-reg-config-cfdi.component.css']
})
export class ModalRegConfigCFDIComponent implements OnInit {

  configForm: FormGroup;
  files: ConfigCFDIModel[]=[];
 

  constructor(private _formBuilder: FormBuilder, private configCFDIService: ConfigCFDIService, private _snackBar: MatSnackBar,) {
    this.configForm = this._formBuilder.group({
      cuenta: ['', [Validators.required]],
      usuario: ['', [Validators.required]],
      passwordUsuario: ['', [Validators.required]],
      lastModifiedOn:[null],
      fileCer:[null],
      fileKey:[null],
      nameFileCer:[''],
      nameFileKey:[''],
      filePasswordKey:[''],
      
       
    })
 
  }
  ngOnInit(): void {

  }

  onSubmit() {
    console.log(this.configForm.value)
    if(this.configForm.valid){
      this.configForm.patchValue({
        lastModifiedOn: new Date(),
       

      });
       
      this.configCFDIService.CrearConfigcfdi(`https://localhost:7141/api/ConfiguracionCDFI`,this.configForm.value);
    }
  }

  fileCer: File[] = [];
  fileKey: File[] = [];

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
        this.configForm.patchValue({ fileCer: base64Data,nameFileCer:file.name });
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
        this.configForm.patchValue({ fileKey: base64Data, nameFileKey:file.name });
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


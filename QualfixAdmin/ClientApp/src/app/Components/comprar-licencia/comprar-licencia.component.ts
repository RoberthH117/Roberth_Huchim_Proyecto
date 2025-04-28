import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { LicenciaModel } from 'src/app/Modelos/Licencia.interface';
import { TipoLicenciaI } from 'src/app/Modelos/TipoLicencia.interface';
import { LicenciaService } from 'src/app/service/licencia.service';
import { TipoLicenciaService } from 'src/app/service/tipo-licencia.service';

declare let paypal: any;

@Component({
  selector: 'app-comprar-licencia',
  templateUrl: './comprar-licencia.component.html',
  styleUrls: ['./comprar-licencia.component.css']
})
export class ComprarLicenciaComponent implements OnInit {
  Licencias: TipoLicenciaI[] = [];
  request: LicenciaModel[] = [];
  modelo: LicenciaModel|undefined;
EstadoLicencia:boolean=false;
  constructor(private el: ElementRef, private renderer: Renderer2, private licenciaService:TipoLicenciaService, private asignarLicencia:LicenciaService) {}

  ngOnInit() {


this.asignarLicencia.getEstadoLicenciaBool().subscribe(data=>{
data=this.EstadoLicencia;

});







    this.ObtenerLicencias().then(() => {
      // Crea el elemento script y carga el SDK de PayPal
      const script = this.renderer.createElement('script');
      script.src = 'https://www.paypal.com/sdk/js?client-id=AdLKDtk1JyeR8YWUXlP-hHhg8NrKkkA4raif2zp9fcIhlH2qYYrYp8tVbBqRk1Z6Fz0Bl7roUM_FUqK_&currency=MXN';
      this.renderer.appendChild(this.el.nativeElement.ownerDocument.body, script);
  
      script.onload = () => {
        if (typeof paypal !== 'undefined') {
          console.log('PayPal SDK cargado correctamente');
          // Renderiza el botón de PayPal para cada licencia
          this.Licencias.forEach(licencia => {
            paypal.Buttons({
              // ... Configuración del botón PayPal
              createOrder: (data: any, actions: any) => {
               
                this.modelo= {
                  
                  TipoId: licencia.tipoLicenciaId,
                  Id_Usuario:''
                };
                
               
                return actions.order.create({
                  purchase_units: [
                    {
                      amount: {
                        value: licencia.costo.toString()
                      },
                      
                    }
                  ]
                });
              },
              onApprove: (data: any, actions: any) => {
                // Aquí van las instrucciones que deseas ejecutar si el pago es aprobado
                
        this.asignarLicencia.generarLicencia(this.modelo).subscribe();
              }
            }).render('#paypal-button-container-' + licencia.tipoLicenciaId);
          });
        }
      };
    });
  }
  
  ObtenerLicencias(): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      this.licenciaService.GetAllTipoLicencia().subscribe((data) => {
        this.Licencias = data;
        console.log(this.Licencias[0].tipoLicenciaId);
        resolve();
      }, error => {
        reject(error);
      });
    });
  }


}
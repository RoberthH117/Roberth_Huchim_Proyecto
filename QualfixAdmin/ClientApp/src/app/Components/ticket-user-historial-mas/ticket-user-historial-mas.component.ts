import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ImagenModelo, Reporte } from 'src/app/Modelos/Ticket.interface';
import { TicketService } from 'src/app/service/ticket.service';

@Component({
  selector: 'app-ticket-user-historial-mas',
  templateUrl: './ticket-user-historial-mas.component.html',
  styleUrls: ['./ticket-user-historial-mas.component.css']
})
export class TicketUserHistorialMasComponent {


  descripcion: any;
  estado: any;
  fechaSolicitud: any;


  public tickets: Reporte[] = [];
public imagenes: ImagenModelo[]=[];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private ticketService: TicketService
  ) { }


  ngOnInit(): void {
    debugger
     const id = this.data.id;
     const idString = id.toString();
    // Realiza la consulta a la API utilizando el ID
    this.ticketService.getIdTicket(idString).subscribe((ticket) => {
    //   // Aquí puedes almacenar los datos del ticket en variables y utilizarlos en la plantilla HTML
    //   // Por ejemplo:
    //   // this.descripcion = ticket.descripcion;
    //   // this.estado = ticket.estado;
    //   // this.fechaSolicitud = ticket.fecha_Solicitud;
    //   // this.imagenes = ticket.imagenes;
    debugger
      this.tickets=ticket;
      this.imagenes = ticket[0].imagenes;

      




     

    });


   
  }
  getBase64Image(base64String: string): string {
    return 'data:image/jpeg;base64,' + base64String;
  }


 




}

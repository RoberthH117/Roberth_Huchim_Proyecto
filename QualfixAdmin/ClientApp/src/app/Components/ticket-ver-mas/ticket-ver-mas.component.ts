import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ImagenModelo, Reporte, ReporteModelo } from 'src/app/Modelos/Ticket.interface';
import { TicketService } from 'src/app/service/ticket.service';

@Component({
  selector: 'app-ticket-ver-mas',
  templateUrl: './ticket-ver-mas.component.html',
  styleUrls: ['./ticket-ver-mas.component.css']
})
export class TicketVerMasComponent {
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

      

      
      console.log(this.tickets[0].imagenes);
      console.log(this.imagenes[0].ruta);


     

    });


   
  }
  getBase64Image(base64String: string): string {
    return 'data:image/jpeg;base64,' + base64String;
  }


  enviarTicket = () => {
    const id = this.tickets.length > 0 ? this.tickets[0].id : '';
    console.log(this.estado);
    const idNumber = Number(id);
    // Aquí puedes llamar a tu nuevo método y pasar el ID y el estado seleccionado como parámetros
    // this.nuevoMetodo(id, this.estado);

this.ticketService.UpdateTicket(idNumber,this.estado).subscribe();

  }




}

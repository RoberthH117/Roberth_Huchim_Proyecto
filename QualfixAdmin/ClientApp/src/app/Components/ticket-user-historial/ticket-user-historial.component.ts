import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Reporte, ReporteModelo } from 'src/app/Modelos/Ticket.interface';
import { TicketService } from 'src/app/service/ticket.service';
import { TicketVerMasComponent } from '../ticket-ver-mas/ticket-ver-mas.component';
import { TicketUserHistorialMasComponent } from '../ticket-user-historial-mas/ticket-user-historial-mas.component';
import { RevisionTicketComponent } from '../revision-ticket/revision-ticket.component';
import { TicketRegistroComponent } from '../ticket-registro/ticket-registro.component';

@Component({
  selector: 'app-ticket-user-historial',
  templateUrl: './ticket-user-historial.component.html',
  styleUrls: ['./ticket-user-historial.component.css']
})
export class TicketUserHistorialComponent {


  [x: string]: any;


  public tickets: Reporte[] = [];
  
    constructor(private http: HttpClient, private ticketService:TicketService,private dialog: MatDialog) {
  
      this.tickets = [];
     }
  
  
  
    ngOnInit(): void {
      // Llama al método para obtener los tickets al inicializar el componente
      this.getTickets();
    }
  
    agregarElemento(){
      const DialogRef = this.dialog.open(TicketRegistroComponent);
    }
    editarElemento(ticket: number) {
      debugger
      const dialog = this.dialog.open(TicketUserHistorialMasComponent, {
       
       data: { id: ticket}
     });
    }
  
  
  
    getTickets() {
      debugger
      this.ticketService.getTicketUser().subscribe(
        (response) => {
          debugger;
          console.log(response);
          if (response) {
            this.tickets = response;
          } else {
            console.error('No se encontró el campo "result" en la respuesta');
          }
        },
        (error) => {
          console.error(error);
        }
      );
    }

}

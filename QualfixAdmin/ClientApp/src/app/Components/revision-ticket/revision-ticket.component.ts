import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ReporteModelo,Reporte } from 'src/app/Modelos/Ticket.interface';
import { TicketService } from 'src/app/service/ticket.service';
import { TicketVerMasComponent } from '../ticket-ver-mas/ticket-ver-mas.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-revision-ticket',
  templateUrl: './revision-ticket.component.html',
  styleUrls: ['./revision-ticket.component.css']
})
export class RevisionTicketComponent {
  [x: string]: any;


public tickets: ReporteModelo[] = [];

  constructor(private http: HttpClient, private ticketService:TicketService,private dialog: MatDialog) {

    this.tickets = [];
   }



  ngOnInit(): void {
    // Llama al método para obtener los tickets al inicializar el componente
    this.getTickets();
  }


  editarElemento(ticket: number) {
    debugger
    const dialog = this.dialog.open(TicketVerMasComponent, {
     
     data: { id: ticket}
   });
  }



  getTickets() {
    this.ticketService.getTicketsPendientes().subscribe(
      (response) => {
        debugger;
        if (response.result) {
          this.tickets = response.result;
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






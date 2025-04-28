import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketUserHistorialMasComponent } from './ticket-user-historial-mas.component';

describe('TicketUserHistorialMasComponent', () => {
  let component: TicketUserHistorialMasComponent;
  let fixture: ComponentFixture<TicketUserHistorialMasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TicketUserHistorialMasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketUserHistorialMasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

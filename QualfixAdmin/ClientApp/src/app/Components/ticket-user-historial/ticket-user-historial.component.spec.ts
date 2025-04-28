import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketUserHistorialComponent } from './ticket-user-historial.component';

describe('TicketUserHistorialComponent', () => {
  let component: TicketUserHistorialComponent;
  let fixture: ComponentFixture<TicketUserHistorialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TicketUserHistorialComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketUserHistorialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

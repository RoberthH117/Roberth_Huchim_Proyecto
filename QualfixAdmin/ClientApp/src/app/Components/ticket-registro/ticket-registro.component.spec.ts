import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketRegistroComponent } from './ticket-registro.component';

describe('TicketRegistroComponent', () => {
  let component: TicketRegistroComponent;
  let fixture: ComponentFixture<TicketRegistroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TicketRegistroComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketRegistroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketVerMasComponent } from './ticket-ver-mas.component';

describe('TicketVerMasComponent', () => {
  let component: TicketVerMasComponent;
  let fixture: ComponentFixture<TicketVerMasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TicketVerMasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketVerMasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

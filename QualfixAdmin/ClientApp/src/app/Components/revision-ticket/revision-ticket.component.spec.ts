import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevisionTicketComponent } from './revision-ticket.component';

describe('RevisionTicketComponent', () => {
  let component: RevisionTicketComponent;
  let fixture: ComponentFixture<RevisionTicketComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevisionTicketComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RevisionTicketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

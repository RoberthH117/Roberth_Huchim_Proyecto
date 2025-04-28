import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalRegClienteComponent } from './modal-reg-cliente.component';

describe('ModalRegClienteComponent', () => {
  let component: ModalRegClienteComponent;
  let fixture: ComponentFixture<ModalRegClienteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalRegClienteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalRegClienteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

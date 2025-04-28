import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEliminarClienteComponent } from './modal-eliminar-cliente.component';

describe('ModalEliminarClienteComponent', () => {
  let component: ModalEliminarClienteComponent;
  let fixture: ComponentFixture<ModalEliminarClienteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalEliminarClienteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalEliminarClienteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

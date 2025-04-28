import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEliminarUsuariosComponent } from './modal-eliminar-usuarios.component';

describe('ModalEliminarUsuariosComponent', () => {
  let component: ModalEliminarUsuariosComponent;
  let fixture: ComponentFixture<ModalEliminarUsuariosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalEliminarUsuariosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalEliminarUsuariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEliminarProductosComponent } from './modal-eliminar-productos.component';

describe('ModalEliminarProductosComponent', () => {
  let component: ModalEliminarProductosComponent;
  let fixture: ComponentFixture<ModalEliminarProductosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalEliminarProductosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalEliminarProductosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

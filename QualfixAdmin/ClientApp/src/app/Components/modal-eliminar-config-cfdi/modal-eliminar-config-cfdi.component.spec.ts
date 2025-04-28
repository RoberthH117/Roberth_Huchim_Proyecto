import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEliminarConfigCFDIComponent } from './modal-eliminar-config-cfdi.component';

describe('ModalEliminarConfigCFDIComponent', () => {
  let component: ModalEliminarConfigCFDIComponent;
  let fixture: ComponentFixture<ModalEliminarConfigCFDIComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalEliminarConfigCFDIComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalEliminarConfigCFDIComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

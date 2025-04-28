import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEditProductosComponent } from './modal-edit-productos.component';

describe('ModalEditProductosComponent', () => {
  let component: ModalEditProductosComponent;
  let fixture: ComponentFixture<ModalEditProductosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalEditProductosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalEditProductosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

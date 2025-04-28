import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalRegProductosComponent } from './modal-reg-productos.component';

describe('ModalRegProductosComponent', () => {
  let component: ModalRegProductosComponent;
  let fixture: ComponentFixture<ModalRegProductosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalRegProductosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalRegProductosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

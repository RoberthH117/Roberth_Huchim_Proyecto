import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalRegUsuariosComponent } from './modal-reg-usuarios.component';

describe('ModalRegUsuariosComponent', () => {
  let component: ModalRegUsuariosComponent;
  let fixture: ComponentFixture<ModalRegUsuariosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalRegUsuariosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalRegUsuariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

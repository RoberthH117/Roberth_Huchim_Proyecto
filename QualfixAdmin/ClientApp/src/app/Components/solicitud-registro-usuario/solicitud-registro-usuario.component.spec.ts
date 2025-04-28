import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitudRegistroUsuarioComponent } from './solicitud-registro-usuario.component';

describe('SolicitudRegistroUsuarioComponent', () => {
  let component: SolicitudRegistroUsuarioComponent;
  let fixture: ComponentFixture<SolicitudRegistroUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolicitudRegistroUsuarioComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SolicitudRegistroUsuarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

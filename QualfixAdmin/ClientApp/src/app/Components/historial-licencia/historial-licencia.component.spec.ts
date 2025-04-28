import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistorialLicenciaComponent } from './historial-licencia.component';

describe('HistorialLicenciaComponent', () => {
  let component: HistorialLicenciaComponent;
  let fixture: ComponentFixture<HistorialLicenciaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HistorialLicenciaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HistorialLicenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

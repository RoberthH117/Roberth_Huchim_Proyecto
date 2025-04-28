import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignarLicenciaComponent } from './asignar-licencia.component';

describe('AsignarLicenciaComponent', () => {
  let component: AsignarLicenciaComponent;
  let fixture: ComponentFixture<AsignarLicenciaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AsignarLicenciaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AsignarLicenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

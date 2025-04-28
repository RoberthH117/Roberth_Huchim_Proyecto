import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoLicenciaComponent } from './tipo-licencia.component';

describe('TipoLicenciaComponent', () => {
  let component: TipoLicenciaComponent;
  let fixture: ComponentFixture<TipoLicenciaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TipoLicenciaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TipoLicenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

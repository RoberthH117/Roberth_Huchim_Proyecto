import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComprarLicenciaComponent } from './comprar-licencia.component';

describe('ComprarLicenciaComponent', () => {
  let component: ComprarLicenciaComponent;
  let fixture: ComponentFixture<ComprarLicenciaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ComprarLicenciaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComprarLicenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

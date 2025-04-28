import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModaleditartipolicenciaComponent } from './modaleditartipolicencia.component';

describe('ModaleditartipolicenciaComponent', () => {
  let component: ModaleditartipolicenciaComponent;
  let fixture: ComponentFixture<ModaleditartipolicenciaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModaleditartipolicenciaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModaleditartipolicenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

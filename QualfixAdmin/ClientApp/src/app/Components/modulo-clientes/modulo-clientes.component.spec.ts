import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuloClientesComponent } from './modulo-clientes.component';

describe('ModuloClientesComponent', () => {
  let component: ModuloClientesComponent;
  let fixture: ComponentFixture<ModuloClientesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModuloClientesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModuloClientesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

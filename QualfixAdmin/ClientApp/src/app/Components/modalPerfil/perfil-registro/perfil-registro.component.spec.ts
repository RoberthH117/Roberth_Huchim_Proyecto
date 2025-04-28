import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PerfilRegistroComponent } from './perfil-registro.component';

describe('PerfilRegistroComponent', () => {
  let component: PerfilRegistroComponent;
  let fixture: ComponentFixture<PerfilRegistroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PerfilRegistroComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PerfilRegistroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

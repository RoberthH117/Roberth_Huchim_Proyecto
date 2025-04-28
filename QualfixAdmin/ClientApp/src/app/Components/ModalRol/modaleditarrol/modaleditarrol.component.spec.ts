import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModaleditarrolComponent } from './modaleditarrol.component';

describe('ModaleditarrolComponent', () => {
  let component: ModaleditarrolComponent;
  let fixture: ComponentFixture<ModaleditarrolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModaleditarrolComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModaleditarrolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalregistrotipolicenciaComponent } from './modalregistrotipolicencia.component';

describe('ModalregistrotipolicenciaComponent', () => {
  let component: ModalregistrotipolicenciaComponent;
  let fixture: ComponentFixture<ModalregistrotipolicenciaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalregistrotipolicenciaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalregistrotipolicenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

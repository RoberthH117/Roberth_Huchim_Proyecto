import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalRegConfigCFDIComponent } from './modal-reg-config-cfdi.component';

describe('ModalRegConfigCFDIComponent', () => {
  let component: ModalRegConfigCFDIComponent;
  let fixture: ComponentFixture<ModalRegConfigCFDIComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalRegConfigCFDIComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalRegConfigCFDIComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEditConfigCFDIComponent } from './modal-edit-config-cfdi.component';

describe('ModalEditConfigCFDIComponent', () => {
  let component: ModalEditConfigCFDIComponent;
  let fixture: ComponentFixture<ModalEditConfigCFDIComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalEditConfigCFDIComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalEditConfigCFDIComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

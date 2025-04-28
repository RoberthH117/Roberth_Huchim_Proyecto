import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalregistrorolComponent } from './modalregistrorol.component';

describe('ModalregistrorolComponent', () => {
  let component: ModalregistrorolComponent;
  let fixture: ComponentFixture<ModalregistrorolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalregistrorolComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalregistrorolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

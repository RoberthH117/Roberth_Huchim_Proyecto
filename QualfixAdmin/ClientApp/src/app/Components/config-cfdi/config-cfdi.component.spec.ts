import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigCFDIComponent } from './config-cfdi.component';

describe('ConfigCFDIComponent', () => {
  let component: ConfigCFDIComponent;
  let fixture: ComponentFixture<ConfigCFDIComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigCFDIComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfigCFDIComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

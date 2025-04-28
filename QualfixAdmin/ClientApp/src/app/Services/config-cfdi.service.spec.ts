import { TestBed } from '@angular/core/testing';

import { ConfigCFDIService } from './config-cfdi.service';

describe('ConfigCFDIService', () => {
  let service: ConfigCFDIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConfigCFDIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

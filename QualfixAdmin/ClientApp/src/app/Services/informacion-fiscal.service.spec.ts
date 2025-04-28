import { TestBed } from '@angular/core/testing';

import { InformacionFiscalService } from './informacion-fiscal.service';

describe('InformacionFiscalService', () => {
  let service: InformacionFiscalService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InformacionFiscalService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

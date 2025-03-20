import { TestBed } from '@angular/core/testing';

import { BudgetCodeService } from './budget-code.service';

describe('BudgetCodeService', () => {
  let service: BudgetCodeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BudgetCodeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetCodeComponent } from './budget-code.component';

describe('BudgetCodeComponent', () => {
  let component: BudgetCodeComponent;
  let fixture: ComponentFixture<BudgetCodeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BudgetCodeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BudgetCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

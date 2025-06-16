import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferUnderEvaluationDialogComponent } from './offer-under-evaluation-dialog.component';

describe('OfferUnderEvaluationDialogComponent', () => {
  let component: OfferUnderEvaluationDialogComponent;
  let fixture: ComponentFixture<OfferUnderEvaluationDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OfferUnderEvaluationDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OfferUnderEvaluationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

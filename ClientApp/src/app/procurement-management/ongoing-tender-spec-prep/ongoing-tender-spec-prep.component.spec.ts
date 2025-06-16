import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OngoingTenderSpecPrepComponent } from './ongoing-tender-spec-prep.component';

describe('OngoingTenderSpecPrepComponent', () => {
  let component: OngoingTenderSpecPrepComponent;
  let fixture: ComponentFixture<OngoingTenderSpecPrepComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OngoingTenderSpecPrepComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OngoingTenderSpecPrepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

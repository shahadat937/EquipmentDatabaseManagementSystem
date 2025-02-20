import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportingYearComponent } from './reporting-year.component';

describe('ReportingYearComponent', () => {
  let component: ReportingYearComponent;
  let fixture: ComponentFixture<ReportingYearComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportingYearComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportingYearComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

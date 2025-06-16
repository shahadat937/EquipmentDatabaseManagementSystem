import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AipPendingViewDialogComponent } from './aip-pending-view-dialog.component';

describe('AipPendingViewDialogComponent', () => {
  let component: AipPendingViewDialogComponent;
  let fixture: ComponentFixture<AipPendingViewDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AipPendingViewDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AipPendingViewDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

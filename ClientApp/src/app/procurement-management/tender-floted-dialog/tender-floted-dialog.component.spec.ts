import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenderFlotedDialogComponent } from './tender-floted-dialog.component';

describe('TenderFlotedDialogComponent', () => {
  let component: TenderFlotedDialogComponent;
  let fixture: ComponentFixture<TenderFlotedDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TenderFlotedDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TenderFlotedDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

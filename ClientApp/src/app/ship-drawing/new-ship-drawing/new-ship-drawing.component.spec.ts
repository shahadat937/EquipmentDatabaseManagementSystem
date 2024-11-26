import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewShipDrawingComponent } from './new-ship-drawing.component';

describe('NewShipDrawingComponent', () => {
  let component: NewShipDrawingComponent;
  let fixture: ComponentFixture<NewShipDrawingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewShipDrawingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewShipDrawingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

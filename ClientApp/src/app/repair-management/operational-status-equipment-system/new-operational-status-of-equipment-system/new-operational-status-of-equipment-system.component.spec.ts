import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewOperationalStatusOfEquipmentSystemComponent } from './new-operational-status-of-equipment-system.component';

describe('NewOperationalStatusOfEquipmentSystemComponent', () => {
  let component: NewOperationalStatusOfEquipmentSystemComponent;
  let fixture: ComponentFixture<NewOperationalStatusOfEquipmentSystemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewOperationalStatusOfEquipmentSystemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewOperationalStatusOfEquipmentSystemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

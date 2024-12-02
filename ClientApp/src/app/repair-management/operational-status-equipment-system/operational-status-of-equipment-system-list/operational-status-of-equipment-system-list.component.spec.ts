import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationalStatusOfEquipmentSystemListComponent } from './operational-status-of-equipment-system-list.component';

describe('OperationalStatusOfEquipmentSystemListComponent', () => {
  let component: OperationalStatusOfEquipmentSystemListComponent;
  let fixture: ComponentFixture<OperationalStatusOfEquipmentSystemListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OperationalStatusOfEquipmentSystemListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OperationalStatusOfEquipmentSystemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

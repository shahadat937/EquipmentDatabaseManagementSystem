import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OplNonoplEquipmentComponent } from './opl-nonopl-equipment.component';

describe('OplNonoplEquipmentComponent', () => {
  let component: OplNonoplEquipmentComponent;
  let fixture: ComponentFixture<OplNonoplEquipmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OplNonoplEquipmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OplNonoplEquipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

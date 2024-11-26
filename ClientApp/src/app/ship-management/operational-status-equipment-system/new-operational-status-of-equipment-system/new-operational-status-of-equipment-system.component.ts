import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from 'src/app/core/models/role';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { AuthService } from 'src/app/core/service/auth.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { SharedService } from 'src/app/shared/shared.service';
import { OperationalStatusOfEquipmentSystemService } from 'src/app/ship-management/service/operational-status-of-equipment-system.service'
import { OperationalStatusOfEquipmentSystem } from 'src/app/ship-management/models/OperationalStatusOfEquipment';

@Component({
  selector: 'app-new-operational-status-of-equipment-system',
  templateUrl: './new-operational-status-of-equipment-system.component.html',
  styleUrls: ['./new-operational-status-of-equipment-system.component.sass']
})
export class NewOperationalStatusOfEquipmentSystemComponent implements OnInit {

  pageTitle: string;
  userRole = Role;
  destination: string;
  btnText: string;
  OperationalStatusOfEquipmentForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel: SelectedModel[];
  selectedBaseName: SelectedModel[];
  selectBaseName: SelectedModel[];
  selectedBranchLevel: SelectedModel[];
  selectedBaseSchoolName: SelectedModel[];
  selectSchoolName: SelectedModel[];

  selectBns: SelectedModel[];
  selectedSqn: SelectedModel[];
  selectedStateOfEquipment: SelectedModel[];
  selectSqn: SelectedModel[];
  selectedOperationalStatus: SelectedModel[];
  selectedShipType: SelectedModel[];
  selectShipType: SelectedModel[];
  selectedEquipmentName: SelectedModel[];
  selectEquipName: SelectedModel[];
  traineeId: any;
  role: any;
  branchId: any;
  organizationId: any;
  selectedCommendingArea: any[];
  selectCommandingArea: SelectedModel[];
  commendingAreaId: any;
  shipImage: any;
  isImage: boolean;
  isFile: boolean;

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private confirmService: ConfirmService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute, private sharedService: SharedService, private OperationalStatusOfEquipmentSystemService: OperationalStatusOfEquipmentSystemService) { }

  ngOnInit(): void {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId = this.authService.currentUserValue.traineeId.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    const id = this.route.snapshot.paramMap.get('operationalStatusOfEquipmentSystemId');
    if (id) {
      console.log(id);
      this.pageTitle = 'Edit Overall Operational Status Of Equipment System';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.OperationalStatusOfEquipmentSystemService.find(+id).subscribe(
        res => {
          console.log(res);
          this.OperationalStatusOfEquipmentForm.patchValue({

            operationalStatusOfEquipmentSystemId: res.operationalStatusOfEquipmentSystemId,
            nameOfEquipmentId: res.nameOfEquipmentId,
            nameOfEquipment: res.nameOfEquipment,
            stateOfEquipmentId: res.stateOfEquipmentId,
            stateOfEquipment: res.stateOfEquipment,
            dateOfDefect: res.dateOfDefect,
            durationOfDefect: res.durationOfDefect,
            baseSchoolNameId: res.baseSchoolNameId,
            baseSchoolName: res.baseSchoolName,
            causesOfDefect: res.causesOfDefect,
            stepsTakenByShipsStaff: res.stepsTakenByShipsStaff,
            stepsTakenByBnDockyard: res.stepsTakenByBnDockyard,
            stepsTakenByNHQ: res.stepsTakenByNHQ,
            stepsTakenByOEM: res.stepsTakenByOEM,
            patternNo: res.patternNo,
            isSparePartsHeldInShip: res.isSparePartsHeldInShip,
            procurementStatusOfSpares: res.procurementStatusOfSpares,
            impactOnOtherSystem: res.impactOnOtherSystem,
            requirementOfShip: res.requirementOfShip,
            remarks: res.remarks,
            descriptionOfDefect: res.descriptionOfDefect,
            isActive: res.isActive,
          });
        }
      );
    } else {
      this.pageTitle = 'Create Overall Operational Status Of Equipment System';
      this.destination = "Add";
      this.btnText = 'Save';
    }

    this.intitializeForm();
    this.getSelectedShipName();
    this.getSelectedStateOfEquipment();
    this.getSelectedEquipmentName();
  }

  intitializeForm() {
    this.OperationalStatusOfEquipmentForm = this.fb.group({
      operationalStatusOfEquipmentSystemId: [0],
      nameOfEquipmentId: [''],
      nameOfEquipment: ['',],
      stateOfEquipmentId: ['',],
      stateOfEquipment: ['',],
      dateOfDefect: ['',],
      durationOfDefect: ['',],
      baseSchoolNameId: ['',],
      baseSchoolName: ['',],
      causesOfDefect: ['',],
      stepsTakenByShipsStaff: ['',],
      stepsTakenByBnDockyard: ['',],
      stepsTakenByNHQ: ['',],
      stepsTakenByOEM: ['',],
      patternNo: ['',],
      isSparePartsHeldInShip: [false],
      procurementStatusOfSpares: ['',],
      impactOnOtherSystem: ['',],
      requirementOfShip: ['',],
      remarks: [''],
      descriptionOfDefect: ['',],
      isActive: [true]
    })
  }

  filterByShipName(value: any) {
    this.selectedBaseSchoolName = this.selectSchoolName.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  filterByEquipmentName(value: any) {
    this.selectedEquipmentName = this.selectedEquipmentName.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }

  getSelectedShipName() {
    //var baseNameId = this.ShipEquipmentInfoForm.value['baseNameId'];
    this.OperationalStatusOfEquipmentSystemService.getSelectedShipName().subscribe(res => {
      this.selectedBaseSchoolName = res;
      this.selectSchoolName = res;

    });
  }

  getSelectedEquipmentName() {
    this.OperationalStatusOfEquipmentSystemService.getSelectedEquipmentName().subscribe(res => {
      this.selectedEquipmentName = res;
      this.selectEquipName = res;

    });
  }

  getSelectedStateOfEquipment() {
    this.OperationalStatusOfEquipmentSystemService.getSelectedStateOfEquipment().subscribe(res => {
      this.selectedStateOfEquipment = res;
    });
  }

  onSubmit() {
    const id = this.OperationalStatusOfEquipmentForm.get('operationalStatusOfEquipmentSystemId').value;
    console.log("Id",id);

    console.log(this.OperationalStatusOfEquipmentForm);

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {

        if (result) {
          this.OperationalStatusOfEquipmentSystemService.update(+id,this.OperationalStatusOfEquipmentForm.value).subscribe(response => {
            this.router.navigateByUrl('/ship-management/operational-status-of-equipment-system-list');
            this.snackBar.open('Information Updated Successfully ', '', {
              duration: 2000,
              verticalPosition: 'bottom',
              horizontalPosition: 'right',
              panelClass: 'snackbar-success'
            });
          }, error => {
            this.validationErrors = error;
          })
        }
      })
    } else {
      console.log(this.OperationalStatusOfEquipmentForm.value);
      this.OperationalStatusOfEquipmentSystemService.submit(this.OperationalStatusOfEquipmentForm.value).subscribe(response => {
        this.router.navigateByUrl('/ship-management/operational-status-of-equipment-system-list');
        this.snackBar.open('Information Inserted Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}

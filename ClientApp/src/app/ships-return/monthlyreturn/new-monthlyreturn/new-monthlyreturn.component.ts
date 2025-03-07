import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MonthlyReturnService } from '../../service/MonthlyReturn.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { MasterData } from '../../../../../src/assets/data/master-data';
import { MonthlyReturn } from '../../models/MonthlyReturn';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { AuthService } from '../../../../../src/app/core/service/auth.service';
import { Role } from '../../../../../src/app/core/models/role';
import { BaseSchoolNameService } from '../../../../../src/app/security/service/BaseSchoolName.service';
import { SharedService } from '../../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-new-monthlyreturn',
  templateUrl: './new-monthlyreturn.component.html',
  styleUrls: ['./new-monthlyreturn.component.sass']
})
export class NewMonthlyReturnComponent implements OnInit {
  pageTitle: string;
  userRole = Role;
  destination: string;
  btnText: string;
  MonthlyReturnForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel: SelectedModel[];
  selectedOperationalStatus: SelectedModel[];
  selectedEquipmentCategory: SelectedModel[];
  selectEquipementCategory: SelectedModel[];
  masterData = MasterData;
  ELEMENT_DATA: MonthlyReturn[] = [];
  MonthlyReturnList: MonthlyReturn[];
  showHideDiv = false;
  isLoading = false;
  traineeId: any;
  role: any;
  branchId: any;
  itemCount: any = 0;
  selectedReportingMonth: SelectedModel[];
  selectReportingMonth: SelectedModel[];
  selectedEquipmentNameByCategory: SelectedModel[];
  selectequipementName: SelectedModel[];
  selectedReturnType: SelectedModel[];
  selectedBaseSchoolName: SelectedModel[];
  selectBaseSchoolName: SelectedModel[];

  selectedModelName: SelectedModel[];
  selectModelName: SelectedModel[];
  probableDefectTime: any;
  picker: any;
  totalOplCount : number = 0;
  totalNonOplCount : number = 0;
  showQty = false;
  warningMessage = ""
  isDamageDiscriptionMadetory : boolean;

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private baseSchoolNameService: BaseSchoolNameService, private confirmService: ConfirmService, private MonthlyReturnService: MonthlyReturnService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId = this.authService.currentUserValue.traineeId.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();

    const id = this.route.snapshot.paramMap.get('damageElectricalId');
    if (id) {
      this.pageTitle = 'Edit Damage Electrical/Radio Electrical Equipment';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.MonthlyReturnService.find(+id).subscribe(
        res => {

          this.MonthlyReturnForm.patchValue({
            monthlyReturnId: res.monthlyReturnId,
            authorityId: res.authorityId,
            baseNameId: res.baseNameId,
            baseSchoolNameId: res.baseSchoolNameId,
            equipmentCategoryId: res.equipmentCategoryId,
            equpmentNameId: res.equpmentNameId,
            reportingMonthId: res.reportingMonthId,
            returnTypeId: res.returnTypeId,
            operationalStatusId: res.operationalStatusId,
            damageDescription: res.damageDescription,
            presentCondition: res.presentCondition,
            reportingDate: res.reportingDate,
            timeOfDefect: res.timeOfDefect,
            probableDefectTime: res.probableDefectTime,
            uploadDocument: res.uploadDocument,
            remarks: res.remarks,
            menuPosition: res.menuPosition,
            isActive: res.isActive,
            shipEquipmentInfoId : res.shipEquipmentInfoId,
            returnQty : res.returnQty

          });
          this.onEquipmentCategorySelectionChange();
          this.OnEquipmentSelectionChange();
        }
      );
    } else {
      this.pageTitle = 'Monthly Return';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();

    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.ShipUser || this.role == this.userRole.LOEO || this.role == this.userRole.LOEOWTR) {
      this.MonthlyReturnForm.get('baseSchoolNameId')?.setValue(this.branchId);
      this.baseSchoolNameService.find(this.branchId).subscribe(res => {
        this.MonthlyReturnForm.get('baseNameId')?.setValue(res?.thirdLevel);
        this.MonthlyReturnForm.get('authorityId')?.setValue(res?.secondLevel);
      });
    }
    

    this.getSelectedEquipmentCategory();
    this.getSelectedReportingMonth();
    this.getSelectedReturnType();
    this.getSelectedOperationalStatus();
    this.getSelectedSchoolByBranchLevelAndThirdLevel();
    // this.getMonthlyReturns();
  }
  intitializeForm() {
    this.MonthlyReturnForm = this.fb.group({
      monthlyReturnId: [0],
      authorityId: [''],
      baseNameId: [''],
      baseSchoolNameId: [],
      equipmentCategoryId: [''],
      equpmentNameId: [''],
      reportingMonthId: [''],
      returnTypeId: [''],
      operationalStatusId: [''],
      damageDescription: [''],
      presentCondition: [''],
      reportingDate: [''],
      timeOfDefect: [''],
      probableDefectTime: [''],
      uploadDocument: [''],
      remarks: [''],
      menuPosition: [1],
      isActive: [true],
      doc: [''],
      shipEquipmentInfoId: [''],
      returnQty : ['']


    })
  }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];

      this.MonthlyReturnForm.patchValue({
        doc: file,
      });
    }
  }
  getSelectedSchoolByBranchLevelAndThirdLevel() {
    this.MonthlyReturnService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res => {
      this.selectedBaseSchoolName = res;
      this.selectBaseSchoolName = res;
    });
  }
  isNonOPL(event){
    //console.log("Selected Value:", event.value);

    // Find the selected item's text
    const selectedText = this.selectedOperationalStatus.find(item => item.value === event.value)?.text;
    if(selectedText === 'OPL'){
      this.isDamageDiscriptionMadetory = false
    }
    else{
      this.isDamageDiscriptionMadetory = true;
    }
  }

  filterByShip(value: any) {
    this.selectedBaseSchoolName = this.selectBaseSchoolName.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  onEquipmentCategorySelectionChange() {
    var equipmentCategoryId = this.MonthlyReturnForm.value['equipmentCategoryId'];
    this.MonthlyReturnService.getSelectedEquipmentNameByCategory(equipmentCategoryId).subscribe(res => {
      this.selectedEquipmentNameByCategory = res
      this.selectequipementName = res
    });
  }

  OnEquipmentSelectionChange() {
    var baseNameId = this.MonthlyReturnForm.value['baseSchoolNameId'] || this.branchId;
    var equpmentNameId = this.MonthlyReturnForm.value['equpmentNameId'];
    if (baseNameId && equpmentNameId) {
      this.MonthlyReturnService.getSelectedModelByShip(baseNameId, equpmentNameId).subscribe(res => {
        this.selectedModelName = res;
        this.selectModelName = res;
        
      })
    }

  }
  OnModelSelectionChange() {
    var shipEquipmentInfoId = this.MonthlyReturnForm.value['shipEquipmentInfoId'];
    if(shipEquipmentInfoId){
      this.showQty = true;
      this.findShipEquipmentInfoById(shipEquipmentInfoId);
    }

  }

  findShipEquipmentInfoById(id){
    this.MonthlyReturnService.findShipEquipmentInfoById(id).subscribe(res=>{
          this.totalOplCount = res?.oplQty || 0;
          this.totalNonOplCount = res?.nonOplQty || 0;
        
    })
  }

  filterByEquipementName(value: any) {
    this.selectedEquipmentNameByCategory = this.selectequipementName.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedEquipmentCategory() {
    this.MonthlyReturnService.getSelectedEquipmentCategory().subscribe(res => {
      this.selectedEquipmentCategory = res
      this.selectEquipementCategory = res
    });
  }
  filterByEquipementCategory(value: any) {
    this.selectedEquipmentCategory = this.selectEquipementCategory.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedReportingMonth() {
    this.MonthlyReturnService.getSelectedReportingMonth().subscribe(res => {
      this.selectedReportingMonth = res
      this.selectReportingMonth = res
    });
  }
  filterByMonth(value: any) {
    this.selectedReportingMonth = this.selectReportingMonth.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedReturnType() {
    this.MonthlyReturnService.getSelectedReturnType().subscribe(res => {
      this.selectedReturnType = res

    });
  }
  getSelectedOperationalStatus() {
    this.MonthlyReturnService.getSelectedOperationalStatus().subscribe(res => {
      this.selectedOperationalStatus = res
    });
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  deleteItem(row) {
    const id = row.monthlyReturnId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      if (result) {
        this.MonthlyReturnService.delete(id).subscribe(() => {
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    })
  }



  onSubmit() {
    const id = this.MonthlyReturnForm.get('monthlyReturnId')?.value;

    const reportingDate = this.sharedService.formatDateTime(this.MonthlyReturnForm.get('reportingDate')?.value);
    this.MonthlyReturnForm.get('reportingDate')?.setValue(reportingDate);

    const timeOfDefect = this.sharedService.formatDateTime(this.MonthlyReturnForm.get('timeOfDefect')?.value);
    this.MonthlyReturnForm.get('timeOfDefect')?.setValue(timeOfDefect);

    const formData = new FormData();
    for (const key of Object.keys(this.MonthlyReturnForm.value)) {

      const value = this.MonthlyReturnForm.value[key];
      if (value !== null && value !== undefined) {
        formData.append(key, value);
      }
    }


    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {

        if (result) {
          this.MonthlyReturnService.update(+id, formData).subscribe(response => {
            //console.log('id with form', id, formData)
            this.router.navigateByUrl('/ships-return/monthlyreturn-list');
            this.snackBar.open('Information Updated Successfully', '', {
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

      var returnQty = this.MonthlyReturnForm.value['returnQty'];
      if(this.totalOplCount< returnQty){
        this.warningMessage = "Return Qty Can't be  getter then total Opl Qty"
        return
      }

      this.MonthlyReturnService.submit(formData).subscribe(response => {
        this.router.navigateByUrl('/ships-return/monthlyreturn-list');
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

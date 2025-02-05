import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HalfYearlyReturnService } from '../../service/HalfYearlyReturn.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MasterData } from '../../../../../src/assets/data/master-data';
import { HalfYearlyReturn } from '../../models/HalfYearlyReturn';
import { AuthService } from '../../../../../src/app/core/service/auth.service';
import { ShipEquipmentInfo } from '../../../../../src/app/ship-management/models/ShipEquipmentInfo';
import { shipEquipmentInfoList } from '../../models/shipEquipmentInfoList';
import { Role } from '../../../../../src/app/core/models/role';

@Component({
  selector: 'app-new-halfyearlyreturn',
  templateUrl: './new-halfyearlyreturn.component.html',
  styleUrls: ['./new-halfyearlyreturn.component.sass']
})
export class NewHalfYearlyReturnComponent implements OnInit {
  pageTitle: string;
  userRole = Role;
  destination: string;
  btnText: string;
  HalfYearlyReturnForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel: SelectedModel[];
  selectedOperationalStatus: SelectedModel[];
  selectedEquipmentCategory: SelectedModel[];
  selectEquipmentCategory: SelectedModel[];
  selectedReportingMonth : SelectedModel [];
  selectReportingMonth : SelectedModel [];
  masterData = MasterData;
  ELEMENT_DATA: HalfYearlyReturn[] = [];
  halfYearlyReturnList: HalfYearlyReturn[];
  showHideDiv = false;
  isLoading = false;
  isShown: boolean = false;
  traineeId: any;
  role: any;
  branchId: any;
  itemCount: any = 0;
  selectedShipEquipmentInfoList: shipEquipmentInfoList[];
  selectedHalfYearlyRunningTime: SelectedModel[];
  selectedEquipmentNameByCategory: SelectedModel[];
  selectEquipmentNameByCategory: SelectedModel[];
  selectedBrand: SelectedModel[];
  selectedBaseSchoolName: SelectedModel[];
  selectBaseSchool: SelectedModel[];
  isUpdateing: boolean = false;
  shipEquipmentInformationId : number;
  dataNotFound : boolean;

  common: boolean;
  gyroCompass: boolean;
  navigationRader: boolean;
  echoSounder : boolean;
  searchRader: boolean;
  helControalRader : boolean;
  trackingRader :boolean;
  surveillanceRadar : boolean;
  trueLayingRader : boolean;
  years: number[] = [];
  currentYear = new Date().getFullYear();
  selectedYear: number = this.currentYear;

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private confirmService: ConfirmService, private HalfYearlyReturnService: HalfYearlyReturnService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId = this.authService.currentUserValue.traineeId.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();

    const id = this.route.snapshot.paramMap.get('halfYearlyReturnId');
    console.log(id);
    if (id) {
      this.isUpdateing = true;
      this.pageTitle = 'Edit Half Yearly Return';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.HalfYearlyReturnService.find(+id).subscribe(
        res => {
          console.log(res.isSatisfactory);
          this.shipEquipmentInformationId = res.shipEquipmentInfoId;
          this.HalfYearlyReturnForm.patchValue({
            halfYearlyReturnId: res.halfYearlyReturnId,
            baseSchoolNameId: res.baseSchoolNameId,
            equipmentCategoryId: res.equipmentCategoryId,
            equpmentNameId: res.equpmentNameId,
            brandId: res.brandId,
            halfYearlyRunningTime: res.halfYearlyRunningTime,
            halfYearlyProblem: res.halfYearlyProblem,
            inputPowerSupply: res.inputPowerSupply,
            totalRunningTime: res.totalRunningTime,
            powerSupplyUnit: res.powerSupplyUnit,
            shipEquipmentInfoId: res.shipEquipmentInfoId,
            remarks: res.remarks,
            isSatisfactory : res.isSatisfactory,
            menuPosition: res.menuPosition,
            isActive: res.isActive,
            reportingMonthId : res.reportingMonthId,
            year: res.year
          });
          this.onEquipmentCategorySelectionChange();
          this. onShipEquipmentInfoList();
        }
        
      );
      
    } else {
      this.pageTitle = 'Half Yearly Return';
      this.destination = "Add";
      this.btnText = 'Save';
    }

    this.intitializeForm();
    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.ShipUser || this.role == this.userRole.LOEO || this.role == this.userRole.LOEOWTR) {
      this.HalfYearlyReturnForm.get('baseSchoolNameId')?.setValue(this.branchId);
    }


    this.getSelectedEquipmentCategory();
    this.getSelectedBrand();
    this.getSelectedSchoolByBranchLevelAndThirdLevel();
    this.getSelectedReportingMonth();
    this.generateYears(1980,this.currentYear);
  }
  intitializeForm() {
    this.HalfYearlyReturnForm = this.fb.group({
      halfYearlyReturnId: [0],
      baseSchoolNameId: [''],
      equipmentCategoryId: [''],
      equpmentNameId: [''],
      brandId: [''],
      halfYearlyRunningTime: [],
      halfYearlyProblem: [],
      inputPowerSupply: [''],
      totalRunningTime: [],
      powerSupplyUnit: [''],
      remarks: [''],
      isSatisfactory : [''],
      menuPosition: [1],
      isActive: [true],
      shipEquipmentInfoId: [''],
      reportingMonthId : [''],
      year : [''],
      
      shipEquipmentInfoList: this.fb.array([
        this.createHalfYearlyReturnData()
      ]),
    })
  }



  getControlLabel(index: number, type: string) {
    return (this.HalfYearlyReturnForm.get('shipEquipmentInfoList') as FormArray)
      ?.at(index)
      ?.get(type)
      ?.value ?? '';
  }

  private createHalfYearlyReturnData() {
    return this.fb.group({
      baseSchoolNameId: [''],
      equipmentCategoryId: [''],
      equpmentNameId: [''],
      equipmentCategory: [''],
      equpmentName: [''],
      brandId: [''],
      qty: [''],
      oplQty: [''],
      nonOplQty: [''],
      brand: [''],
      powerSupply: [''],
      halfYearlyRunningTime: [],
      totalRunningTime: [],
      halfYearlyProblem: [],
      powerSupplyUnit: [''],
      model: [''],
      type: [''],
      stateOfEquipment: [''],
      remarks: [''],
      shipEquipmentInfoId :[''],
      isSatisfactory: [''],
      location : [''],
      purpose: [''],
      manufacturerNameAndAddress : [''],
      yearOfInstallation : [''],
      acquisitionMethodName: [''],
      reportingMonthId : [''],
      year : ['']



    });
  }
  clearList() {
    const control = <FormArray>this.HalfYearlyReturnForm.controls["shipEquipmentInfoList"];
    while (control.length) {
      control.removeAt(control.length - 1);
    }
    control.clearValidators();
  }

  getShipEquipmentInfoListonClick() {
    const control = <FormArray>this.HalfYearlyReturnForm.controls["shipEquipmentInfoList"];
    for (let i = 0; i < this.selectedShipEquipmentInfoList.length; i++) {
      control.push(this.createHalfYearlyReturnData());
    }
    this.HalfYearlyReturnForm.patchValue({ shipEquipmentInfoList: this.selectedShipEquipmentInfoList });
  }
  getSelectedSchoolByBranchLevelAndThirdLevel() {
    this.HalfYearlyReturnService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res => {
      this.selectedBaseSchoolName = res;
      this.selectBaseSchool = res
    });
  }
  filterByShip(value: any) {
    this.selectedBaseSchoolName = this.selectBaseSchool.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  onEquipmentCategorySelectionChange() {
    var equipmentCategoryId = this.HalfYearlyReturnForm.value['equipmentCategoryId'];
    console.log(equipmentCategoryId);
    this.HalfYearlyReturnService.getSelectedEquipmentNameByCategory(equipmentCategoryId).subscribe(res => {
      this.selectedEquipmentNameByCategory = res
      this.selectEquipmentNameByCategory = res
    });
  }
  filterByEquipementName(value: any) {
    this.selectedEquipmentNameByCategory = this.selectEquipmentNameByCategory.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  filterByMonth(value:any){
    this.selectedReportingMonth=this.selectReportingMonth.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  onShipEquipmentInfoList() {

    let shipNameId = this.HalfYearlyReturnForm.value['baseSchoolNameId'] || this.branchId;
    let equipmentCategoryId = this.HalfYearlyReturnForm.value['equipmentCategoryId'];
    let equpmentNameId = this.HalfYearlyReturnForm.value['equpmentNameId'];

    if (shipNameId && equipmentCategoryId && equpmentNameId) {

      if (equpmentNameId === this.masterData.equepmentName.GyroCompass) {
        this.allEqumentStateFalse();
        this.gyroCompass = true;
      }
      else if (equpmentNameId === this.masterData.equepmentName.NavigationRadar || equpmentNameId === this.masterData.equepmentName.SearchRadar || equpmentNameId === this.masterData.equepmentName.HelControlRadar || equpmentNameId === this.masterData.equepmentName.TrackingRadar || equpmentNameId === this.masterData.equepmentName.TrueLyingRuder || equpmentNameId === this.masterData.equepmentName.SurveillanceRadar) {
        this.allEqumentStateFalse();
        this.navigationRader = true;
      }
      else if( equpmentNameId === this.masterData.equepmentName.EchoSounder){
    
        this.allEqumentStateFalse();
        this.echoSounder = true;
      }
      else {
        this.allEqumentStateFalse()
        this.common = true;
      }

      this.isShown = true;
      this.HalfYearlyReturnService.getShipEquipmentInfoListForHalfYearly(equipmentCategoryId, equpmentNameId, shipNameId).subscribe(res => {
        console.log("Res", res)
        this.selectedShipEquipmentInfoList = res.map((item: any) => {
          return { ...item, remarks: '' };
        });
        this.clearList();
        this.getShipEquipmentInfoListonClick();
        console.log(this.selectedShipEquipmentInfoList);
      });
      
  
      
    }

  }
  getSelectedEquipmentCategory() {
    this.HalfYearlyReturnService.getSelectedEquipmentCategory().subscribe(res => {
      this.selectedEquipmentCategory = res;
      this.selectEquipmentCategory = res
    });
  }
  filterByEquipmentCategory(value: any) {
    this.selectedEquipmentCategory = this.selectEquipmentCategory.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  // getSelectedHalfYearlyRunningTime(){
  //   this.HalfYearlyReturnService.getSelectedHalfYearlyRunningTime().subscribe(res=>{
  //     this.selectedHalfYearlyRunningTime=res
  //   }); 
  // }
  getSelectedBrand() {
    this.HalfYearlyReturnService.getSelectedBrand().subscribe(res => {
      this.selectedBrand = res
    });
  }
  // getSelectedOperationalStatus(){
  //   this.HalfYearlyReturnService.getSelectedOperationalStatus().subscribe(res=>{
  //     this.selectedOperationalStatus=res
  //   }); 
  // }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  deleteItem(row) {
    const id = row.halfYearlyReturnId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      if (result) {
        this.HalfYearlyReturnService.delete(id).subscribe(() => {
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

  getSelectedReportingMonth(){
    this.HalfYearlyReturnService.getSelectedReportingMonth().subscribe(res=>{
      this.selectedReportingMonth=res
      this.selectReportingMonth=res
    }); 
  }

  allEqumentStateFalse() {
    this.gyroCompass = false
    this.navigationRader = false;
    this.echoSounder = false;

  }

  generateYears(startYear: number, endYear: number): void {
    for (let year = startYear; year <= endYear; year++) {
      this.years.push(year);
    }
  }

  onSubmit() {

    // if (this.role == this.userRole.ShipStaff || this.role != this.userRole.LOEO) {
    //   this.HalfYearlyReturnForm.get('baseSchoolNameId').setValue(this.branchId);
    // }
    const id = this.HalfYearlyReturnForm.get('halfYearlyReturnId')?.value;
    // this.MonthlyReturnForm.get('reportingDate').setValue((new Date(this.MonthlyReturnForm.get('reportingDate').value)).toUTCString());
    // this.MonthlyReturnForm.get('timeOfDefect').setValue((new Date(this.MonthlyReturnForm.get('timeOfDefect').value)).toUTCString());

    // const formData = new FormData();
    // for (const key of Object.keys(this.MonthlyReturnForm.value)) {
    //   const value = this.MonthlyReturnForm.value[key];
    //   formData.append(key, value);
    // }
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
      // this.HalfYearlyReturnForm.value);
  this.HalfYearlyReturnForm.value.shipEquipmentInfoList.shipEquipmentInfoId =this.shipEquipmentInformationId;
  // console.log("result ",this.HalfYearlyReturnForm.value.shipEquipmentInfoList.shipEquipmentInfoId )


        if (result) {
          console.log(this.HalfYearlyReturnForm.value);
          this.HalfYearlyReturnService.update(+id, this.HalfYearlyReturnForm.value).subscribe(response => {
            this.router.navigateByUrl('/ships-return/halfyearlyreturn-list');
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
    }
    else {
      console.log(this.HalfYearlyReturnForm.value.shipEquipmentInfoList);
      this.HalfYearlyReturnService.submit(this.HalfYearlyReturnForm.value.shipEquipmentInfoList
      ).subscribe(response => {
        this.router.navigateByUrl('/ships-return/halfyearlyreturn-list');
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

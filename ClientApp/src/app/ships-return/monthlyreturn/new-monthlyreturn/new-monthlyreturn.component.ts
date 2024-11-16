import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MonthlyReturnService } from '../../service/MonthlyReturn.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { MonthlyReturn } from '../../models/MonthlyReturn';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';
import { BaseSchoolNameService } from 'src/app/security/service/BaseSchoolName.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-monthlyreturn',
  templateUrl: './new-monthlyreturn.component.html',
  styleUrls: ['./new-monthlyreturn.component.sass']
})
export class NewMonthlyReturnComponent implements OnInit {
  pageTitle: string;
  userRole = Role;
  destination:string;
  btnText:string;
  MonthlyReturnForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[];
  selectedOperationalStatus:SelectedModel[];
  selectedEquipmentCategory:SelectedModel[];
  selectEquipementCategory:SelectedModel[];
  masterData = MasterData;
  ELEMENT_DATA: MonthlyReturn[] = [];
  MonthlyReturnList:MonthlyReturn[];
  showHideDiv = false;
  isLoading = false;
  traineeId:any;
  role:any;
  branchId:any;
  itemCount:any =0;
  selectedReportingMonth:SelectedModel[];
  selectReportingMonth:SelectedModel[];
  selectedEquipmentNameByCategory:SelectedModel[];
  selectequipementName:SelectedModel[];
  selectedReturnType:SelectedModel[];
  selectedBaseSchoolName:SelectedModel[];
  selectBaseSchoolName:SelectedModel[];

  constructor(private snackBar: MatSnackBar,private authService: AuthService, private baseSchoolNameService: BaseSchoolNameService,private confirmService: ConfirmService,private MonthlyReturnService: MonthlyReturnService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, private sharedService : SharedService) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    
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
            uploadDocument: res.uploadDocument,
            remarks: res.remarks,
            menuPosition: res.menuPosition,
            isActive: res.isActive
          });     
          this.onEquipmentCategorySelectionChange();     
        }
      );
    } else {
      this.pageTitle = 'Damage Electrical/Radio Electrical Equipment';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();

    if(this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO){
      this.MonthlyReturnForm.get('baseSchoolNameId').setValue(this.branchId);
      this.baseSchoolNameService.find(this.branchId).subscribe(res=>{
    
        this.MonthlyReturnForm.get('baseNameId').setValue(res.thirdLevel);
        this.MonthlyReturnForm.get('authorityId').setValue(res.secondLevel);
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
      authorityId:[''],
      baseNameId: [''],
      baseSchoolNameId: [],
      equipmentCategoryId: [''],
      equpmentNameId: [''],
      reportingMonthId: [''],
      returnTypeId: [''],
      operationalStatusId:[''],
      damageDescription: [''],
      presentCondition:[''],
      reportingDate:[''],
      timeOfDefect:[''],
      uploadDocument:[''],
      remarks:[''],
      menuPosition:[1],
      isActive: [true],
      doc:['']
    })
  }
  onFileChanged(event){
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
   
      this.MonthlyReturnForm.patchValue({
        doc: file,
      });
    }
  }
  getSelectedSchoolByBranchLevelAndThirdLevel(){
    this.MonthlyReturnService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res=>{
      this.selectedBaseSchoolName=res;
      this.selectBaseSchoolName=res;
    }); 
  }
  filterByShip(value:any){
    this.selectedBaseSchoolName=this.selectBaseSchoolName.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  onEquipmentCategorySelectionChange(){
   var equipmentCategoryId= this.MonthlyReturnForm.value['equipmentCategoryId'];
   this.MonthlyReturnService.getSelectedEquipmentNameByCategory(equipmentCategoryId).subscribe(res=>{
    this.selectedEquipmentNameByCategory=res
    this.selectequipementName=res
  }); 
  }
  filterByEquipementName(value:any){
    this.selectedEquipmentNameByCategory=this.selectequipementName.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedEquipmentCategory(){
    this.MonthlyReturnService.getSelectedEquipmentCategory().subscribe(res=>{
      this.selectedEquipmentCategory=res
      this.selectEquipementCategory=res
    }); 
  }
  filterByEquipementCategory(value:any){
    this.selectedEquipmentCategory=this.selectEquipementCategory.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedReportingMonth(){
    this.MonthlyReturnService.getSelectedReportingMonth().subscribe(res=>{
      this.selectedReportingMonth=res
      this.selectReportingMonth=res
    }); 
  }
  filterByMonth(value:any){
    this.selectedReportingMonth=this.selectReportingMonth.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedReturnType(){
    this.MonthlyReturnService.getSelectedReturnType().subscribe(res=>{
      this.selectedReturnType=res
    
    }); 
  }
  getSelectedOperationalStatus(){
    this.MonthlyReturnService.getSelectedOperationalStatus().subscribe(res=>{
      this.selectedOperationalStatus=res      
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
    const id = this.MonthlyReturnForm.get('monthlyReturnId').value;  
    // this.MonthlyReturnForm.get('reportingDate').setValue((new Date(this.MonthlyReturnForm.get('reportingDate').value)).toUTCString());

    const reportingDate = this.sharedService.formatDateTime(this.MonthlyReturnForm.get('reportingDate').value)
    this.MonthlyReturnForm.get('reportingDate').setValue(reportingDate);

    const timeOfDefect = this.sharedService.formatDateTime(this.MonthlyReturnForm.get('timeOfDefect').value)
    this.MonthlyReturnForm.get('timeOfDefect').setValue(timeOfDefect);

    const formData = new FormData();
    for (const key of Object.keys(this.MonthlyReturnForm.value)) {
      const value = this.MonthlyReturnForm.value[key];
      formData.append(key, value);
    }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.MonthlyReturnService.update(+id,formData).subscribe(response => {
           this.router.navigateByUrl('/ships-return/monthlyreturn-list');
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

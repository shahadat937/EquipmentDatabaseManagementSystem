import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DailyWorkStateService } from '../../service/DailyWorkState.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { MasterData } from 'src/assets/data/master-data';
import {BaseSchoolNameService} from '../../../../app/security/service/BaseSchoolName.service'
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-dailyworkstate',
  templateUrl: './new-dailyworkstate.component.html',
  styleUrls: ['./new-dailyworkstate.component.sass']
})
export class NewDailyWorkStateComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  DailyWorkStateForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  selectedLetterType:SelectedModel[];
  selectLetterType:SelectedModel[];
  selectedDealingOfficer:SelectedModel[];
  selectedBaseSchoolName:SelectedModel[];
  selectedActionTaken:SelectedModel[];
  selectedPriority:SelectedModel[];
  selectPriority:SelectedModel[];
  traineeId:any;
  role:any;
  branchId:any;
  organizationId:any;
  selectedCommendingArea:any[];
  commendingAreaId:any;

  constructor(private snackBar: MatSnackBar,private BaseSchoolNameService:BaseSchoolNameService,private authService: AuthService,private confirmService: ConfirmService,private DailyWorkStateService: DailyWorkStateService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, private sharedService : SharedService) { }

  ngOnInit(): void {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    const id = this.route.snapshot.paramMap.get('dailyWorkStateId'); 
    if (id) {
      this.pageTitle = 'Edit Daily Work State';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.DailyWorkStateService.find(+id).subscribe(
        res => {
          this.DailyWorkStateForm.patchValue({          

            dailyWorkStateId: res.dailyWorkStateId,
            letterTypeId: res.letterTypeId,
            dealingOfficerId: res.dealingOfficerId,
            actionTakenId: res.actionTakenId,
            priorityId: res.priorityId,
            date: res.date,
            workFrom: res.workFrom,
            letterNo: res.letterNo,
            subject: res.subject,
            dealingStaff: res.dealingStaff,
            deadLine: res.deadLine,
            fileUpload: res.fileUpload,
            status: res.status,
            menuPosition: res.menuPosition,
            isActive: res.isActive,
            remarks: res.remarks,
          });       
        console.log("res1");
        console.log(res);
        //this.onCommendingAreaSelectionChangeGetBaseName();
        //this.onOrganizationSelectionChange();
        }
      );
    } else {
      this.pageTitle = 'Create Daily Work State';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getSelectedLetterType();
    this.getSelectedDealingOfficer();
    this.getSelectedActionTaken();
    this.getSelectedPriority();
  }
  intitializeForm() {
    this.DailyWorkStateForm = this.fb.group({
      dailyWorkStateId: [0],
      letterTypeId:[],
      dealingOfficerId: [],
      actionTakenId: [],
      priorityId: [],
      date: [""],
      workFrom:[],
      letterNo: [''],
      subject: [''],
      dealingStaff:  [''],
      deadLine:  [''],
      fileUpload: [''],
      document:[''],
      remarks: [''],
      status: [0],
      menuPosition: [0],
      isActive: [true],
      
    })
  }
  // getSelectedSchoolByBranchLevelAndThirdLevel(){
  //   this.DailyWorkStateService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res=>{
  //     this.selectedBaseSchoolName=res;
  //     console.log(res);
  //   }); 
  // }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log(file);
      this.DailyWorkStateForm.patchValue({
        document: file,
      });
    }
  }
  getSelectedLetterType(){
    this.DailyWorkStateService.getSelectedLetterType().subscribe(res=>{
      this.selectedLetterType=res
      this.selectLetterType=res
    }); 
  }
  filterByLetterType(value:any){
    this.selectedLetterType=this.selectLetterType.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedDealingOfficer(){
    this.DailyWorkStateService.getSelectedDealingOfficer().subscribe(res=>{
      this.selectedDealingOfficer=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedActionTaken(){
    this.DailyWorkStateService.getSelectedActionTaken().subscribe(res=>{
      this.selectedActionTaken=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedPriority(){
    this.DailyWorkStateService.getSelectedPriority().subscribe(res=>{
      this.selectedPriority=res
      this.selectLetterType=res
    }); 
  }
  filterByPriority(value:any){
    this.selectedPriority=this.selectPriority.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  

  onSubmit() {
    const id = this.DailyWorkStateForm.get('dailyWorkStateId').value; 

    const date = this.sharedService.formatDateTime(this.DailyWorkStateForm.get('date').value);
    this.DailyWorkStateForm.get('date').setValue(date);
    
    const deadLine = this.sharedService.formatDateTime(this.DailyWorkStateForm.get('deadLine').value);
    this.DailyWorkStateForm.get('deadLine').setValue(deadLine);
    

    const formData = new FormData();
    for (const key of Object.keys(this.DailyWorkStateForm.value)) {
      const value = this.DailyWorkStateForm.value[key];
      formData.append(key, value);
    }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.DailyWorkStateService.update(+id,formData).subscribe(response => {
            this.router.navigateByUrl('/dailywork-management/dailyworkstate-list');
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
      this.DailyWorkStateService.submit(formData).subscribe(response => {
        this.router.navigateByUrl('/dailywork-management/dailyworkstate-list');
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

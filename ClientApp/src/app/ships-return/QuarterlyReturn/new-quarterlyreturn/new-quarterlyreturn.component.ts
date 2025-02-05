import { YearlyReturnService } from './../../service/YearlyReturn.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectedModel } from '../../../core/models/selectedModel';
import { Role } from '../../../core/models/role';
import { AuthService } from '../../../core/service/auth.service';

@Component({
  selector: 'app-new-yearlyreturn',
  templateUrl: './new-quarterlyreturn.compnent.html',
  styleUrls: ['./new-quarterly.component.css'],
})
export class NewQuarterlyReturnComponent implements OnInit {
  YearlyReturnForm!: FormGroup; 
  selectedBaseSchoolName: SelectedModel[] = [];
  selectedReportingMonth: SelectedModel[] = [];
  selectedOperationalStatus: SelectedModel[] = [];
  confirmService: any;

  validationErrors: string[] = [];
  pageTitle: string;
  destination: string;
  btnText: string;
  role: any;
  userRole = Role;
  branchId : any;

  reportYears = [
    { id: 1, year: '2020' },
    { id: 2, year: '2021' },
    { id: 3, year: '2022' },
    { id: 4, year: '2023' },
    { id: 5, year: '2024' },
    { id: 6, year: '2025' }
  ];
  selectShip: SelectedModel[];
  selectReportingMonth: SelectedModel[];
  
  constructor(
    private fb: FormBuilder, 
    private YearlyReturnService: YearlyReturnService,
    private snackBar: MatSnackBar,
    private router: Router, 
    private route: ActivatedRoute,
    private authService : AuthService
  ) {}

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    // this.traineeId = this.authService.currentUserValue.traineeId.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
  
    const id = this.route.snapshot.paramMap.get('yearlyReturnId');
    if (id) {
      this.pageTitle = 'Edit Yearly Return';
      this.destination = "Edit";
      this.btnText = 'Update';
  
      this.YearlyReturnService.find(+id).subscribe(
        res => {
          this.YearlyReturnForm.patchValue({
            yearlyReturnId: res.yearlyReturnId,
            baseSchoolNameId: res.baseSchoolNameId,
            operationalStatusId: res.operationalStatusId,
            reportingMonthId: res.reportingMonthId,
            reportingYearId: res.reportingYearId,
            menuPosition: res.menuPosition,
            isActive: res.isActive
          });

        }
      );
    } else {
      this.pageTitle = 'Add Yearly Return';
      this.destination = "Add";
      this.btnText = 'Save';
    }
  
    this.initializeForm();
    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.ShipUser || this.role == this.userRole.LOEO) {
      this.YearlyReturnForm.get('baseSchoolNameId')?.setValue(this.branchId);
    }
    this.getSelectedReportingMonth();
    this.getSelectedOperationalStatus();
    this.getSelectedSchoolByBranchLevelAndThirdLevel();
  }
  

  initializeForm() {
    this.YearlyReturnForm = this.fb.group({
      yearlyReturnId:[0],
      baseSchoolNameId: [''],
      operationalStatusId: [''], 
      reportingMonthId: [''], 
      reportingYearId: [''],
    });
  }

  // Load data for Ship Names
  getSelectedSchoolByBranchLevelAndThirdLevel() {
    this.YearlyReturnService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(
      (res: SelectedModel[]) => {
        this.selectedBaseSchoolName = res; 
        this.selectShip=res// Ensure API response matches SelectedModel type
      },
      (error) => {
        console.error('Error loading Ship Names:', error);
      }
    );
  }
  filterByShip(value:any){
    this.selectedBaseSchoolName=this.selectShip.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  filterByMonth(value:any){
    this.selectedReportingMonth=this.selectReportingMonth.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }

  // Load data for Reporting Month
  getSelectedReportingMonth() {
    this.YearlyReturnService.getSelectedReportingMonth().subscribe(
      (res: SelectedModel[]) => {
        this.selectedReportingMonth = res;
        this.selectReportingMonth=res
      },
      (error) => {
        console.error('Error loading Reporting Month:', error);
      }
    );
  }

  // Load data for Operational Status
  getSelectedOperationalStatus() {
    this.YearlyReturnService.getSelectedOperationalStatus().subscribe(
      (res: SelectedModel[]) => {
        this.selectedOperationalStatus = res;
      },
      (error) => {
        console.error('Error loading Operational Status:', error);
      }
    );
  }

  onSubmit() {
    console.log('Form Value Before Submission:', this.YearlyReturnForm.value);
    const id = this.YearlyReturnForm.get('yearlyReturnId')?.value;
  
    
    const formData = new FormData();
    for (const key of Object.keys(this.YearlyReturnForm.value)) {
      const value = this.YearlyReturnForm.value[key];
      if (value !== null && value !== undefined) {
        formData.append(key, value);
      }
    } 
    if (id) {
   
      this.confirmService.confirm('Confirm Update', 'Are you sure you want to update this item?').subscribe(result => {
        if (result) {
          this.YearlyReturnService.update(+id, formData).subscribe(response => {
            console.log('Update successful:', response);
            this.router.navigateByUrl('/ships-return/add-quarterlyreturn');
            this.snackBar.open('Information Updated Successfully', '', {
              duration: 2000,
              verticalPosition: 'bottom',
              horizontalPosition: 'right',
              panelClass: 'snackbar-success'
            });
          }, error => {
            this.validationErrors = error;
          });
        }
      });
    } else {
      this.YearlyReturnService.submit(formData).subscribe(response => {
        console.log(response)
        this.router.navigateByUrl('/ships-return/quarterly-return');
        this.snackBar.open('Information Inserted Successfully', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
      }, error => {
        this.validationErrors = error;
      });
    }
  }
  

}

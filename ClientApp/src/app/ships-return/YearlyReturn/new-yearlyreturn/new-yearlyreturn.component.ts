import { YearlyReturnService } from './../../service/YearlyReturn.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-new-yearlyreturn',
  templateUrl: './new-yearlyreturn.component.html',
  styleUrls: ['./new-yearlyreturn.component.css'],
})
export class YearlyReturnComponent implements OnInit {
  YearlyReturnForm!: FormGroup;
  selectedBaseSchoolName: SelectedModel[] = [];
  selectedReportingMonth: SelectedModel[] = [];
  selectedOperationalStatus: SelectedModel[] = [];
  selectShip: SelectedModel[];
  // confirmService: any;

  validationErrors: string[] = [];
  pageTitle: string;
  destination: string;
  btnText: string;
  authService: any;
  reportYears = [
    { id: 1, year: '2020' },
    { id: 2, year: '2021' },
    { id: 3, year: '2022' },
    { id: 4, year: '2023' }
  ];
  selectReportingMonth: SelectedModel[];

  constructor(
    private fb: FormBuilder,
    private YearlyReturnService: YearlyReturnService,
    private snackBar: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private confirmService : ConfirmService
  ) { }

  ngOnInit(): void {
    // this.role = this.authService.currentUserValue.role.trim();
    // this.traineeId = this.authService.currentUserValue.traineeId.trim();
    // this.branchId = this.authService.currentUserValue.branchId.trim();

    const id = this.route.snapshot.paramMap.get('yearlyReturnId');
    console.log(this.route.snapshot.paramMap);
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
            fileUpload: res.fileUpload,
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
    this.getSelectedReportingMonth();
    this.getSelectedOperationalStatus();
    this.getSelectedSchoolByBranchLevelAndThirdLevel();
  }


  initializeForm() {
    this.YearlyReturnForm = this.fb.group({
      yearlyReturnId: [0],
      baseSchoolNameId: [''],
      operationalStatusId: [''],
      reportingMonthId: [''],
      reportingYearId: [''],
      fileUpload: [''],
      doc: ['']

    });
  }


  getSelectedSchoolByBranchLevelAndThirdLevel() {
    this.YearlyReturnService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(
      (res: SelectedModel[]) => {
        this.selectedBaseSchoolName = res;
        this.selectShip = res;
      },
      (error) => {
        console.error('Error loading Ship Names:', error);
      }
    );
  }


  getSelectedReportingMonth() {
    this.YearlyReturnService.getSelectedReportingMonth().subscribe(
      (res: SelectedModel[]) => {
        this.selectedReportingMonth = res;
        this.selectReportingMonth = res
      },
      (error) => {
        console.error('Error loading Reporting Month:', error);
      }
    );
  }
  filterByShip(value:any){
    this.selectedBaseSchoolName=this.selectShip.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  filterByMonth(value:any){
    this.selectedReportingMonth=this.selectReportingMonth.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
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

  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];

      this.YearlyReturnForm.patchValue({
        doc: file,
      });
    }
  }

  onSubmit() {
    console.log('Form Value Before Submission:', this.YearlyReturnForm.value);
    const id = this.YearlyReturnForm.get('yearlyReturnId')?.value;
    console.log(id);


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
            this.router.navigateByUrl('/ships-return/yearlyreturn-list');
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
        this.router.navigateByUrl('/ships-return/yearlyreturn-list');
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

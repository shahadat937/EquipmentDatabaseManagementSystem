import { QuarterlyReturnService } from './../../service/QuarterlyReturn.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectedModel } from '../../../core/models/selectedModel';
import { Role } from '../../../core/models/role';
import { AuthService } from '../../../core/service/auth.service';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-QuarterlyReturn',
  templateUrl: './new-quarterlyreturn.compnent.html',
  styleUrls: ['./new-quarterly.component.css'],
})
export class NewQuarterlyReturnComponent implements OnInit {
  QuarterlyReturnForm!: FormGroup;
  selectedBaseSchoolName: SelectedModel[] = [];
  selectedReportingMonth: SelectedModel[] = [];
  selectedOperationalStatus: SelectedModel[] = [];


  validationErrors: string[] = [];
  pageTitle: string;
  destination: string;
  btnText: string;
  role: any;
  userRole = Role;
  branchId: any;

  selectedReportingYears: SelectedModel[]
  selectShip: SelectedModel[];
  selectReportingMonth: SelectedModel[];

  constructor(
    private fb: FormBuilder,
    private QuarterlyReturnService: QuarterlyReturnService,
    private snackBar: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private confirmService : ConfirmService
  ) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    // this.traineeId = this.authService.currentUserValue.traineeId.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();

    const id = this.route.snapshot.paramMap.get('quarterlyReturnId');
    console.log(this.route.snapshot.paramMap);
    if (id) {
      this.pageTitle = 'Edit Yearly Return';
      this.destination = "Edit";
      this.btnText = 'Update';

      this.QuarterlyReturnService.find(+id).subscribe(
        res => {
          this.QuarterlyReturnForm.patchValue({
            QuarterlyReturnId: res.quarterlyReturnId,
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
    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.ShipUser || this.role == this.userRole.LOEO || this.role == this.userRole.LOEOWTR) {
      this.QuarterlyReturnForm.get('baseSchoolNameId')?.setValue(this.branchId);
    }
    this.getSelectedReportingMonth();
    this.getSelectedOperationalStatus();
    this.getSelectedSchoolByBranchLevelAndThirdLevel();
    this.getSelectedReportingYear();
 
  }


  initializeForm() {
    this.QuarterlyReturnForm = this.fb.group({
      QuarterlyReturnId: [0],
      baseSchoolNameId: [''],
      operationalStatusId: [''],
      reportingMonthId: [''],
      reportingYearId: [''],
      fileUpload: [''],
      doc: ['']

    });
  }

  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];

      this.QuarterlyReturnForm.patchValue({
        doc: file,
      });
    }
  }

  // Load data for Ship Names
  getSelectedSchoolByBranchLevelAndThirdLevel() {
    this.QuarterlyReturnService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(
      (res: SelectedModel[]) => {
        this.selectedBaseSchoolName = res;
        this.selectShip = res// Ensure API response matches SelectedModel type
      },
      (error) => {
        console.error('Error loading Ship Names:', error);
      }
    );
  }
  filterByShip(value: any) {
    this.selectedBaseSchoolName = this.selectShip.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  filterByMonth(value: any) {
    this.selectedReportingMonth = this.selectReportingMonth.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }

  // Load data for Reporting Month
  getSelectedReportingMonth() {
    this.QuarterlyReturnService.getSelectedReportingMonth().subscribe(
      (res: SelectedModel[]) => {
        this.selectedReportingMonth = res;
        this.selectReportingMonth = res
      },
      (error) => {
        console.error('Error loading Reporting Month:', error);
      }
    );
  }
  getSelectedReportingYear() {
    this.QuarterlyReturnService.getSelectedReportingYears().subscribe(
      (res: SelectedModel[]) => {
        this.selectedReportingYears = res;
        console.log(res);

      },
      (error) => {
        console.error('Error loading Reporting Month:', error);
      }
    );
  }

  // Load data for Operational Status
  getSelectedOperationalStatus() {
    this.QuarterlyReturnService.getSelectedOperationalStatus().subscribe(
      (res: SelectedModel[]) => {
        this.selectedOperationalStatus = res;
      },
      (error) => {
        console.error('Error loading Operational Status:', error);
      }
    );
  }

  onSubmit() {
    const id = this.QuarterlyReturnForm.get('QuarterlyReturnId')?.value;
    // console.log(this.QuarterlyReturnForm.value);


    const formData = new FormData();
    for (const key of Object.keys(this.QuarterlyReturnForm.value)) {
      const value = this.QuarterlyReturnForm.value[key];
      if (value !== null && value !== undefined) {
        formData.append(key, value);
      }
    }
    if (id) {

      this.confirmService.confirm('Confirm Update', 'Are you sure you want to update this item?').subscribe(result => {
        if (result) {
          this.QuarterlyReturnService.update(+id, formData).subscribe(response => {
            this.router.navigateByUrl('/ships-return/quarterly-return');
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
      this.QuarterlyReturnService.submit(formData).subscribe(response => {
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

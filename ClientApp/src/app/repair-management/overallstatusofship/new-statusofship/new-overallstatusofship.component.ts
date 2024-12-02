import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ProcurementService } from 'src/app/procurement-management/service/Procurement.service';
import { YearlyReturnService } from 'src/app/ships-return/service/YearlyReturn.service';
import {OverallStatusOfShip} from '../../service/OverallStatusofShip.service'
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { SharedService } from 'src/app/shared/shared.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-statusofdefectivesystem-list',
    templateUrl: './new-overallstatusofship.component.html', 
    styleUrls: ['./new-overallstatusofship.component.css']
  })

  export class NewOverallStatusofShip implements OnInit {
OverallStatusOfShipForm: FormGroup;
selectedBaseSchoolName: SelectedModel[] = [];
  selectedReportingMonth: SelectedModel[] = [];
  selectedOperationalStatus: SelectedModel[] = [];
  btnText: string;
  pageTitle: string;
  destination: string;
  validationErrors: string[] = [];
  selectShip: SelectedModel[];

    constructor( private fb: FormBuilder,private YearlyReturnService: YearlyReturnService, private OverallStatusOfShip: OverallStatusOfShip, private router: Router,  private route: ActivatedRoute, private confirmService: ConfirmService, private sharedService : SharedService, private snackBar: MatSnackBar,) { }
    ngOnInit(): void {
      const id = this.route.snapshot.paramMap.get('statusOfShipId'); 
      if (id) {
        this.pageTitle = 'Edit Procurement';
        this.destination = "Edit";
        this.btnText = 'Update';
        this.OverallStatusOfShip.find(+id).subscribe(
          res => {
            this.OverallStatusOfShipForm.patchValue({
              statusOfShipId: res.statusOfShipId,          
             baseSchoolNameId: res.baseSchoolNameId,
            operationalStatusId: res.operationalStatusId,
            reasonOfBeingNonOperation: res.reasonOfBeingNonOperation,
              dateFromNonOperational: res.dateFromNonOperational,
            });       
          
          }
        );
      } else {
        this.pageTitle = 'Create Procurement';
        this.destination = "Add";
        this.btnText = 'Save';
      }

     
        this.initializeForm();
        this.getSelectedSchoolByBranchLevelAndThirdLevel()
        this.getSelectedOperationalStatus()
    }
    initializeForm(){
      this.OverallStatusOfShipForm = this.fb.group({
        statusOfShipId: [0],
        baseSchoolNameId: [''],
        operationalStatusId: [''],
        reasonOfBeingNonOperation: [''],
        dateFromNonOperational: [''],
      });
    }
    getSelectedSchoolByBranchLevelAndThirdLevel(){
      this.OverallStatusOfShip.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res=>{
        this.selectedBaseSchoolName=res;
        this.selectShip=res;
      }); 
      
    }
    filterByShip(value:any){
      this.selectedBaseSchoolName=this.selectShip.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
    }
    getSelectedOperationalStatus() {
      this.OverallStatusOfShip.getSelectedOperationalStatus().subscribe(
        (res: SelectedModel[]) => {
          this.selectedOperationalStatus = res;
        },
        (error) => {
          console.error('Error loading Operational Status:', error);
        }
      );
    }

    onSubmit() {
 
      const id = this.OverallStatusOfShipForm.get('statusOfShipId')?.value;
    
      const dateFromNonOperational = this.sharedService.formatDateTime(this.OverallStatusOfShipForm.get('dateFromNonOperational').value)
       this.OverallStatusOfShipForm.get('dateFromNonOperational').setValue(dateFromNonOperational);

      const formData = new FormData();
      for (const key of Object.keys(this.OverallStatusOfShipForm.value)) {
        const value = this.OverallStatusOfShipForm.value[key];
        if (value !== null && value !== undefined) {
          formData.append(key, value);
        }
      } 
      if (id) {
     
        this.confirmService.confirm('Confirm Update', 'Are you sure you want to update this item?').subscribe(result => {
          if (result) {
            this.OverallStatusOfShip.update(+id, formData).subscribe(response => {
              console.log('Update successful:', response);
              this.router.navigateByUrl('/repair-management/overallstatusofship-list');
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
        this.OverallStatusOfShip.submit(formData).subscribe(response => {
          this.router.navigateByUrl('/repair-management/overallstatusofship-list');
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


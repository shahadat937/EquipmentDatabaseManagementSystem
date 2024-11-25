import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ProcurementService } from 'src/app/procurement-management/service/Procurement.service';
import { YearlyReturnService } from 'src/app/ships-return/service/YearlyReturn.service';

@Component({
    selector: 'app-statusofdefectivesystem-list',
    templateUrl: './new-overallstatusofship.component.html', 
    styleUrls: ['./new-overallstatusofship.component.css']
  })

  export class NewOverallStatusofShip implements OnInit {
OverallStatusOfShipForm: any;
selectedBaseSchoolName: any;
selectedOperationalStatus: any;
    constructor( private fb: FormBuilder,private YearlyReturnService: YearlyReturnService) { }
    ngOnInit(): void {
      this.OverallStatusOfShipForm = this.fb.group({
        baseSchoolNameId: ['', Validators.required],
        operationalStatusId: ['', Validators.required],
        reasonForNoOperation: ['', Validators.required],
        date: ['', Validators.required],
      });

        this.getSelectedSchoolByBranchLevelAndThirdLevel()
        this.getSelectedOperationalStatus()
    }
    getSelectedSchoolByBranchLevelAndThirdLevel(){
      this.YearlyReturnService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res=>{
        this.selectedBaseSchoolName=res;
     
      }); 
      
    }
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

  }


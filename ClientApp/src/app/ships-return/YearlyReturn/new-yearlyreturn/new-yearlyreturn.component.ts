import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MonthlyReturnService } from '../../service/MonthlyReturn.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Component({
    selector: 'app-new-yearlyreturn',
    templateUrl: './new-yearlyreturn.component.html',
    styleUrls: ['./new-yearlyreturn.component.css']
  })
  export class YearlyRetrunComponent implements OnInit{
YearlyReturnForm: any;
selectedBaseSchoolName:SelectedModel[];
selectedReportingMonth: SelectedModel[];
selectedOperationalStatus: any;

    
constructor(private MonthlyReturnService: MonthlyReturnService) { }

ngOnInit(): void {
        this.getSelectedSchoolByBranchLevelAndThirdLevel()
        this.getSelectedReportingMonth()
        this.getSelectedOperationalStatus()
    }

    getSelectedSchoolByBranchLevelAndThirdLevel(){
        this.MonthlyReturnService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res=>{
          this.selectedBaseSchoolName=res;
        //   this.selectBaseSchoolName=res;
        }); 
      }
      getSelectedReportingMonth(){
        this.MonthlyReturnService.getSelectedReportingMonth().subscribe(res=>{
          this.selectedReportingMonth=res
        //   this.selectReportingMonth=res
        }); 
      }
      getSelectedOperationalStatus(){
        this.MonthlyReturnService.getSelectedOperationalStatus().subscribe(res=>{
          this.selectedOperationalStatus=res      
        }); 
      }
    
  }





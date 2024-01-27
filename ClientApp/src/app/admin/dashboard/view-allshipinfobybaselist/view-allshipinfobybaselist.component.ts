import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { ActivatedRoute,Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import {dashboardService} from '../services/dashboard.service';
import {BaseSchoolNameService} from '../../../security/service/BaseSchoolName.service'


@Component({
  selector: 'app-view-allshipinfobybaselist',
  templateUrl: './view-allshipinfobybaselist.component.html', 
  styleUrls: ['./view-allshipinfobybaselist.component.sass']
})
export class ViewAllShipInfoByBaseListComponent implements OnInit {

  masterData = MasterData;
  //ELEMENT_DATA: ShipInformation[] = [];
  isLoading = false;
  shipinfoList:any[];
  baseSchoolNameId:any;
  operationalStatusId:any;
  shipInfoByBase:any;
  selectedBaseName:any;
  
  constructor(private snackBar: MatSnackBar,private BaseSchoolNameService:BaseSchoolNameService,private dashboardService:dashboardService,private route: ActivatedRoute,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.baseSchoolNameId = this.route.snapshot.paramMap.get('baseSchoolNameId'); 
    this.operationalStatusId = this.route.snapshot.paramMap.get('operationalStatusId'); 
    this.getShipInfoByShipType();
    this.getShipInfoByByBase();
    this.getBaseName();
  }
 
  getBaseName(){
    this.BaseSchoolNameService.find(this.baseSchoolNameId).subscribe(res=>{
      this.selectedBaseName=res.schoolName
      console.log(this.selectedBaseName)
      console.log(res)
    }); 
  }

  getShipInfoByShipType(){
    this.dashboardService.getShipInformationListByShipType(11).subscribe(response => {           
       this.shipinfoList=response;
     })
   }

   getShipInfoByByBase(){
    this.dashboardService.getShipInfoByBase(this.baseSchoolNameId,this.operationalStatusId).subscribe(response => {           
       this.shipInfoByBase=response;
       console.log("this.shipInfoByBase");
        console.log(this.shipInfoByBase);
     })
   }
}

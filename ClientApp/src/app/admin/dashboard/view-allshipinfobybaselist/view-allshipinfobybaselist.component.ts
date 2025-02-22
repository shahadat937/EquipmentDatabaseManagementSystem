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
  showHideDiv: false;
  
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
   
    }); 
  }

  getShipInfoByShipType(){
    this.dashboardService.getShipInformationListByShipType(11).subscribe(response => {           
       this.shipinfoList=response;
     })
   }
   printSingle() {
    this.showHideDiv = false;
    this.print();
  }

print() { 
  let printContents, popupWin;
  printContents = document.getElementById('print-routine').innerHTML;
  popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
  popupWin.document.open();
  popupWin.document.write(`
    <html>
      <head>
        <style>
          body { width: 99%; font-family: Arial, sans-serif; }
          label {
            font-weight: 400;
            font-size: 13px;
            padding: 2px;
            margin-bottom: 5px;
          }
          table, td, th {
            border: 1px solid silver;
            text-align: center;
            padding: 8px;
          }
          table th {
            font-size: 13px;
            text-align: center;
            background-color: #f2f2f2;
            padding: 8px;
          }
          table td {
            font-size: 13px;
            text-align: left;
            padding: 8px;
          }
          table {
            border-collapse: collapse;
            width: 98%;
          }
          th, td {
            height: 26px;
          }
          .header-text {
            text-align: center;
          }
          .header-text h3 {
            margin: 0;
          }
        </style>
      </head>
      <body onload="window.print();window.close()">
        <div class="header-text">
          <h3>Ship Information Details</h3>
        </div>
        <br>
        <hr>
        <table class="table tbl-by-group">
          <thead>
            <tr>
              <th><h5>Name</h5></th>
              <th><h5>SQN</h5></th>
              <th><h5>OPL/NON-OPL</h5></th>
            </tr>
          </thead>
          <tbody>
            ${printContents}
          </tbody>
        </table>
      </body>
    </html>
  `);
  popupWin.document.close();
}


   getShipInfoByByBase(){
    this.dashboardService.getShipInfoByBase(this.baseSchoolNameId,this.operationalStatusId).subscribe(response => {           
       this.shipInfoByBase=response;

     })
   }
}

import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import {dashboardService} from '../services/dashboard.service'

@Component({
  selector: 'app-shipinformationbytype-list',
  templateUrl: './shipinformationbytype-list.component.html', 
  styleUrls: ['./shipinformationbytype-list.component.sass']
})
export class ShipInformationByTypeListComponent implements OnInit {

  masterData = MasterData;
  //ELEMENT_DATA: ShipInformation[] = [];
  isLoading = false;
  shipinfoList:any[];
  showHideDiv = false;
  groupArrays:{ schoolName: string; courses: any; }[];
  
  constructor(private snackBar: MatSnackBar,private dashboardService:dashboardService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getShipInfoByShipType();
  }
  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }
  printSingle() {
    this.showHideDiv = false;
    this.print();
  }
  // print() {
  //   let printContents, popupWin;
  //   printContents = document.getElementById("print-routine").innerHTML;
  //   popupWin = window.open( "top=0,left=0,height=100%,width=auto");
  //   popupWin.document.open();
  //   popupWin.document.write(`
  //     <html>
  //       <head>
  //         <style>
  //         body{  width: 99%;}
  //           label { font-weight: 400;
  //                   font-size: 13px;
  //                   padding: 2px;
  //                   margin-bottom: 5px;
  //                 }
  //           table, td, th {
  //                 border: 1px solid silver;
  //                   }
  //                   table td {
  //                 font-size: 13px;
  //                   }
  //                   .table.table.tbl-by-group.db-li-s-in tr .cl-action{
  //                     display: none;
  //                   }
        
  //                   .table.table.tbl-by-group.db-li-s-in tr td{
  //                     text-align:center;
  //                     padding: 0px 5px;
  //                   }
                    
  //                 }
  //                 .table.table.tbl-by-group.db-li-s-in tr .btn-tbl-edit {
  //                   display:none;
  //                 }
                  
                    
  //                   table th {
  //                 font-size: 13px;
  //                   }
  //             table {
  //                   border-collapse: collapse;
  //                   width: 98%;
  //                   }
  //               th {
  //                   height: 26px;
  //                   }
  //               .header-text{
  //                 text-align:center;
  //               }
  //               .header-text h3{
  //                 margin:0;
  //               }
  //         </style>
  //       </head>
  //       <body onload="window.print();window.close()">
  //         <div class="header-text">
  //         <h3>Ship's OPL/NON-OPL State</h3>
          
  //         </div>
  //         <br>
  //         <hr>
  //         ${printContents}
          
  //       </body>
  //     </html>`);
  //   popupWin.document.close();
  // }
  print() {
    let printContents, popupWin;
    printContents = document.getElementById("print-routine").innerHTML;
    popupWin = window.open("", "top=0,left=0,height=100%,width=auto");
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <style>
            body {
                width: 99%;
                font-family: Arial, sans-serif;
            }
            label {
                font-weight: 400;
                font-size: 13px;
                padding: 2px;
                margin-bottom: 5px;
            }
            table, td, th {
                border: 1px solid silver;
                border-collapse: collapse;
            }
            table {
                width: 98%;
                margin: 0 auto;
            }
            th, td {
                text-align: center;
                padding: 5px;
                font-size: 13px;
            }
            th {
                background-color: #f2f2f2;
                font-weight: bold;
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
            <h3>Ship's OPL/NON-OPL State</h3>
          </div>
          <br>
          <hr>
          ${printContents}
        </body>
      </html>`);
    popupWin.document.close();
}

 
  getShipInfoByShipType(){
    this.dashboardService.getShipInformationListByShipType(11).subscribe(response => {           
       this.shipinfoList=response;
         // this gives an object with dates as keys
         const groups = this.shipinfoList.reduce((groups, courses) => {
          const schoolName = courses.schoolName;
          if (!groups[schoolName]) {
            groups[schoolName] = [];
          }
          groups[schoolName].push(courses);
          return groups;
        }, {});

        // Edit: to add it in the array format instead
        this.groupArrays = Object.keys(groups).map((schoolName) => {
          return {
            schoolName,
            courses: groups[schoolName]
          };
        });
       console.log(this.shipinfoList);
     })
   }
}

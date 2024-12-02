import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import {dashboardService} from '../services/dashboard.service'
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';
import { debounceTime } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-establishmentbytype-list',
  templateUrl: './establishmentbytype-list.component.html', 
  styleUrls: ['./establishmentbytype-list.component.sass']
})
export class EstablishmentByTypeListComponent implements OnInit {

  masterData = MasterData;
  //ELEMENT_DATA: ShipInformation[] = [];
  isLoading = false;
  shipinfoList:any[];
  roles = Role
  groupArrays:{ schoolName: string; courses: any; }[];
  showHideDiv = false;
  role : string
  branchId : string
  searchText="";
  private searchSubject: Subject<string> = new Subject();
  
  constructor(private snackBar: MatSnackBar,private dashboardService:dashboardService,private router: Router,private confirmService: ConfirmService, private authService : AuthService) { }
  
  ngOnInit() {
    this.role = this.authService.currentUserValue.role
    this.branchId = this.authService.currentUserValue.branchId
    this.searchSubject.pipe(
      debounceTime(300) 
    ).subscribe((searchText) => {
      this.searchText = searchText;
      this.getEstablishmentByShipType();
    });
    this.getEstablishmentByShipType();
  }
  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }
  printSingle() {
    this.showHideDiv = false;
    this.print();
  }
  applyFilter(searchText: string) {
    this.searchSubject.next(searchText);
  }
  print() {
    let printContents, popupWin;
    printContents = document.getElementById("print-routine").innerHTML;
    popupWin = window.open( "top=0,left=0,height=100%,width=auto");
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <style>
          body{  width: 99%;}
            label { font-weight: 400;
                    font-size: 13px;
                    padding: 2px;
                    margin-bottom: 5px;
                  }
            table, td, th {
                  border: 1px solid silver;
                    }
                    table td {
                  font-size: 13px;
                    }
                    .table.table.tbl-by-group.db-li-s-in tr .col.table-img.img-on-hover{
                      display: none;
                    }
        
                    .table.table.tbl-by-group.db-li-s-in tr td{
                      text-align:center;
                      padding: 0px 5px;
                    }
                    
                  }
                  .table.table.tbl-by-group.db-li-s-in tr .btn-tbl-edit {
                    display:none;
                  }
                  
                    
                    table th {
                  font-size: 13px;
                    }
              table {
                    border-collapse: collapse;
                    width: 98%;
                    }
                th {
                    height: 26px;
                    }
                .header-text{
                  text-align:center;
                }
                .header-text h3{
                  margin:0;
                }
          </style>
        </head>
        <body onload="window.print();window.close()">
          <div class="header-text">
          <h3>Total Establishment </h3>
          
          </div>
          <br>
          <hr>
          ${printContents}
          
        </body>
      </html>`);
    popupWin.document.close();
  }
 
  getEstablishmentByShipType(){
   if(this.role === this.roles.AreaCommander || this.role === this.roles.FLO || this.role === this.roles.CSO || this.role === this.roles.FLOStaff){
    this.dashboardService.getShipInformationListByShipTypeAndCommandingArea(6, this.branchId).subscribe(response => {           
      this.shipinfoList=response;
   
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
    })

   }
   else{
    this.dashboardService.getShipInformationListByShipType(6).subscribe(response => {           
      this.shipinfoList=response;
   
      const groups = this.shipinfoList.reduce((groups, courses) => {
       const schoolName = courses.schoolName;
       if (!groups[schoolName]) {
         groups[schoolName] = [];
       }
       groups[schoolName].push(courses);
       return groups;
     }, {});


     this.groupArrays = Object.keys(groups).map((schoolName) => {
       return {
         schoolName,
         courses: groups[schoolName]
       };
     });
    })
   }
   }
}

import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ShipInformation } from '../../models/ShipInformation';
import { ShipInformationService } from '../../service/ShipInformation.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from '../../../../../src/app/core/service/confirm.service';
import { MasterData } from '../../../../../src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../../../../src/app/core/service/auth.service';
import { Role } from '../../../../../src/app/core/models/role';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { SharedService } from '../../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-shipinformation-list',
  templateUrl: './shipinformation-list.component.html',
  styleUrls: ['./shipinformation-list.component.sass']
})
export class ShipInformationListComponent implements OnInit {

  masterData = MasterData;
  userRole = Role;
  ELEMENT_DATA: ShipInformation[] = [];
  isLoading = false;
  groupArrays: { authorityName: string; courses: any; }[];
  fileUrl = 'https://localhost:44395/content/';
  showHideDiv = false;
  itemCount: any = 0;

  traineeId: any;
  role: any;
  branchId: any;
  isCommsndingAreaUsers : boolean;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 100,
    length: 1
  }
  searchText = "";
  private searchSubject: Subject<string> = new Subject();

  displayedColumns: string[] = ['ser', 'authorityName', 'baseName', 'baseSchoolName', 'sqnName', 'operationalStatus', 'actions'];
  dataSource: MatTableDataSource<ShipInformation> = new MatTableDataSource();

  selection = new SelectionModel<ShipInformation>(true, []);
  
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private ShipInformationService: ShipInformationService,private router: Router,private confirmService: ConfirmService, public SharedService: SharedService) { }
  
  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId = this.authService.currentUserValue.traineeId.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    this.getShips();

   
    this.searchSubject.pipe(
      debounceTime(300) // Adjust debounce time as needed
    ).subscribe((searchText) => {
      this.searchText = searchText;
      this.paging.pageIndex = this.masterData.paging.pageIndex
      this.paging.length = 1;
      this.getShips();
    });

    this.userRoleCheck();

  }


  getShips(){
    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO || this.role == this.userRole.ShipUser || this.role == this.userRole.LOEOWTR) {
      //console.log("test");
      this.getShipInformations(this.branchId); // BaseSchoolNameId in DB
    }
    else if (this.role === this.userRole.AreaCommander) {
      this.getShipInformationsByAuthorityId() 
    }
    else {
      this.getShipInformations(0);
    }
  }

  getShipInformations(shipId) {
    this.isLoading = true;
    this.ShipInformationService.getShipInformations(this.paging.pageIndex, this.paging.pageSize, this.searchText, shipId).subscribe(response => {
      //console.log(response);
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;
      
      const groups = this.dataSource.data.reduce((groups, courses) => {
        const schoolName = courses.authorityName;
        if (!groups[schoolName]) {
          groups[schoolName] = [];
        }
        groups[schoolName].push(courses);
        return groups;
      }, {});

      // Edit: to add it in the array format instead
      this.groupArrays = Object.keys(groups).map((authorityName) => {
        return {
          authorityName,
          courses: groups[authorityName]
        };
      });
    })
  }

  getShipInformationsByAuthorityId() {
    this.isLoading = true;
    this.ShipInformationService.getShipInformationsByAuthorityId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.branchId).subscribe(response => {
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;

      const groups = this.dataSource.data.reduce((groups, courses) => {
        const schoolName = courses.authorityName;
        if (!groups[schoolName]) {
          groups[schoolName] = [];
        }
        groups[schoolName].push(courses);
        return groups;
      }, {});

      this.groupArrays = Object.keys(groups).map((authorityName) => {
        return {
          authorityName,
          courses: groups[authorityName]
        };
      });
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    // if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO) {
    //   this.getShipInformations(this.branchId);
    // } else {
    //   this.getShipInformations(0);
    // }
    this.getShips()
  }

  // applyFilter(searchText: any){ 
  //   this.searchText = searchText;
  //   if(this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO){
  //     this.getShipInformations(this.branchId);
  //   }else{
  //     this.getShipInformations(0);
  //   }
  // } 
  applyFilter(searchText: string) {
    this.searchSubject.next(searchText);
  }

  userRoleCheck(){
    if(this.role === this.userRole.AreaCommander || this.role === this.userRole.CSO || this.role === this.userRole.FLO || this.role === this.userRole.FLOStaff){
      this.isCommsndingAreaUsers = true;
    }
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
  //         <h3>Ship Info List</h3>

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
    printContents = document.getElementById("print-routine")?.innerHTML;

    // Remove the <img> elements and <button> elements with class 'btn-tbl-edit' and 'btn-tbl-view'
    printContents = printContents.replace(/<img[^>]*>/g, ''); // Remove images
    printContents = printContents.replace(/<button[^>]*class="btn-tbl-edit"[^>]*>.*?<\/button>/g, ''); // Remove edit buttons
    printContents = printContents.replace(/<button[^>]*class="btn-tbl-view"[^>]*>.*?<\/button>/g, ''); // Remove view buttons

    // Remove any content after the "OPL/NON-OPL" column
    printContents = printContents.replace(/<td[^>]*class="cl-action"[^>]*>.*?<\/td>/g, ''); // Remove OPL/NON-OPL buttons

    // Now open the popup and print
    popupWin = window.open("", "_blank", "top=0,left=0,height=100%,width=auto");
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <style>
            body { width: 99%; }
            label { font-weight: 400; font-size: 13px; padding: 2px; margin-bottom: 5px; }
            table, td, th { border: 1px solid silver; }
            table td { font-size: 13px; }
            .table.table.tbl-by-group.db-li-s-in tr .cl-action { display: none; }
            .table.table.tbl-by-group.db-li-s-in tr td { text-align: center; padding: 0px 5px; }
            table th { font-size: 13px; }
            table { border-collapse: collapse; width: 98%; }
            th { height: 26px; }
            .header-text { text-align: center; }
            .header-text h3 { margin: 0; }
          </style>
        </head>
        <body onload="window.print(); window.close();">
          <div class="header-text">
            <h3>Ship Info List</h3>
          </div>
          <br>
          <hr>
          ${printContents}
        </body>
      </html>`
    );
    popupWin.document.close();
  }

  deleteItem(row) {
    const id = row.shipInformationId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.ShipInformationService.delete(id).subscribe(() => {
          if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO) {
            this.getShipInformations(this.branchId);
          } else {
            this.getShipInformations(0);
          }
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    })
  }
}

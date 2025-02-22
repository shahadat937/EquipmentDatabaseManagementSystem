import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { HalfYearlyReturn } from '../../models/HalfYearlyReturn';
import { HalfYearlyReturnService } from '../../service/HalfYearlyReturn.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from '../../../../../src/app/core/service/confirm.service';
import{MasterData} from '../../../../../src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedService } from '../../../../../src/app/shared/shared.service';
import { Role } from '../../../../../src/app/core/models/role';
import { AuthService } from '../../../../../src/app/core/service/auth.service';

@Component({
  selector: 'app-halfyearlyreturn-list',
  templateUrl: './halfyearlyreturn-list.component.html', 
  styleUrls: ['./halfyearlyreturn-list.component.sass']
})
export class HalfYearlyReturnListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: HalfYearlyReturn[] = [];
  isLoading = false;
  groupArrays:{ authorityName: string; courses: any; }[];
  fileUrl=  'https://localhost:44395/content/';
  showHideDiv = false;
  itemCount:any =0;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 5,
    length: 1
  }
  searchText="";
  userRoles = Role;
  role : string;
  branchId : string;
  isCommandingAreaUsers : boolean;

  displayedColumns: string[] = [ 'ser','authorityName','baseName', 'baseSchoolName','sqnName','operationalStatus','actions'];
  dataSource: MatTableDataSource<HalfYearlyReturn> = new MatTableDataSource();

  selection = new SelectionModel<HalfYearlyReturn>(true, []);
  
  constructor(private snackBar: MatSnackBar,private HalfYearlyReturnService: HalfYearlyReturnService,private router: Router,private confirmService: ConfirmService, public SharedService: SharedService, private  authService : AuthService) { }
  
  ngOnInit() {
    this.branchId = this.authService.currentUserValue.branchId
    this.role = this.authService.currentUserValue.role;
    this.getHalfYearlyReturns();
  }
 
  getHalfYearlyReturns() {
    this.isLoading = true;

    //console.log(this.branchId);

    if(this.role === this.userRoles.AreaCommander || this.role === this.userRoles.FLOStaff || this.role === this.userRoles.FLO || this.role === this.userRoles.CSO){
      this.getHalfYearlyShipReturnByCommandingArea();
    }
    else if(this.role === this.userRoles.LOEO || this.role === this.userRoles.LOEOWTR || this.role === this.userRoles.ShipUser){
      this.getHalfYearlyShipReturns(this.branchId)
    }
    else{
      this.getHalfYearlyShipReturns(0);
    }
  }

  getHalfYearlyShipReturns(shipId){
    this.HalfYearlyReturnService.getHalfYearlyReturns(this.paging.pageIndex, this.paging.pageSize,this.searchText, shipId).subscribe(response => {

      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    
    })
  }

  getHalfYearlyShipReturnByCommandingArea(){
    this.isCommandingAreaUsers = true;
      this.HalfYearlyReturnService.getHalfYearlyReturnsByAuthorityId(this.paging.pageIndex, this.paging.pageSize,this.searchText, this.branchId).subscribe(response => {

          this.dataSource.data = response.items; 
          this.paging.length = response.totalItemsCount    
          this.isLoading = false;                    
        })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getHalfYearlyReturns();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getHalfYearlyReturns();
  } 
  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }
  printSingle() {
    this.showHideDiv = false;
    this.print();
  }
  print() {
    let printContents, popupWin;
    printContents = document.getElementById("print-routine")?.innerHTML;
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
                    .table.table.tbl-by-group.db-li-s-in tr .cl-action{
                      display: none;
                    }
        
                    .table.table.tbl-by-group.db-li-s-in tr td .cl-action{
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
          <h3>Half Yearly Return List </h3>
          
          </div>
          <br>
          <hr>
          ${printContents}
          
        </body>
      </html>`);
    popupWin.document.close();
  }
  deleteItem(row) {
    const id = row.halfYearlyReturnId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.HalfYearlyReturnService.delete(id).subscribe(() => {
          this.getHalfYearlyReturns();
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

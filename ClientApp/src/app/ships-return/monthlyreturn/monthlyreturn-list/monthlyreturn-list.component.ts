import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MonthlyReturn } from '../../models/MonthlyReturn';
import { MonthlyReturnService } from '../../service/MonthlyReturn.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from '../../../../../src/app/core/service/confirm.service';
import { MasterData } from '../../../../../src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { SharedService } from '../../../../../src/app/shared/shared.service';
import { Role } from '../../../../../src/app/core/models/role';
import { AuthService } from '../../../../../src/app/core/service/auth.service';

@Component({
  selector: 'app-monthlyreturn-list',
  templateUrl: './monthlyreturn-list.component.html',
  styleUrls: ['./monthlyreturn-list.cmponent.css']
})
export class MonthlyReturnListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: MonthlyReturn[] = [];
  damageReturns: MonthlyReturn[] = [];
  defectReturns: MonthlyReturn[] = [];
  monthlyReturns: MonthlyReturn[] = [];
  isLoading = false;
  groupArrays: { authorityName: string; courses: any; }[];
  fileUrl = 'https://localhost:44395/content/';
  showHideDiv = false;
  itemCount: any = 0;
  paging = {
    pageIndex: 1,
    pageSize: 10,
    length: 1
  }
  searchText = "";
  userRoles = Role
  role: string;
  branchId: string
  isCommandingAreaUsers: boolean;
  private searchSubject: Subject<string> = new Subject();


  displayedColumns: string[] = ['ser', 'authorityName', 'baseName', 'baseSchoolName', 'sqnName', 'operationalStatus', 'actions'];
  dataSource: MatTableDataSource<MonthlyReturn> = new MatTableDataSource();

  selection = new SelectionModel<MonthlyReturn>(true, []);
  selectedFilter: any;

  constructor(private snackBar: MatSnackBar, private MonthlyReturnService: MonthlyReturnService, private router: Router, private confirmService: ConfirmService, public SharedService: SharedService, private authService: AuthService) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role;
    this.branchId = this.authService.currentUserValue.branchId;

    this.getMonthlyReturns();
    this.searchSubject.pipe(
      debounceTime(300) // Adjust debounce time as needed
    ).subscribe((searchText) => {
      this.searchText = searchText;
      this.getMonthlyReturns();
    });
    this.userRoleCheck();
  }

  getMonthlyReturns() {
    console.log(this.branchId)
    this.isLoading = true;
    if(this.role === this.userRoles.AreaCommander || this.role === this.userRoles.FLO || this.role === this.userRoles.FLOStaff || this.role === this.userRoles.CSO){
      this.getShipMonthlyReturnsByAuthority();

    }
    else if(this.role === this.userRoles.LOEO || this.role === this.userRoles.ShipUser){
      console.log(22)
      this.getMonthltShipReturns(this.branchId);
    }
    else{

     this.getMonthltShipReturns(0); // get All Ship Return 

    }

  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getMonthlyReturns();
  }


  getMonthltShipReturns (shipId){
    this.MonthlyReturnService.getMonthlyReturns(this.paging.pageIndex, this.paging.pageSize, this.searchText, shipId).subscribe(response => {
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount

      this.damageReturns = response.items.filter(item => item.returnType === 'Damage');
      this.defectReturns = response.items.filter(item => item.returnType === 'Defective');
      this.monthlyReturns = response.items.filter(item => item.returnType === 'monthly');

      this.isLoading = false;
      this.itemCount = response.items.length;

    })
  }

  getShipMonthlyReturnsByAuthority(){
    this.MonthlyReturnService.getMonthlyReturnsByAuthorityId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.branchId).subscribe(response => {

      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount

      this.damageReturns = response.items.filter(item => item.returnType === 'Damage');
      this.defectReturns = response.items.filter(item => item.returnType === 'Defective');
      this.monthlyReturns = response.items.filter(item => item.returnType === 'monthly');

      this.isLoading = false;
      this.itemCount = response.items.length;

    })
  }
  applyFilter(searchText: string) {
    this.searchSubject.next(searchText);
  }
  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }
  printSingle() {
    this.showHideDiv = false;
    this.print();
  }

  userRoleCheck() {
    if (this.role === this.userRoles.AreaCommander || this.role === this.userRoles.FLO || this.role === this.userRoles.CSO || this.role === this.userRoles.FLOStaff) {
      this.isCommandingAreaUsers = true;
    }
  }
  print() {
    let printContents, popupWin;
    printContents = document.getElementById("print-routine")?.innerHTML;
    popupWin = window.open("top=0,left=0,height=100%,width=auto");
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
          <h3>Ship Info List</h3>
          
          </div>
          <br>
          <hr>
          ${printContents}
          
        </body>
      </html>`);
    popupWin.document.close();
  }
  deleteItem(row) {
    const id = row.monthlyReturnId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {

      if (result) {
        this.MonthlyReturnService.delete(id).subscribe(() => {
          this.getMonthlyReturns();
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

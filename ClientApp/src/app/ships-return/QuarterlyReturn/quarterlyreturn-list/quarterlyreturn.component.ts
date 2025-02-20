import { ConfirmService } from '../../../core/service/confirm.service';
import { MasterData } from '../../../../assets/data/master-data';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { QuarterlyReturn } from '../../models/QuarterlyReturn';
import { MatSnackBar } from '@angular/material/snack-bar';
import { QuarterlyReturnService } from '../../service/QuarterlyReturn.service';
import { Router } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';
import { SelectionModel } from '@angular/cdk/collections';import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { SharedService } from '../../../../../src/app/shared/shared.service';
import { Role } from '../../../../../src/app/core/models/role';
import { AuthService } from '../../../../../src/app/core/service/auth.service';
@Component({
    selector: 'app-Quarterlyretrun-list',
    templateUrl: './quarterlyreturn-list.component.html',
    styleUrls: ['./quarterlyreturn-list.component.css']
  })
  export class QuarterlyRetrunComponent implements OnInit{
    masterData = MasterData;
    ELEMENT_DATA: QuarterlyReturn[] = [];
    paging = {
      pageIndex: this.masterData.paging.pageIndex,
      pageSize: this.masterData.paging.pageSize,
      length: 1
    }
    userRoles = Role
    role : string;
    branchId : string;
    searchText="";
    private searchSubject: Subject<string> = new Subject(); 
    dataSource: MatTableDataSource<QuarterlyReturn> = new MatTableDataSource();
  isLoading: boolean;
  itemCount: number;
  selection = new SelectionModel<QuarterlyReturn>(true, []);
  showHideDiv: boolean;
  isCommandingAreaUser : boolean;

  constructor(private snackBar: MatSnackBar,private QuarterlyReturnService: QuarterlyReturnService,private router: Router,private confirmService: ConfirmService, public SharedService: SharedService, private authService : AuthService) { }
    ngOnInit(): void {
      this.role = this.authService.currentUserValue.role;
      this.branchId = this.authService.currentUserValue.branchId;
      this.getQuarterlyReturn()
      this.searchSubject.pipe(
        debounceTime(300) // Adjust debounce time as needed
      ).subscribe((searchText) => {
        this.searchText = searchText;
        this.getQuarterlyReturn();
      });
    }

    

    getQuarterlyReturn() {
      this.isLoading = true;
      if(this.role === this.userRoles.AreaCommander || this.role === this.userRoles.FLO || this.role === this.userRoles.CSO || this.role === this.userRoles.FLOStaff){
        
        this.getQuarterlyReturnsByCommandingArea();
      }
      else if(this.role === this.userRoles.LOEO || this.role === this.userRoles.LOEOWTR || this.role === this.userRoles.ShipUser || this.role === this.userRoles.ShipStaff ){
        this.getQuarterlyReturns(this.branchId);
      }
      else{
        this.getQuarterlyReturns(0); // getAll
      }
     
    }

    getQuarterlyReturns (shipId){
      this.QuarterlyReturnService.getQuarterlyReturn(this.paging.pageIndex, this.paging.pageSize, this.searchText, shipId).subscribe(response => {
        this.dataSource.data = response.items;
        console.log(response.items);
       
        this.paging.length = response.totalItemsCount
        this.isLoading = false;
        this.itemCount = response.items.length;
  
      })
    }

    getQuarterlyReturnsByCommandingArea(){
      this.isCommandingAreaUser = true;
        this.QuarterlyReturnService.getQuarterlyReturnByAuthorityId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.branchId).subscribe(response => {
          
          this.dataSource.data = response.items;
          this.paging.length = response.totalItemsCount
          this.isLoading = false;
          this.itemCount = response.items.length;
    
        })
    }


    pageChanged(event: PageEvent) {
      this.paging.pageIndex = event.pageIndex
      this.paging.pageSize = event.pageSize
      this.paging.pageIndex = this.paging.pageIndex + 1
      this.getQuarterlyReturn();
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
    print() {
      let printContents, popupWin;
    
      printContents = document.getElementById("print-routine")?.innerHTML;
      popupWin = window.open("", "top=0,left=0,height=100%,width=auto");
    
      popupWin.document.open();
      popupWin.document.write(`
        <html>
          <head>
            <style>
              body {
                font-family: Arial, sans-serif;
                margin: 0;
                padding: 10px;
                width: 99%;
              }
              label {
                font-weight: 400;
                font-size: 13px;
                padding: 2px;
                margin-bottom: 5px;
              }
              table, td, th {
                border: 1px solid silver;
              }
              table {
                border-collapse: collapse;
                width: 98%;
                margin: 0 auto;
              }
              th, td {
                font-size: 13px;
                text-align: left;
                padding: 8px;
              }
              th {
                background-color: #f2f2f2;
                height: 26px;
              }
              .header-text {
                text-align: center;
                margin-bottom: 10px;
              }
              .header-text h3 {
                margin: 0;
              }
              .table-img img-on-hover {
                text-align: center;
                padding: 0px 5px;
              }
              .btn-tbl-edit, .btn-tbl-delete {
                display: none; /* Hide edit and delete buttons */
              }
              /* Remove last column from the table */
              th:last-child, td:last-child {
                display: none;
              }
            </style>
          </head>
          <body onload="window.print();window.close()">
            <div class="header-text">
              <h3>Quarterly Return Information</h3>
            </div>
            <br>
            <hr>
            ${printContents}
          </body>
        </html>`);
      popupWin.document.close();
    }
    
    deleteItem(row) {
      const id = row.quarterlyReturnId; 
      this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
    
        if (result) {
          this.QuarterlyReturnService.delete(id).subscribe(() => {
            this.getQuarterlyReturn();
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





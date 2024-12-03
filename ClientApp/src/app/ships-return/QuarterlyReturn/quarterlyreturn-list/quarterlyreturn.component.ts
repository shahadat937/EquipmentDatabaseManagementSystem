import { ConfirmService } from '../../../core/service/confirm.service';
// import { ConfigService } from 'src/app/config/config.service';
import { MasterData } from '../../../../assets/data/master-data';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
// import { MasterData } from 'src/assets/data/master-data';
import { YearlyReturn } from '../../models/YearlyReturn';
import { MatSnackBar } from '@angular/material/snack-bar';
import { YearlyReturnService } from '../../service/YearlyReturn.service';
import { Router } from '@angular/router';
// import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PageEvent } from '@angular/material/paginator';
import { SelectionModel } from '@angular/cdk/collections';import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { SharedService } from 'src/app/shared/shared.service';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
@Component({
    selector: 'app-yearlyretrun-list',
    templateUrl: './quarterlyreturn-list.component.html',
    styleUrls: ['./quarterlyreturn-list.component.css']
  })
  export class QuarterlyRetrunComponent implements OnInit{
    masterData = MasterData;
    ELEMENT_DATA: YearlyReturn[] = [];
    paging = {
      pageIndex: this.masterData.paging.pageIndex,
      pageSize: this.masterData.paging.pageSize,
      length: 1
    }
    userRoles = Role
    role : string;
    branchId : string;
    reportYears = [
      { id: 1, year: '2020' },
      { id: 2, year: '2021' },
      { id: 3, year: '2022' },
      { id: 4, year: '2023' }
    ];
    searchText="";
    private searchSubject: Subject<string> = new Subject(); 
    dataSource: MatTableDataSource<YearlyReturn> = new MatTableDataSource();
  isLoading: boolean;
  itemCount: number;
  selection = new SelectionModel<YearlyReturn>(true, []);
  showHideDiv: boolean;
  isCommandingAreaUser : boolean;

  constructor(private snackBar: MatSnackBar,private YearlyReturnService: YearlyReturnService,private router: Router,private confirmService: ConfirmService, public SharedService: SharedService, private authService : AuthService) { }
    ngOnInit(): void {
      this.role = this.authService.currentUserValue.role;
      this.branchId = this.authService.currentUserValue.branchId;
      this.getYearlyReturn()
      this.searchSubject.pipe(
        debounceTime(300) // Adjust debounce time as needed
      ).subscribe((searchText) => {
        this.searchText = searchText;
        this.getYearlyReturn();
      });
    }

    // getYearlyReturn(){
    //   this.isLoading = true;
    //   this.YearlyReturnService.getYearlyReturn(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response=>{
    //     this.dataSource.data = response.items; 
    //     this.dataSource.data = response.items.map((item) => ({
    //       ...item,
    //       year: this.reportYears.find((r) => r.id === item.reportingYearId)?.year || 'Unknown'
    //     }));
    //   this.paging.length = response.totalItemsCount 
    //   this.isLoading = false;  
    //   this.itemCount = response.items.length;
    //   })
    // }

    getYearlyReturn() {
      this.isLoading = true;
      if(this.role === this.userRoles.AreaCommander || this.role === this.userRoles.FLO || this.role === this.userRoles.CSO || this.role === this.userRoles.FLOStaff){
        this.isCommandingAreaUser = true;
        this.YearlyReturnService.getYearlyReturnByAuthorityId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.branchId).subscribe(response => {
          
          this.dataSource.data = response.items;
          this.dataSource.data = response.items.map((item) => ({
            ...item,
            year: this.reportYears.find((r) => r.id === item.reportingYearId)?.year || '-'
          }));
          this.paging.length = response.totalItemsCount
          this.isLoading = false;
          this.itemCount = response.items.length;
    
        })
  
      }
      else{
        this.YearlyReturnService.getYearlyReturn(this.paging.pageIndex, this.paging.pageSize, this.searchText).subscribe(response => {
          this.dataSource.data = response.items;
          this.dataSource.data = response.items.map((item) => ({
            ...item,
            year: this.reportYears.find((r) => r.id === item.reportingYearId)?.year || '-'
          }));
          this.paging.length = response.totalItemsCount
          this.isLoading = false;
          this.itemCount = response.items.length;
    
        })
      }
     
    }

    pageChanged(event: PageEvent) {
      this.paging.pageIndex = event.pageIndex
      this.paging.pageSize = event.pageSize
      this.paging.pageIndex = this.paging.pageIndex + 1
      this.getYearlyReturn();
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
  
      printContents = document.getElementById("print-routine").innerHTML;
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
      const id = row.yearlyReturnId; 
      this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
    
        if (result) {
          this.YearlyReturnService.delete(id).subscribe(() => {
            this.getYearlyReturn();
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





import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MonthlyReturn } from '../../models/MonthlyReturn';
import { MonthlyReturnService } from '../../service/MonthlyReturn.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-monthlyreturn-list',
  templateUrl: './monthlyreturn-list.component.html', 
  styleUrls: ['./monthlyreturn-list.component.sass']
})
export class MonthlyReturnListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: MonthlyReturn[] = [];
  isLoading = false;
  groupArrays:{ authorityName: string; courses: any; }[];
  fileUrl=  'https://localhost:44395/content/';
  showHideDiv = false;
  itemCount:any =0;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser','authorityName','baseName', 'baseSchoolName','sqnName','operationalStatus','actions'];
  dataSource: MatTableDataSource<MonthlyReturn> = new MatTableDataSource();

  selection = new SelectionModel<MonthlyReturn>(true, []);
  
  constructor(private snackBar: MatSnackBar,private MonthlyReturnService: MonthlyReturnService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getMonthlyReturns();
  }
 
  getMonthlyReturns() {
    this.isLoading = true;
    this.MonthlyReturnService.getMonthlyReturns(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;  
      this.itemCount = response.items.length;
     

       // this gives an object with dates as keys
      //  const groups = this.dataSource.data.reduce((groups, courses) => {
      //   const schoolName = courses.authorityName;
      //   if (!groups[schoolName]) {
      //     groups[schoolName] = [];
      //   }
      //   groups[schoolName].push(courses);
      //   return groups;
      // }, {});

      // Edit: to add it in the array format instead
      // this.groupArrays = Object.keys(groups).map((authorityName) => {
      //   return {
      //     authorityName,
      //     courses: groups[authorityName]
      //   };
      // });
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getMonthlyReturns();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getMonthlyReturns();
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

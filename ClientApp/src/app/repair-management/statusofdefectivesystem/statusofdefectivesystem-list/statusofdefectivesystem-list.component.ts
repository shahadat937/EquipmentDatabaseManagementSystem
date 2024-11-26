import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { OperationalState } from '../../models/OperationalState';
import { OperationalStateService } from '../../service/OperationalState.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-statusofdefectivesystem-list',
  templateUrl: './statusofdefectivesystem-list.component.html', 
  styleUrls: ['./statusofdefectivesystem-list.component.sass']
})
export class StatusOfDefectiveSystemListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: OperationalState[] = [];
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
  private searchSubject: Subject<string> = new Subject(); 
    
  traineeId:any;
  role:any;
  branchId:any;  
  userRole = Role;

  displayedColumns: string[] = [ 'ser','authorityName','baseName', 'baseSchoolName','sqnName','operationalStatus','actions'];
  dataSource: MatTableDataSource<OperationalState> = new MatTableDataSource();

  selection = new SelectionModel<OperationalState>(true, []);
  
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private OperationalStateService: OperationalStateService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    

    this.getOperationalStates();
    this.searchSubject.pipe(
      debounceTime(300)
    ).subscribe((searchText) => {
      this.searchText = searchText;
      this.getOperationalStates();
    });
  }
 
  getOperationalStates() {
    this.isLoading = true;
    this.OperationalStateService.getOperationalStates(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
      console.log(this.dataSource.data);
      this.itemCount = response.items.length;
      console.log("itemCountdata");
      console.log(this.dataSource.data);
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getOperationalStates();
  }

  // applyFilter(searchText: any){ 
  //   this.searchText = searchText;
  //   this.getOperationalStates();
  // } 
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
    const id = row.operationalStateId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.OperationalStateService.delete(id).subscribe(() => {
          this.getOperationalStates();
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

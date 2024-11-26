import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MasterData } from 'src/assets/data/master-data';
import { OverallShipStatus } from '../../models/OverallStatusOfShip';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {OverallStatusOfShip} from '../../service/OverallStatusofShip.service'
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-overall-ship-status-list',
  templateUrl: './overallstatusofship-list.component.html',
  styleUrls: ['./overallstatusofship-list.component.css']
})
export class OverallShipStatusListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: OverallShipStatus[] = [];
  dataSource = new MatTableDataSource<any>([]);
  searchText="";
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 5,
    length: 1
  }
  isLoading: boolean;
  itemCount: number;
  selection = new SelectionModel<OverallShipStatus>(true, []);
  private searchSubject: Subject<string> = new Subject(); 
  showHideDiv: boolean;

  constructor(private snackBar: MatSnackBar, private OverallStatusOfShip: OverallStatusOfShip, private router: Router, private confirmService: ConfirmService) {}

  ngOnInit(): void {
    this.getOverallStatusofShip()
    this.searchSubject.pipe(
      debounceTime(300) // Adjust debounce time as needed
    ).subscribe((searchText) => {
      this.searchText = searchText;
      this.getOverallStatusofShip();
    });
  }

  getOverallStatusofShip(){
    this.OverallStatusOfShip.getOverallStatusofShip(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response=>{
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount 
      this.isLoading = false;  
      this.itemCount = response.items.length;
    })
  }


  applyFilter(searchText: string) {
    this.searchSubject.next(searchText);
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getOverallStatusofShip();
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
    printContents = document.getElementById("print-overall-status").innerHTML;
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
    const id = row.statusOfShipId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
  
      if (result) {
        this.OverallStatusOfShip.delete(id).subscribe(() => {
          this.getOverallStatusofShip();
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

import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Sqn } from '../../models/Sqn';
import { SqnService } from '../../service/Sqn.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-sqn-list',
  templateUrl: './sqn-list.component.html', 
  styleUrls: ['./sqn-list.component.sass']
})
export class SqnListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: Sqn[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<Sqn> = new MatTableDataSource();

  selection = new SelectionModel<Sqn>(true, []);
  
  constructor(private snackBar: MatSnackBar,private SqnService: SqnService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getSqns();
  }
 
  getSqns() {
    this.isLoading = true;
    this.SqnService.getSqns(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getSqns();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getSqns();
  } 

  deleteItem(row) {
    const id = row.sqnId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.SqnService.delete(id).subscribe(() => {
          this.getSqns();
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

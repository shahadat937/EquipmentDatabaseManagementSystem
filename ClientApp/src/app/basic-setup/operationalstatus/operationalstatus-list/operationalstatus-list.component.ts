import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { OperationalStatus } from '../../models/OperationalStatus';
import { OperationalStatusService } from '../../service/OperationalStatus.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-operationalstatus-list',
  templateUrl: './operationalstatus-list.component.html', 
  styleUrls: ['./operationalstatus-list.component.sass']
})
export class OperationalStatusListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: OperationalStatus[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks'];
  dataSource: MatTableDataSource<OperationalStatus> = new MatTableDataSource();

  selection = new SelectionModel<OperationalStatus>(true, []);
  
  constructor(private snackBar: MatSnackBar,private OperationalStatusService: OperationalStatusService,private router: Router,private confirmService: ConfirmService, public SharedService: SharedService) { }
  
  ngOnInit() {
    this.getOperationalStatuss();
  }
 
  getOperationalStatuss() {
    this.isLoading = true;
    this.OperationalStatusService.getOperationalStatuss(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getOperationalStatuss();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getOperationalStatuss();
  } 

  deleteItem(row) {
    const id = row.operationalStatusId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.OperationalStatusService.delete(id).subscribe(() => {
          this.getOperationalStatuss();
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

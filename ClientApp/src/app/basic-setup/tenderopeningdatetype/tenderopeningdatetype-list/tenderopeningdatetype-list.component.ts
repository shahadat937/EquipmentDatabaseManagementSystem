import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { TenderOpeningDateType } from '../../models/TenderOpeningDateType';
import { TenderOpeningDateTypeService } from '../../service/TenderOpeningDateType.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-tenderopeningdatetype-list',
  templateUrl: './tenderopeningdatetype-list.component.html', 
  styleUrls: ['./tenderopeningdatetype-list.component.sass']
})
export class TenderOpeningDateTypeListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: TenderOpeningDateType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<TenderOpeningDateType> = new MatTableDataSource();

  selection = new SelectionModel<TenderOpeningDateType>(true, []);
  
  constructor(private snackBar: MatSnackBar,private TenderOpeningDateTypeService: TenderOpeningDateTypeService,private router: Router,private confirmService: ConfirmService, public SharedService: SharedService) { }
  
  ngOnInit() {
    this.getTenderOpeningDateTypes();
  }
 
  getTenderOpeningDateTypes() {
    this.isLoading = true;
    this.TenderOpeningDateTypeService.getTenderOpeningDateTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getTenderOpeningDateTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getTenderOpeningDateTypes();
  } 

  deleteItem(row) {
    const id = row.tenderOpeningDateTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.TenderOpeningDateTypeService.delete(id).subscribe(() => {
          this.getTenderOpeningDateTypes();
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

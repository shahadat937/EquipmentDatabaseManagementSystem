import { ReturnType } from './../../../basic-setup/models/ReturnType';
import { MonthlyReturn } from './../../../ships-return/models/MonthlyReturn';
import { MonthlyReturnService } from './../../../ships-return/service/MonthlyReturn.service';
import { Component, OnInit } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-monthlyreturn-list',
  templateUrl: './returntype-list.component.html',
  styleUrls: ['./returntype-list.component.css']
})
export class ReturnTypeListComponet implements OnInit {
  
  masterData = MasterData;
  ELEMENT_DATA: MonthlyReturn[] = [];
  isLoading = false;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  };
  searchText = '';
  
  // Separate arrays for damage and defect returns
  damageReturns: MonthlyReturn[] = [];
  defectReturns: MonthlyReturn[] = [];
  selectedFilter: any;
  displayedColumns: string[] = ['schoolName'];
  dataSource: MatTableDataSource<MonthlyReturn> = new MatTableDataSource();
  selection = new SelectionModel<MonthlyReturn>(true, []);

  constructor(
    private MonthlyReturnService: MonthlyReturnService,
    private router: Router,
    private confirmService: ConfirmService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.getMonthlyReturns();
  }

  // Get MonthlyReturns from service
  getMonthlyReturns() {
    this.isLoading = true;
    this.MonthlyReturnService.getMonthlyReturns(this.paging.pageIndex, this.paging.pageSize, this.searchText).subscribe(response => {
        console.log('API Response:', response);
        
        // Filter damage and defect returns separately
        this.damageReturns = response.items.filter(item => item.returnType === 'Damage');
        this.defectReturns = response.items.filter(item => item.returnType === 'Defective');
        
        this.paging.length = response.totalItemsCount;
        this.isLoading = false;
    })
}


  // Handle pagination changes
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex + 1;
    this.paging.pageSize = event.pageSize;
    this.getMonthlyReturns();
  }

  // Search filtering logic
  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getMonthlyReturns();
  }

  // Handle delete action
  deleteItem(row: MonthlyReturn) {
    const id = row.monthlyReturnId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      if (result) {
        this.MonthlyReturnService.delete(id).subscribe(() => {
          this.getMonthlyReturns();
          this.snackBar.open('Information Deleted Successfully', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        });
      }
    });
  }
}

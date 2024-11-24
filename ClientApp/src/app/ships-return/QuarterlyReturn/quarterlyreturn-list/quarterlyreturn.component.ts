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
import { SelectionModel } from '@angular/cdk/collections';
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
    searchText="";
    // private searchSubject: Subject<string> = new Subject(); 
    dataSource: MatTableDataSource<YearlyReturn> = new MatTableDataSource();
  isLoading: boolean;
  itemCount: number;
  selection = new SelectionModel<YearlyReturn>(true, []);

  constructor(private snackBar: MatSnackBar,private YearlyReturnService: YearlyReturnService,private router: Router,private confirmService: ConfirmService) { }
    ngOnInit(): void {
      this.getYearlyReturn()
    }

    getYearlyReturn(){
      this.isLoading = true;
      this.YearlyReturnService.getYearlyReturn(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response=>{
        this.dataSource.data = response.items; 
        console.log(response)
      this.paging.length = response.totalItemsCount 
      this.isLoading = false;  
      this.itemCount = response.items.length;
      })
    }

    pageChanged(event: PageEvent) {
      this.paging.pageIndex = event.pageIndex
      this.paging.pageSize = event.pageSize
      this.paging.pageIndex = this.paging.pageIndex + 1
      this.getYearlyReturn();
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





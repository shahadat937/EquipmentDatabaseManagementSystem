import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MasterData } from 'src/assets/data/master-data';
import { YearlyReturn } from '../../models/YearlyReturn';
import { MatSnackBar } from '@angular/material/snack-bar';
import { YearlyReturnService } from '../../service/YearlyReturn.service';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PageEvent } from '@angular/material/paginator';
@Component({
    selector: 'app-yearlyretrun-list',
    templateUrl: './yearlyreturn-list.component.html',
    styleUrls: ['./yearlyreturn-list.component.css']
  })
  export class NewYearlyRetrunComponent implements OnInit{
    masterData = MasterData;
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

  constructor(private snackBar: MatSnackBar,private YearlyReturnService: YearlyReturnService,private router: Router,private confirmService: ConfirmService) { }
    ngOnInit(): void {

    }

    getYearlyReturn(){
      this.isLoading = true;
      this.YearlyReturnService.getYearlyReturn(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response=>{
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
      this.getYearlyReturn();
    }
    
  }





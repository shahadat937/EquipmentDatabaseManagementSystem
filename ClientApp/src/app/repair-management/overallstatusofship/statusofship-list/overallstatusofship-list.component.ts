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


  constructor(private snackBar: MatSnackBar, private OverallStatusOfShip: OverallStatusOfShip, private router: Router, private confirmService: ConfirmService) {}

  ngOnInit(): void {
    this.getOverallStatusofShip()
  }

  getOverallStatusofShip(){
    this.OverallStatusOfShip.getOverallStatusofShip(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response=>{
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount 
      this.isLoading = false;  
      this.itemCount = response.items.length;
    })
  }


  applyFilter(filterValue: string): void {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getOverallStatusofShip();
  }

  // Print functionality
  printOverallShipStatus(): void {
    // Add logic for print functionality
    console.log('Print Overall Ship Status');
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

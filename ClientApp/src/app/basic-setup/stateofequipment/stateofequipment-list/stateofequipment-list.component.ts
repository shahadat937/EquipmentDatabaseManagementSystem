import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { StateOfEquipment } from '../../models/StateOfEquipment';
import { StateOfEquipmentService } from '../../service/StateOfEquipment.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-stateofequipment-list',
  templateUrl: './stateofequipment-list.component.html', 
  styleUrls: ['./stateofequipment-list.component.sass']
})
export class StateOfEquipmentListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: StateOfEquipment[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: 1,
    pageSize: 10,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<StateOfEquipment> = new MatTableDataSource();

  selection = new SelectionModel<StateOfEquipment>(true, []);
  
  constructor(private snackBar: MatSnackBar,private StateOfEquipmentService: StateOfEquipmentService,private router: Router,private confirmService: ConfirmService, public SharedService: SharedService) { }
  
  ngOnInit() {
    this.getStateOfEquipments();
  }
 
  getStateOfEquipments() {
    this.isLoading = true;
    this.StateOfEquipmentService.getStateOfEquipments(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getStateOfEquipments();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getStateOfEquipments();
  } 

  deleteItem(row) {
    const id = row.stateOfEquipmentId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.StateOfEquipmentService.delete(id).subscribe(() => {
          this.getStateOfEquipments();
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

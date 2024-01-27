import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { GroupName } from '../../models/GroupName';
import { GroupNameService } from '../../service/GroupName.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-groupname-list',
  templateUrl: './groupname-list.component.html', 
  styleUrls: ['./groupname-list.component.sass']
})
export class GroupNameListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: GroupName[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<GroupName> = new MatTableDataSource();

  selection = new SelectionModel<GroupName>(true, []);
  
  constructor(private snackBar: MatSnackBar,private GroupNameService: GroupNameService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getGroupNames();
  }
 
  getGroupNames() {
    this.isLoading = true;
    this.GroupNameService.getGroupNames(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getGroupNames();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getGroupNames();
  } 

  deleteItem(row) {
    const id = row.groupNameId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.GroupNameService.delete(id).subscribe(() => {
          this.getGroupNames();
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

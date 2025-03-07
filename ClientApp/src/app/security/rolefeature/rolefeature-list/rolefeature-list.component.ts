import { Component, OnInit, ViewChild,ElementRef  } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { RoleFeature } from '../../models/rolefeature';
import { RoleFeatureService } from '../../service/rolefeature.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data'
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-rolefeature',
  templateUrl: './rolefeature-list.component.html',
  styleUrls: ['./rolefeature-list.component.sass']
})
export class RoleFeatureListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: RoleFeature[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: 1,
    pageSize: 10,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = ['ser','roleName','featureName','add','update','delete','report', 'actions'];
  dataSource: MatTableDataSource<RoleFeature> = new MatTableDataSource();


   selection = new SelectionModel<RoleFeature>(true, []);

  
  constructor(private snackBar: MatSnackBar,private RoleFeatureService: RoleFeatureService,private router: Router,private confirmService: ConfirmService, public SharedService: SharedService) { }

  ngOnInit() {
    this.getRoleFeatures();
    
  }
 
  getRoleFeatures() {
    this.isLoading = true;
    this.RoleFeatureService.getRoleFeatures(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     //console.log(response);

      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
  
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getRoleFeatures();
 
  }
  applyFilter(searchText: any){ 
    this.paging.pageIndex = 1;
    this.searchText = searchText;
    this.getRoleFeatures();
  } 

  deleteItem(row) {
    const Roleid = row.roleId; 
    const FeatureKey = row.featureKey;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item').subscribe(result => {
      if (result) {
        this.RoleFeatureService.delete(Roleid,FeatureKey).subscribe(() => {
          this.getRoleFeatures();
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 3000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    })
    
  }
}

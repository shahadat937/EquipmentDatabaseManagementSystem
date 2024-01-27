import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ShipEquipmentInfo } from '../../models/ShipEquipmentInfo';
import { ShipEquipmentInfoService } from '../../service/ShipEquipmentInfo.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';

@Component({
  selector: 'app-shipequipmentinfo-list',
  templateUrl: './shipequipmentinfo-list.component.html', 
  styleUrls: ['./shipequipmentinfo-list.component.sass']
})
export class ShipEquipmentInfoListComponent implements OnInit {
  userRole = Role;
  masterData = MasterData;
  ELEMENT_DATA: ShipEquipmentInfo[] = [];
  isLoading = false;
    
  traineeId:any;
  role:any;
  branchId:any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser','equipmentCategory', 'equpmentName','stateOfEquipment','qty','model','actions'];
  dataSource: MatTableDataSource<ShipEquipmentInfo> = new MatTableDataSource();

  selection = new SelectionModel<ShipEquipmentInfo>(true, []);
  
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private ShipEquipmentInfoService: ShipEquipmentInfoService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)


    if(this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO || this.role == this.userRole.ShipUser){
      this.getShipEquipmentInfos(this.branchId);
    }else{
      this.getShipEquipmentInfos(0);
    }
  }
 
  getShipEquipmentInfos(shipId) {
    this.isLoading = true;
    this.ShipEquipmentInfoService.getShipEquipmentInfos(this.paging.pageIndex, this.paging.pageSize,this.searchText,shipId).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    if(this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO){
      this.getShipEquipmentInfos(this.branchId);
    }else{
      this.getShipEquipmentInfos(0);
    }
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    if(this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO){
      this.getShipEquipmentInfos(this.branchId);
    }else{
      this.getShipEquipmentInfos(0);
    }
  } 

  deleteItem(row) {
    const id = row.shipEquipmentInfoId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ShipEquipmentInfoService.delete(id).subscribe(() => {
          if(this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO){
            this.getShipEquipmentInfos(this.branchId);
          }else{
            this.getShipEquipmentInfos(0);
          }
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

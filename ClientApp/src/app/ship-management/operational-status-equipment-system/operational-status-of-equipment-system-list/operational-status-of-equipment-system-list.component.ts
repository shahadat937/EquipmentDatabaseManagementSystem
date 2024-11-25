import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { OperationalStatusOfEquipmentSystemService } from 'src/app/ship-management/service/operational-status-of-equipment-system.service'
import { MasterData } from 'src/assets/data/master-data';
import { OperationalStatusOfEquipmentSystem } from 'src/app/ship-management/models/OperationalStatusOfEquipment';
import { MatTableDataSource } from '@angular/material/table';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-operational-status-of-equipment-system-list',
  templateUrl: './operational-status-of-equipment-system-list.component.html',
  styleUrls: ['./operational-status-of-equipment-system-list.component.sass']
})
export class OperationalStatusOfEquipmentSystemListComponent implements OnInit {

  userRole = Role;
  masterData = MasterData;
  ELEMENT_DATA: OperationalStatusOfEquipmentSystem[] = [];
  isLoading = false;
  showHideDiv = false;
  traineeId: any;
  role: any;
  branchId: any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 5,
    length: 1
  }
  searchText = "";
  equipmentCategoryId: string;
  stateOfEquipmentId: string;
  equipmentNameId: string;

  displayedColumns: string[] = ['ser', 'actions'];
  dataSource: MatTableDataSource<OperationalStatusOfEquipmentSystem> = new MatTableDataSource();

  constructor(private OperationalStatusOfEquipmentSystemService: OperationalStatusOfEquipmentSystemService, private snackBar: MatSnackBar, private authService: AuthService, private router: Router, private confirmService: ConfirmService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this. getOperationalStatusOfEquipment();
  }

  getOperationalStatusOfEquipment() {
    this.OperationalStatusOfEquipmentSystemService.getOperationalStatusOfEquipment(this.paging.pageIndex, this.paging.pageSize, this.searchText).subscribe(response => {
      console.log(response);
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount
    })


  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getOperationalStatusOfEquipment();
  }
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getOperationalStatusOfEquipment();
  }
  deleteItem(row) {

    const id = row.operationalStatusOfEquipmentSystemId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      if (result) {
        this.OperationalStatusOfEquipmentSystemService.delete(id).subscribe(() => {
          this.getOperationalStatusOfEquipment()
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

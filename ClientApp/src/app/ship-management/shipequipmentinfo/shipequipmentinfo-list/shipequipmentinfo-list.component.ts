import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ShipEquipmentInfo } from '../../models/ShipEquipmentInfo';
import { ShipEquipmentInfoService } from '../../service/ShipEquipmentInfo.service';
import { SelectionModel } from '@angular/cdk/collections';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
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
  showHideDiv = false;
  traineeId:any;
  role:any;
  branchId:any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 5,
    length: 1
  }
  searchText = "";
  equipmentCategoryId: string;
  stateOfEquipmentId: string

  displayedColumns: string[] = ['ser', 'equipmentCategory', 'equpmentName', 'stateOfEquipment', 'qty', 'model', 'actions'];
  dataSource: MatTableDataSource<ShipEquipmentInfo> = new MatTableDataSource();

  selection = new SelectionModel<ShipEquipmentInfo>(true, []);

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private ShipEquipmentInfoService: ShipEquipmentInfoService, private router: Router, private confirmService: ConfirmService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId = this.authService.currentUserValue.traineeId.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    this.equipmentCategoryId = this.route.snapshot.paramMap.get("shipequipmentCategoryId");
    this.stateOfEquipmentId = this.route.snapshot.paramMap.get("stateOfEquipmentId");

    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO || this.role == this.userRole.ShipUser) {
      this.getShipEquipmentInfos(this.branchId);
    } else {
      this.getShipEquipmentInfos(0);
    }
  }

  getShipEquipmentInfos(shipId) {
    if (this.stateOfEquipmentId, this.equipmentCategoryId) {
      this.ShipEquipmentInfoService.getShipEquipmentByCategoryIdAndStateOfEquipmentStatus(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.equipmentCategoryId, this.stateOfEquipmentId).subscribe(response => {
        this.dataSource.data = response.items;
        this.paging.length = response.totalItemsCount
      })

    } else {
      this.isLoading = true;
      this.ShipEquipmentInfoService.getShipEquipmentInfos(this.paging.pageIndex, this.paging.pageSize, this.searchText, shipId).subscribe(response => {

        this.dataSource.data = response.items;
        this.paging.length = response.totalItemsCount
        this.isLoading = false;
      })
    }
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO) {
      this.getShipEquipmentInfos(this.branchId);
    } else {
      this.getShipEquipmentInfos(0);
    }
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO) {
      this.getShipEquipmentInfos(this.branchId);
    } else {
      this.getShipEquipmentInfos(0);
    }
  } 
  printSingle() {
    this.showHideDiv = false;
    this.print();
  }
 
print() {
  const dataSource = this.dataSource.data; // Access your mat-table dataSource
  if (!dataSource || dataSource.length === 0) {
    console.error('No data available for printing!');
    return;
  }

  const popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
  if (!popupWin) {
    console.error('Failed to open popup for printing!');
    return;
  }

  // Generate headers dynamically
  const tableHeaders = `
    <tr>
      <th>Ser.</th>
      <th>Equipment Category</th>
      <th>Equipment Name</th>
      <th>State of Equipment</th>
      <th>Qty</th>
      <th>Model</th>
    </tr>`;

  // Generate rows dynamically
  const tableRows = dataSource
    .map((row, index) => {
      return `
        <tr>
          <td>${index + 1}</td>
          <td>${row.equipmentCategory || ''}</td>
          <td>${row.equpmentName || ''}</td>
          <td>${row.stateOfEquipment || ''}</td>
          <td>${row.qty || ''}</td>
          <td>${row.model || ''}</td>
        </tr>`;
    })
    .join('');

  // Write the content to the popup
  popupWin.document.open();
  popupWin.document.write(`
    <html>
      <head>
        <title>Print Routine</title>
        <style>
          body {
            font-family: Arial, sans-serif;
          }
          table {
            border-collapse: collapse;
            width: 100%;
          }
          th, td {
            border: 1px solid #ddd;
            text-align: left;
            padding: 8px;
          }
          th {
            background-color: #f2f2f2;
            font-weight: bold;
          }
          .header-text {
            text-align: center;
            margin-bottom: 20px;
          }
          .header-text h3 {
            margin: 0;
          }
        </style>
      </head>
      <body onload="window.print();window.close()">
        <div class="header-text">
          <h3>Ship Info List</h3>
        </div>
        <hr>
        <table>
          <thead>${tableHeaders}</thead>
          <tbody>${tableRows}</tbody>
        </table>
      </body>
    </html>
  `);
  popupWin.document.close();
}




  deleteItem(row) {
    const id = row.shipEquipmentInfoId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      if (result) {
        this.ShipEquipmentInfoService.delete(id).subscribe(() => {
          if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO) {
            this.getShipEquipmentInfos(this.branchId);
          } else {
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

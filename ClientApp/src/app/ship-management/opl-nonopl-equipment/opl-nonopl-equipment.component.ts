import { Component, OnInit, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ShipEquipmentInfo } from '../models/ShipEquipmentInfo';
import { ShipEquipmentInfoService } from '../service/ShipEquipmentInfo.service';
import { SelectionModel, UniqueSelectionDispatcher } from '@angular/cdk/collections';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from '../../../../src/app/core/service/confirm.service';
import { MasterData } from '../../../../src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../../../src/app/core/service/auth.service';
import { Role } from '../../../../src/app/core/models/role';
import { SharedService } from '../../../../src/app/shared/shared.service';
import { Subject, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';

@Component({
  selector: 'app-opl-nonopl-equipment',
  templateUrl: './opl-nonopl-equipment.component.html',
  styleUrls: ['./opl-nonopl-equipment.component.sass']
})
export class OplNonoplEquipmentComponent implements OnInit {


  userRole = Role;
  masterData = MasterData;
  ELEMENT_DATA: ShipEquipmentInfo[] = [];
  isLoading = false;
  showHideDiv = false;
  traineeId: any;
  role: any;
  branchId: any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 10,
    length: 1
  }
  searchText = "";
  equipmentCategoryId: string;
  stateOfEquipmentId: string;
  equipmentNameId: string;
  isCommandingAreaUsers: boolean;
  searchTextChanged = new Subject<string>();
  private searchSubscription!: Subscription;
  sortColumn = "";
  sortDirection = ""
  currentSortKey = ""
  status: string;

  displayedColumns: string[] = ['ser', 'shipName', 'equipmentCategory', 'equpmentName', 'qty', 'model', 'brand', 'techSpecification', 'manufacturerNameAndAddress', 'acquisitionMethodName', 'yearOfInstallation', 'location', 'stateOfEquipment', 'powerSupply', 'avrbrand', 'avrmodel', 'interfaceProtocol', 'composition', 'defectDescription', 'remarks', 'actions'];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  selection = new SelectionModel<ShipEquipmentInfo>(true, []);

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private ShipEquipmentInfoService: ShipEquipmentInfoService, private router: Router, private confirmService: ConfirmService, private route: ActivatedRoute, public SharedService: SharedService) {
  }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId = this.authService.currentUserValue.traineeId.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    this.equipmentCategoryId = this.route.snapshot.paramMap.get("shipequipmentCategoryId") ?? "";
    this.equipmentNameId = this.route.snapshot.paramMap.get('equipmentNameId') ?? "";
    this.status = this.route.snapshot.paramMap.get('status');

    this.loadData();
    this.userRoleCheck();
    this.setupSearchDebounce();
  }

  getShipEquipmentInfos(shipId) {

    let isOpl = this.status === 'opl' ? true : false;
    if (this.equipmentCategoryId && this.equipmentNameId) {
      if (this.status == '1') {
        this.status = 'opl'
        this.stateOfEquipmentId = '1'
      }
      else if (this.status == '2') {
        this.status = 'nonOpl'
        this.stateOfEquipmentId = '2'
      }

      if (this.role === this.userRole.AreaCommander) {
        this.getShipEquipmentForAreaCommander();

      }
      else {
        this.ShipEquipmentInfoService.getShipEquipmentByCategoryIdNameIdAndStateOfEquipmentStatus(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.equipmentCategoryId, this.equipmentNameId, this.stateOfEquipmentId).subscribe(response => {
          this.dataSource.data = response.items;
          this.paging.length = response.totalItemsCount
        })
      }
    }
    else if (this.equipmentCategoryId) {

      if (this.role === this.userRole.AreaCommander || this.role === this.userRole.FLO || this.role === this.userRole.FLOStaff || this.role === this.userRole.CSO) {



        if (this.status === 'opl') {
          this.stateOfEquipmentId = '1'
        }
        else if (this.status === 'nonOpl') {
          this.stateOfEquipmentId = '2'
        }
        else {
          this.stateOfEquipmentId = '0';
        }

        this.ShipEquipmentInfoService.getShipEquipmentByCategoryIdAndStateOfEquipmentAndCommandingAreaId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.equipmentCategoryId, this.stateOfEquipmentId, this.branchId).subscribe(response => {
          this.dataSource.data = response
          this.paging.length = response[0]?.totalCount || 0
        })

      }
      else {
        this.ShipEquipmentInfoService.getShipEquipmentByCategoryIdAndOplNonOplQty(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.equipmentCategoryId, isOpl).subscribe(response => {
          this.dataSource.data = response.items;
          this.paging.length = response.totalItemsCount
        })
      }


    }
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.ShipUser || this.role == this.userRole.LOEO || this.role == this.userRole.LOEOWTR) {
      this.getShipEquipmentInfos(this.branchId);
    } else {
      this.getShipEquipmentInfos(0);
    }
  }

  // applyFilter(searchText: any) {
  //   this.searchText = searchText;
  //   if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO) {
  //     this.getShipEquipmentInfos(this.branchId);
  //   }
  //   else {
  //     this.getShipEquipmentInfos(0);
  //   }
  // }

  setupSearchDebounce() {
    this.searchSubscription = this.searchTextChanged.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(searchText => {
      this.loadData();
    });
  }

  loadData() {
    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO || this.role == this.userRole.ShipUser || this.role === this.userRole.LOEOWTR) {
      this.getShipEquipmentInfos(this.branchId);
    } else {
      this.getShipEquipmentInfos(0);
    }
  }

  applyFilter(searchText: any) {
    this.searchTextChanged.next(searchText);
  }

  userRoleCheck() {
    if (this.role === this.userRole.AreaCommander || this.role === this.userRole.FLO || this.role === this.userRole.FLOStaff) {
      this.isCommandingAreaUsers = true;
    }
  }

  getShipEquipments(shipId) {
    this.isLoading = true;
    this.ShipEquipmentInfoService.getShipEquipmentInfos(this.paging.pageIndex, this.paging.pageSize, this.searchText, shipId, this.sortColumn, this.sortDirection).subscribe(response => {
      this.dataSource.data = response.items;
      console.log(response.items);
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
    })
  }

  sortByKey(key) {
    this.sortDirection = this.sortDirection === 'desc' ? 'asc' : "desc"
    this.sortColumn = key;

    if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO || this.role == this.userRole.ShipUser || this.role === this.userRole.LOEOWTR) {
      this.getShipEquipmentInfos(this.branchId);
    } else {
      this.getShipEquipmentInfos(0);
    }
  }

  getShipEquipmentForAreaCommander() {
    this.ShipEquipmentInfoService.getShipEquipmentByCategoryIdNameIdStateOfEquipmentStatusAndCommandingAreaId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.equipmentCategoryId, this.equipmentNameId, this.stateOfEquipmentId, this.branchId).subscribe(response => {
      this.dataSource.data = response
      this.paging.length = response[0]?.totalCount || 0
    })
  }

  ngOnDestroy() {
    this.searchSubscription.unsubscribe(); // Prevent memory leaks
  }

  printSingle() {
    this.showHideDiv = false;
    this.print();
  }


  selectAll = false;
  toggleSelectAll() {
    this.dataSource.data.forEach(row => (row.selected = this.selectAll));
  }

  print() {
    let selectedRows = this.dataSource.data.filter(row => row.selected);

    // If no rows selected, print all rows
    if (selectedRows.length === 0) {
      selectedRows = this.dataSource.data;
    }

    if (!selectedRows || selectedRows.length === 0) {
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
      <th class="vertical-header">Ser.</th>
                 <th class="vertical-header">Ship Name</th>
                 <th class="vertical-header">Equipment Name</th>
                 <th class="vertical-header">Equipment Category</th>
                ${this.status === 'opl' ? `<th class="vertical-header">OPL Qty</th>` : ''}
                ${this.status === 'nonOpl' ? `<th class="vertical-header">Non-OPL Qty</th>` : ''} 
                 <th class="vertical-header">Brand</th>
                 <th class="vertical-header">Model</th>
                 <th class="vertical-header">Tech Specification</th>
                 <th class="vertical-header">Manufacturer Info</th>
                 <th class="vertical-header">Acquisition Method</th>
                 <th class="vertical-header">Year of Installation</th>
                 <th class="vertical-header">Location</th>   
                 <th class="vertical-header">Power Supply</th>                               
                 <th class="vertical-header">Interface Protocol</th>
                 <th class="vertical-header">Composition</th>
                 <th class="vertical-header">Defect Description</th>
                 <th class="vertical-header">Remarks</th>
                 <th class="vertical-header">Last Return</th>

    </tr>`;
    // it work
    // Generate rows dynamically based on selected data
    const tableRows = selectedRows
      .map((row, index) => {
        return `
        <tr>
           <td class="vertical-header">${index + 1 || '-'}</td>
                      <td class="vertical-header">${row.schoolName || "-"}</td>
                      <td class="vertical-header">${row.equpmentName || '-'}</td>
                      <td class="vertical-header">${row.equipmentCategory || '-'}</td>
                      ${this.status === 'opl' ? `<td class="vertical-header">${row.oplQty || '-'}</td>` : ''}
                      ${this.status === 'nonOpl' ? `<td class="vertical-header">${row.nonOplQty || '-'}</td>` : ''}
                      <td class="vertical-header">${row.brand || '-'}</td>
                      <td class="vertical-header">${row.model || '-'}</td>
                      <td class="vertical-header">${row.techSpecification || '-'}</td>
                      <td class="vertical-header">${row.manufacturerNameAndAddress || '-'}</td>
                      <td class="vertical-header">${row.acquisitionMethodName || '-'}</td>
                      <td class="vertical-header">${row.yearOfInstallation || '-'}</td>
                      <td class="vertical-header">${row.location || '-'}</td>
                      <td class="vertical-header">${row.powerSupply || '-'}</td>
                      
                      <td class="vertical-header">${row.interfaceProtocol || "-"}</td>
                      <td class="vertical-header">${row.composition || '-'}</td>               
                      <td class="vertical-header">${row.defectDescription || '-'}</td>
                      <td class="vertical-header">${row.remarks || '-'}</td>
                      <td class="vertical-header">${this.formatDate(row.lastRetrunModificationDate) || '-'}</td>
        </tr>`;
      })
      .join('');

    // Write the content to the popup
    popupWin.document.open();
    popupWin.document.write(`
    <html>
      <head>
        <title>Print</title>
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
        </style>
      </head>
      <body onload="window.print();window.close()">
        <div class="header-text">
          <h3>Ship Equipment List</h3>
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

  formatDate(dateString: string): string {
    if (dateString) {
      const date = new Date(dateString);
      return date.toLocaleDateString('en-US', { month: 'short', day: '2-digit', year: 'numeric' });
    }
    return ""

  }

  deleteItem(row) {
    const id = row.shipEquipmentInfoId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      if (result) {
        this.ShipEquipmentInfoService.delete(id).subscribe(() => {
          if (this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO || this.role == this.userRole.LOEOWTR || this.role == this.userRole.ShipUser) {
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

import { SharedService } from '../../../../../src/app/shared/shared.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Procurement } from '../../models/Procurement';
import { ProcurementService } from '../../service/Procurement.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from '../../../../../src/app/core/service/confirm.service';
import { MasterData } from '../../../../../src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Role } from '../../../../../src/app/core/models/role';
import { AuthService } from '../../../../../src/app/core/service/auth.service';
import { DatePipe } from '@angular/common';
import { Project } from '../../models/Project';
import { ProjectService } from '../../service/project.service';
import { UnsubscribeOnDestroyAdapter } from '../../../shared/UnsubscribeOnDestroyAdapter';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.sass']
})
export class ProjectListComponent extends UnsubscribeOnDestroyAdapter implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: Procurement[] = [];
  isLoading = false;
  groupArrays: { authorityName: string; courses: any; }[];
  showHideDiv = false;
  itemCount: any = 0;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 5,
    length: 1
  }
  ElectricalEqipmentpaging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 5,
    length: 1
  }
  hardwarelEqipmentpaging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 5,
    length: 1
  }
  userRoles = Role
  role: string;
  branchId: string;
  isCommandingAreaId: boolean;
  searchByOptions = ["shipname", "equipmentname"]
  searchText = "";
  isShipNameChecked: boolean = true;
  isEquipmentChecked: boolean;
  ShipNameSelelect = "";
  searchBy = "shipname";
  procurementMethodId1: number
  procurementMethodId2: number
  procurementMethodName1: string
  procurementMethodName2: string
  selectedProcurementTypeId: number;
  isCommandingAreaUsers: boolean;
  orderBy: string = "";
  orderDirection: string = 'desc';
  electricalEquipmentProject: any;
  electricalEquipmentProjectName: any;
  electricalEquipmentProjectCode: any;
  militaryHardwareProject: any;
  militaryHardwareProjectName: any;
  militaryHardwareProjectCode: any;
  financialYearName: string

  selectedMethod: any;
  dataSource: MatTableDataSource<Project> = new MatTableDataSource();

  selection = new SelectionModel<Project>(true, []);

  constructor(private snackBar: MatSnackBar, private ProcurementService: ProcurementService, private router: Router, private confirmService: ConfirmService, public SharedService: SharedService, private authService: AuthService, private datePipe: DatePipe, private ProjectService: ProjectService) { 
    super();
   }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role;
    this.branchId = this.authService.currentUserValue.branchId;

    this.getProject();
  }

  getProject() {
    this.isLoading = true;
    this.ProjectService.getProject(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.orderBy, this.orderDirection).subscribe(response => {
      this.dataSource.data = response.items;
      console.log(this.dataSource.data);

      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;
    })
  }




  getBaseSchoolNames(baseSchoolNameDtos: any[]): string {
    return baseSchoolNameDtos && baseSchoolNameDtos.length > 0
      ? baseSchoolNameDtos.map(school => school.baseSchoolName).join(', ')
      : '-';
  }


  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getProject();
  }


  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.paging.pageIndex = this.masterData.paging.pageIndex
    this.getProject();
  }

  sortByKey(key) {
    this.orderDirection = this.orderDirection === 'desc' ? 'asc' : "desc"
    this.orderBy = key;
    this.paging.pageIndex = 1;
    this.getProject();
  }

isEditable(financialName) {
  // console.log(financialName)

    const currentDate = new Date();
    const currentYear = currentDate.getFullYear();
    const currentMonth = currentDate.getMonth() + 1; // Months are 0-based
    let fyStartYear = currentMonth >= 7 ? currentYear : currentYear - 1;
    let fyEndYear = fyStartYear + 1;

    // Expected FY format (short)
    let currentFY = `FY ${fyStartYear % 100}-${fyEndYear % 100}`;

    // Normalize input: remove extra spaces and format to "FY XX-XX"
    financialName = financialName
        .replace(/\s+/g, ' ') // Replace multiple spaces with a single space
        .trim() // Remove leading and trailing spaces
        .replace(/\d{4}/g, match => match.slice(-2)); // Convert full years to last two digits
    return financialName !== currentFY;
}

  toggle() {
    this.showHideDiv = !this.showHideDiv;
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
             td {
              font-size: 0.8rem;
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

            /* Custom Table */
            .custom-table {
              width: 100%;
              border-collapse: collapse;
              table-layout: auto;
            }

            /* Vertical Header */
            .vertical-header {
              writing-mode: vertical-rl;
              transform: rotate(180deg);
              text-align: center;
              max-height: 150px;
              border: 1px solid rgba(0, 0, 0, 0.1);
              padding: 3px;
              word-wrap: break-word;
              max-height: 150px;
            }


            .btn-tbl-edit, .btn-tbl-delete {
              margin: 5px;
            }

            .col-white {
              color: white;
            }

          </style>
        </head>
        <body onload="window.print();window.close()">
          <div class="header-text">
            <h3>Project List</h3>
          </div>
          <hr>
<table class="custom-table">
  <thead>
    <tr>
      <th class="vertical-header">Ser.</th>
      <th class="vertical-header">Equipment Name</th>
      <th class="vertical-header">Ship Name</th>
      <th class="vertical-header">Qty</th>
      <th class="vertical-header">Agency</th>
      <th class="vertical-header">E-Price</th>
      <th class="vertical-header">FC/LC Name</th>
      <th class="vertical-header">Group Name</th>
      <th class="vertical-header">Budget Code</th>
      <th class="vertical-header">Financial Year</th>
      <th class="vertical-header">Controlled Name</th>
      <th class="vertical-header">Contract Signing Date</th>
      <th class="vertical-header">Foreign Training Schedule & Contents Received</th>
      <th class="vertical-header">Foreign Training Approval for Officer Nomination (CNS)</th>
      <th class="vertical-header">Foreign Training GO Approval</th>
      <th class="vertical-header">Foreign Training Date</th>
      <th class="vertical-header">Foreign Training Remarks</th>
      <th class="vertical-header">FAT Schedule</th>
      <th class="vertical-header">FAT Acceptance</th>
      <th class="vertical-header">FAT Approval for Offcer Nomination (CNS)</th>
      <th class="vertical-header">FAT GO Approval</th>
      <th class="vertical-header">FAT Date</th>
      <th class="vertical-header">FAT Remarks</th>
      <th class="vertical-header">Shipment & Delivery</th>
      <th class="vertical-header">STW</th>
      <th class="vertical-header">HAT</th>
      <th class="vertical-header">SAT</th>
      <th class="vertical-header">Local Training</th>
      <th class="vertical-header">Final Acceptance</th>
      <th class="vertical-header">Installation Remarks</th>
    </tr>
  </thead>
  <tbody>
    ${dataSource
      .map((row, index) => {
        return `
          <tr>
            <td class="vertical-header">${index + 1 || '-'}</td>
            <td class="vertical-header">${row.equpmentName || '-'}</td>
            <td class="vertical-header">${this.getBaseSchoolNames(row.baseSchoolNameDtos) || '-'}</td>
            <td class="vertical-header">${row.qty || '-'}</td>
            <td class="vertical-header">${row.dgdpNssdName || '-'}</td>
            <td class="vertical-header">${row.ePrice || '-'}</td>
            <td class="vertical-header">${row.fcLcName || '-'}</td>
            <td class="vertical-header">${row.groupName || '-'}</td>
            <td class="vertical-header">${row.budgetCode || '-'}(${row.budgetCodeName})</td>
            <td class="vertical-header">${row.financialYearName || '-'}</td>
            <td class="vertical-header">${row.controlledName || '-'}</td>
            <td class="vertical-header">${row.contractSigningDate? this.formatDate(row.contractSigningDate) : "-" }</td>
            <td class="vertical-header">${row.foreignTrainingScheduleAndContractReceived? this.formatDate(row.foreignTrainingScheduleAndContractReceived) : "-"}</td>
            <td class="vertical-header">${row.foreignTrainingApprovalForOfficerNomination? this.formatDate(row.foreignTrainingApprovalForOfficerNomination) : "-"}</td>
            <td class="vertical-header">${row.foreignTrainingGoApproval? this.formatDate(row.foreignTrainingGoApproval) : "-"}</td>
            <td class="vertical-header">${row.foreignTraningDate? this.formatDate(row.foreignTraningDate) : "-"}</td>
            <td class="vertical-header">${row.foreignTrainingRemark}</td>
            <td class="vertical-header">${row.fatSchedule? this.formatDate(row.fatSchedule) : "-"}</td>
            <td class="vertical-header">${row.fatAcceptance? this.formatDate(row.fatAcceptance) : "-"}</td>
            <td class="vertical-header">${row.fatApprovalForOfficerNomination? this.formatDate(row.fatApprovalForOfficerNomination) : '-'}</td>
            <td class="vertical-header">${row.fatGoApproval? this.formatDate(row.fatGoApproval) : '-'}</td>
            <td class="vertical-header">${row.fatDate? this.formatDate(row.fatDate) :'-'}</td>
            <td class="vertical-header">${row.fatRemarks || '-'}</td>
            <td class="vertical-header">${row.shipmentAndDelivery? this.formatDate(row.shipmentAndDelivery) : '-'}</td>
            <td class="vertical-header">${row.stw? this.formatDate(row.stw)  : '-'}</td>
            <td class="vertical-header">${row.hat? this.formatDate(row.hat)  : '-'}</td>
            <td class="vertical-header">${row.sat? this.formatDate(row.sat)  : '-'}</td>
            <td class="vertical-header">${row.localTraining? this.formatDate(row.localTraining)  : '-'}</td>
            <td class="vertical-header">${row.finalAcceptance? this.formatDate(row.finalAcceptance)  : '-'}</td>
            <td class="vertical-header">${row.installationRemarks || '-'}</td>
          </tr>
        `;
      })
        .join('')}
            </tbody>
          </table>
        </body>
      </html>
    `);



    popupWin.document.close();
  }
  deleteItem(row) {
    const id = row.projectId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      if (result) {
        this.ProjectService.delete(id).subscribe(() => {
          this.getProject();
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

  formatDate(dateString) {
    const date = new Date(dateString);

    if (isNaN(date.getTime())) {
      return '-';
    }

    const year = date.getFullYear();
    const monthIndex = date.getMonth();
    const monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    const month = monthNames[monthIndex];
    const day = String(date.getDate()).padStart(2, '0');

    return `${day} ${month} ,${year}`;
  }


}

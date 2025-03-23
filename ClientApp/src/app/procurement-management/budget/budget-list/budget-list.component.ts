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
import { Budget } from '../../models/Budget';
import { BudgetService } from '../../service/budget.service';
import { UnsubscribeOnDestroyAdapter } from '../../../shared/UnsubscribeOnDestroyAdapter';

@Component({
  selector: 'app-budget-list',
  templateUrl: './budget-list.component.html',
  styleUrls: ['./budget-list.component.sass']
})
export class BudgetListComponent extends UnsubscribeOnDestroyAdapter implements OnInit {

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
  electricalEquipmentBudget: any;
  electricalEquipmentBudgetName: any;
  electricalEquipmentBudgetCode: any;
  militaryHardwareBudget: any;
  militaryHardwareBudgetName: any;
  militaryHardwareBudgetCode: any;
  financialYearName: string

  selectedMethod: any;
  dataSource: MatTableDataSource<Budget> = new MatTableDataSource();

  selection = new SelectionModel<Budget>(true, []);

  constructor(private snackBar: MatSnackBar, private ProcurementService: ProcurementService, private router: Router, private confirmService: ConfirmService, public SharedService: SharedService, private authService: AuthService, private datePipe: DatePipe, private BudgetService: BudgetService) { 
    super();
  }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role;
    this.branchId = this.authService.currentUserValue.branchId;

    this.getBudget();
    this.getLeastestFyElectrialEquipmentBudget();
    this.getLeastestFyMilitaryHardwareBudget();
  }

  getBudget() {
    this.isLoading = true;
    this.BudgetService.getBudget(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.orderBy, this.orderDirection).subscribe(response => {
      this.dataSource.data = response.items;

      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;
    })
  }
  getLeastestFyElectrialEquipmentBudget() {
    this.isLoading = true;
    this.BudgetService.getLeatestFyBudget(this.ElectricalEqipmentpaging.pageIndex, this.ElectricalEqipmentpaging.pageSize, "", this.orderBy, this.orderDirection, this.masterData.budgetCode.electricalEquipment).subscribe(response => {
      this.electricalEquipmentBudget = response.items;
      
      this.ElectricalEqipmentpaging.length = response.totalItemsCount
      this.isLoading = false;
      this.electricalEquipmentBudgetName = this.electricalEquipmentBudget[0]?.budgetCodeName ?? "-"
      this.electricalEquipmentBudgetCode = this.electricalEquipmentBudget[0]?.budgetCode ?? "-"
      this.financialYearName = this.electricalEquipmentBudget[0]?.financialYearName ? this.militaryHardwareBudget[0]?.financialYearName : "-";

    })
  
  }
  getLeastestFyMilitaryHardwareBudget() {
    this.isLoading = true;
    this.BudgetService.getLeatestFyBudget(this.hardwarelEqipmentpaging.pageIndex, this.hardwarelEqipmentpaging.pageSize, "", this.orderBy, this.orderDirection, this.masterData.budgetCode.militaryHardware).subscribe(response => {
      this.militaryHardwareBudget = response.items;
      this.hardwarelEqipmentpaging.length = response.totalItemsCount
      this.isLoading = false;
      this.militaryHardwareBudgetName = this.militaryHardwareBudget[0]?.budgetCodeName ?? "-"
      this.militaryHardwareBudgetCode = this.militaryHardwareBudget[0]?.budgetCode ?? "-"
      this.financialYearName = this.militaryHardwareBudget[0]?.financialYearName ? this.militaryHardwareBudget[0]?.financialYearName : "-";

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
    this.getBudget();
  }
  ElectricalEquipmentpageChanged(event: PageEvent) {
    this.ElectricalEqipmentpaging.pageIndex = event.pageIndex
    this.ElectricalEqipmentpaging.pageSize = event.pageSize
    this.ElectricalEqipmentpaging.pageIndex = this.ElectricalEqipmentpaging.pageIndex + 1
    this.getLeastestFyElectrialEquipmentBudget();
  }
  militaryHardwarepageChanged(event: PageEvent) {
    this.hardwarelEqipmentpaging.pageIndex = event.pageIndex
    this.hardwarelEqipmentpaging.pageSize = event.pageSize
    this.hardwarelEqipmentpaging.pageIndex = this.hardwarelEqipmentpaging.pageIndex + 1
    this.getLeastestFyMilitaryHardwareBudget();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.paging.pageIndex = this.masterData.paging.pageIndex
    this.getBudget();
  }

  sortByKey(key) {
    this.orderDirection = this.orderDirection === 'desc' ? 'asc' : "desc"
    this.orderBy = key;
    this.paging.pageIndex = 1;
    this.getBudget();
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

    if(this.role === Role.MasterAdmin){
      return false;
    }
    else{
   
      return financialName !== currentFY;
    }
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
            <h3>Budget List</h3>
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
                <th class="vertical-header">Contractual Price FC</th>
                <th class="vertical-header">Contractual Price LC</th>
                <th class="vertical-header">Budget Code</th>
                <th class="vertical-header">Financial Year</th>
                <th class="vertical-header">Controlled</th> 
                <th class="vertical-header">Delivery Date</th> 
                <th class="vertical-header">FAT</th> 
                <th class="vertical-header">Foreign Training</th> 

                <th class="vertical-header">Allocated Budget</th>
                <th class="vertical-header">Allocated Budget Date</th>
                <th class="vertical-header">Revised Allocated Budget </th>
                <th class="vertical-header">Revised Allocated Budget Date </th>
                <th class="vertical-header">Bill Paid FC (DNW&EE) </th>
                <th class="vertical-header">Bill Paid LC (DNW&EE) </th>
                <th class="vertical-header">Bill Paid Others</th>
                <th class="vertical-header">Terms Of Payment (%)</th>
                <th class="vertical-header">Remarks</th>
              </tr>
            </thead>
            <tbody>
              ${dataSource
        .map((row, index) => {
          return `
                    <tr>
                      <td class="vertical-header">${index + 1 || '-'}</td>
                      <td class="vertical-header">${row.equpmentName|| '-'}</td>
                      <td class="vertical-header">${ this.getBaseSchoolNames(row.baseSchoolNameDtos) || "-"}</td>
                      <td class="vertical-header">${row.qty|| '-'}</td>
                      <td class="vertical-header">${row.dgdpNssdName|| '-'}</td>                     
                      <td class="vertical-header">${row.contractualPriceFC|| '-'}</td>                     
                      <td class="vertical-header">${row.contractualPriceLC|| '-'}</td>                     
                      <td class="vertical-header">${row.budgetCode|| '-'}</td>
                      <td class="vertical-header">${row.financialYearName || '-'}</td>
                      <td class="vertical-header">${row.controlledName|| '-'}</td>
                      <td class="vertical-header">${this.formatDate(row.deliveryDate)|| '-'}</td>
                      <td class="vertical-header">${row.fat || '-'}</td>
                      <td class="vertical-header">${row.foreignTraining || '-'}</td>
                      <td class="vertical-header">${row.allocatedBudget || '-'}</td>
                      <td class="vertical-header">${row.allocatedBudgetDate? this.formatDate(row.allocatedBudgetDate): '-'}</td>
                      <td class="vertical-header">${row.revisedAllocatedBudget || '-'}</td>
                      <td class="vertical-header">${row.revisedAllocatedBudgetDate? this.formatDate(row.revisedAllocatedBudgetDate): '-'}</td>
                      <td class="vertical-header">${row.billPainDNWEEFC || '-'}</td>
                      <td class="vertical-header">${row.billPainDNWEELC || '-'}</td>
                      <td class="vertical-header">${row.billPaidOther || '-'}</td>
                      <td class="vertical-header">${row.termsOfPayment || '-'}</td>
                      <td class="vertical-header">${row.remarks || '-'}</td>
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
    const id = row.budgetId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      if (result) {
        this.BudgetService.delete(id).subscribe(() => {
          this.getBudget();
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

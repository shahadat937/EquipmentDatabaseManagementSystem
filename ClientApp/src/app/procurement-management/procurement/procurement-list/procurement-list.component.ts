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


@Component({
  selector: 'app-procurement-list',
  templateUrl: './procurement-list.component.html',
  styleUrls: ['./procurement-list.component.sass']
})
export class ProcurementListComponent implements OnInit {

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
  userRoles  = Role
  role : string;
  branchId : string;
  isCommandingAreaId : boolean;
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
  isCommandingAreaUsers : boolean;
  displayedColumns: string[] = ['ser', 'schoolName', 'procurementType', 'groupName', 'equpmentName', 'qty', 'ePrice', 'fcLcName', 'dgdpNssdName', 'controlledName', 'tecName', 'sentToDgdpNssdDate', 'tenderOpeningDateTypeName', 'tenderOpeningDate', 'offerReceivedDate', 'sentForContractDate', 'clarificationToOemSentDate', 'contractSignedDate', 'paymentStatus', 'remarks', 'actions'];
  selectedMethod : any;
  dataSource: MatTableDataSource<Procurement> = new MatTableDataSource();

  selection = new SelectionModel<Procurement>(true, []);

  constructor(private snackBar: MatSnackBar, private ProcurementService: ProcurementService, private router: Router, private confirmService: ConfirmService, public SharedService: SharedService, private authService : AuthService, private datePipe: DatePipe) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role;
    this.branchId = this.authService.currentUserValue.branchId;

    this.getProcurements();

  }

  getProcurements() {
    this.isLoading = true;
    this.ProcurementService.getProcurements(this.paging.pageIndex, this.paging.pageSize, this.searchText).subscribe(response => {
      this.dataSource.data = response.items;
      
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

  getTenderOpeingDateAndTenderOpeningCount(tenderOpingdto: any[]): any {
    return tenderOpingdto && tenderOpingdto.length > 0
      ? tenderOpingdto.map((tenderDate, index) => {
          const date = this.datePipe.transform(tenderDate.tenderOpeningDate, 'dd MMM, yyyy');
          const count = tenderOpingdto.length > 1 && tenderDate.tenderOpeningCount
            ? ` (${tenderDate.tenderOpeningCount})` : ''; // Add count if more than 1 tender date exists
          return date + count;
        }).join('<br>')
      : '-';
  }
  
  

  getProcurementsByPeocureMethodIdAndAuthorityId(procurementMethodId) {
    this.ProcurementService.getProcurementsByProcurementMethodIdAndAuthorityId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.searchBy, procurementMethodId, this.branchId).subscribe(response => {
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;
    })
  }


  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getProcurements();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    // if (!this.isEquipmentChecked && !this.isShipNameChecked)
    //   this.searchBy = ""
    // this.getProcurementsByPeocureMethodId(this.selectedProcurementTypeId);
    this.paging.pageIndex =  this.masterData.paging.pageIndex
    this.getProcurements();
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
            <h3>Procurement List</h3>
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
                <th class="vertical-header">FC/LC</th>
                <th class="vertical-header">Group</th>
                <th class="vertical-header">Budget Code</th>
                <th class="vertical-header">Financial Year</th>
                <th class="vertical-header">Controlled</th> 


                <th class="vertical-header">Sent For AIP</th>
                <th class="vertical-header">AIP Approval Date</th>
                <th class="vertical-header">Indent Sent Date	</th>
                <th class="vertical-header">Tender Floated Date</th>
                <th class="vertical-header">Tender Opening Date</th>
                <th class="vertical-header">Offer Received Date & Under Evaluation</th>
                <th class="vertical-header">Sent For Contract</th>
                <th class="vertical-header">Contract Signed Date</th>
                <th class="vertical-header">Remaks</th>
              </tr>
            </thead>
            <tbody>
              ${dataSource
        .map((row, index) => {
          console.log(row);
          return `
                    <tr>
                      <td class="vertical-header">${index + 1 || '-'}</td>
                      <td class="vertical-header">${row.equpmentName|| '-'}</td>
                      <td class="vertical-header">${ this.getBaseSchoolNames(row.baseSchoolNameDtos) || "-"}</td>
                      <td class="vertical-header">${row.qty|| '-'}</td>
                      <td class="vertical-header">${row.dgdpNssdName|| '-'}</td>
                      <td class="vertical-header">${row.ePrice|| '-'}</td>
                      <td class="vertical-header">${row.fcLcId|| '-'}</td>
                      <td class="vertical-header">${row.groupName|| '-'}</td>
                      <td class="vertical-header">${row.budgetCode|| '-'}</td>
                      <td class="vertical-header">${row.financialYearName || '-'}</td>
                      <td class="vertical-header">${row.controlledName|| '-'}</td>


                      <td class="vertical-header">${this.formatDate(row.sentForAIPDate) ?? '-'}</td>
                      <td class="vertical-header">${this.formatDate(row.aipApprovalDate) ??  '-'}</td>
                      <td class="vertical-header">${ this.formatDate(row.indentSentDate) ?? '-'}</td>
                      <td class="vertical-header">${this.formatDate(row.tenderFloatedDate) ?? '-'}</td>
                      <td class="vertical-header">${this.getTenderOpeingDateAndTenderOpeningCount(row.procurementTenderOpeningDto)}</td>
                      <td class="vertical-header">${ this.formatDate(row.offerReceivedDateAndUpdateEvaluation) ?? '-'}</td>
                      <td class="vertical-header">${ this.formatDate(row.sentForContractDate) ?? '-'}</td>
                      <td class="vertical-header">${ this.formatDate(row.sentForContractDate) ?? '-'}</td>
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
    const id = row.procurementId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      if (result) {
        this.ProcurementService.delete(id).subscribe(() => {
         this.getProcurements();
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
  // onShipNameCheckboxChange(event: any) {

  //   if (this.isShipNameChecked) {
  //     this.isEquipmentChecked = false;
  //   }
  //   this.searchBy = "shipname"

  // }
  // onEquipmentCheckboxChange(event: any) {

  //   if (this.isEquipmentChecked) {
  //     this.isShipNameChecked = false;
  //     this.searchBy = "equipmentname"
  //   }

  // }
  formatDate(dateString) {
    const date = new Date(dateString);
  
    if (isNaN(date.getTime())) {
      return '-';
    }
  
    const year = date.getFullYear();
    const monthIndex = date.getMonth(); // Month index (0-11)
    const monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    const month = monthNames[monthIndex]; // Get the month name in MMM format
    const day = String(date.getDate()).padStart(2, '0');
  
    return `${day} ${month} ,${year}`;
  }
  

}

import { SharedService } from 'src/app/shared/shared.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Procurement } from '../../models/Procurement';
import { ProcurementService } from '../../service/Procurement.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


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
  searchByOptions = ["shipname", "equipmentname"]
  searchText = "";
  isShipNameChecked: boolean = true;
  isEquipmentChecked: boolean;
  ShipNameSelelect = "";
  searchBy = "shipname"; // by Default Search by Ship Name Selected;
  procurementMethodId1: number
  procurementMethodId2: number
  procurementMethodName1: string
  procurementMethodName2: string
  selectedProcurementTypeId: number;

  displayedColumns: string[] = ['ser', 'schoolName', 'procurementType', 'groupName', 'equpmentName', 'qty', 'ePrice', 'fcLcName', 'dgdpNssdName', 'controlledName', 'tecName', 'sentToDgdpNssdDate', 'tenderOpeningDateTypeName', 'tenderOpeningDate', 'offerReceivedDate', 'sentForContractDate', 'clarificationToOemSentDate', 'contractSignedDate', 'paymentStatus', 'remarks', 'actions'];
  dataSource: MatTableDataSource<Procurement> = new MatTableDataSource();

  selection = new SelectionModel<Procurement>(true, []);

  constructor(private snackBar: MatSnackBar, private ProcurementService: ProcurementService, private router: Router, private confirmService: ConfirmService, public SharedService: SharedService) { }

  ngOnInit() {
    this.getProcurementMethods()
    // this.getProcurements();

  }

  getProcurements() {
    this.isLoading = true;
    console.log(this.searchBy);
    this.ProcurementService.getProcurements(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.searchBy).subscribe(response => {
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;
    })
  }

  getProcurementsByPeocureMethodId(procurementMethodId) {
    this.ProcurementService.getProcurementsByProcurementMethodId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.searchBy, procurementMethodId).subscribe(response => {
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;
    })
  }

  getProcurementMethods() {
    this.isLoading = true;
    this.ProcurementService.getProcurementMethods(this.paging.pageIndex, this.paging.pageSize, this.searchText).subscribe(response => {

      console.log(response);
      this.procurementMethodId1 = response.items[0]?.procurementMethodId;
      this.procurementMethodId2 = response.items[1]?.procurementMethodId;
      this.procurementMethodName1 = response.items[0]?.name;
      this.procurementMethodName2 = response.items[1]?.name;
      this.selectedProcurementTypeId = response.items[0]?.procurementMethodId;
      this.getProcurementsByPeocureMethodId(this.procurementMethodId1)

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
    if (!this.isEquipmentChecked && !this.isShipNameChecked)
      this.searchBy = ""
    this.getProcurementsByPeocureMethodId(this.selectedProcurementTypeId);
  }
  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }
  printSingle() {
    this.showHideDiv = false;
    this.print();
  }
  filterByMethod(methodId: number) {
    console.log(methodId);
    this.selectedProcurementTypeId = methodId;
    this.getProcurementsByPeocureMethodId(methodId);
  }
  // print() {
  //   let printContents, popupWin;
  //   printContents = document.getElementById("print-routine").innerHTML;
  //   popupWin = window.open("top=0,left=0,height=100%,width=auto");
  //   popupWin.document.open();
  //   popupWin.document.write(`
  //     <html>
  //       <head>
  //         <style>
  //         body{  width: 99%;}
  //           label { font-weight: 400;
  //                   font-size: 13px;
  //                   padding: 2px;
  //                   margin-bottom: 5px;
  //                 }
  //           table, td, th {
  //                 border: 1px solid silver;
  //                   }
  //                   table td {
  //                 font-size: 13px;
  //                   }
  //                   .table.table.tbl-by-group.db-li-s-in tr .cl-action{
  //                     display: none;
  //                   }

  //                   .table.table.tbl-by-group.db-li-s-in tr td{
  //                     text-align:center;
  //                     padding: 0px 5px;
  //                   }

  //                 }
  //                 .table.table.tbl-by-group.db-li-s-in tr .btn-tbl-edit {
  //                   display:none;
  //                 }


  //                   table th {
  //                 font-size: 13px;
  //                   }
  //             table {
  //                   border-collapse: collapse;
  //                   width: 98%;
  //                   }
  //               th {
  //                   height: 26px;
  //                   }
  //               .header-text{
  //                 text-align:center;
  //               }
  //               .header-text h3{
  //                 margin:0;
  //               }
  //         </style>
  //       </head>
  //       <body onload="window.print();window.close()">
  //         <div class="header-text">
  //         <h3>Ship Info List</h3>

  //         </div>
  //         <br>
  //         <hr>
  //         ${printContents}

  //       </body>
  //     </html>`);
  //   popupWin.document.close();
  // }

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
            <h3>Ship Equipment List</h3>
          </div>
          <hr>
          <table class="custom-table">
            <thead>
              <tr>
                <th class="vertical-header">Ser.</th>
                <th class="vertical-header">Ship Name</th>
                <th class="vertical-header">Procurement Type</th>
                <th class="vertical-header">Group Name</th>
                <th class="vertical-header">Equipment Name</th>
                
                <th class="vertical-header">Qty</th>
                <th class="vertical-header">E-Price</th>
                <th class="vertical-header">FC/LC</th>
                <th class="vertical-header">DGDP/NSSD Name</th>
                <th class="vertical-header">Controlled</th>
                <th class="vertical-header">TEC</th>
                <th class="vertical-header">Sent to DGDP/NSSD Date</th>
                <th class="vertical-header">Tender Opening Date Type</th>   
                <th class="vertical-header">Tender Opening Date</th>
                <th class="vertical-header">Offer Received Date</th>
                <th class="vertical-header">Clarification to OEM Sent Date</th>
                <th class="vertical-header">Clarification to OEM Received Date</th>                
                <th class="vertical-header">Clarification to User Sent Date</th>
                <th class="vertical-header">Clarification to User Received Date</th>

                <th class="vertical-header">Tech TEC Sent Date</th>
                <th class="vertical-header">Tech TEC Received Date</th>
                <th class="vertical-header">Min for FO Sent Date</th>
                <th class="vertical-header">Min for FO Received Date</th>
                <th class="vertical-header">Sent to DTS Date</th>

                <th class="vertical-header">FO Received Date</th>
                <th class="vertical-header">FO TEC Sent Date</th>
                <th class="vertical-header">FO TEC Received Date</th>

                <th class="vertical-header">Final Contract Min Sent Date</th>
                <th class="vertical-header">Final Contract Min Received Date</th>
                <th class="vertical-header">Sent for Contract Date	</th>
                <th class="vertical-header">Contract Signed Date</th>
                <th class="vertical-header">Remaks</th>
              </tr>
            </thead>
            <tbody>
              ${dataSource
        .map((row, index) => {
          return `
                    <tr>
                      <td class="vertical-header">${index + 1 || '-'}</td>
                      <td class="vertical-header">${row.schoolName || "-"}</td>
                      <td class="vertical-header">${row.procurementType || '-'}</td>
                      <td class="vertical-header">${row.groupName || '-'}</td>
                      <td class="vertical-header">${row.equpmentName || '-'}</td>

                      <td class="vertical-header">${row.qty || '-'}</td>
                      <td class="vertical-header">${row.ePrice || '-'}</td>
                      <td class="vertical-header">${row.fcLcName || '-'}</td>
                      <td class="vertical-header">${row.dgdpNssdName || '-'}</td>
                      <td class="vertical-header">${row.controlledName || '-'}</td>
                      <td class="vertical-header">${row.tecName || '-'}</td>
                      <td class="vertical-header">${row.sentToDgdpNssdDate? this.formatDate(row.sentToDgdpNssdDate) : '-' }</td>
                      <td class="vertical-header">${row.tenderOpeningDateTypeName || '-'}</td>
                      <td class="vertical-header">${row.tenderOpeningDate? this.formatDate(row.tenderOpeningDate) : '-'}</td>
                      <td class="vertical-header">${row.offerReceivedDate || '-'}</td>
                      <td class="vertical-header">${row.clarificationToOemSentDate || '-'}</td>
                      <td class="vertical-header">${row.clarificationToOemReceivedDate || '-'}</td>
                      <td class="vertical-header">${row.clarificationToUserSentDate || '-'}</td>
                      <td class="vertical-header">${row.clarificationToUserReceivedDate || '-'}</td>

                      <td class="vertical-header">${row.techTecSentDate || '-'}</td>
                      <td class="vertical-header">${row.techTecReceivedDate || '-'}</td>
                      <td class="vertical-header">${row.minForFoSentDate || '-'}</td>
                      <td class="vertical-header">${row.minForFoReceivedDate || '-'}</td>
                      <td class="vertical-header">${row.sentToDtsDate || '-'}</td>
                      
                      <td class="vertical-header">${row.foReceivedDate || '-'}</td>
                      <td class="vertical-header">${row.foTecSentDate || '-'}</td>
                      <td class="vertical-header">${row.foTecReceivedDate || '-'}</td>
                      
                      <td class="vertical-header">${row.finalContractMinSentDate || '-'}</td>
                      <td class="vertical-header">${row.finalContractMinReceivedDate || '-'}</td>
                      <td class="vertical-header">${row.sentForContractDate || '-'}</td>
                      <td class="vertical-header">${row.contractSignedDate || '-'}</td>
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
          this.getProcurementsByPeocureMethodId(this.selectedProcurementTypeId);
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
  onShipNameCheckboxChange(event: any) {

    if (this.isShipNameChecked) {
      this.isEquipmentChecked = false;
    }
    this.searchBy = "shipname"

  }
  onEquipmentCheckboxChange(event: any) {

    if (this.isEquipmentChecked) {
      this.isShipNameChecked = false;
      this.searchBy = "equipmentname"
    }

  }
 formatDate(dateString) {
    const date = new Date(dateString);
  
    if (isNaN(date.getTime())) {
      
      return '-';
    }
  
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
  
    return `${day}-${month}-${year}`;
  }

}

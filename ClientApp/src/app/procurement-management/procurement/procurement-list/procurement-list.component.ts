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

  displayedColumns: string[] = ['ser', 'schoolName', 'procurementType', 'groupName', 'equpmentName', 'qty', 'ePrice', 'fcLcName', 'dgdpNssdName', 'controlledName', 'tecName', 'sentToDgdpNssdDate', 'tenderOpeningDateTypeName', 'tenderOpeningDate', 'offerReceivedDate', 'sentForContractDate', 'contractSignedDate', 'paymentStatus', 'remarks', 'actions'];
  dataSource: MatTableDataSource<Procurement> = new MatTableDataSource();

  selection = new SelectionModel<Procurement>(true, []);

  constructor(private snackBar: MatSnackBar, private ProcurementService: ProcurementService, private router: Router, private confirmService: ConfirmService) { }

  ngOnInit() {
    this.getProcurements();
  }

  getProcurements() {
    this.isLoading = true;
    console.log(this.searchBy);
    this.ProcurementService.getProcurements(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.searchBy).subscribe(response => {
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;


      //  // this gives an object with dates as keys
      //  const groups = this.dataSource.data.reduce((groups, courses) => {
      //   const schoolName = courses.authorityName;
      //   if (!groups[schoolName]) {
      //     groups[schoolName] = [];
      //   }
      //   groups[schoolName].push(courses);
      //   return groups;
      // }, {});

      // // Edit: to add it in the array format instead
      // this.groupArrays = Object.keys(groups).map((authorityName) => {
      //   return {
      //     authorityName,
      //     courses: groups[authorityName]
      //   };
      // });
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
      this.searchBy=""
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
    let printContents, popupWin;
    printContents = document.getElementById("print-routine").innerHTML;
    popupWin = window.open("top=0,left=0,height=100%,width=auto");
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <style>
          body{  width: 99%;}
            label { font-weight: 400;
                    font-size: 13px;
                    padding: 2px;
                    margin-bottom: 5px;
                  }
            table, td, th {
                  border: 1px solid silver;
                    }
                    table td {
                  font-size: 13px;
                    }
                    .table.table.tbl-by-group.db-li-s-in tr .cl-action{
                      display: none;
                    }
        
                    .table.table.tbl-by-group.db-li-s-in tr td{
                      text-align:center;
                      padding: 0px 5px;
                    }
                    
                  }
                  .table.table.tbl-by-group.db-li-s-in tr .btn-tbl-edit {
                    display:none;
                  }
                  
                    
                    table th {
                  font-size: 13px;
                    }
              table {
                    border-collapse: collapse;
                    width: 98%;
                    }
                th {
                    height: 26px;
                    }
                .header-text{
                  text-align:center;
                }
                .header-text h3{
                  margin:0;
                }
          </style>
        </head>
        <body onload="window.print();window.close()">
          <div class="header-text">
          <h3>Ship Info List</h3>
          
          </div>
          <br>
          <hr>
          ${printContents}
          
        </body>
      </html>`);
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


}

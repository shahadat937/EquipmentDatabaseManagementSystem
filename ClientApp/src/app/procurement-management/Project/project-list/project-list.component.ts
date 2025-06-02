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
import { color } from 'echarts';
interface Paging {
  pageIndex: number;
  pageSize: number;
  length: number;
}

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
 

  pagingConfig: Paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 5,
    length: 1
  };

  foreignTrainingPageing: Paging = { ...this.pagingConfig };
  fatPageing: Paging = { ...this.pagingConfig };
  installationPageing: Paging = {...this.pagingConfig}

  foreignTrainingProject : any;
  fatProject : any;
  installationProject : any;

  foreignTrainingProjectSearch = ""
  fatProjectSearch = ""
  installationProjectSearch = ""

  barCharData : any;
  barCharHoverData : any;


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

  
  barChartOptions = {
    title: {
        text: 'Project'
    },
    tooltip: {
        trigger: 'axis',
        formatter: function (params) {
            let tooltipContent = `${params[0].name}<br/>`;
            tooltipContent += `Total : ${params[0].value}<br/>`;
            tooltipContent += `Equipment : Custom data for ${params[0].name}`;
            return tooltipContent;
        }
    },
    xAxis: {
        data: ['Foreign Training', 'FAT', 'Installation Name'],
        axisLabel: {
            rotate: 0, // Rotate labels 
            interval: 0, // Make sure every label is shown
            fontSize: 12, // Adjust font size for better visibility
        }
    },
    yAxis: {},
    color: [],
    series: [
        {
            name: 'Sales',
            type: 'bar', // You can change this to 'line', 'pie', etc.
            data: [0,0,0,0,0,0]
        }
    ]
};


  constructor(private snackBar: MatSnackBar, private ProcurementService: ProcurementService, private router: Router, private confirmService: ConfirmService, public SharedService: SharedService, private authService: AuthService, private datePipe: DatePipe, private ProjectService: ProjectService) { 
    super();
   }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role;
    this.branchId = this.authService.currentUserValue.branchId;

    this.getProject();
    this.getForeignTraingProject();
    this.getFatProject();
    this.getInstallationProject();
    this.getBarChartData();
  }

  getProject() {
    this.isLoading = true;
    this.ProjectService.getProject(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.orderBy, this.orderDirection).subscribe(response => {
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


  foreignTrainingApplyFilter(searchText: any) {
    this.foreignTrainingProjectSearch = searchText;
    this.foreignTrainingPageing.pageIndex = this.masterData.paging.pageIndex
    this.getForeignTraingProject();
  }
  foreignTrainingPageChanged(event: PageEvent) {
    this.foreignTrainingPageing.pageIndex = event.pageIndex
    this.foreignTrainingPageing.pageSize = event.pageSize
    this.foreignTrainingPageing.pageIndex = this.foreignTrainingPageing.pageIndex + 1
    this.getForeignTraingProject();
  }

  fatApplyFilter(searchText: any) {
    this.fatProjectSearch = searchText;
    this.fatPageing.pageIndex = this.masterData.paging.pageIndex
    this.getFatProject();
  }
  fatTrainingPageChanged(event: PageEvent) {
    this.fatPageing.pageIndex = event.pageIndex
    this.fatPageing.pageSize = event.pageSize
    this.fatPageing.pageIndex = this.fatPageing.pageIndex + 1
    this.getFatProject();
  }

  installationApplyFilter(searchText: any) {
    this.installationProjectSearch = searchText;
    this.installationPageing.pageIndex = this.masterData.paging.pageIndex
    this.getInstallationProject();
  }
  installationPageChanged(event: PageEvent) {
    this.installationPageing.pageIndex = event.pageIndex
    this.installationPageing.pageSize = event.pageSize
    this.installationPageing.pageIndex = this.installationPageing.pageIndex + 1
    this.getInstallationProject();
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
      if(this.role === Role.MasterAdmin){
        return false;
      }
      else{
     
        return financialName !== currentFY;
      }
  }

getForeignTraingProject() {
  this.isLoading = true;
  this.ProjectService.getForeignTrainingProject(this.foreignTrainingPageing.pageIndex, this.foreignTrainingPageing.pageSize, this.foreignTrainingProjectSearch, this.orderBy, this.orderDirection).subscribe(response => {
    this.foreignTrainingProject = response.items;

    this.foreignTrainingPageing.length = response.totalItemsCount
    this.isLoading = false;
  })
}

getFatProject() {
  this.isLoading = true;
  this.ProjectService.getFatTrainingProject(this.fatPageing.pageIndex, this.fatPageing.pageSize, this.fatProjectSearch, this.orderBy, this.orderDirection).subscribe(response => {
    this.fatProject = response.items;

    this.fatPageing.length = response.totalItemsCount
    this.isLoading = false;
  })
}

getInstallationProject() {
  this.isLoading = true;
  this.ProjectService.getInstallationTrainingProject(this.installationPageing.pageIndex, this.installationPageing.pageSize, this.installationProjectSearch, this.orderBy, this.orderDirection).subscribe(response => {
    this.installationProject = response.items;
    this.installationPageing.length = response.totalItemsCount
    this.isLoading = false;
  })
}


getBarChartData() {
  this.ProjectService.getBarChartData().subscribe(res => {
   this.barCharData = [res.foreignTrainingCount, res.fatCount, res.installationCount];
   this.barCharHoverData = [res.foreignTrainingItems, res.fatItems, res.installationItems]

   this.updateBarChartOptions();
    
  }, error => {
    console.error('Error fetching pie chart data', error);
  });
}
updateBarChartOptions() {
  this.barChartOptions = {
      title: {
          text: 'Project'
      },
      tooltip: {
          trigger: 'axis',
          formatter: (params) => {
              let categoryName = params[0].name;
              let dataIndex = params[0].dataIndex; // Get the index of the hovered bar
              
              // Retrieve the corresponding equipment data from barCharHoverData
              let equipmentInfo = this.barCharHoverData?.[dataIndex]?.length
                  ? this.barCharHoverData[dataIndex].join(', ')
                  : '-';

              return `${categoryName}<br/>
                      Total : ${params[0].value}<br/>
                      Equipment : ${equipmentInfo}`;
          }
      },
      xAxis: {
        data: ['Foreign Training', 'FAT', 'Installation Name'],
        axisLabel: {
            rotate: 0, // Rotate labels 
            interval: 0, // Make sure every label is shown
            fontSize: 12, // Adjust font size for better visibility
        }
      },
      yAxis: {},
      color:['#7154ff'],
      series: [
          {
              name: 'Project',
              type: 'bar',
              data: this.barCharData
          }
      ]
  };
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

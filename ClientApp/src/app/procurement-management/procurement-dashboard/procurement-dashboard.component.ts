import { SharedService } from '../../../../src/app/shared/shared.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Procurement } from '../models/Procurement';
import { ProcurementService } from '../service/Procurement.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from '../../../../src/app/core/service/confirm.service';
import { MasterData } from '../../../../src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Role } from '../../../../src/app/core/models/role';
import { AuthService } from '../../../../src/app/core/service/auth.service';
import { DatePipe } from '@angular/common';
import { UnsubscribeOnDestroyAdapter } from '../../../../src/app/shared/UnsubscribeOnDestroyAdapter';
import * as echarts from 'echarts';


interface Paging {
  pageIndex: number;
  pageSize: number;
  length: number;
}

@Component({
  selector: 'app-procurement-dashboard',
  templateUrl: './procurement-dashboard.component.html',
  styleUrls: ['./procurement-dashboard.component.scss']
})



export class ProcurementDashboardComponent extends UnsubscribeOnDestroyAdapter  implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: Procurement[] = [];
  isLoading = false;
  groupArrays: { authorityName: string; courses: any; }[];
  showHideDiv = false;
  itemCount: any = 0;
  pagingConfig: Paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 5,
    length: 1
  };

  aipPendingpaging: Paging = { ...this.pagingConfig };
  onGoingTenderpaging: Paging = { ...this.pagingConfig };
  tenderFloatingpaging: Paging = {...this.pagingConfig}
  tenderOpeningPageing: Paging = {...this.pagingConfig}
  offerUnderEvaluationPageing: Paging = {...this.pagingConfig}
  
  aipPendingSearchText = "";
  tenderOnGoingSearchText = "";
  tenderFloatingSearchText = ""
  tenderOpeningSearchText = ""
  offerUnderEvulationSearchText = ""
  orderBy : string = "";
  orderDirection : string = 'desc';
  AipPending : any;
  tenderOngoing : any;
  tenderFloating: any;
  tenderOpening: any;
  offerUnderEvaluation : any;
  pieChartData1: any = { }; // Default initialization
  pieChartData2: any = {};

  width: number = 200;
  height: number = 200;
  barCharData : any;
  barCharHoverData : any;

  
 

  barChartOptions = {
    title: {
        text: 'Procurement Data'
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
        data: ['Sent AIP', 'AIP Approved', 'Indent Date', 'Tender Floated Data', 'Offer R & E', 'Date Of Contact'],
        axisLabel: {
            rotate: 25, // Rotate labels 
            interval: 0, // Make sure every label is shown
            fontSize: 12, // Adjust font size for better visibility
        }
    },
    yAxis: {},
    series: [
        {
            name: 'Sales',
            type: 'bar', // You can change this to 'line', 'pie', etc.
            data: [0,0,0,0,0,0]
        }
    ]
};


  constructor(private snackBar: MatSnackBar, private ProcurementService: ProcurementService, private router: Router, private confirmService: ConfirmService, public SharedService: SharedService, private authService : AuthService, private datePipe: DatePipe) {  super()}

  ngOnInit(): void {
    this.getAipPendingProcurements();
    this.getTenderOngoingProcurements();
    this.getTenderFloatingProcurements();
    this.getTenderOpeningProcurements();
    this.getOfferUnderEvulationProcurements();
    this.getPieChartData();
    this.getBarChartData();
  }

  
// -------------------- PieChart 1 --------------------------

  
  pieChartOptions = {
    title: {
      text: 'Loading...',  // Indicate that the chart is still loading
      left: 'center'
    },
    tooltip: {
      trigger: 'item'
    },
    legend: {
      orient: 'vertical',
      left: 'left'
    },
    series: [
      {
        name: '',
        type: 'pie',
        radius: '50%',
        data: [
          { value: 0, name: 'Total Procurement' }, 
          { value: 0, name: 'Contract Signed' }   
        ],
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }
    ]
  };
  

  getPieChartData() {
    this.ProcurementService.getPieChartData().subscribe(res => {
      if (res && res[0]) {
        this.pieChartData1 = res[0];
        this.pieChartData2 = res[1];
        this.updatePieChartOptions();
        this.updatePieChartOptions2();
      } else {
        console.error('Invalid data received');
      }
    }, error => {
      console.error('Error fetching pie chart data', error);
    });
  }

  updatePieChartOptions() {
      this.pieChartOptions = {
        title: {
          text: `${this.pieChartData1.financialYear}`,
          left: 'center'
        },
        tooltip: {
          trigger: 'item',
          formatter: function (params) {
            // Show only the label, not the count
            return params.name;
        }
        } as echarts.TooltipComponentOption,
        legend: {
          orient: 'vertical',
          left: 'left'
        },
        series: [
          {
            name: '',
            type: 'pie',
            radius: '50%',
            data: [
              { value: this.pieChartData1.totalProcurementCount || 0, name: `Total Procurement : ${this.pieChartData1.totalProcurementCount}` },
              { value: this.pieChartData1.totalContractSignedProcurementCount || 0, name: `Contract Signed : ${this.pieChartData1.totalContractSignedProcurementCount}` }
            ],
            emphasis: {
              itemStyle: {
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowColor: 'rgba(0, 0, 0, 0.5)'
              }
            }
          }
        ]
      };    
  }



  // -------------------- PieChart 2 --------------------------

  pieChartOptions2 = {
    title: {
      text: 'Loading...',  // Indicate that the chart is still loading
      left: 'center'
    },
    tooltip: {
      trigger: 'item'
    },
    legend: {
      orient: 'vertical',
      left: 'left'
    },
    series: [
      {
        name: '',
        type: 'pie',
        radius: '50%',
        data: [
          { value: 0, name: 'Total Procurement' }, 
          { value: 0, name: 'Contract Signed' }   
        ],
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }
    ]
  };
  


  updatePieChartOptions2() {
      this.pieChartOptions2 = {
        title: {
          text: `${this.pieChartData2.financialYear}`,
          left: 'center'
        },
        tooltip: {
          trigger: 'item',
          formatter: function (params) {
            // Show only the label, not the count
            return params.name;
        }
        } as echarts.TooltipComponentOption,
        legend: {
          orient: 'vertical',
          left: 'left'
        },
        series: [
          {
            name: '',
            type: 'pie',
            radius: '50%',
            data: [
              { value: this.pieChartData2.totalProcurementCount || 0, name: `Total Procurement : ${this.pieChartData2.totalProcurementCount}` },
              { value: this.pieChartData2.totalContractSignedProcurementCount || 0, name: `Contract Signed : ${this.pieChartData2.totalContractSignedProcurementCount}` }
            ],
            emphasis: {
              itemStyle: {
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowColor: 'rgba(0, 0, 0, 0.5)'
              }
            }
          }
        ]
      };    
  }


  //------------------ Bar Chart -----------------------

  getBarChartData() {
    this.ProcurementService.getBarChartData().subscribe(res => {
     this.barCharData = [res.sentAipCount, res.aipApprovedCount, res.indentCount, res.tenderFloatedCount, res.offerReceivedCount, res.countractSingCount];
     this.barCharHoverData = [res.sentAipItems, res.aipApprovedItems, res.indentItems, res.tenderFloatedItem, res.offerEvulationItems, res.contractSingedItems]

     console.log(this.barCharHoverData);
     this.updateBarChartOptions();
      
    }, error => {
      console.error('Error fetching pie chart data', error);
    });
  }


  updateBarChartOptions() {
    this.barChartOptions = {
        title: {
            text: 'Procurement Data'
        },
        tooltip: {
            trigger: 'axis',
            formatter: (params) => {
                let categoryName = params[0].name;
                let dataIndex = params[0].dataIndex; // Get the index of the hovered bar
                
                // Retrieve the corresponding equipment data from barCharHoverData
                let equipmentInfo = this.barCharHoverData?.[dataIndex]?.length
                    ? this.barCharHoverData[dataIndex].join(', ')
                    : 'No Data';

                return `${categoryName}<br/>
                        Total : ${params[0].value}<br/>
                        Equipment : ${equipmentInfo}`;
            }
        },
        xAxis: {
            data: ['Sent AIP', 'AIP Approved', 'Indent Date', 'Tender Floated Data', 'Offer R & E', 'Date Of Contact'],
            axisLabel: {
                rotate: 25,
                interval: 0,
                fontSize: 12,
            }
        },
        yAxis: {},
        series: [
            {
                name: 'Procurement',
                type: 'bar',
                data: this.barCharData
            }
        ]
    };
}

  
  
    //---------------- AIP Pending Procuremnt ---------------
  getAipPendingProcurements() {
    this.isLoading = true;
    this.ProcurementService.getAIPPendingProcurement(this.aipPendingpaging.pageIndex, this.aipPendingpaging.pageSize, this.aipPendingSearchText, this.orderBy, this.orderDirection).subscribe(response => {
      console.log(response)
      this.AipPending = response.items;      
      this.aipPendingpaging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;
    })
  } 
  
  aipPendingPageChanged(event: PageEvent) {
    this.aipPendingpaging.pageIndex = event.pageIndex
    this.aipPendingpaging.pageSize = event.pageSize
    this.aipPendingpaging.pageIndex = this.aipPendingpaging.pageIndex + 1
    this.getAipPendingProcurements();
  }

  aipPendingapplyFilter(searchText: any) {
    this.aipPendingSearchText = searchText;
    this.aipPendingpaging.pageIndex =  this.masterData.paging.pageIndex
    this.getAipPendingProcurements();
  }
  //---------------- TenderOnGoingrocurements ---------------
  getTenderOngoingProcurements() {
    this.isLoading = true;
    this.ProcurementService.getOngoingTenderSpecPreparationProcurement(this.onGoingTenderpaging.pageIndex, this.onGoingTenderpaging.pageSize, this.tenderOnGoingSearchText, this.orderBy, this.orderDirection).subscribe(response => {
      this.tenderOngoing = response.items;      
      this.onGoingTenderpaging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;
    })
  } 
  
  tenderOngoingPageChanged(event: PageEvent) {
    this.onGoingTenderpaging.pageIndex = event.pageIndex
    this.onGoingTenderpaging.pageSize = event.pageSize
    this.onGoingTenderpaging.pageIndex = this.onGoingTenderpaging.pageIndex + 1
    this.getTenderOngoingProcurements();
  }

  tenderOngoingapplyFilter(searchText: any) {
    this.tenderOnGoingSearchText = searchText.trim();
    this.onGoingTenderpaging.pageIndex =  this.masterData.paging.pageIndex
    this.getTenderOngoingProcurements();
  }

  //---------------- TenderFloatingProcurements ---------------

  getTenderFloatingProcurements() {
    this.isLoading = true;
    this.ProcurementService.getTenderFloatedProcurement(this.tenderFloatingpaging.pageIndex, this.tenderFloatingpaging.pageSize, this.tenderFloatingSearchText, this.orderBy, this.orderDirection).subscribe(response => {
      this.tenderFloating = response.items;      
      this.tenderFloatingpaging.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;
    })
  } 
  
  tenderFloatingPageChanged(event: PageEvent) {
    this.tenderFloatingpaging.pageIndex = event.pageIndex
    this.tenderFloatingpaging.pageSize = event.pageSize
    this.tenderFloatingpaging.pageIndex = this.tenderFloatingpaging.pageIndex + 1
    this.getTenderFloatingProcurements();
  }

  tenderFloatingapplyFilter(searchText: any) {
    this.tenderFloatingSearchText = searchText.trim();
    this.tenderFloatingpaging.pageIndex =  this.masterData.paging.pageIndex
    this.getTenderFloatingProcurements();
  }
  //---------------- Tender Opening Procurements ---------------

  getTenderOpeningProcurements() {
    this.isLoading = true;
    this.ProcurementService.getTenderOpeningProcurement(this.tenderOpeningPageing.pageIndex, this.tenderOpeningPageing.pageSize, this.tenderOpeningSearchText, this.orderBy, this.orderDirection).subscribe(response => {
      this.tenderOpening = response.items;      
      this.tenderOpeningPageing.length = response.totalItemsCount
      this.isLoading = false;
      this.itemCount = response.items.length;
    })
  } 
  
  tenderOpeningPageChanged(event: PageEvent) {
    this.tenderOpeningPageing.pageIndex = event.pageIndex
    this.tenderOpeningPageing.pageSize = event.pageSize
    this.tenderOpeningPageing.pageIndex = this.tenderOpeningPageing.pageIndex + 1
    this.getTenderOpeningProcurements();
  }

  tenderOpeningapplyFilter(searchText: any) {
    this.tenderOpeningSearchText = searchText.trim();
    this.tenderOpeningPageing.pageIndex =  this.masterData.paging.pageIndex
    this.getTenderOpeningProcurements();
  }

    //---------------- Offer Under Evalution ---------------

    getOfferUnderEvulationProcurements() {
      this.isLoading = true;
      this.ProcurementService.getOfferUnderEvulationProcurement(this.offerUnderEvaluationPageing.pageIndex, this.offerUnderEvaluationPageing.pageSize, this.offerUnderEvulationSearchText, this.orderBy, this.orderDirection).subscribe(response => {
        this.offerUnderEvaluation = response.items;      
        this.offerUnderEvaluationPageing.length = response.totalItemsCount
        this.isLoading = false;
        this.itemCount = response.items.length;
      })
    } 
    
    offerUnderEvulationPageChanged(event: PageEvent) {
      this.offerUnderEvaluationPageing.pageIndex = event.pageIndex
      this.offerUnderEvaluationPageing.pageSize = event.pageSize
      this.offerUnderEvaluationPageing.pageIndex = this.offerUnderEvaluationPageing.pageIndex + 1
      this.getOfferUnderEvulationProcurements();
    }
  
    offerUnderEvulationapplyFilter(searchText: any) {
      this.offerUnderEvulationSearchText = searchText.trim();
      this.offerUnderEvaluationPageing.pageIndex =  this.masterData.paging.pageIndex
      this.getOfferUnderEvulationProcurements();
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
            ? ` (${tenderDate.tenderOpeningCount})` : ''; 
          return date + count;
        }).join('<br>')
      : '-';
  }

  getLastTenderOpeningCount(tenderOpingdto: any[]): string {
    return tenderOpingdto && tenderOpingdto.length > 0
      ? tenderOpingdto[tenderOpingdto.length - 1].tenderOpeningCount || ''
      : '';
  }

  getLastTenderOpeningDate(tenderOpingdto: any[]): string {
    if (tenderOpingdto && tenderOpingdto.length > 0) {
      const lastTenderDate = tenderOpingdto[tenderOpingdto.length - 1].tenderOpeningDate;
      return this.datePipe.transform(lastTenderDate, 'dd MMM, yyyy') || '';
    }
    return '';
  }
  
  
  

}

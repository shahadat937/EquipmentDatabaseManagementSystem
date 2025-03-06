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

interface Paging {
  pageIndex: number;
  pageSize: number;
  length: number;
}

@Component({
  selector: 'app-procurement-dashboard',
  templateUrl: './procurement-dashboard.component.html',
  styleUrls: ['./procurement-dashboard.component.sass']
})



export class ProcurementDashboardComponent  implements OnInit {
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

  constructor(private snackBar: MatSnackBar, private ProcurementService: ProcurementService, private router: Router, private confirmService: ConfirmService, public SharedService: SharedService, private authService : AuthService, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.getAipPendingProcurements();
    this.getTenderOngoingProcurements();
    this.getTenderFloatingProcurements();
    this.getTenderOpeningProcurements();
    this.getOfferUnderEvulationProcurements();
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
  

}

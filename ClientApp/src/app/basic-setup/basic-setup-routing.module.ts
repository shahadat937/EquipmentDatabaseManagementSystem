import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
// import { BaseNameListComponent } from './basename/basename-list/basename-list.component';
// import { NewBaseNameComponent } from './basename/new-basename/new-basename.component';
import {NewSqnComponent} from './sqn/new-sqn/new-sqn.component'
import {SqnListComponent} from './sqn/sqn-list/sqn-list.component'
import {NewOperationalStatusComponent} from './operationalstatus/new-operationalstatus/new-operationalstatus.component';
import {OperationalStatusListComponent} from './operationalstatus/operationalstatus-list/operationalstatus-list.component'
import {NewShipTypeComponent} from './shiptype/new-shiptype/new-shiptype.component'
import {StateOfEquipmentListComponent} from './stateofequipment/stateofequipment-list/stateofequipment-list.component';
import {NewStateOfEquipmentComponent} from './stateofequipment/new-stateofequipment/new-stateofequipment.component'
import {GroupNameListComponent}  from './groupname/groupname-list/groupname-list.component';
import {NewGroupNameComponent} from './groupname/new-groupname/new-groupname.component';
import {NewTenderOpeningDateTypeComponent} from './tenderopeningdatetype/new-tenderopeningdatetype/new-tenderopeningdatetype.component'
import {TenderOpeningDateTypeListComponent} from './tenderopeningdatetype/tenderopeningdatetype-list/tenderopeningdatetype-list.component'
import {NewLetterTypeComponent} from './lettertype/new-lettertype/new-lettertype.component'
import { NewReportTypeComponent } from './reporttype/new-reporttype/new-reporttype.component';
import { NewProjectStatusComponent } from './projectstatus/new-projectstatus/new-projectstatus.component';
import { NewProcurementTypeComponent } from './procurementtype/new-procurementtype/new-procurementtype.component';
import { NewDealingOfficerComponent } from './dealingofficer/new-dealingofficer/new-dealingofficer.component';
import { NewDgdpNssdComponent } from './dgdpnssd/new-dgdpnssd/new-dgdpnssd.component';
import { NewFcLcComponent } from './fclc/new-fclc/new-fclc.component';
import { NewControlledComponent } from './controlled/new-controlled/new-controlled.component';
import { NewEquipmentCategoryComponent } from './equipmentcategory/new-equipmentcategory/new-equipmentcategory.component';
import { NewEqupmentNameComponent } from './equpmentname/new-equpmentname/new-equpmentname.component';
import {NewShipDrowingComponent} from './shipdrowing/new-shipdrowing/new-shipdrowing.component';
import {NewBrandComponent} from './brand/new-brand/new-brand.component';
import {NewAcquisitionMethodComponent} from './acquisition-method/new-acquisition-method/new-acquisition-method.component'
import { NewRunningTimeComponent } from './runningtime/new-runningtime/new-runningtime.component';
import { NewQuarterlyReturnNoTypeComponent } from './quarterlyreturnnotype/new-quarterlyreturnnotype/new-quarterlyreturnnotype.component';
import {ReportingMonthComponent} from './reportingmonth/new-reportingmonth/new-reportingmonth.component';
import {ReturnTypeComponent} from './returntype/new-returntype/new-returntype.component';
import { NewTecComponent } from './tec/new-tec/new-tec.component';
import { NewProcurementMethodComponent } from './procurementmethod/new-procurementmethod/new-procurementmethod.component';
import { NewPaymentStatusComponent } from './paymentstatus/new-paymentstatus/new-paymentstatus.component';
import { NewEnvelopeComponent } from './envelope/new-envelope/new-envelope.component';
import { NewBookTypeComponent } from './booktype/new-booktype/new-booktype.component';
import {NewSchoolNameComponent} from './new-schoolname/new-schoolname.component'
import { ReportingYearComponent } from './reporting-year/reporting-year.component';
import { NewCommendingAreaComponent } from './new-commendingarea/new-commendingarea.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  // {
  //   path: 'add-district',
  //   component: NewDistrictComponent,
  // },
  {
    path: 'add-shipdrowing',
    component: NewShipDrowingComponent,
  },
  {
    path: 'update-shipdrowing/:shipDrowingId',
    component: NewShipDrowingComponent,
  },

  {
    path: 'add-reportingmonth',
    component: ReportingMonthComponent,
  },
  {
    path: 'update-reportingmonth/:reportingMonthId',
    component: ReportingMonthComponent,
  },
  {
    path: 'add-reportingyear',
    component: ReportingYearComponent,
  },
  {
    path: 'update-reportingyear/:reportingYearId',
    component: ReportingYearComponent,
  },

  {
    path: 'add-returntype',
    component: ReturnTypeComponent,
  },
  {
    path: 'update-returntype/:returnTypeId',
    component: ReturnTypeComponent,
  },

  // {
  //   path: 'basename-list',
  //   component: BaseNameListComponent,
  // },
  // { path: 'update-basename/:baseNameId', 
  // component: NewBaseNameComponent, 
  // },
  // {
  //   path: 'add-basename',
  //   component: NewBaseNameComponent,
  // },

  {
    path: 'sqn-list',
    component: SqnListComponent,
  },
  { path: 'update-sqn/:sqnId', 
  component: NewSqnComponent, 
  },
  {
    path: 'add-sqn',
    component: NewSqnComponent,
  },

  
  {
    path: 'operationalstatus-list',
    component: OperationalStatusListComponent,
  },
  { path: 'update-operationalstatus/:operationalStatusId', 
  component: NewOperationalStatusComponent, 
  },
  {
    path: 'add-operationalstatus',
    component: NewOperationalStatusComponent,
  },

  { path: 'update-shiptype/:shipTypeId', 
  component: NewShipTypeComponent, 
  },
  {
    path: 'add-shiptype',
    component: NewShipTypeComponent,
  },

  {
    path: 'stateofequipment-list',
    component: StateOfEquipmentListComponent,
  },
  { path: 'update-stateofequipment/:stateOfEquipmentId', 
  component: NewStateOfEquipmentComponent, 
  },
  {
    path: 'add-stateofequipment',
    component: NewStateOfEquipmentComponent,
  },
  
  {
    path: 'groupname-list',
    component: GroupNameListComponent,
  },
  { path: 'update-groupname/:groupNameId', 
  component: NewGroupNameComponent, 
  },
  {
    path: 'add-groupname',
    component: NewGroupNameComponent,
  },
  {
    path: 'tenderopeningdatetype-list',
    component: TenderOpeningDateTypeListComponent,
  },
  { path: 'update-tenderopeningdatetype/:tenderOpeningDateTypeId', 
  component: NewTenderOpeningDateTypeComponent, 
  },
  {
    path: 'add-tenderopeningdatetype',
    component: NewTenderOpeningDateTypeComponent,
  },
  {
    path: 'add-lettertype',
    component: NewLetterTypeComponent,
  },
  {
    path: 'update-lettertype/:letterTypeId',
    component: NewLetterTypeComponent,
  },
  {
    path: 'add-reporttype',
    component: NewReportTypeComponent,
  },
  {
    path: 'update-reporttype/:reportTypeId',
    component: NewReportTypeComponent,
  },
  {
    path: 'add-projectstatus',
    component: NewProjectStatusComponent,
  },
  {
    path: 'update-projectstatus/:projectStatusId',
    component: NewProjectStatusComponent,
  },
  {
    path: 'add-procurementtype',
    component: NewProcurementTypeComponent,
  },
  {
    path: 'update-procurementtype/:procurementTypeId',
    component: NewProcurementTypeComponent,
  },
  {
    path: 'add-dealingofficer',
    component: NewDealingOfficerComponent,
  },
  {
    path: 'update-dealingofficer/:dealingOfficerId',
    component: NewDealingOfficerComponent,
  },
  {
    path: 'add-dgdpnssd',
    component: NewDgdpNssdComponent,
  },
  {
    path: 'update-dgdpnssd/:dgdpNssdId',
    component: NewDgdpNssdComponent,
  },
  {
    path: 'add-fclc',
    component: NewFcLcComponent,
  },
  {
    path: 'update-fclc/:fcLcId',
    component: NewFcLcComponent,
  },
    {
      path: 'new-commandingarea',
      component: NewCommendingAreaComponent,
    },
  
    {
      path: 'update-commandingarea/:baseSchoolNameId',
      component: NewCommendingAreaComponent,
    },
  {
    path: 'add-controlled',
    component: NewControlledComponent,
  },
  {
    path: 'update-controlled/:controlledId',
    component: NewControlledComponent,
  },
  {
    path: 'add-equipmentcategory',
    component: NewEquipmentCategoryComponent,
  },
  {
    path: 'update-equipmentcategory/:equipmentCategoryId',
    component: NewEquipmentCategoryComponent,
  },
  {
    path: 'add-equpmentname',
    component: NewEqupmentNameComponent,
  },
  {
    path: 'update-equpmentname/:equpmentNameId',
    component: NewEqupmentNameComponent,
  },
  {
    path: 'add-runningtime',
    component: NewRunningTimeComponent,
  },
  {
    path: 'update-runningtime/:runningTimeId',
    component: NewRunningTimeComponent,
  },
  {
    path: 'add-quarterlyreturnnotype',
    component: NewQuarterlyReturnNoTypeComponent,
  },
  {
    path: 'update-quarterlyreturnnotype/:quarterlyReturnNoTypeId',
    component: NewQuarterlyReturnNoTypeComponent,
  },

  { path: 'update-brand/:brandId', 
  component: NewBrandComponent, 
  },
  {
    path: 'add-brand',
    component: NewBrandComponent,
  },
  
  { path: 'update-acquisitionmethod/:acquisitionMethodId', 
  component: NewAcquisitionMethodComponent, 
  },
  {
    path: 'add-acquisitionmethod',
    component: NewAcquisitionMethodComponent,
  },
  { path: 'update-tec/:tecId', 
  component: NewTecComponent, 
  },
  {
    path: 'add-tec',
    component: NewTecComponent,
  },
  { path: 'update-procurementmethod/:procurementMethodId', 
  component: NewProcurementMethodComponent, 
  },
  {
    path: 'add-procurementmethod',
    component: NewProcurementMethodComponent,
  },

  { path: 'update-paymentstatus/:paymentStatusId', 
  component: NewPaymentStatusComponent, 
  },
  {
    path: 'add-paymentstatus',
    component: NewPaymentStatusComponent,
  },
  { path: 'update-envelope/:envelopeId', 
  component: NewEnvelopeComponent, 
  },
  {
    path: 'add-envelope',
    component: NewEnvelopeComponent,
  },
  { path: 'update-booktype/:bookTypeId', 
  component: NewBookTypeComponent, 
  },
  {
    path: 'add-booktype',
    component: NewBookTypeComponent,
  },
  {
    path: 'new-baseschool',
    component: NewSchoolNameComponent,
  },

  {
    path: 'update-baseschool/:baseSchoolNameId',
    component: NewSchoolNameComponent,
  },
  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class BasicSetupRoutingModule { }

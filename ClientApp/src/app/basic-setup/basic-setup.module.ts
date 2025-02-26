import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { BasicSetupRoutingModule } from './basic-setup-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
// import { BaseNameListComponent } from './basename/basename-list/basename-list.component';
// import { NewBaseNameComponent } from './basename/new-basename/new-basename.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
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
import {NewBrandComponent} from './brand/new-brand/new-brand.component'
import {NewAcquisitionMethodComponent} from './acquisition-method/new-acquisition-method/new-acquisition-method.component'
import { NewRunningTimeComponent } from './runningtime/new-runningtime/new-runningtime.component';
import { NewQuarterlyReturnNoTypeComponent } from './quarterlyreturnnotype/new-quarterlyreturnnotype/new-quarterlyreturnnotype.component';
import {ReportingMonthComponent} from './reportingmonth/new-reportingmonth/new-reportingmonth.component'
import {ReturnTypeComponent} from './returntype/new-returntype/new-returntype.component';
import { NewTecComponent } from './tec/new-tec/new-tec.component';
import { NewProcurementMethodComponent } from './procurementmethod/new-procurementmethod/new-procurementmethod.component';
import { NewPaymentStatusComponent } from './paymentstatus/new-paymentstatus/new-paymentstatus.component';
import { NewEnvelopeComponent } from './envelope/new-envelope/new-envelope.component';
import { NewBookTypeComponent } from './booktype/new-booktype/new-booktype.component';
import { NewSchoolNameComponent } from './new-schoolname/new-schoolname.component';
import { ReportingYearComponent } from './reporting-year/reporting-year.component';
import { NewCommendingAreaComponent } from './new-commendingarea/new-commendingarea.component';
// import {NewSchoolNameComponent} from './new-schoolname/new-schoolname.component'

@NgModule({
  declarations: [
    NewBrandComponent,
    ReturnTypeComponent,
    NewBookTypeComponent,
    // NewBaseNameComponent,
    // BaseNameListComponent,
    // NewBaseNameComponent,
    ReportingMonthComponent,
    NewSqnComponent,
    SqnListComponent,
    NewOperationalStatusComponent,
    OperationalStatusListComponent,
    NewShipTypeComponent,
    StateOfEquipmentListComponent,
    NewStateOfEquipmentComponent,
    GroupNameListComponent,
    NewGroupNameComponent,
    NewTenderOpeningDateTypeComponent,
    TenderOpeningDateTypeListComponent,
    NewLetterTypeComponent,
    NewReportTypeComponent,
    NewProjectStatusComponent,
    NewProcurementTypeComponent,
    NewDealingOfficerComponent,
    NewDgdpNssdComponent,
    NewFcLcComponent,
    NewControlledComponent,
    NewEquipmentCategoryComponent,
    NewEqupmentNameComponent,
    NewShipDrowingComponent,
    NewAcquisitionMethodComponent,
    NewRunningTimeComponent,
    NewQuarterlyReturnNoTypeComponent,
    NewTecComponent,
    NewProcurementMethodComponent,
    NewPaymentStatusComponent,
    NewEnvelopeComponent,
    NewSchoolNameComponent,
    ReportingYearComponent,
    NewCommendingAreaComponent

  ],
  imports: [
    CommonModule,
    BasicSetupRoutingModule,
    CommonModule,
    FormsModule,  
    ReactiveFormsModule,
    NgxDatatableModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatStepperModule,
    MatSnackBarModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MaterialFileInputModule,
    MatProgressSpinnerModule,
    HttpClientModule,
    MatAutocompleteModule
    
  ]
})
export class BasicSetupModule { }

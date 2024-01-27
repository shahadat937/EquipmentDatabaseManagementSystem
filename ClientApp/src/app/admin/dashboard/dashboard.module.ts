import { NgModule } from '@angular/core';
import { NgMarqueeModule } from 'ng-marquee';
import { CommonModule } from '@angular/common';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { MainComponent } from './main/main.component';
import { NgxEchartsModule } from 'ngx-echarts';
import { NgApexchartsModule } from 'ng-apexcharts';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FullCalendarModule } from "@fullcalendar/angular";
import { MatMenuModule } from '@angular/material/menu';
import {RouterModule} from '@angular/router';
// import { LocalcourseListComponent } from './localcourse/localcourse-list/localcourse-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MaterialFileInputModule } from 'ngx-material-file-input';
// import { CentralExamNominatedListComponent } from './centralexamnominated-list/centralexamnominated-list.component';

import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import listPlugin from "@fullcalendar/list";
import interactionPlugin from "@fullcalendar/interaction";
import {ShipInformationByTypeListComponent} from './shipinformationbytype-list/shipinformationbytype-list.component'
import {BoatInformationByTypeListComponent} from './boatinformationbytype-list/boatinformationbytype-list.component'
import {EstablishmentByTypeListComponent} from './establishmentbytype-list/establishmentbytype-list.component'
import {ViewAllShipInfoByBaseListComponent} from './view-allshipinfobybaselist/view-allshipinfobybaselist.component'
import {ViewShipDrowingListComponent} from './view-shipdrowinglist/view-shipdrowinglist.component'
import {ViewShipInformationDetailsComponent} from './view-shipinformationdetails/view-shipinformationdetails.component'


FullCalendarModule.registerPlugins([
  dayGridPlugin,
  timeGridPlugin,
  listPlugin,
  interactionPlugin,
]);

@NgModule({
  declarations: [
    MainComponent,
    ShipInformationByTypeListComponent,
    BoatInformationByTypeListComponent,
    EstablishmentByTypeListComponent,
    ViewAllShipInfoByBaseListComponent,
    ViewShipDrowingListComponent,
    ViewShipInformationDetailsComponent
    // LocalcourseListComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    DashboardRoutingModule,
    NgxEchartsModule.forRoot({
      echarts: () => import('echarts'),
    }),
    PerfectScrollbarModule,
    MatIconModule,
    NgApexchartsModule,
    MatButtonModule,
    MatMenuModule,
    CommonModule,
    CommonModule,
    FormsModule,
    FullCalendarModule,  
    ReactiveFormsModule,
    NgxDatatableModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatStepperModule,
    MatSnackBarModule,
    MatSelectModule,
    MatDatepickerModule,
    MaterialFileInputModule,
    NgMarqueeModule 
  ],
})
export class DashboardModule {}

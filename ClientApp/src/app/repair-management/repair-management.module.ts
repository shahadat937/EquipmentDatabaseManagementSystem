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
import {RepairManagementRoutingModule} from './repair-management-routing.module'
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import {StatusOfDefectiveSystemListComponent} from './statusofdefectivesystem/statusofdefectivesystem-list/statusofdefectivesystem-list.component';
import {NewStatusOfDefectiveSystemComponent} from './statusofdefectivesystem/new-statusofdefectivesystem/new-statusofdefectivesystem.component'
import { NewOverallStatusofShip } from './overallstatusofship/new-statusofship/new-overallstatusofship.component';
import { OverallShipStatusListComponent } from './overallstatusofship/statusofship-list/overallstatusofship-list.component';
import { NewOperationalStatusOfEquipmentSystemComponent } from './operational-status-equipment-system/new-operational-status-of-equipment-system/new-operational-status-of-equipment-system.component';
import { OperationalStatusOfEquipmentSystemListComponent } from './operational-status-equipment-system/operational-status-of-equipment-system-list/operational-status-of-equipment-system-list.component';


@NgModule({
  declarations: [
    StatusOfDefectiveSystemListComponent,
    NewStatusOfDefectiveSystemComponent,
    NewOverallStatusofShip,
    OverallShipStatusListComponent,
    NewOperationalStatusOfEquipmentSystemComponent,
    OperationalStatusOfEquipmentSystemListComponent
  ],
  imports: [
    CommonModule,
    RepairManagementRoutingModule,
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
export class RepairManagementModule { }

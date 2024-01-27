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
import { ShipManagementRoutingModule } from './ship-management-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import {NewShipInformationComponent} from './shipinformation/new-shipinformation/new-shipinformation.component'
import {ShipInformationListComponent} from './shipinformation/shipinformation-list/shipinformation-list.component'
import { ShipEquipmentInfoListComponent } from './shipequipmentinfo/shipequipmentinfo-list/shipequipmentinfo-list.component';
import { NewShipEquipmentInfoComponent } from './shipequipmentinfo/new-shipequipmentinfo/new-shipequipmentinfo.component';



@NgModule({
  declarations: [
    NewShipInformationComponent,
    ShipInformationListComponent,
    ShipEquipmentInfoListComponent,
    NewShipEquipmentInfoComponent,
  ],
  imports: [
    CommonModule,
    ShipManagementRoutingModule,
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
export class ShipManagementModule { }

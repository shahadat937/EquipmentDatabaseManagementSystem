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
import { ShipReturnRoutingModule } from './ships-return-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import {NewMonthlyReturnComponent} from '../../app/ships-return/monthlyreturn/new-monthlyreturn/new-monthlyreturn.component'
import {MonthlyReturnListComponent} from '../../app/ships-return/monthlyreturn/monthlyreturn-list/monthlyreturn-list.component'
import { HalfYearlyReturnListComponent } from './halfyearlyreturn/halfyearlyreturn-list/halfyearlyreturn-list.component';
import { NewHalfYearlyReturnComponent } from './halfyearlyreturn/new-halfyearlyreturn/new-halfyearlyreturn.component';
import { NewYearlyRetrunComponent } from './YearlyReturn/yearlyreturnlist/yearlyreturn-list.component';
import { YearlyRetrunComponent } from './YearlyReturn/new-yearlyreturn/new-yearlyreturn.component';

@NgModule({
  declarations: [
    NewMonthlyReturnComponent,
    MonthlyReturnListComponent,
    HalfYearlyReturnListComponent,
    NewHalfYearlyReturnComponent,
    NewYearlyRetrunComponent,
    YearlyRetrunComponent,
    // ShipEquipmentInfoListComponent,
    // NewShipEquipmentInfoComponent,
  ],
  imports: [
    CommonModule,
    ShipReturnRoutingModule,
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
export class ShipsReturnModule { }

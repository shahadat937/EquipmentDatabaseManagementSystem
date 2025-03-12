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
import { ProcurementManagementRoutingModule } from './procurement-management-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { ProcurementListComponent } from './procurement/procurement-list/procurement-list.component';
import { NewProcurementComponent } from './procurement/new-procurement/new-procurement.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ProcurementDashboardComponent } from './procurement-dashboard/procurement-dashboard.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { ChartsModule } from 'ng2-charts';
import { NgxEchartsDirective } from 'ngx-echarts';
import { NgxEchartsModule } from 'ngx-echarts';
import { NewProjectComponent } from './Project/new-project/new-project.component';
import { ProjectListComponent } from './Project/project-list/project-list.component';
import { BudgetListComponent } from './budget/budget-list/budget-list.component';
import { NewBudgetComponent } from './budget/new-budget/new-budget.component';


@NgModule({
  declarations: [
     ProcurementListComponent,
     NewProcurementComponent,
     ProcurementDashboardComponent,
     NewProjectComponent,
     ProjectListComponent,
     BudgetListComponent,
     NewBudgetComponent,
  ],
  imports: [
    CommonModule,
    ProcurementManagementRoutingModule,
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
   MatAutocompleteModule,
   MatCheckboxModule,
   NgxChartsModule,
   ChartsModule,
   NgxEchartsModule.forRoot({
    echarts: () => import('echarts') // Lazy-load ECharts
  })
    
  ]
})
export class ProcurementManagementModule { }

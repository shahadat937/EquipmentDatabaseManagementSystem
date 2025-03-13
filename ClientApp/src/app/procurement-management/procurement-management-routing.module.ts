import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { ProcurementListComponent } from './procurement/procurement-list/procurement-list.component';
import { NewProcurementComponent } from './procurement/new-procurement/new-procurement.component';
import { ProcurementDashboardComponent } from './procurement-dashboard/procurement-dashboard.component';
import { BudgetListComponent } from './budget/budget-list/budget-list.component';
import { NewBudgetComponent } from './budget/new-budget/new-budget.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  {
    path: 'procurement-dashboard',
    component: ProcurementDashboardComponent,
  },
  {
    path: 'procurement-list',
    component: ProcurementListComponent,
  },
  { path: 'update-procurement/:procurementId', 
  component: NewProcurementComponent, 
  },
  {
    path: 'add-procurement',
    component: NewProcurementComponent,
  },
  {
    path: 'budget-list',
    component: BudgetListComponent,
  },
  { path: 'update-budget/:budgetId', 
  component: NewBudgetComponent, 
  },
  {
    path: 'add-budget',
    component: NewBudgetComponent,
  },
  


  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class ProcurementManagementRoutingModule { }

import { NewQuarterlyReturnComponent } from './QuarterlyReturn/new-quarterlyreturn/new-quarterlyreturn.component';
import { QuarterlyRetrunComponent } from './QuarterlyReturn/quarterlyreturn-list/quarterlyreturn.component';

import { YearlyReturnComponent } from './YearlyReturn/new-yearlyreturn/new-yearlyreturn.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import {NewMonthlyReturnComponent} from '../../app/ships-return/monthlyreturn/new-monthlyreturn/new-monthlyreturn.component'
import {MonthlyReturnListComponent} from '../../app/ships-return/monthlyreturn/monthlyreturn-list/monthlyreturn-list.component'
import { HalfYearlyReturnListComponent } from './halfyearlyreturn/halfyearlyreturn-list/halfyearlyreturn-list.component';
import { NewHalfYearlyReturnComponent } from './halfyearlyreturn/new-halfyearlyreturn/new-halfyearlyreturn.component';
import {NewYearlyRetrunComponent} from './YearlyReturn/yearlyreturnlist/yearlyreturn-list.component'
const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  {
    path: 'monthlyreturn-list',
    component: MonthlyReturnListComponent,
  },
  { path: 'update-monthly/:damageElectricalId', 
  component: NewMonthlyReturnComponent, 
  },
  {
    path: 'add-monthly',
    component: NewMonthlyReturnComponent,
  },

  {
    path: 'halfyearlyreturn-list',
    component: HalfYearlyReturnListComponent,
  },
  { path: 'update-halfyearlyreturn/:halfYearlyReturnId', 
  component: NewHalfYearlyReturnComponent, 
  },
  {
    path: 'add-halfyearlyreturn',
    component: NewHalfYearlyReturnComponent,
  },
  {
    path: 'yearlyreturn-list',
    component: NewYearlyRetrunComponent,
  },
  {
    path: 'add-yearlyreturn',
    component: YearlyReturnComponent,
  },
  {
    path: 'update-yearlyreturn/:yearlyReturnId',
    component: YearlyReturnComponent,
  },
  {
    path: 'quarterly-return',
    component: QuarterlyRetrunComponent,
  },
  {
    path: 'add-quarterlyreturn',
    component: NewQuarterlyReturnComponent
  },

  {
    path: 'update-quaterlyreturn/:quarterlyReturnId',
    component: NewQuarterlyReturnComponent,
  },

  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class ShipReturnRoutingModule { }

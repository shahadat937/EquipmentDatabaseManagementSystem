import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { DailyWorkStateListComponent } from './dailyworkstate/dailyworkstate-list/dailyworkstate-list.component';
import { NewDailyWorkStateComponent } from './dailyworkstate/new-dailyworkstate/new-dailyworkstate.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  {
    path: 'dailyworkstate-list',
    component: DailyWorkStateListComponent,
  },
  { path: 'update-dailyworkstate/:dailyWorkStateId', 
  component: NewDailyWorkStateComponent, 
  },
  {
    path: 'add-dailyworkstate',
    component: NewDailyWorkStateComponent,
  },
  


  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class DailyWorkManagementRoutingModule { }

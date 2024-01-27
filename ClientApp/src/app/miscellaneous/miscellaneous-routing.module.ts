import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { NewBookUserManualBRInfoComponent } from './bookusermanualbrinfo/new-bookusermanualbrinfo/new-bookusermanualbrinfo.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  // {
  //   path: 'monthlyreturn-list',
  //   component: MonthlyReturnListComponent,
  // },
  { path: 'update-bookusermanualbrinfo/:bookUserManualBRInfoId', 
  component: NewBookUserManualBRInfoComponent, 
  },
  {
    path: 'add-bookusermanualbrinfo',
    component: NewBookUserManualBRInfoComponent,
  },


  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class MiscellaneousRoutingModule { }

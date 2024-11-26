import { OverallShipStatusListComponent } from './overallstatusofship/statusofship-list/overallstatusofship-list.component';
import { NewOverallStatusofShip } from './overallstatusofship/new-statusofship/new-overallstatusofship.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import {StatusOfDefectiveSystemListComponent} from './statusofdefectivesystem/statusofdefectivesystem-list/statusofdefectivesystem-list.component';
import {NewStatusOfDefectiveSystemComponent} from './statusofdefectivesystem/new-statusofdefectivesystem/new-statusofdefectivesystem.component'
const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  {
    path: 'statusofdefectivesystem-list',
    component: StatusOfDefectiveSystemListComponent,
  },
  { path: 'update-statusofdefectivesystem/:operationalStateId', 
  component: NewStatusOfDefectiveSystemComponent, 
  },
  {
    path: 'add-statusofdefectivesystem',
    component: NewStatusOfDefectiveSystemComponent,
  },
  {
    path: 'add-overallstatusofship',
    component: NewOverallStatusofShip,
  },
 {
    path: 'overallstatusofship-list',
    component: OverallShipStatusListComponent,
 },
 {
  path: 'update-StatusOfShips/:statusOfShipId',
  component: NewOverallStatusofShip,
},


  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class RepairManagementRoutingModule { }

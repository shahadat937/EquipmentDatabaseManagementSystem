import { OverallShipStatusListComponent } from './overallstatusofship/statusofship-list/overallstatusofship-list.component';
import { NewOverallStatusofShip } from './overallstatusofship/new-statusofship/new-overallstatusofship.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import {StatusOfDefectiveSystemListComponent} from './statusofdefectivesystem/statusofdefectivesystem-list/statusofdefectivesystem-list.component';
import {NewStatusOfDefectiveSystemComponent} from './statusofdefectivesystem/new-statusofdefectivesystem/new-statusofdefectivesystem.component'
import {NewOperationalStatusOfEquipmentSystemComponent} from './operational-status-equipment-system/new-operational-status-of-equipment-system/new-operational-status-of-equipment-system.component'
import {OperationalStatusOfEquipmentSystemListComponent} from './operational-status-equipment-system/operational-status-of-equipment-system-list/operational-status-of-equipment-system-list.component'
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
{
  path: 'add-operational-status-of-equipment-system',
  component: NewOperationalStatusOfEquipmentSystemComponent
},
{
  path: 'operational-status-of-equipment-system-list',
  component: OperationalStatusOfEquipmentSystemListComponent
},
{
  path: 'add-overall-operational-status-of-equipment-system-list',
  component: NewOperationalStatusOfEquipmentSystemComponent
},
{
  path: 'update-overall-operational-status-of-equipment-system-list/:operationalStatusOfEquipmentSystemId',
  component: NewOperationalStatusOfEquipmentSystemComponent
},


  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class RepairManagementRoutingModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import {NewShipInformationComponent} from './shipinformation/new-shipinformation/new-shipinformation.component'
import {ShipInformationListComponent} from './shipinformation/shipinformation-list/shipinformation-list.component'
import { ShipEquipmentInfoListComponent } from './shipequipmentinfo/shipequipmentinfo-list/shipequipmentinfo-list.component';
import { NewShipEquipmentInfoComponent } from './shipequipmentinfo/new-shipequipmentinfo/new-shipequipmentinfo.component';
import { OplNonoplEquipmentComponent } from './opl-nonopl-equipment/opl-nonopl-equipment.component';
// import { NewOperationalStatusOfEquipmentSystemComponent } from './operational-status-equipment-system/new-operational-status-of-equipment-system/new-operational-status-of-equipment-system.component';
// import { OperationalStatusOfEquipmentSystemListComponent } from './operational-status-equipment-system/operational-status-of-equipment-system-list/operational-status-of-equipment-system-list.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  {
    path: 'shipinformation-list',
    component: ShipInformationListComponent,
  },
  { path: 'update-shipinformation/:shipInformationId', 
  component: NewShipInformationComponent, 
  },
  {
    path: 'add-shipinformation',
    component: NewShipInformationComponent,
  },
  {
    path: 'shipequipmentinfo-list',
    component: ShipEquipmentInfoListComponent,
  },
  {
    path: 'shipequipmentinfo-list/:shipequipmentCategoryId/:stateOfEquipmentId',
    component: ShipEquipmentInfoListComponent,
  },
  {
    path: 'opl-nonopl-shipequipmentinfo-list/:shipequipmentCategoryId/:status',
    component: OplNonoplEquipmentComponent,
  },
  {
    path: 'opl-nonopl-shipequipmentinfo-list/:shipequipmentCategoryId/:equipmentNameId/:status',
    component: OplNonoplEquipmentComponent,
  },
  {
    path: 'shipequipmentinfo-list/:shipequipmentCategoryId/:equipmentNameId/:stateOfEquipmentId',
    component: ShipEquipmentInfoListComponent,
  },
  { path: 'update-shipequipmentinfo/:shipEquipmentInfoId', 
  component: NewShipEquipmentInfoComponent, 
  },
  {
    path: 'add-shipequipmentinfo',
    component: NewShipEquipmentInfoComponent,
  },
  // {
  //   path: 'add-operational-status-of-equipment-system',
  //   component: NewOperationalStatusOfEquipmentSystemComponent
  // },
  // {
  //   path: 'operational-status-of-equipment-system-list',
  //   component: OperationalStatusOfEquipmentSystemListComponent
  // },
  // {
  //   path: 'add-overall-operational-status-of-equipment-system-list',
  //   component: NewOperationalStatusOfEquipmentSystemComponent
  // },
  // {
  //   path: 'update-overall-operational-status-of-equipment-system-list/:operationalStatusOfEquipmentSystemId',
  //   component: NewOperationalStatusOfEquipmentSystemComponent
  // },


  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class ShipManagementRoutingModule { }

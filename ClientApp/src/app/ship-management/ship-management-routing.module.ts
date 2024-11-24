import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import {NewShipInformationComponent} from './shipinformation/new-shipinformation/new-shipinformation.component'
import {ShipInformationListComponent} from './shipinformation/shipinformation-list/shipinformation-list.component'
import { ShipEquipmentInfoListComponent } from './shipequipmentinfo/shipequipmentinfo-list/shipequipmentinfo-list.component';
import { NewShipEquipmentInfoComponent } from './shipequipmentinfo/new-shipequipmentinfo/new-shipequipmentinfo.component';

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


  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class ShipManagementRoutingModule { }

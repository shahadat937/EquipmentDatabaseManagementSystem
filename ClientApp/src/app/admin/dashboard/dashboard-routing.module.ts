import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main/main.component';
import { DashboardComponent as shipDashboard } from './../../ship/dashboard/dashboard.component';
import {ShipInformationByTypeListComponent} from './shipinformationbytype-list/shipinformationbytype-list.component'
import {BoatInformationByTypeListComponent} from './boatinformationbytype-list/boatinformationbytype-list.component'
import {EstablishmentByTypeListComponent} from './establishmentbytype-list/establishmentbytype-list.component'
import {ViewAllShipInfoByBaseListComponent} from './view-allshipinfobybaselist/view-allshipinfobybaselist.component'
import {ViewShipDrowingListComponent} from './view-shipdrowinglist/view-shipdrowinglist.component'
import {ViewShipInformationDetailsComponent} from './view-shipinformationdetails/view-shipinformationdetails.component'

const routes: Routes = [
  {
    path: '',
    redirectTo: 'main',
    pathMatch: 'full',
  },
  {
    path: 'main',
    component: MainComponent,
  },

//   {
//     path: 'passwordupdate-student',
//     component: NewPasswordChangeComponent,
//   },BoatInformationByTypeListComponent
{
  path: 'shipinformationbytype-list',
  component: ShipInformationByTypeListComponent,
},

{
  path: 'boatinformationbytype-list',
  component: BoatInformationByTypeListComponent,
},
{
  path: 'establishmentbytype-list',
  component: EstablishmentByTypeListComponent,
},
{
  path: 'view-allshipinfobybaselist/:baseSchoolNameId/:operationalStatusId',
  component: ViewAllShipInfoByBaseListComponent,
},
{

  path: 'ship-dashboard',
  component: shipDashboard,
},
{
  path: 'viewshipdrowing-list',
  component: ViewShipDrowingListComponent,
},

{
  path: 'view-shipinformationdetails/:shipInformationId/:dbType',
  component: ViewShipInformationDetailsComponent,
},


];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}

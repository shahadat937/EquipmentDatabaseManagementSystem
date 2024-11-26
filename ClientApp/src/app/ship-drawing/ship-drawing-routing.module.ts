import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewShipDrawingComponent } from './new-ship-drawing/new-ship-drawing.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },
  
  {
    path: 'add-shipdrowing',
    component: NewShipDrawingComponent,
  },
  {
    path: 'update-shipdrowing/:shipDrowingId',
    component: NewShipDrawingComponent,
  },
]
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShipDrawingRoutingModule { }

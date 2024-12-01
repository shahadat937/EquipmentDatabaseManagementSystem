import { Page404Component } from './../authentication/page404/page404.component';
import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NewUserManual } from './new-usermanual/new-usermanual.component';
import { UserManualList } from './user-manual-list/usermanual-list.component';

const routes: Routes = [
    {
        path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
    },
    {
        path: 'add-usermanual',
        component: NewUserManual
    },
    { path: 'update-usermanual/:userManualId', 
        component: NewUserManual 
        },
    {
        path: 'usermanual-list',
        component: UserManualList
    },
    { path: '**', component: Page404Component },

];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })

  export class UserManualRoutingModule {}
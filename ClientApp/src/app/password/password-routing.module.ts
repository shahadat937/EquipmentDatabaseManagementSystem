import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {PasswordchangeComponent} from './passwordchange/passwordchange.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  // {
  //   path: 'profile-update',
  //   component: NewProfileUpdateComponent,
  // },

  {
    path: 'password-change',
    component: PasswordchangeComponent,
  },
  {
    path: 'password/password-change',
    component: PasswordchangeComponent,
  },
  // {
  //   path: 'passwordupdate-student',
  //   component: NewPasswordChangeComponent,
  // },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PasswordRoutingModule { }

import { Page404Component } from './authentication/page404/page404.component';
import { AuthLayoutComponent } from './layout/app-layout/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './layout/app-layout/main-layout/main-layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/guard/auth.guard';
import { Role } from './core/models/role';
const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'repair-management',
        canActivate: [AuthGuard],
        data: {
          role: [Role.ShipUser, Role.MasterAdmin, Role.DD, Role.OffceStaffDNWEE, Role.SOL, Role.SOR, Role.StaffOfficer, Role.DNWNEEOfficeStaff, Role.Director, Role.SOL]
        },
        loadChildren: () =>
          import('./repair-management/repair-management.module').then((m) => m.RepairManagementModule),
      },

      {
        path: 'ships-return',
        canActivate: [AuthGuard],
        data: {
          role: [Role.MasterAdmin, Role.DD, Role.OffceStaffDNWEE, Role.SOL, Role.SOR, Role.StaffOfficer, Role.DNWNEEOfficeStaff, Role.LOEO, Role.ShipStaff, Role.Director, Role.SOL, Role.AreaCommander, Role.FLO, Role.FLOStaff, Role.CSO]
        },
        loadChildren: () =>
          import('./ships-return/ships-return.module').then((m) => m.ShipsReturnModule),
      },

      {
        path: 'miscellaneous',
        canActivate: [AuthGuard],
        data: {
          role: [Role.ShipUser, Role.MasterAdmin, Role.DD, Role.OffceStaffDNWEE, Role.Director, Role.SOL, Role.SOR, Role.StaffOfficer, Role.DNWNEEOfficeStaff]
        },
        loadChildren: () =>
          import('./miscellaneous/miscellaneous.module').then((m) => m.MiscellaneousModule),
      },

      {
        path: 'ship-management',
        canActivate: [AuthGuard],
        data: {
          role: [Role.ShipUser, Role.MasterAdmin, Role.DD, Role.OffceStaffDNWEE, Role.Director, Role.SOL, Role.SOR, Role.StaffOfficer, Role.LOEO, Role.DNWNEEOfficeStaff, Role.ShipStaff, Role.AreaCommander, Role.FLO, Role.FLOStaff, Role.CSO]
        },
        loadChildren: () =>
          import('./ship-management/ship-management.module').then((m) => m.ShipManagementModule),
      },
      {
        path: 'dailywork-management',
        canActivate: [AuthGuard],
        data: {
          role: [Role.MasterAdmin, Role.DD, Role.OffceStaffDNWEE, Role.Director, Role.SOL, Role.SOR, Role.StaffOfficer, Role.DNWNEEOfficeStaff]
        },
        loadChildren: () =>
          import('./dailywork-management/dailywork-management.module').then((m) => m.DailyWorkManagementModule),
      },
      {
        path: 'procurement-management',
        canActivate: [AuthGuard],
        data: {
          role: [Role.MasterAdmin, Role.DD, Role.OffceStaffDNWEE, Role.Director, Role.SOL, Role.SOR, Role.StaffOfficer, Role.DNWNEEOfficeStaff]
        },
        loadChildren: () =>
          import('./procurement-management/procurement-management.module').then((m) => m.ProcurementManagementModule),
      },

      {
        path: 'basic-setup',
        canActivate: [AuthGuard],
        data: {
          role: [Role.MasterAdmin, Role.DD, Role.Director, Role.SOL, Role.SOR, Role.StaffOfficer, Role.DNWNEEOfficeStaff]
        },
        loadChildren: () =>
          import('./basic-setup/basic-setup.module').then((m) => m.BasicSetupModule),
      },
      {
        path: 'ship-drawing',
        canActivate: [AuthGuard],
        data: {
          role: [Role.MasterAdmin, Role.DD, Role.OffceStaffDNWEE, Role.Director, Role.SOL, Role.SOR, Role.StaffOfficer, Role.DNWNEEOfficeStaff, Role.AreaCommander, Role.CSO, Role.FLO, Role.FLOStaff]
        },
        loadChildren: () =>
          import('./ship-drawing/ship-drawing.module').then((m) => m.ShipDrawingModule),
      },


      {
        path: 'security',
        canActivate: [AuthGuard],
        data: {
          role: [Role.MasterAdmin, Role.DD, Role.Director, Role.SOL, Role.SOR, Role.StaffOfficer, Role.DNWNEEOfficeStaff],
        },
        loadChildren: () =>
          import('./security/security.module').then((m) => m.SecurityModule),
      },
      {
        path: 'password',
        canActivate: [AuthGuard],
        data: {
          role: [Role.MasterAdmin, Role.SuperAdmin, Role.ShipStaff, Role.LOEO, Role.EXO, Role.DataEntry, Role.DNWNEEOfficeStaff, Role.CO, Role.StaffOfficer, Role.AreaCommander, Role.Director, Role.SOL, Role.SOR, Role.DD, Role.OffceStaffDNWEE, Role.SOL, Role.SOR, Role.ShipUser, Role.DataEntry, Role.FLO, Role.FLOStaff, Role.CSO],
        },
        loadChildren: () =>
          import('./password/password.module').then((m) => m.PasswordModule),
      },


      { path: '', redirectTo: '/authentication/signin', pathMatch: 'full' },
      {
        path: 'admin',
        canActivate: [AuthGuard],
        data: {
          role: [Role.MasterAdmin, Role.SuperAdmin, Role.ShipStaff, Role.LOEO, Role.EXO, Role.DataEntry, Role.DNWNEEOfficeStaff, Role.CO, Role.StaffOfficer, Role.AreaCommander, Role.Director, Role.SOL, Role.SOR, Role.DD, Role.OffceStaffDNWEE, Role.SOL, Role.SOR, Role.ShipUser, Role.DataEntry, Role.FLO, Role.FLOStaff, Role.CSO],

        },
        loadChildren: () =>
          import('./admin/admin.module').then((m) => m.AdminModule),
      },

    ],
  },
  {
    path: 'authentication',
    component: AuthLayoutComponent,
    loadChildren: () =>
      import('./authentication/authentication.module').then(
        (m) => m.AuthenticationModule
      ),
  },
  {
    path: 'ship',
    canActivate: [AuthGuard],
    data: {
      role: [Role.DD, Role.OffceStaffDNWEE, Role.Director, Role.SOL, Role.SOR, Role.MasterAdmin, Role.SuperAdmin, Role.StaffOfficer, Role.DNWNEEOfficeStaff],
    },
    loadChildren: () =>
      import('./ship/ship.module').then((m) => m.ShipModule),
  },
  { path: '**', component: Page404Component },
];
@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule { }

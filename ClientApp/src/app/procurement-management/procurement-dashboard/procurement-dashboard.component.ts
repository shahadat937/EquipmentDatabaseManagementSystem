import { SharedService } from '../../../../src/app/shared/shared.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Procurement } from '../models/Procurement';
import { ProcurementService } from '../service/Procurement.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from '../../../../src/app/core/service/confirm.service';
import { MasterData } from '../../../../src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Role } from '../../../../src/app/core/models/role';
import { AuthService } from '../../../../src/app/core/service/auth.service';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-procurement-dashboard',
  templateUrl: './procurement-dashboard.component.html',
  styleUrls: ['./procurement-dashboard.component.sass']
})
export class ProcurementDashboardComponent  implements OnInit {

  constructor(private snackBar: MatSnackBar, private ProcurementService: ProcurementService, private router: Router, private confirmService: ConfirmService, public SharedService: SharedService, private authService : AuthService, private datePipe: DatePipe) { }

  ngOnInit(): void {
  }

}

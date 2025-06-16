import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';



@Component({
  selector: 'app-aip-pending-view-dialog',
  templateUrl: './aip-pending-view-dialog.component.html',
  styleUrls: ['./aip-pending-view-dialog.component.sass']
})
export class AipPendingViewDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}

  ngOnInit(): void {
  }

}

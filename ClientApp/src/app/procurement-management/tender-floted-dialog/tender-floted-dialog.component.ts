import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-tender-floted-dialog',
  templateUrl: './tender-floted-dialog.component.html',
  styleUrls: ['./tender-floted-dialog.component.sass']
})
export class TenderFlotedDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}

  ngOnInit(): void {
  }

}

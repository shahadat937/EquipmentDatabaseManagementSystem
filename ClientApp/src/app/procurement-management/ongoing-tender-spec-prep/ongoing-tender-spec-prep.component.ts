import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';


@Component({
  selector: 'app-ongoing-tender-spec-prep',
  templateUrl: './ongoing-tender-spec-prep.component.html',
  styleUrls: ['./ongoing-tender-spec-prep.component.sass']
})
export class OngoingTenderSpecPrepComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}

  ngOnInit(): void {
    
  }
  getBaseSchoolNames(baseSchoolNameDtos: any[]): string {
    return baseSchoolNameDtos && baseSchoolNameDtos.length > 0 
      ? baseSchoolNameDtos.map(school => school.baseSchoolName).join(', ') 
      : '-';
  }

}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TenderOpeningDateTypeService } from '../../service/TenderOpeningDateType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-tenderopeningdatetype',
  templateUrl: './new-tenderopeningdatetype.component.html',
  styleUrls: ['./new-tenderopeningdatetype.component.sass']
})
export class NewTenderOpeningDateTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  TenderOpeningDateTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private TenderOpeningDateTypeService: TenderOpeningDateTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('tenderOpeningDateTypeId'); 
    if (id) {
      this.pageTitle = 'Edit TenderOpeningDateType';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.TenderOpeningDateTypeService.find(+id).subscribe(
        res => {
          this.TenderOpeningDateTypeForm.patchValue({          

            tenderOpeningDateTypeId: res.tenderOpeningDateTypeId,
            name:  res.name,
            shortName:  res.shortName,
            remarks:  res.remarks,
            status:  res.status,
            menuPosition:  res.menuPosition,
            isActive:  res.isActive
          });          
        }
      );
    } else {
      this.pageTitle = 'Create TenderOpeningDateType';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.TenderOpeningDateTypeForm = this.fb.group({
      tenderOpeningDateTypeId: [0],
      name: ['', Validators.required],
      shortName:   [''],
      remarks:  [''],
      status:  [''],
      menuPosition:  [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.TenderOpeningDateTypeForm.get('tenderOpeningDateTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.TenderOpeningDateTypeService.update(+id,this.TenderOpeningDateTypeForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/tenderopeningdatetype-list');
            this.snackBar.open('Information Updated Successfully ', '', {
              duration: 2000,
              verticalPosition: 'bottom',
              horizontalPosition: 'right',
              panelClass: 'snackbar-success'
            });
          }, error => {
            this.validationErrors = error;
          })
        }
      })
    } else {
      this.TenderOpeningDateTypeService.submit(this.TenderOpeningDateTypeForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/tenderopeningdatetype-list');
        this.snackBar.open('Information Inserted Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }
}

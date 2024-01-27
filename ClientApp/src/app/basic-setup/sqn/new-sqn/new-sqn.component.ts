import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SqnService } from '../../service/Sqn.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-sqn',
  templateUrl: './new-sqn.component.html',
  styleUrls: ['./new-sqn.component.sass']
})
export class NewSqnComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  SqnForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private SqnService: SqnService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('sqnId'); 
    if (id) {
      this.pageTitle = 'Edit Sqn';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.SqnService.find(+id).subscribe(
        res => {
          this.SqnForm.patchValue({          

            sqnId: res.sqnId,
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
      this.pageTitle = 'Create Sqn';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.SqnForm = this.fb.group({
      sqnId: [0],
      name: ['', Validators.required],
      shortName:   [''],
      remarks:  [''],
      status:  [''],
      menuPosition:  [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.SqnForm.get('sqnId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.SqnService.update(+id,this.SqnForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/sqn-list');
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
      this.SqnService.submit(this.SqnForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/sqn-list');
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

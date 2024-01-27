import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OperationalStatusService } from '../../service/OperationalStatus.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-operationalstatus',
  templateUrl: './new-operationalstatus.component.html',
  styleUrls: ['./new-operationalstatus.component.sass']
})
export class NewOperationalStatusComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  OperationalStatusForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private OperationalStatusService: OperationalStatusService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('operationalStatusId'); 
    if (id) {
      this.pageTitle = 'Edit OperationalStatus';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.OperationalStatusService.find(+id).subscribe(
        res => {
          this.OperationalStatusForm.patchValue({          

            operationalStatusId: res.operationalStatusId,
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
      this.pageTitle = 'Create OperationalStatus';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.OperationalStatusForm = this.fb.group({
      operationalStatusId: [0],
      name: ['', Validators.required],
      shortName:   [''],
      remarks:  [''],
      status:  [''],
      menuPosition:  [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.OperationalStatusForm.get('operationalStatusId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.OperationalStatusService.update(+id,this.OperationalStatusForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/operationalstatus-list');
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
      this.OperationalStatusService.submit(this.OperationalStatusForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/operationalstatus-list');
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

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { StateOfEquipmentService } from '../../service/StateOfEquipment.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-stateofequipment',
  templateUrl: './new-stateofequipment.component.html',
  styleUrls: ['./new-stateofequipment.component.sass']
})
export class NewStateOfEquipmentComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  StateOfEquipmentForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private StateOfEquipmentService: StateOfEquipmentService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('stateOfEquipmentId'); 
    if (id) {
      this.pageTitle = 'Edit StateOfEquipment';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.StateOfEquipmentService.find(+id).subscribe(
        res => {
          this.StateOfEquipmentForm.patchValue({          

            stateOfEquipmentId: res.stateOfEquipmentId,
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
      this.pageTitle = 'Create StateOfEquipment';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.StateOfEquipmentForm = this.fb.group({
      stateOfEquipmentId: [0],
      name: ['', Validators.required],
      shortName:   [''],
      remarks:  [''],
      status:  [''],
      menuPosition:  [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.StateOfEquipmentForm.get('stateOfEquipmentId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.StateOfEquipmentService.update(+id,this.StateOfEquipmentForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/stateofequipment-list');
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
      this.StateOfEquipmentService.submit(this.StateOfEquipmentForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/stateofequipment-list');
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

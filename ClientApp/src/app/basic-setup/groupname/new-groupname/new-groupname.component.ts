import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupNameService } from '../../service/GroupName.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-groupname',
  templateUrl: './new-groupname.component.html',
  styleUrls: ['./new-groupname.component.sass']
})
export class NewGroupNameComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  GroupNameForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private GroupNameService: GroupNameService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('groupNameId'); 
    if (id) {
      this.pageTitle = 'Edit GroupName';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.GroupNameService.find(+id).subscribe(
        res => {
          this.GroupNameForm.patchValue({          

            groupNameId: res.groupNameId,
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
      this.pageTitle = 'Create GroupName';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.GroupNameForm = this.fb.group({
      groupNameId: [0],
      name: ['', Validators.required],
      shortName:   [''],
      remarks:  [''],
      status:  [''],
      menuPosition:  [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.GroupNameForm.get('groupNameId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.GroupNameService.update(+id,this.GroupNameForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/groupname-list');
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
      this.GroupNameService.submit(this.GroupNameForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/groupname-list');
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

import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
// import { UserManualService } from '../../service/UserManual.service';
import { MatSnackBar } from '@angular/material/snack-bar';
// import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
// import { SharedServiceService } from 'src/app/shared/shared-service.service';

@Component({
  selector: 'app-new-usermanual',
  templateUrl: './new-usermanual.component.html',
  styleUrls: ['./new-usermanual.component.css']
})
export class NewUserManual extends UnsubscribeOnDestroyAdapter implements OnInit {
  buttonText:string;
  loading = false;
  pageTitle: string;
  destination:string;
  selectedRoles:any;
  selectRoles:SelectedModel[];
  UserManualForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) {
    super();
  }

  ngOnInit(): void {
   
    this.intitializeForm();
  
  }
  intitializeForm() {
    this.UserManualForm = this.fb.group({
      userManualId: [0],
      roleName: [''],
      doc:[''],
      docfile: [''],
      isActive: [true],
    })
  }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.UserManualForm.patchValue({
        docfile: file,
      });
    }
  }

 
  filterByRoles(value:any){
    this.selectedRoles=this.selectRoles.filter(x=>x.text.toLowerCase().includes(value.toLowerCase().replace(/\s/g,'')))
  }

}
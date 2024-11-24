import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { RoleService } from '../../service/role.service';

@Component({
  selector: 'app-new-role',
  templateUrl: './new-role.component.html',
  styleUrls: ['./new-role.component.sass']
})
export class NewRoleComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  roleForm: FormGroup;
  validationErrors: string[] = [];
  roleId : string;

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private roleService: RoleService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.roleId = this.route.snapshot.paramMap.get('roleId'); 
    if (this.roleId) {
      this.pageTitle = 'Edit Role';
      this.destination='Edit';
      this.buttonText="Update";
      this.roleService.find(this.roleId).subscribe(
        res => {
          console.log(res);
          this.roleForm.patchValue({      

            roleId: res.id,
            roleName: res.name,       
          
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Role';
      this.destination='Add';
      this.buttonText="Save";
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.roleForm = this.fb.group({
      roleId: [0],
      roleName: ['', Validators.required]
    
    })
  }
  
  onSubmit() {
    if (this.roleId) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item').subscribe(result => {
        if (result) {
          this.roleService.update(this.roleId,this.roleForm.value).subscribe(response => {
            this.router.navigateByUrl('/security/role-list');
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
      this.roleService.submit(this.roleForm.value).subscribe(response => {
        this.router.navigateByUrl('/security/role-list');
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

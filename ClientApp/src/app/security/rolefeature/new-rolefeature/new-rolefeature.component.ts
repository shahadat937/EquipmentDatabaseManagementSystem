import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RoleFeatureService } from '../../service/rolefeature.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-edit-rolefeature',
  templateUrl: './new-rolefeature.component.html',
  styleUrls: ['./new-rolefeature.component.sass'] 
})
export class NewRoleFeatureComponent implements OnInit {
  pageTitle: string; 
  destination:string;
  btnText:string;
  Roleid:number;
  FeatureKey:number;
  RoleFeatureForm: FormGroup;
  buttonText:string;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[];
  selectedrole:SelectedModel[];
  selectRole: SelectedModel[];
  selectedfeature:SelectedModel[];
  selectFeature:SelectedModel[];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private RoleFeatureService: RoleFeatureService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const rid = this.route.snapshot.paramMap.get('roleId'); 
    this.Roleid = Number(rid);
    console.log(rid);
    const fid = this.route.snapshot.paramMap.get('featureId'); 
    this.FeatureKey = Number(fid)
    if (this.Roleid && this.FeatureKey) {
      this.pageTitle = 'Edit RoleFeature';
      this.destination = "Edit";
      this.buttonText= "Update"
      this.RoleFeatureService.find(this.Roleid, this.FeatureKey).subscribe(
        res => {
          console.log(res);
          this.RoleFeatureForm.patchValue({          
            roleId: res.roleId,
            featureId: res.featureKey,
            add: res.add,
            update:res.update,
            delete:res.delete,
            report:res.report,
            isActive:res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'Create RoleFeature';
      this.destination = "Add";
      this.buttonText= "Save"
    }
    this.intitializeForm();
    this.getselectedrole();
    this.getselectedfeature();
   
  }
  intitializeForm() {
    this.RoleFeatureForm = this.fb.group({
      roleId:['', Validators.required],
      featureKey: ['', Validators.required],
      add: [true],
      update: [true],
      delete: [true],
      report: [true],
      isActive: [true],
    
    })
  }

  getselectedrole(){
    this.RoleFeatureService.getselectedrole().subscribe(res=>{
      this.selectedrole=res
      this.selectRole=res
    });
  }
  filterByRole(value: any) {
    this.selectedrole = this.selectRole.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }

  getselectedfeature(){
    this.RoleFeatureService.getselectedfeature().subscribe(res=>{
      this.selectedfeature=res
      this.selectFeature=res
    });
  }
  filterByFeature(value: any) {
    this.selectedfeature = this.selectFeature.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }

  onSubmit() {
    const rid = this.route.snapshot.paramMap.get('roleId'); 
    this.Roleid = Number(rid);
    console.log(this.Roleid)
    const fid = this.route.snapshot.paramMap.get('featureKey'); 
    this.FeatureKey = Number(fid)
    if (this.Roleid && this.FeatureKey) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item').subscribe(result => {
        if (result) {
          this.RoleFeatureService.update(this.Roleid,this.FeatureKey,this.RoleFeatureForm.value).subscribe(response => {
            this.router.navigateByUrl('/security/rolefeature-list');
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
      this.RoleFeatureService.submit(this.RoleFeatureForm.value).subscribe(response => {
        console.log('response', this.RoleFeatureForm.value)
        this.router.navigateByUrl('/security/rolefeature-list');
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

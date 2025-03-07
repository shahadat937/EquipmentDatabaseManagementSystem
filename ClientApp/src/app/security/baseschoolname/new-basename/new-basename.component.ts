import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseSchoolNameService } from '../../service/BaseSchoolName.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { BaseSchoolName } from '../../models/BaseSchoolName';
import { MasterData } from 'src/assets/data/master-data';

@Component({
  selector: 'app-new-basename',
  templateUrl: './new-basename.component.html',
  styleUrls: ['./new-basename.component.sass']
})
export class NewBaseNameComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  masterData = MasterData;
  BaseNameForm: FormGroup;
  baseNameList: BaseSchoolName[];
  validationErrors: string[] = [];
  selectedOrganization:SelectedModel[];
  selectedCommendingArea:SelectedModel[];
  selectCommandingArea:SelectedModel[];
  organizationId:any;
  commendingAreaId:any;
  isShown:boolean=false;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  displayedColumns: string[] = [ 'ser','schoolLogo', 'schoolName', 'shortName',  'actions'];

  constructor(private snackBar: MatSnackBar,private BaseSchoolNameService: BaseSchoolNameService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute,private confirmService:ConfirmService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('baseSchoolNameId'); 
    if (id) {
      this.pageTitle = 'Edit Base Name';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.BaseSchoolNameService.find(+id).subscribe(
        res => {
          this.BaseNameForm.patchValue({          
            
            baseSchoolNameId: res.baseSchoolNameId,
            schoolName: res.schoolName,
            shortName: res.shortName,
            schoolLogo: res.schoolLogo,
            //status: res.status,
            //menuPosition:res.menuPosition,
            isActive: res.isActive,
            contactPerson: res.contactPerson,
            address: res.address,
            telephone: res.telephone,
            cellphone: res.cellphone,
            email: res.email,
            fax: res.fax,
            branchLevel: res.branchLevel,
            firstLevel: res.firstLevel,
            secondLevel: res.secondLevel,
            thirdLevel: res.thirdLevel,
            //fourthLevel: res.fourthLevel,
            //fifthLevel: res.fifthLevel,
            serverName: res.serverName,
          
          }); 
          this.onOrganizationSelectionChangeGetCommendingArea();         
        }
      );
    } else {
      this.pageTitle = 'Create Base Name';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    //this.getOrganizationList();
    this.onOrganizationSelectionChangeGetCommendingArea();
  }

  

  // getSelectedOrganization(){
  //   this.BaseSchoolNameService.getSelectedOrganization().subscribe(res=>{
  //     this.selectedOrganization=res
  //     //console.log(this.selectedOrganization);
  //   });
  // }

  onOrganizationSelectionChangeGetCommendingArea(){
    this.organizationId=this.BaseNameForm.value['firstLevel'];
    //console.log(this.organizationId)    
    this.BaseSchoolNameService.getSelectedCommendingArea(this.organizationId).subscribe(res=>{
      this.selectedCommendingArea=res
      this.selectCommandingArea=res
    });        
  }
  filterByCommandingArea(value: any) {
    this.selectedCommendingArea = this.selectCommandingArea.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  onCommendingAreaSelectionChangeGetBaseList(){
    this.commendingAreaId=this.BaseNameForm.value['secondLevel'];
    //console.log(this.commendingAreaId);
    this.getBaseNameList(this.commendingAreaId);
            
  }

  getBaseNameList(commendingAreaId){
    this.isShown=true;
    this.BaseSchoolNameService.getBaseNameList(commendingAreaId).subscribe(res=>{
      this.baseNameList=res
      //console.log(this.baseNameList);
    });
  }

  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      //console.log('ImagE')
     //console.log(file);
      this.BaseNameForm.patchValue({
        image: file,
      });
    }
  }
  intitializeForm() {
    this.BaseNameForm = this.fb.group({
      
      baseSchoolNameId: [0],
      schoolName: [''],
      shortName: [''],
      schoolLogo: [''],
      image:[''],
      status: [''],
      menuPosition: [''],
      isActive: [true],
      contactPerson: [],
      address: [],
      telephone: [],
      cellphone: [],
      email: [],
      fax: [],
      branchLevel: [3],
      firstLevel: [this.masterData.UserLevel.navy],
      secondLevel: [""],
      thirdLevel: [""],
      fourthLevel: [""],
      fifthLevel: [""],
      serverName: [],
    
    })
  }
  
  onSubmit() {
    const id = this.BaseNameForm.get('baseSchoolNameId').value;
    ////console.log(id);
    const formData = new FormData();
    for (const key of Object.keys(this.BaseNameForm.value)) {
      const value = this.BaseNameForm.value[key];
      formData.append(key, value);
    }
    //console.log(formData)
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        //console.log(result);
        if (result) {
          
          this.BaseSchoolNameService.update(+id,formData).subscribe(response => {
            this.router.navigateByUrl('/security/new-basename');
            this.getBaseNameList(this.commendingAreaId);
            this.BaseNameForm.reset();
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
      this.BaseSchoolNameService.submit(formData).subscribe(response => {
        //this.router.navigateByUrl('/basic-setup/baseschoolname-list');
        this.getBaseNameList(this.commendingAreaId);
        this.BaseNameForm.reset();
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

  deleteItem(row) {
    const id = row.baseSchoolNameId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.BaseSchoolNameService.delete(id).subscribe(() => {
          this.getBaseNameList(this.commendingAreaId);
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    })    
  }

}

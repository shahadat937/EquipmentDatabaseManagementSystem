import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ShipDrowingService } from '../../service/ShipDrowing.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';
import { MasterData } from 'src/assets/data/master-data';
import { ShipDrowing } from '../../models/ShipDrowing';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import {BaseSchoolNameService} from '../../../../app/security/service/BaseSchoolName.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-shipdrowing',
  templateUrl: './new-shipdrowing.component.html',
  styleUrls: ['./new-shipdrowing.component.sass']
})
export class NewShipDrowingComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ShipDrowingForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  traineeId:any;
  role:any;
  branchId:any;
  userRole = Role;
  selectedDepartmentName:SelectedModel[];
  ShipDrowingList:SelectedModel[];
  masterData = MasterData;
  organizationId:any;
  selectedCommendingArea:any[];
  selectedBaseName:any[];
  commendingAreaId:any;
  selectedBaseSchoolName:any[];


  ELEMENT_DATA: ShipDrowing[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: 1,
    pageSize: 10,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser','authority','baseName','baseSchoolName','name','fileUpload','actions'];
  dataSource: MatTableDataSource<ShipDrowing> = new MatTableDataSource();

  selection = new SelectionModel<ShipDrowing>(true, []);

  constructor(private snackBar: MatSnackBar,private BaseSchoolNameService:BaseSchoolNameService,private authService: AuthService,private confirmService: ConfirmService,private ShipDrowingService: ShipDrowingService, private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('shipDrowingId'); 

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit ShipDrowing';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ShipDrowingService.find(+id).subscribe(
        res => {
          this.ShipDrowingForm.patchValue({                 
            shipDrowingId: res.shipDrowingId,
            baseSchoolNameId:res.baseSchoolNameId,
            authorityId:res.authorityId,
            baseNameId:res.baseNameId,
            name: res.name,
            shortName: res.shortName,
            fileUpload: res.fileUpload,
            remarks: res.remarks,
           // menuPosition: res.menuPosition,
            isActive: res.isActive
          }); 
          console.log("res");
          console.log(res);       
          this.onCommendingAreaSelectionChangeGetBaseName();
          this.onOrganizationSelectionChange();  
        }
      );
    } else {
      this.pageTitle = 'Create ShipDrowing';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getShipDrowings();
    this.onOrganizationSelectionChangeGetCommendingArea(); 
    // if(this.role != this.userRole.SuperAdmin){
    //   console.log("dd");
    //   this.ShipDrowingForm.get('departmentNameId').setValue(this.branchId);
    //   this.onDepartmentSelectionChangeGetShipDrowingList();
    // }
    // this.GetDepartmentNameById(this.masterData.schoolDept.navalAviation);
  }
  intitializeForm() {
    this.ShipDrowingForm = this.fb.group({
      shipDrowingId: [0],
      authorityId:[],
      baseNameId:[],
      baseSchoolNameId:[''],
      name: [''],
      shortName: [''],
      fileUpload: [''],
      remarks:[''],
      menuPosition:[1],
      doc:[''],
      isActive: [true]
    })
  }

  onOrganizationSelectionChangeGetCommendingArea(){
    this.organizationId=MasterData.UserLevel.navy;
    console.log(this.organizationId+" organization")    
    this.BaseSchoolNameService.getSelectedCommendingArea(this.organizationId).subscribe(res=>{
      this.selectedCommendingArea=res
      console.log("selected comanding area");
    //  console.log(this.selectedCommendingArea);
    });        
  }
  onOrganizationSelectionChange(){
    var baseNameId = this.ShipDrowingForm.value['baseNameId'];
    // this.ShipInformationService.getSelectedSchoolByBranchLevelAndThirdLevel(baseNameId).subscribe(res=>{
    //   this.selectedBaseSchoolName=res
    //   console.log(res)
    //   console.log(res)
    // }); 

   // this.baseNameId=this.UserForm.value['thirdLevel'];
    console.log(baseNameId);
    this.BaseSchoolNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedBaseSchoolName=res
      console.log(this.selectedBaseName);
    }); 
  }
  onCommendingAreaSelectionChangeGetBaseName(){
    this.commendingAreaId=this.ShipDrowingForm.value['authorityId'];
    console.log("comandinf area")
    console.log(this.commendingAreaId);
    this.BaseSchoolNameService.getSelectedBaseName(this.commendingAreaId).subscribe(res=>{
      this.selectedBaseName=res
      console.log(this.selectedBaseName);
    });  
    //this.getBaseNameList(this.commendingAreaId);
            
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getShipDrowings();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getShipDrowings();
  } 


  getShipDrowings(){
    this.ShipDrowingService.getShipDrowings(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      console.log("data---");
      console.log("data---",this.dataSource.data)
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  deleteItem(row) {
    const id = row.shipDrowingId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ShipDrowingService.delete(id).subscribe(() => {
         this.getShipDrowings();
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

  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
     console.log(file);
      this.ShipDrowingForm.patchValue({
        doc: file,
      });
    }
  }

  onSubmit() {
    const id = this.ShipDrowingForm.get('shipDrowingId').value;   

   // this.ShipDrowingForm.get('date').setValue((new Date(this.ShipDrowingForm.get('date').value)).toUTCString()) ;
    // this.ProcurementForm.get('tenderopeningDate').setValue((new Date(this.ProcurementForm.get('tenderopeningDate').value)).toUTCString()) ;
    // this.ProcurementForm.get('workOrderDate').setValue((new Date(this.ProcurementForm.get('workOrderDate').value)).toUTCString()) ;
    // this.ProcurementForm.get('dateOfDelivery').setValue((new Date(this.ProcurementForm.get('dateOfDelivery').value)).toUTCString()) ;
    
   // console.log(this.ProcurementForm.value)

    const formData = new FormData();
    for (const key of Object.keys(this.ShipDrowingForm.value)) {
      const value = this.ShipDrowingForm.value[key];
      formData.append(key, value);
    }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ShipDrowingService.update(+id,formData).subscribe(response => {
            this.reloadCurrentRoute();
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
      this.ShipDrowingService.submit(formData).subscribe(response => {
        // this.router.navigateByUrl('/basic-setup/ShipDrowing-list');
        this.reloadCurrentRoute();
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

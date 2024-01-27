import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ShipInformationService } from '../../service/ShipInformation.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { MasterData } from 'src/assets/data/master-data';
import {BaseSchoolNameService} from '../../../../app/security/service/BaseSchoolName.service'
import { Role } from 'src/app/core/models/role';

@Component({
  selector: 'app-new-shipinformation',
  templateUrl: './new-shipinformation.component.html',
  styleUrls: ['./new-shipinformation.component.sass']
})
export class NewShipInformationComponent implements OnInit {
  pageTitle: string;
  userRole = Role;
  destination:string;
  btnText:string;
  ShipInformationForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  selectedBaseName:SelectedModel[];
  selectedBranchLevel:SelectedModel[];
  selectedBaseSchoolName:SelectedModel[];
  selectedSqn:SelectedModel[];
  selectedOperationalStatus:SelectedModel[];
  selectedShipType:SelectedModel[];
  traineeId:any;
  role:any;
  branchId:any;
  organizationId:any;
  selectedCommendingArea:any[];
  commendingAreaId:any;

  constructor(private snackBar: MatSnackBar,private BaseSchoolNameService:BaseSchoolNameService,private authService: AuthService,private confirmService: ConfirmService,private ShipInformationService: ShipInformationService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    const id = this.route.snapshot.paramMap.get('shipInformationId'); 
    if (id) {
      this.pageTitle = 'Edit ShipInformation';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ShipInformationService.find(+id).subscribe(
        res => {
          this.ShipInformationForm.patchValue({          

            shipInformationId: res.shipInformationId,
            baseNameId: res.baseNameId,
            baseSchoolNameId: res.baseSchoolNameId,
            sqnId: res.sqnId,
            operationalStatusId: res.operationalStatusId,
            shipTypeId: res.shipTypeId,
            controlCode: res.controlCode,
            callSign: res.callSign,
            nickName: res.nickName,
            class: res.class,
            fileUpload: res.fileUpload,
            displacementFullLoad: res.displacementFullLoad,
            displacementLightWeight: res.displacementLightWeight,
            manufacturer: res.manufacturer,
            dateOfCommission: res.dateOfCommission,
            lengthLoa: res.lengthLoa,
            lengthHom: res.lengthHom,
            breadthBmld: res.breadthBmld,
            depth: res.depth,
            draftFwd: res.draftFwd,
            draftAft: res.draftAft,
            freshWaterCapacity: res.freshWaterCapacity,
            maximumSpeed: res.maximumSpeed,
            fualCapacity: res.fualCapacity,
            maximumContinuousSpeed: res.maximumContinuousSpeed,
            luboilCapacity: res.luboilCapacity,
            cruisingSpeed: res.cruisingSpeed,
            economicSpeed: res.economicSpeed,
            address: res.address,
            authority: res.authority,
            contactNo: res.contactNo,
            status: res.status,
            menuPosition: res.menuPosition,
            isActive: res.isActive,
            remarks: res.remarks,
            authorityId:res.authorityId,
          });       
        console.log("res1");
        console.log(res);
        this.onCommendingAreaSelectionChangeGetBaseName();
        this.onOrganizationSelectionChange();
        }
      );
    } else {
      this.pageTitle = 'Create ShipInformation';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();

    if(this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO){
      this.ShipInformationForm.get('baseSchoolNameId').setValue(this.branchId);
      this.BaseSchoolNameService.find(this.branchId).subscribe(res=>{
        console.log(res);
        this.ShipInformationForm.get('baseNameId').setValue(res.thirdLevel);
        this.ShipInformationForm.get('authorityId').setValue(res.secondLevel);
      });
    }


    this.getSelectedBaseName(this.branchId);
    this.getSelectedOrganizationByBranch();
    this.getSelectedSqn();
    this.getSelectedOperationalStatus();
    this.getSelectedShipType();
    this.onOrganizationSelectionChangeGetCommendingArea();   
  }
  intitializeForm() {
    this.ShipInformationForm = this.fb.group({
      shipInformationId: [0],
      authorityId:[],
      baseNameId: [],
      baseSchoolNameId: [],
      sqnId: [],
      operationalStatusId: [],
      shipTypeId:[],
      controlCode: [''],
      callSign: [''],
      nickName:  [''],
      class:  [''],
      fileUpload: [''],
      displacementFullLoad: [''],
      displacementLightWeight:  [''],
      manufacturer: [''],
      dateOfCommission: [''],
      lengthLoa: [''],
      lengthHom: [''],
      breadthBmld: [''],
      depth: [''],
      draftFwd: [''],
      draftAft: [''],
      freshWaterCapacity: [''],
      maximumSpeed: [''],
      fualCapacity: [''],
      maximumContinuousSpeed: [''],
      luboilCapacity: [''],
      cruisingSpeed: [''],
      economicSpeed: [''],
      address: [''],
      authority: [''],
      contactNo: [''],
      status: [0],
      menuPosition: [''],
      isActive: [true],
      remarks: [''],
      doc:['']
    })
  }
  
  onOrganizationSelectionChangeGetCommendingArea(){
    this.organizationId=MasterData.UserLevel.navy;
    console.log(this.organizationId+" organization")    
    this.BaseSchoolNameService.getSelectedCommendingArea(this.organizationId).subscribe(res=>{
      this.selectedCommendingArea=res
      console.log("selected comanding area");
      console.log(this.selectedCommendingArea);
    });        
  }

  onCommendingAreaSelectionChangeGetBaseName(){
    this.commendingAreaId=this.ShipInformationForm.value['authorityId'];
    console.log("comandinf area")
    console.log(this.commendingAreaId);
    this.BaseSchoolNameService.getSelectedBaseName(this.commendingAreaId).subscribe(res=>{
      this.selectedBaseName=res
      console.log(this.selectedBaseName);
    });  
    //this.getBaseNameList(this.commendingAreaId);
            
  }

  getSelectedBaseName(value){
    console.log("444");
    console.log(value)
    this.ShipInformationService.getSelectedSchoolName(value).subscribe(res=>{
      this.selectedBaseName=res
      console.log(res)
      console.log(res)
    }); 
  }
  onOrganizationSelectionChange(){
    var baseNameId = this.ShipInformationForm.value['baseNameId'];
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

  getSelectedSqn(){
    this.ShipInformationService.getSelectedSqn().subscribe(res=>{
      this.selectedSqn=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedOperationalStatus(){
    this.ShipInformationService.getSelectedOperationalStatus().subscribe(res=>{
      this.selectedOperationalStatus=res
      console.log(res)
      console.log(res)
    }); 
  }
  onFileChanged(event){
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log(file);
      this.ShipInformationForm.patchValue({
        doc: file,
      });
    }
  }

  getSelectedShipType(){
    this.ShipInformationService.getSelectedShipType().subscribe(res=>{
      this.selectedShipType=res
      console.log(res)
      console.log(res)
    }); 
  }


  getSelectedOrganizationByBranch(){
    this.ShipInformationService.getSelectedOrganizationByBranchLevel().subscribe(res=>{
      this.selectedBranchLevel=res
      console.log(res)
      console.log(res)
    }); 
  }
  // getSelectedOrganizationByBaseNameId(){
  //   this.ShipInformationService.getSelectedOrganizationByBranchLevel().subscribe(res=>{
  //     this.selectedBranchLevel=res
  //     console.log(res)
  //     console.log(res)
  //   }); 
  // }

  onSubmit() {
    const id = this.ShipInformationForm.get('shipInformationId').value;   

    this.ShipInformationForm.get('dateOfCommission').setValue((new Date(this.ShipInformationForm.get('dateOfCommission').value)).toUTCString());

    console.log(this.ShipInformationForm.value)

    const formData = new FormData();
    for (const key of Object.keys(this.ShipInformationForm.value)) {
      const value = this.ShipInformationForm.value[key];
      formData.append(key, value);
    }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ShipInformationService.update(+id,formData).subscribe(response => {
            this.router.navigateByUrl('/ship-management/shipinformation-list');
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
      this.ShipInformationService.submit(formData).subscribe(response => {
        this.router.navigateByUrl('/ship-management/shipinformation-list');
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

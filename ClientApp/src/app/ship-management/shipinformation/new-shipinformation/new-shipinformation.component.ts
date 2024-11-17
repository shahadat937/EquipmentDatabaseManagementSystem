import { SharedService } from './../../../shared/shared.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>; 
  pageTitle: string;
  userRole = Role;
  destination:string;
  btnText:string;
  ShipInformationForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  selectedBaseName:SelectedModel[];
  selectBaseName:SelectedModel[];
  selectedBranchLevel:SelectedModel[];
  selectedBaseSchoolName:SelectedModel[];
  selectBns:SelectedModel[];
  selectedSqn:SelectedModel[];
  selectSqn:SelectedModel[];
  selectedOperationalStatus:SelectedModel[];
  selectedShipType:SelectedModel[];
  selectShipType:SelectedModel[];
  traineeId:any;
  role:any;
  branchId:any;
  organizationId:any;
  selectedCommendingArea:any[];
  selectCommandingArea:SelectedModel[];
  commendingAreaId:any;
  shipImage : any;
  isImage : boolean;
  isFile : boolean;

  constructor(private snackBar: MatSnackBar,private BaseSchoolNameService:BaseSchoolNameService,private authService: AuthService,private confirmService: ConfirmService,private ShipInformationService: ShipInformationService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, private sharedService : SharedService
  ) { }

  ngOnInit(): void {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();


    const id = this.route.snapshot.paramMap.get('shipInformationId'); 
    if (id) {
      this.pageTitle = 'Edit ShipInformation';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ShipInformationService.find(+id).subscribe(
        res => {
          this.shipImage = res.fileUpload;
          this.isImage = this.checkImage(res.fileUpload)
          this.isFile = this.checkFile(res.fileUpload)
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
            menuPosition: 0,
            isActive: res.isActive,
            remarks: res.remarks,
            authorityId:res.authorityId,
          });       
 
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
    this.BaseSchoolNameService.getSelectedCommendingArea(this.organizationId).subscribe(res=>{
      this.selectedCommendingArea=res

      this.selectCommandingArea=res
    });        
  }
  filterByEquipmentCategory(value:any){
    this.selectedCommendingArea=this.selectCommandingArea.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }

  onCommendingAreaSelectionChangeGetBaseName(){
    this.commendingAreaId=this.ShipInformationForm.value['authorityId'];

    this.BaseSchoolNameService.getSelectedBaseName(this.commendingAreaId).subscribe(res=>{
      this.selectedBaseName=res

    });  
    //this.getBaseNameList(this.commendingAreaId);
            
  }
  filterBySchool(value:any){
    this.selectedBaseName=this.selectBaseName.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }

  getSelectedBaseName(value){

    this.ShipInformationService.getSelectedSchoolName(value).subscribe(res=>{
      this.selectedBaseName=res

    }); 
  }
  onOrganizationSelectionChange(){
    var baseNameId = this.ShipInformationForm.value['baseNameId'];
    // this.ShipInformationService.getSelectedSchoolByBranchLevelAndThirdLevel(baseNameId).subscribe(res=>{
    //   this.selectedBaseSchoolName=res
    //   console.log(res)

    // }); 

   // this.baseNameId=this.UserForm.value['thirdLevel'];

  
    this.BaseSchoolNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedBaseSchoolName=res
      this.selectBaseName=res
    }); 
  }
  filterByShip(value:any){
    this.selectedBaseSchoolName=this.selectBaseName.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }

  getSelectedSqn(){
    this.ShipInformationService.getSelectedSqn().subscribe(res=>{
      this.selectedSqn=res

      this.selectSqn=res

    }); 
  }
  filterBySqn(value:any){
    this.selectedSqn=this.selectSqn.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedOperationalStatus(){
    this.ShipInformationService.getSelectedOperationalStatus().subscribe(res=>{
      this.selectedOperationalStatus=res
     
    }); 
  }
  
  onFileChanged(event: Event) {
    const input = event.target as HTMLInputElement;
    console.log(input.files[0]);
  
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      const reader = new FileReader();
  
      // Check if it's an image
      this.isImage = this.checkImage(file.name);
      this.isFile = this.checkFile(file.name);
      console.log(this.isFile);
  
      // If it's an image, read the file as a Data URL
      if (this.isImage) {
        reader.onload = () => {
          this.shipImage = reader.result as string;      
        };
        reader.readAsDataURL(file);
      } else {
        // For non-image files, you can set the file name or handle it accordingly
        this.shipImage = ''; // Optionally, clear previous image content if file is not an image
      }
  
      // Update the form with the selected file
      if (this.ShipInformationForm && this.ShipInformationForm.controls['doc']) {
        this.ShipInformationForm.patchValue({
          doc: file,
        });
      }
    }
  }
  

  getSelectedShipType(){
    this.ShipInformationService.getSelectedShipType().subscribe(res=>{
      this.selectedShipType=res

      this.selectShipType=res

    }); 
  }
  filterByShipType(value:any){
    this.selectedShipType=this.selectShipType.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }


  getSelectedOrganizationByBranch(){
    this.ShipInformationService.getSelectedOrganizationByBranchLevel().subscribe(res=>{
      this.selectedBranchLevel=res
 
    }); 
  }
  // getSelectedOrganizationByBaseNameId(){
  //   this.ShipInformationService.getSelectedOrganizationByBranchLevel().subscribe(res=>{
  //     this.selectedBranchLevel=res
  //    
  //   }); 
  // }
  handleImageError() {
    this.shipImage = ''; 
  }

  removeImage(event: Event) {
    event.preventDefault(); 

   
    this.shipImage = '';

   
    if (this.fileInput && this.fileInput.nativeElement) {
      this.fileInput.nativeElement.value = ''; 
    }
  }

  checkImage(fileUrl: string) {
    return /\.(jpg|jpeg|png|gif)$/i.test(fileUrl);
  }

  // checkFile(fileUrl: string): boolean {
  //   const allowedExtensions = /\.(txt|pdf|xls|xlsx|doc|docx|ppt|pptx)$/i;
  //   return allowedExtensions.test(fileUrl);
  // }

  checkFile(fileUrl: string): boolean {
    // List of allowed file extensions
    const allowedExtensions = /\.(txt|pdf|xls|xlsx|doc|docx|ppt|pptx)$/i;
  
    // Extract the file name or path from the URL if it's a URL
    const extractedFileName = fileUrl.split('/').pop() || ''; // Get the last segment after 
    return allowedExtensions.test(extractedFileName);
  }
  

  onSubmit() {
    const id = this.ShipInformationForm.get('shipInformationId').value;   

    // this.ShipInformationForm.get('dateOfCommission').setValue((new Date(this.ShipInformationForm.get('dateOfCommission').value)));
    // this.ShipInformationForm.get('dateOfCommission').setValue(dateOfCommission);
    const dateOfCommission = this.sharedService.formatDateTime(this.ShipInformationForm.get('dateOfCommission').value)
    this.ShipInformationForm.get('dateOfCommission').setValue(dateOfCommission);

    const formData = new FormData();
    if(!this.shipImage){
      this.ShipInformationForm.value.fileUpload = null;
    }
    console.log(this.ShipInformationForm.value)
    for (const key of Object.keys(this.ShipInformationForm.value)) {
      let  value = this.ShipInformationForm.value[key];
      formData.append(key, value);
      if(!value){
        value = ""
      }
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

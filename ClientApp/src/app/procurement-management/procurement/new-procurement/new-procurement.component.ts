import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcurementService } from '../../service/Procurement.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { MasterData } from 'src/assets/data/master-data';
import {BaseSchoolNameService} from '../../../../app/security/service/BaseSchoolName.service'
import {SharedService} from 'src/app/shared/shared.service'

@Component({
  selector: 'app-new-procurement',
  templateUrl: './new-procurement.component.html',
  styleUrls: ['./new-procurement.component.sass']
})
export class NewProcurementComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ProcurementForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  selectedBaseName:SelectedModel[];
  selectedBranchLevel:SelectedModel[];
  selectedBaseSchoolName:SelectedModel[];
  selectedProcurementMethod:SelectedModel[];
  selectedProcurementType:SelectedModel[];
  selectedEnvelope:SelectedModel[];
  selectedGroupName:SelectedModel[];
  selectedEquipmentName:SelectedModel[];
  selectedControlled:SelectedModel[];
  selectedFcLc:SelectedModel[];
  selectedDgdpNssd:SelectedModel[];
  selectedTec:SelectedModel[];
  selectedTenderOpeningDateType:SelectedModel[];
  selectedPaymentStatus:SelectedModel[];
  traineeId:any;
  role:any;
  branchId:any;
  organizationId:any;
  selectedCommendingArea:any[];
  commendingAreaId:any;
  method: any;

  constructor(private snackBar: MatSnackBar,private BaseSchoolNameService:BaseSchoolNameService,private authService: AuthService,private confirmService: ConfirmService,private ProcurementService: ProcurementService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, private sharedService : SharedService) { }

  ngOnInit(): void {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    const id = this.route.snapshot.paramMap.get('procurementId'); 
    if (id) {
      this.pageTitle = 'Edit Procurement';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ProcurementService.find(+id).subscribe(
        res => {
          this.ProcurementForm.patchValue({          

            procurementId: res.procurementId,
            baseSchoolNameId: res.baseSchoolNameId,
            procurementMethodId: res.procurementMethodId,
            envelopeId: res.envelopeId,
            paymentStatusId: res.paymentStatusId,
            procurementTypeId: res.procurementTypeId,
            groupNameId: res.groupNameId,
            equpmentNameId: res.equpmentNameId,
            controlledId: res.controlledId,
            fcLcId: res.fcLcId,
            dgdpNssdId: res.dgdpNssdId,
            tecId: res.tecId,
            tenderOpeningDateTypeId: res.tenderOpeningDateTypeId,
            qty: res.qty,
            ePrice: res.ePrice,
            sentToDgdpNssdDate: res.sentToDgdpNssdDate,
            tenderOpeningDate: res.tenderOpeningDate,
            offerReceivedDate: res.offerReceivedDate,
            contractMinSentDate: res.contractMinSentDate,
            contractMinReceived: res.contractMinReceived,
            sentForContractDate: res.sentForContractDate,
            contractSignedDate: res.contractSignedDate,
            aip: res.aip,
            clarificationToOemSentDate: res.clarificationToOemSentDate,
            clarificationToOemReceivedDate: res.clarificationToOemReceivedDate,
            clarificationToUserSentDate: res.clarificationToUserSentDate,
            clarificationToUserReceivedDate: res.clarificationToUserReceivedDate,
            techTecSentDate: res.techTecSentDate,
            techTecReceivedDate: res.techTecReceivedDate,
            minForFoSentDate: res.minForFoSentDate,
            minForFoReceivedDate: res.minForFoReceivedDate,
            sentToDtsDate: res.sentToDtsDate,
            foReceivedDate: res.foReceivedDate,
            foTecSentDate: res.foTecSentDate,
            foTecReceivedDate: res.foTecReceivedDate,
            finalContractMinSentDate: res.finalContractMinSentDate,
            finalContractMinReceivedDate: res.finalContractMinReceivedDate,
            status: res.status,
            menuPosition: res.menuPosition,
            isActive: res.isActive,
            remarks: res.remarks,
          });       
        console.log("res1");
        console.log(res);
        //this.onCommendingAreaSelectionChangeGetBaseName();
        //this.onOrganizationSelectionChange();
        }
      );
    } else {
      this.pageTitle = 'Create Procurement';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getSelectedSchoolByBranchLevelAndThirdLevel();
    this.getSelectedProcurementMethod();
    this.getSelectedEnvelope();
    this.getSelectedGroupName();
    this.getSelectedEqupmentName();
    this.getSelectedControlled();
    this.getSelectedProcurementType();   
    this.getSelectedFcLc();
    this.getSelectedDgdpNssd();
    this.getSelectedTec();
    this.getSelectedTenderOpeningDateType();
    this.getSelectedPaymentStatus();
  }
  intitializeForm() {
    this.ProcurementForm = this.fb.group({
      procurementId: [0],
      baseSchoolNameId:[],
      procurementMethodId: [],
      envelopeId: [],
      paymentStatusId: [],
      procurementTypeId: [],
      groupNameId:[],
      equpmentNameId: [""],
      controlledId: [""],
      fcLcId:  [""],
      dgdpNssdId:  [""],
      tecId: [""],
      tenderOpeningDateTypeId: [""],
      qty:  [""],
      ePrice: [""],
      sentToDgdpNssdDate: [""],
      tenderOpeningDate: [""],
      offerReceivedDate: [""],
      contractMinSentDate: [""],
      contractMinReceived: [""],
      sentForContractDate: [""],
      contractSignedDate: [""],
      aip: [""],
      clarificationToOemSentDate: [""],
      clarificationToOemReceivedDate: [""],
      clarificationToUserSentDate: [""],
      clarificationToUserReceivedDate: [""],
      techTecSentDate: [""],
      techTecReceivedDate: [""],
      minForFoSentDate: [""],
      minForFoReceivedDate: [""],
      sentToDtsDate: [""],
      foReceivedDate: [""],
      foTecSentDate: [""],
      foTecReceivedDate: [""],
      finalContractMinSentDate: [""],
      finalContractMinReceivedDate: [""],
      remarks: [""],
      status: [0],
      menuPosition: [""],
      isActive: [true],
      
    })
  }
  onStatus(dropdown) {
    if (dropdown.isUserInput) {
      this.method = dropdown.source.value;
      console.log(this.method);
    }
  }
  getSelectedSchoolByBranchLevelAndThirdLevel(){
    this.ProcurementService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res=>{
      this.selectedBaseSchoolName=res;
      console.log(res);
    }); 
  }
  getSelectedProcurementMethod(){
    this.ProcurementService.getSelectedProcurementMethod().subscribe(res=>{
      this.selectedProcurementMethod=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedEnvelope(){
    this.ProcurementService.getSelectedEnvelope().subscribe(res=>{
      this.selectedEnvelope=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedProcurementType(){
    this.ProcurementService.getSelectedProcurementType().subscribe(res=>{
      this.selectedProcurementType=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedGroupName(){
    this.ProcurementService.getSelectedGroupName().subscribe(res=>{
      this.selectedGroupName=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedEqupmentName(){
    this.ProcurementService.getSelectedEqupmentName().subscribe(res=>{
      this.selectedEquipmentName=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedControlled(){
    this.ProcurementService.getSelectedControlled().subscribe(res=>{
      this.selectedControlled=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedFcLc(){
    this.ProcurementService.getSelectedFcLc().subscribe(res=>{
      this.selectedFcLc=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedDgdpNssd(){
    this.ProcurementService.getSelectedDgdpNssd().subscribe(res=>{
      this.selectedDgdpNssd=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedTec(){
    this.ProcurementService.getSelectedTec().subscribe(res=>{
      this.selectedTec=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedTenderOpeningDateType(){
    this.ProcurementService.getSelectedTenderOpeningDateType().subscribe(res=>{
      this.selectedTenderOpeningDateType=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedPaymentStatus(){
    this.ProcurementService.getSelectedPaymentStatus().subscribe(res=>{
      this.selectedPaymentStatus=res
      console.log(res)
      console.log(res)
    }); 
  }

  onSubmit() {
    const id = this.ProcurementForm.get('procurementId').value;   

    //this.ShipInformationForm.get('dateOfCommission').setValue((new Date(this.ShipInformationForm.get('dateOfCommission').value)).toUTCString());

   const sentToDgdpNssdDate = this.sharedService.formatDateTime(this.ProcurementForm.get('sentToDgdpNssdDate').value)
   this.ProcurementForm.get('sentToDgdpNssdDate').setValue(sentToDgdpNssdDate);

   const tenderOpeningDate = this.sharedService.formatDateTime(this.ProcurementForm.get('tenderOpeningDate').value)
   this.ProcurementForm.get('tenderOpeningDate').setValue(tenderOpeningDate);

   const offerReceivedDate = this.sharedService.formatDateTime(this.ProcurementForm.get('offerReceivedDate').value)
   this.ProcurementForm.get('offerReceivedDate').setValue(offerReceivedDate);

   const sentForContractDate = this.sharedService.formatDateTime(this.ProcurementForm.get('sentForContractDate').value)
   this.ProcurementForm.get('sentForContractDate').setValue(sentForContractDate);

   const contractSignedDate = this.sharedService.formatDateTime(this.ProcurementForm.get('contractSignedDate').value)
   this.ProcurementForm.get('contractSignedDate').setValue(contractSignedDate);
   

   if(this.method ===1 ){
    const contractMinSentDate = this.sharedService.formatDateTime(this.ProcurementForm.get('contractMinSentDate').value)
    this.ProcurementForm.get('contractMinSentDate').setValue(contractMinSentDate);
 
    const contractMinReceived = this.sharedService.formatDateTime(this.ProcurementForm.get('contractMinReceived').value)
    this.ProcurementForm.get('contractMinReceived').setValue(contractMinReceived);
   }

   if(this.method === 2){
    const clarificationToOemSentDate = this.sharedService.formatDateTime(this.ProcurementForm.get('clarificationToOemSentDate').value)
    this.ProcurementForm.get('clarificationToOemSentDate').setValue(clarificationToOemSentDate);

    const clarificationToOemReceivedDate = this.sharedService.formatDateTime(this.ProcurementForm.get('clarificationToOemReceivedDate').value)
    this.ProcurementForm.get('clarificationToOemReceivedDate').setValue(clarificationToOemReceivedDate);

    const clarificationToUserSentDate = this.sharedService.formatDateTime(this.ProcurementForm.get('clarificationToUserSentDate').value)
    this.ProcurementForm.get('clarificationToUserSentDate').setValue(clarificationToUserSentDate);

    const clarificationToUserReceivedDate = this.sharedService.formatDateTime(this.ProcurementForm.get('clarificationToUserReceivedDate').value)
    this.ProcurementForm.get('clarificationToUserReceivedDate').setValue(clarificationToUserReceivedDate);

    const techTecSentDate = this.sharedService.formatDateTime(this.ProcurementForm.get('techTecSentDate').value)
    this.ProcurementForm.get('techTecSentDate').setValue(techTecSentDate);

    const techTecReceivedDate = this.sharedService.formatDateTime(this.ProcurementForm.get('techTecReceivedDate').value)
    this.ProcurementForm.get('techTecReceivedDate').setValue(techTecReceivedDate);

    const minForFoSentDate = this.sharedService.formatDateTime(this.ProcurementForm.get('minForFoSentDate').value)
    this.ProcurementForm.get('minForFoSentDate').setValue(minForFoSentDate);

    const sentToDtsDate = this.sharedService.formatDateTime(this.ProcurementForm.get('sentToDtsDate').value)
    this.ProcurementForm.get('sentToDtsDate').setValue(sentToDtsDate);

  
    const foReceivedDate = this.sharedService.formatDateTime(this.ProcurementForm.get('foReceivedDate').value)
    this.ProcurementForm.get('foReceivedDate').setValue(foReceivedDate);

  
    const foTecSentDate = this.sharedService.formatDateTime(this.ProcurementForm.get('foTecSentDate').value)
    this.ProcurementForm.get('foTecSentDate').setValue(foTecSentDate);

  
    const foTecReceivedDate = this.sharedService.formatDateTime(this.ProcurementForm.get('foTecReceivedDate').value)
    this.ProcurementForm.get('foTecReceivedDate').setValue(foTecReceivedDate);

  
    const finalContractMinSentDate = this.sharedService.formatDateTime(this.ProcurementForm.get('finalContractMinSentDate').value)
    this.ProcurementForm.get('finalContractMinSentDate').setValue(finalContractMinSentDate);

  
    const minForFoReceivedDate = this.sharedService.formatDateTime(this.ProcurementForm.get('minForFoReceivedDate').value)
    this.ProcurementForm.get('minForFoReceivedDate').setValue(minForFoReceivedDate);
    
    const finalContractMinReceivedDate = this.sharedService.formatDateTime(this.ProcurementForm.get('finalContractMinReceivedDate').value)
    this.ProcurementForm.get('finalContractMinReceivedDate').setValue(finalContractMinReceivedDate);

  

   }

   



    //   const value = this.ShipInformationForm.value[key];
    //   formData.append(key, value);
    // }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ProcurementService.update(+id,this.ProcurementForm.value).subscribe(response => {
            this.router.navigateByUrl('/procurement-management/procurement-list');
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
      this.ProcurementService.submit(this.ProcurementForm.value).subscribe(response => {
        this.router.navigateByUrl('/procurement-management/procurement-list');
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

import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcurementService } from '../../service/Procurement.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { AuthService } from '../../../../../src/app/core/service/auth.service';
import { MasterData } from '../../../../../src/assets/data/master-data';
import {BaseSchoolNameService} from '../../../../app/security/service/BaseSchoolName.service'
import {SharedService} from '../../../../../src/app/shared/shared.service'

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
  selectBaseSchoolName:SelectedModel[];
  selectedProcurementMethod:SelectedModel[];
  selectProcurementMethod:SelectedModel[];
  selectedProcurementType:SelectedModel[];
  selectProcurementType:SelectedModel[];
  selectedEnvelope:SelectedModel[];
  selectEnvelop:SelectedModel[];
  selectedGroupName:SelectedModel[];
  selectGroupName:SelectedModel[];
  selectedEquipmentName:SelectedModel[];
  selectEquipmentName:SelectedModel[];
  selectedControlled:SelectedModel[];
  selectControlled:SelectedModel[];
  selectedFcLc:SelectedModel[];
  selectFcLc:SelectedModel[];
  selectedDgdpNssd:SelectedModel[];
  selectDgdpNssd:SelectedModel[];
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
  selectedFinancialYears : any;

  constructor(private snackBar: MatSnackBar,private BaseSchoolNameService:BaseSchoolNameService,private authService: AuthService,private confirmService: ConfirmService,private ProcurementService: ProcurementService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, private sharedService : SharedService) { }

  ngOnInit(): void {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    //console.log(this.role, this.traineeId,  this.branchId)

    const id = this.route.snapshot.paramMap.get('procurementId'); 
    if (id) {
      this.pageTitle = 'Edit Procurement';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ProcurementService.find(+id).subscribe(res => {
        console.log(res);
    
        // Patch the simple fields
        this.ProcurementForm.patchValue({          
          procurementId: res.procurementId,
          equpmentNameId: res.equpmentNameId,
          baseSchoolNameId: res.baseSchoolNameId,
          qty: res.qty,
          groupNameId: res.groupNameId,
          dgdpNssdId: res.dgdpNssdId,
          ePrice: res.ePrice,
          budgetCode: res.budgetCode,
          financialYearId: res.financialYearId,
          controlledId: res.controlledId,
          fcLcId: res.fcLcId,
          sentForAIPDate: res.sentForAIPDate,
          aipApprovalDate: res.aipApprovalDate,
          indentSentDate: res.indentSentDate,
          tenderFloatedDate: res.tenderFloatedDate,
          offerReceivedDateAndUpdateEvaluation: res.offerReceivedDateAndUpdateEvaluation,
          sentForContractDate: res.sentForContractDate,
          contractSignedDate: res.contractSignedDate,
          isActive: res.isActive,
          remarks: res.remarks
        });
    
        // Patch the FormArray for procurementTenderOpeningDto
        const procurementTenderOpeningArray = this.ProcurementForm.get('procurementTenderOpeningDto') as FormArray;
        procurementTenderOpeningArray.clear(); // Clear existing controls before adding new ones
    
        if (res.procurementTenderOpeningDto && res.procurementTenderOpeningDto.length > 0) {
          res.procurementTenderOpeningDto.forEach(item => {
            procurementTenderOpeningArray.push(
              this.fb.group({
                tenderOpeningDate: [item.tenderOpeningDate],
                tenderOpeningCount: [item.tenderOpeningCount]
              })
            );
          });
        }
      });
    }
    else {
      this.pageTitle = 'Create Procurement';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.initializeForm();
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
    this.getSelectedFinancialYear();
  }


  initializeForm() {
    this.ProcurementForm = this.fb.group({
      procurementId: [0],
      equpmentNameId: [""],
      baseSchoolNameId: [],  
      qty: [''],
      dgdpNssdId: [""],
      ePrice: [""],
      fcLcId: [""],
      groupNameId: [],
      budgetCode: [''],
      financialYearId: [''],
      controlledId: [""],
      sentForAIPDate: [""],
      aipApprovalDate: [""],
      indentSentDate: [""],
      numberOfTenderOpening: [""],
      // tenderOpeningDate: [""],
      tenderFloatedDate: [""],
      offerReceivedDateAndUpdateEvaluation: [""],
      sentForContractDate: [""],
      contractSignedDate: [""],
      remarks: [""],
      isActive: [true],
      procurementTenderOpeningDto : this.fb.array([this.createTenderOpeningDateList(1)]) 
    });
  }  

  private createTenderOpeningDateList(count: number) {
    return this.fb.group({
      procurementTenderOpeningId: [0],
      procurementId: [''],
      tenderOpeningDate: [''],
      tenderOpeningCount: [this.getOrdinalSuffix(count)] 
    });
  }


  get procurementTenderOpeningDto(): FormArray {
    return this.ProcurementForm.get("procurementTenderOpeningDto") as FormArray;
  }
  
  addTenderOpeningDate() {
    const tenderOpeningArray = this.ProcurementForm.get('procurementTenderOpeningDto') as FormArray;
    const count = tenderOpeningArray.length + 1; // Count starts from 1
    const ordinalSuffix = this.getOrdinalSuffix(count);
  
    const newTenderOpening = this.fb.group({
      procurementTenderOpeningId: [0],
      procurementId: [''],
      tenderOpeningDate: [''],
      tenderOpeningCount: [ordinalSuffix] // Assign ordinal value (e.g., "1st", "2nd")
    });
  
    tenderOpeningArray.push(newTenderOpening);
  }
  
  removeTenderOpeningDate(index: number) {
    const tenderOpeningArray = this.ProcurementForm.get('procurementTenderOpeningDto') as FormArray;
    tenderOpeningArray.removeAt(index);
  }
  
  /** Converts number to ordinal format (1st, 2nd, 3rd, etc.) */
  getOrdinalSuffix(i: number): string {
    const j = i % 10, k = i % 100;
    if (j === 1 && k !== 11) return `${i}st`;
    if (j === 2 && k !== 12) return `${i}nd`;
    if (j === 3 && k !== 13) return `${i}rd`;
    return `${i}th`;
  }
  

  onStatus(dropdown) {
    if (dropdown.isUserInput) {
      this.method = dropdown.source.value;
      //console.log(this.method);
    }
  }
  getSelectedSchoolByBranchLevelAndThirdLevel(){
    this.ProcurementService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res=>{
      this.selectedBaseSchoolName=res;
      this.selectBaseSchoolName=res
    }); 
  }
  filterByShip(value:any){
    this.selectedBaseSchoolName=this.selectBaseSchoolName.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedProcurementMethod(){
    this.ProcurementService.getSelectedProcurementMethod().subscribe(res=>{
      this.selectedProcurementMethod=res
      this.selectProcurementMethod=res
    }); 
  }
  getSelectedFinancialYear(){
    this.ProcurementService.getSelectedFinancialYear().subscribe(res=>{
      this.selectedFinancialYears=res
    }); 
  }
  filterByProcurementMethod(value:any){
    this.selectedProcurementMethod=this.selectProcurementMethod.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedEnvelope(){
    this.ProcurementService.getSelectedEnvelope().subscribe(res=>{
      this.selectedEnvelope=res
      this.selectEnvelop=res
    }); 
  }

  filterByEnvelop(value:any){
    this.selectedEnvelope=this.selectEnvelop.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }

  getSelectedProcurementType(){
    this.ProcurementService.getSelectedProcurementType().subscribe(res=>{
      this.selectedProcurementType=res
      this.selectEnvelop=res
    }); 
  }
  filterByProcurementType(value:any){
    this.selectedProcurementType=this.selectEnvelop.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }

  getSelectedGroupName(){
    this.ProcurementService.getSelectedGroupName().subscribe(res=>{
      this.selectedGroupName=res
     this.selectGroupName=res
    }); 
  }
  filterByGroupName(value:any){
    this.selectedGroupName=this.selectGroupName.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedEqupmentName(){
    this.ProcurementService.getSelectedEqupmentName().subscribe(res=>{
      this.selectedEquipmentName=res
      this.selectEquipmentName=res
    }); 
  }
  filterByEquipementName(value:any){
    this.selectedEquipmentName=this.selectEquipmentName.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }

  getSelectedControlled(){
    this.ProcurementService.getSelectedControlled().subscribe(res=>{
      this.selectedControlled=res
      this.selectControlled=res
    }); 
  }
  filterByControlled(value:any){
    this.selectedControlled=this.selectControlled.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedFcLc(){
    this.ProcurementService.getSelectedFcLc().subscribe(res=>{
      this.selectedFcLc=res
      this.selectFcLc=res
    }); 
  }
  filterByFcLc(value:any){
    this.selectedFcLc=this.selectFcLc.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedDgdpNssd(){
    this.ProcurementService.getSelectedDgdpNssd().subscribe(res=>{
      this.selectedDgdpNssd=res
      this.selectDgdpNssd=res
    }); 
  }
  filterByDgdpNssd(value:any){
    this.selectedDgdpNssd=this.selectDgdpNssd.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedTec(){
    this.ProcurementService.getSelectedTec().subscribe(res=>{
      this.selectedTec=res
    }); 
  }
  getSelectedTenderOpeningDateType(){
    this.ProcurementService.getSelectedTenderOpeningDateType().subscribe(res=>{
      this.selectedTenderOpeningDateType=res
    }); 
  }
  getSelectedPaymentStatus(){
    this.ProcurementService.getSelectedPaymentStatus().subscribe(res=>{
      this.selectedPaymentStatus=res
    }); 
  }

  onSubmit() {
    const id = this.ProcurementForm.get('procurementId')?.value;   

    

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

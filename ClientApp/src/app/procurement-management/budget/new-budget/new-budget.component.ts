import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcurementService } from '../../service/Procurement.service';
import { BudgetService } from '../../service/budget.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { AuthService } from '../../../../../src/app/core/service/auth.service';
import { MasterData } from '../../../../../src/assets/data/master-data';
import { BaseSchoolNameService } from '../../../../app/security/service/BaseSchoolName.service'
import { SharedService } from '../../../../../src/app/shared/shared.service'

@Component({
  selector: 'app-new-budget',
  templateUrl: './new-budget.component.html',
  styleUrls: ['./new-budget.component.sass']
})
export class NewBudgetComponent implements OnInit {

  pageTitle: string;
  destination: string;
  btnText: string;
  BudgetForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel: SelectedModel[];
  selectedBaseName: SelectedModel[];
  selectedBranchLevel: SelectedModel[];
  selectedBaseSchoolName: SelectedModel[];
  selectBaseSchoolName: SelectedModel[];
  selectedProcurementMethod: SelectedModel[];
  selectProcurementMethod: SelectedModel[];
  selectedProcurementType: SelectedModel[];
  selectProcurementType: SelectedModel[];
  selectedEnvelope: SelectedModel[];
  selectEnvelop: SelectedModel[];
  selectedGroupName: SelectedModel[];
  selectGroupName: SelectedModel[];
  selectedEquipmentName: SelectedModel[];
  selectEquipmentName: SelectedModel[];
  selectedControlled: SelectedModel[];
  selectControlled: SelectedModel[];
  selectedFcLc: SelectedModel[];
  selectFcLc: SelectedModel[];
  selectedDgdpNssd: SelectedModel[];
  selectDgdpNssd: SelectedModel[];
  selectedTec: SelectedModel[];
  selectedTenderOpeningDateType: SelectedModel[];
  selectedPaymentStatus: SelectedModel[];
  traineeId: any;
  role: any;
  branchId: any;
  organizationId: any;
  selectedCommendingArea: any[];
  commendingAreaId: any;
  method: any;
  selectedFinancialYears: any;
  selectedBudgetCode: any;

  constructor(private snackBar: MatSnackBar, private BaseSchoolNameService: BaseSchoolNameService, private authService: AuthService, private confirmService: ConfirmService, private ProcurementService: ProcurementService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute, private sharedService: SharedService, private BudgetService: BudgetService) { }

  ngOnInit(): void {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId = this.authService.currentUserValue.traineeId.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();

    const id = this.route.snapshot.paramMap.get('budgetId');
    if (id) {
      this.pageTitle = 'Edit Procurement';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.BudgetService.find(+id).subscribe(res => {
        console.log(res);

        this.BudgetForm.patchValue({
          budgetId: res.budgetId,
          equpmentNameId: res.equpmentNameId,
          baseSchoolNameId: res.baseSchoolNameId || [], 
          qty: res.qty,
          dgdpNssdId: res.dgdpNssdId,
          contractualPriceLC: res.contractualPriceLC,
          contractualPriceFC: res.contractualPriceFC,
          budgetCodeId: res.budgetCodeId,
          financialYearId: res.financialYearId,
          controlledId: res.controlledId,
          deliveryDate: res.deliveryDate,
          fat: res.fat || '', // Ensuring default value
          foreignTraining: res.foreignTraining || '',
          allocatedBudget: res.allocatedBudget,
          allocatedBudgetDate: res.allocatedBudgetDate,
          revisedAllocatedBudget: res.revisedAllocatedBudget,
          revisedAllocatedBudgetDate: res.revisedAllocatedBudgetDate,
          billPainDNWEEFC: res.billPainDNWEEFC,
          billPainDNWEELC: res.billPainDNWEELC,
          billPaidOther: res.billPaidOther || '',
          termsOfPayment: res.termsOfPayment,
          remarks: res.remarks || '',
          isActive: true
        });


      });
    }
    else {
      this.pageTitle = 'Create Procurement';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.initializeForm();
    this.getSelectedSchoolByBranchLevelAndThirdLevel();
    this.getSelectedEqupmentName();
    this.getSelectedControlled();
    this.getSelectedProcurementType();
    this.getSelectedFcLc();
    this.getSelectedDgdpNssd();
    this.getSelectedTec();
    this.getSelectedTenderOpeningDateType();
    this.getSelectedPaymentStatus();
    this.getSelectedFinancialYear();
    this.getSelectedBudgetCode();
  }



  initializeForm() {
    this.BudgetForm = this.fb.group({
      budgetId: [0, Validators.required],
      equpmentNameId: [null],
      baseSchoolNameId: [[]], //  a list of IDs
      qty: [null],
      dgdpNssdId: [null],
      contractualPriceLC: [null],
      contractualPriceFC: [null],
      budgetCodeId: [null],
      financialYearId: [null],
      controlledId: [null],
      deliveryDate: [null],
      fat: [''],
      foreignTraining: [''],
      allocatedBudget: [null],
      allocatedBudgetDate: [null],
      revisedAllocatedBudget: [null],
      revisedAllocatedBudgetDate: [null],
      billPainDNWEEFC: [null],
      billPainDNWEELC: [null],
      billPaidOther: [''],
      termsOfPayment: [null],
      remarks: [''],
      isActive: [true], // Default to true
    });
  }


  getSelectedSchoolByBranchLevelAndThirdLevel() {
    this.ProcurementService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res => {
      this.selectedBaseSchoolName = res;
      this.selectBaseSchoolName = res
    });
  }
  filterByShip(value: any) {
    this.selectedBaseSchoolName = this.selectBaseSchoolName.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedProcurementMethod() {
    this.ProcurementService.getSelectedProcurementMethod().subscribe(res => {
      this.selectedProcurementMethod = res
      this.selectProcurementMethod = res
    });
  }
  getSelectedFinancialYear() {
    this.ProcurementService.getSelectedFinancialYear().subscribe(res => {
      this.selectedFinancialYears = res
    });
  }
  getSelectedBudgetCode() {
    this.ProcurementService.getSelectedBudgetCode().subscribe(res => {
      this.selectedBudgetCode = res
    });
  }
  filterByProcurementMethod(value: any) {
    this.selectedProcurementMethod = this.selectProcurementMethod.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }



  getSelectedProcurementType() {
    this.ProcurementService.getSelectedProcurementType().subscribe(res => {
      this.selectedProcurementType = res
      this.selectEnvelop = res
    });
  }
  filterByProcurementType(value: any) {
    this.selectedProcurementType = this.selectEnvelop.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }

  getSelectedGroupName() {
    this.ProcurementService.getSelectedGroupName().subscribe(res => {
      this.selectedGroupName = res
      this.selectGroupName = res
    });
  }
  filterByGroupName(value: any) {
    this.selectedGroupName = this.selectGroupName.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedEqupmentName() {
    this.ProcurementService.getSelectedEqupmentName().subscribe(res => {
      this.selectedEquipmentName = res
      this.selectEquipmentName = res
    });
  }
  filterByEquipementName(value: any) {
    this.selectedEquipmentName = this.selectEquipmentName.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }

  getSelectedControlled() {
    this.ProcurementService.getSelectedControlled().subscribe(res => {
      this.selectedControlled = res
      this.selectControlled = res
    });
  }
  filterByControlled(value: any) {
    this.selectedControlled = this.selectControlled.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedFcLc() {
    this.ProcurementService.getSelectedFcLc().subscribe(res => {
      this.selectedFcLc = res
      this.selectFcLc = res
    });
  }
  filterByFcLc(value: any) {
    this.selectedFcLc = this.selectFcLc.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedDgdpNssd() {
    this.ProcurementService.getSelectedDgdpNssd().subscribe(res => {
      this.selectedDgdpNssd = res
      this.selectDgdpNssd = res
    });
  }
  filterByDgdpNssd(value: any) {
    this.selectedDgdpNssd = this.selectDgdpNssd.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedTec() {
    this.ProcurementService.getSelectedTec().subscribe(res => {
      this.selectedTec = res
    });
  }
  getSelectedTenderOpeningDateType() {
    this.ProcurementService.getSelectedTenderOpeningDateType().subscribe(res => {
      this.selectedTenderOpeningDateType = res
    });
  }
  getSelectedPaymentStatus() {
    this.ProcurementService.getSelectedPaymentStatus().subscribe(res => {
      this.selectedPaymentStatus = res
    });
  }

  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;

    input.value = input.value.replace(/[^0-9.]/g, '');

    const parts = input.value.split('.');
    if (parts.length > 2) {
      input.value = parts[0] + '.' + parts.slice(1).join('');
    }

    const numericValue = parseFloat(input.value);
    if (numericValue > 100) {
      input.value = '100'; // or null, based on your requirement
    }
  }











  onSubmit() {
    const id = this.BudgetForm.get('budgetId')?.value;

    // 🔹 format dates
    const fieldsToFormat = [
      'deliveryDate',
      'allocatedBudgetDate',
      'revisedAllocatedBudgetDate'      
    ];

    fieldsToFormat.forEach(field => {
      const control = this.BudgetForm.get(field);
      if (control && control.value) {
        control.setValue(this.sharedService.formatDateTime(control.value));
      }
    });

   


    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        console.log(this.BudgetForm.value);

        if (result) {
          this.BudgetService.update(+id, this.BudgetForm.value).subscribe(response => {
            this.router.navigateByUrl('/procurement-management/budget-list');
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
      this.BudgetService.submit(this.BudgetForm.value).subscribe(response => {
        this.router.navigateByUrl('/procurement-management/budget-list');
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

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BudgetCodeService } from '../../service/budget-code.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { MasterData } from '../../../../../src/assets/data/master-data';
import { BudgetCode } from '../../models/BudgetCode';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from '../../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-budget-code',
  templateUrl: './budget-code.component.html',
  styleUrls: ['./budget-code.component.sass']
})
export class BudgetCodeComponent implements OnInit {

  pageTitle: string;
  destination: string;
  btnText: string;
  BudgetCodeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel: SelectedModel[];

  masterData = MasterData;
  ELEMENT_DATA: BudgetCode[] = [];
  isLoading = false;

  paging = {
    pageIndex: 1,
    pageSize: 10,
    length: 1
  }
  searchText = "";

  displayedColumns: string[] = ['ser', 'budgetCodeName', 'code', 'remarks', 'actions'];
  dataSource: MatTableDataSource<BudgetCode> = new MatTableDataSource();

  selection = new SelectionModel<BudgetCode>(true, []);

  constructor(private snackBar: MatSnackBar, private confirmService: ConfirmService, private BudgetCodeService: BudgetCodeService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('budgetCodeId');
      if (id) {
        this.pageTitle = 'Edit BudgetCode';
        this.destination = "Edit";
        this.btnText = 'Update';
        this.BudgetCodeService.find(+id).subscribe(
          res => {
            this.BudgetCodeForm.patchValue({
              budgetCodeId: res.budgetCodeId,
              budgetCodeName: res.budgetCodeName,
              code: res.code,
              remarks: res.remarks,
              isActive: res.isActive
            });
          }
        );
      } else {
        this.pageTitle = 'Create BudgetCode';
        this.destination = "Add";
        this.btnText = 'Save';
      }
    })
    this.intitializeForm();
    this.getBudgetCodes();
  }
  intitializeForm() {
    this.BudgetCodeForm = this.fb.group({
      budgetCodeId: [0],
      budgetCodeName: [''],
      code: [''],
      remarks: [''],

      isActive: [true]
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getBudgetCodes();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getBudgetCodes();
  }

  getBudgetCodes() {
    this.isLoading = true;
    this.BudgetCodeService.getBudgetCodes(this.paging.pageIndex, this.paging.pageSize, this.searchText).subscribe(response => {

      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
    })
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  deleteItem(row) {
    const id = row.budgetCodeId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {

      if (result) {
        this.BudgetCodeService.delete(id).subscribe(() => {
          this.getBudgetCodes();
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

  onSubmit() {
    const id = this.BudgetCodeForm.get('budgetCodeId')?.value;
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {

        if (result) {
          this.BudgetCodeService.update(+id, this.BudgetCodeForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/add-budget-coden');
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
      this.BudgetCodeService.submit(this.BudgetCodeForm.value).subscribe(response => {
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

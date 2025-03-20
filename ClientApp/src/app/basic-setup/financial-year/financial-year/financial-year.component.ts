import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FinancialYearService } from '../../service/financial-year.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { MasterData } from '../../../../../src/assets/data/master-data';
import { FinancialYear } from '../../models/FinancialYear';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from '../../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-financial-year',
  templateUrl: './financial-year.component.html',
  styleUrls: ['./financial-year.component.sass']
})
export class FinancialYearComponent implements OnInit {

  
  pageTitle: string;
  destination: string;
  btnText: string;
  FinancialYearForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel: SelectedModel[];

  masterData = MasterData;
  ELEMENT_DATA: FinancialYear[] = [];
  isLoading = false;

  paging = {
    pageIndex: 1,
    pageSize: 10,
    length: 1
  }
  searchText = "";

  displayedColumns: string[] = ['ser', 'financialYearName',  'remark', 'actions'];
  dataSource: MatTableDataSource<FinancialYear> = new MatTableDataSource();

  selection = new SelectionModel<FinancialYear>(true, []);

  constructor(private snackBar: MatSnackBar, private confirmService: ConfirmService, private FinancialYearService: FinancialYearService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('financialYearId');
      if (id) {
        this.pageTitle = 'Edit FinancialYear';
        this.destination = "Edit";
        this.btnText = 'Update';
        this.FinancialYearService.find(+id).subscribe(
          res => {
            this.FinancialYearForm.patchValue({
              financialYearId: res.financialYearId,
              financialYearName: res.financialYearName,
              remark: res.remark,
              isActive: res.isActive
            });
          }
        );
      } else {
        this.pageTitle = 'Create FinancialYear';
        this.destination = "Add";
        this.btnText = 'Save';
      }
    })
    this.intitializeForm();
    this.getFinancialYears();
  }
  intitializeForm() {
    this.FinancialYearForm = this.fb.group({
      financialYearId: [0],
      financialYearName: [''],
      remark: [''],
      isActive: [true]
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getFinancialYears();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getFinancialYears();
  }

  getFinancialYears() {
    this.isLoading = true;
    this.FinancialYearService.getFinancialYears(this.paging.pageIndex, this.paging.pageSize, this.searchText).subscribe(response => {

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
    const id = row.financialYearId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {

      if (result) {
        this.FinancialYearService.delete(id).subscribe(() => {
          this.getFinancialYears();
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
    const id = this.FinancialYearForm.get('financialYearId')?.value;
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {

        if (result) {
          this.FinancialYearService.update(+id, this.FinancialYearForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/add-financial-year');
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
      this.FinancialYearService.submit(this.FinancialYearForm.value).subscribe(response => {
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

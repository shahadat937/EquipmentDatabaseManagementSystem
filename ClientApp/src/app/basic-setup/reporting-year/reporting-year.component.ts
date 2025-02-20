import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportingYearService } from '../service/ReportingYear.service';
import { SelectedModel } from '../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from '../../../../src/assets/data/master-data';
import { ReportingYear } from '../models/ReportingYear';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from '../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-reporting-year',
  templateUrl: './reporting-year.component.html',
  styleUrls: ['./reporting-year.component.sass']
})
export class ReportingYearComponent implements OnInit {

  pageTitle: string;
  destination:string;
  btnText:string;
  ReportingYearForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: ReportingYear[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'year','isActive','actions'];
  dataSource: MatTableDataSource<ReportingYear> = new MatTableDataSource();

  selection = new SelectionModel<ReportingYear>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ReportingYearService: ReportingYearService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    
    this.route.paramMap.subscribe(params => { 
      const id = +params.get('reportingYearId');    
      if (id) {
        this.pageTitle = 'Edit ReportingYear';
        this.destination = "Edit";
        this.btnText = 'Update';
        this.loadData(id);  // Call the method to load data with the id
      } else {
        this.pageTitle = 'Create ReportingYear';
        this.destination = "Add";
        this.btnText = 'Save';
      }
    });
  
    this.intitializeForm();
    this.getReportingYears();
  }
  
  intitializeForm() {
    this.ReportingYearForm = this.fb.group({
      reportingYearId: [0],
      year: ['', Validators.required],
      isActive: ['', Validators.required]
    })
  }

  loadData(id){
    this.ReportingYearService.find(+id).subscribe(
      res => {
        this.ReportingYearForm.patchValue({          

          reportingYearId: res.reportingYearId,
          year:  res.year,
          isActive:  res.isActive ? 1 : 0
        });          
      }
    );
  }
  
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getReportingYears();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getReportingYears();
  } 

  getReportingYears() {
    this.isLoading = true;
    this.ReportingYearService.getReportingYears(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.reportingYearId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ReportingYearService.delete(id).subscribe(() => {
          this.getReportingYears();
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
    const id = this.ReportingYearForm.get('reportingYearId')?.value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ReportingYearService.update(+id,this.ReportingYearForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-reportingyear');
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
      this.ReportingYearService.submit(this.ReportingYearForm.value).subscribe(response => {
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

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportingMonthService } from '../../service/ReportingMonth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { ReportingMonth } from '../../models/ReportingMonth';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-reportingmonth',
  templateUrl: './new-reportingmonth.component.html',
  styleUrls: ['./new-reportingmonth.component.sass']
})
export class ReportingMonthComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ReportingMonthForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: ReportingMonth[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<ReportingMonth> = new MatTableDataSource();

  selection = new SelectionModel<ReportingMonth>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ReportingMonthService: ReportingMonthService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('reportingMonthId'); 
    if (id) {
      this.pageTitle = 'Edit ReportingMonth';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ReportingMonthService.find(+id).subscribe(
        res => {
          this.ReportingMonthForm.patchValue({          

            reportingMonthId: res.reportingMonthId,
            name:  res.name,
            shortName:  res.shortName,
            remarks:  res.remarks,
            status:  res.status,
            menuPosition:  res.menuPosition,
            isActive:  res.isActive
          });          
        }
      );
    } else {
      this.pageTitle = 'Create ReportingMonth';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getReportingMonths();
  }
  intitializeForm() {
    this.ReportingMonthForm = this.fb.group({
      reportingMonthId: [0],
      name: ['', Validators.required],
      shortName:   [''],
      remarks:  [''],
      status:  [''],
      menuPosition:  [''],
      isActive: [true]
    })
  }
  
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getReportingMonths();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getReportingMonths();
  } 

  getReportingMonths() {
    this.isLoading = true;
    this.ReportingMonthService.getReportingMonths(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.reportingMonthId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ReportingMonthService.delete(id).subscribe(() => {
          this.getReportingMonths();
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
    const id = this.ReportingMonthForm.get('reportingMonthId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ReportingMonthService.update(+id,this.ReportingMonthForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-reportingmonth');
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
      this.ReportingMonthService.submit(this.ReportingMonthForm.value).subscribe(response => {
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

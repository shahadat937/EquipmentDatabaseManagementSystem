import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportTypeService } from '../../service/ReportType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { MasterData } from 'src/assets/data/master-data';
import { ReportType } from '../../models/ReportType';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-reporttype',
  templateUrl: './new-reporttype.component.html',
  styleUrls: ['./new-reporttype.component.sass']
})
export class NewReportTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ReportTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: ReportType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<ReportType> = new MatTableDataSource();

  selection = new SelectionModel<ReportType>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ReportTypeService: ReportTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('reportTypeId'); 
    if (id) {
      this.pageTitle = 'Edit Report Type';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ReportTypeService.find(+id).subscribe(
        res => {
          this.ReportTypeForm.patchValue({          

            reportTypeId: res.reportTypeId,
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
      this.pageTitle = 'Create Report Type';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getReportTypes();
  }
  intitializeForm() {
    this.ReportTypeForm = this.fb.group({
      reportTypeId: [0],
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
    this.getReportTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getReportTypes();
  } 

  getReportTypes() {
    this.isLoading = true;
    this.ReportTypeService.getReportTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.reportTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ReportTypeService.delete(id).subscribe(() => {
          this.getReportTypes();
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
    const id = this.ReportTypeForm.get('reportTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ReportTypeService.update(+id,this.ReportTypeForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-reporttype');
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
      this.ReportTypeService.submit(this.ReportTypeForm.value).subscribe(response => {
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

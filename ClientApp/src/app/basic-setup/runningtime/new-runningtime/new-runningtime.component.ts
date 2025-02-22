import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RunningTimeService } from '../../service/RunningTime.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { RunningTime } from '../../models/RunningTime';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-runningtime',
  templateUrl: './new-runningtime.component.html',
  styleUrls: ['./new-runningtime.component.sass']
})
export class NewRunningTimeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  RunningTimeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: RunningTime[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<RunningTime> = new MatTableDataSource();

  selection = new SelectionModel<RunningTime>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private RunningTimeService: RunningTimeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('runningTimeId'); 
    if (id) {
      this.pageTitle = 'Edit Running Time';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.RunningTimeService.find(+id).subscribe(
        res => {
          this.RunningTimeForm.patchValue({          

            runningTimeId: res.runningTimeId,
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
      this.pageTitle = 'Create Running Time';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getRunningTimes();
  }
  intitializeForm() {
    this.RunningTimeForm = this.fb.group({
      runningTimeId: [0],
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
    this.getRunningTimes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getRunningTimes();
  } 

  getRunningTimes() {
    this.isLoading = true;
    this.RunningTimeService.getRunningTimes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.runningTimeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.RunningTimeService.delete(id).subscribe(() => {
          this.getRunningTimes();
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
    const id = this.RunningTimeForm.get('runningTimeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.RunningTimeService.update(+id,this.RunningTimeForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-runningtime');
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
      this.RunningTimeService.submit(this.RunningTimeForm.value).subscribe(response => {
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

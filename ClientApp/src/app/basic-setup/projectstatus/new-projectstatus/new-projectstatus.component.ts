import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectStatusService } from '../../service/ProjectStatus.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { MasterData } from 'src/assets/data/master-data';
import { ProjectStatus } from '../../models/ProjectStatus';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-new-projectstatus',
  templateUrl: './new-projectstatus.component.html',
  styleUrls: ['./new-projectstatus.component.sass']
})
export class NewProjectStatusComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ProjectStatusForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: ProjectStatus[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<ProjectStatus> = new MatTableDataSource();

  selection = new SelectionModel<ProjectStatus>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ProjectStatusService: ProjectStatusService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('projectStatusId'); 
    if (id) {
      this.pageTitle = 'Edit Project Status';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ProjectStatusService.find(+id).subscribe(
        res => {
          this.ProjectStatusForm.patchValue({          

            projectStatusId: res.projectStatusId,
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
      this.pageTitle = 'Create Project Status';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getProjectStatuses();
  }
  intitializeForm() {
    this.ProjectStatusForm = this.fb.group({
      projectStatusId: [0],
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
    this.getProjectStatuses();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getProjectStatuses();
  } 

  getProjectStatuses() {
    this.isLoading = true;
    this.ProjectStatusService.getProjectStatuses(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.projectStatusId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ProjectStatusService.delete(id).subscribe(() => {
          this.getProjectStatuses();
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
    const id = this.ProjectStatusForm.get('projectStatusId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ProjectStatusService.update(+id,this.ProjectStatusForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-projectstatus');
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
      this.ProjectStatusService.submit(this.ProjectStatusForm.value).subscribe(response => {
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

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ControlledService } from '../../service/Controlled.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { MasterData } from 'src/assets/data/master-data';
import { Controlled } from '../../models/Controlled';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-new-controlled',
  templateUrl: './new-controlled.component.html',
  styleUrls: ['./new-controlled.component.sass']
})
export class NewControlledComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ControlledForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: Controlled[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<Controlled> = new MatTableDataSource();

  selection = new SelectionModel<Controlled>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ControlledService: ControlledService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('controlledId'); 
    if (id) {
      this.pageTitle = 'Edit Controlled';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ControlledService.find(+id).subscribe(
        res => {
          this.ControlledForm.patchValue({          

            controlledId: res.controlledId,
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
      this.pageTitle = 'Create Controlled';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getControlleds();
  }
  intitializeForm() {
    this.ControlledForm = this.fb.group({
      controlledId: [0],
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
    this.getControlleds();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getControlleds();
  } 

  getControlleds() {
    this.isLoading = true;
    this.ControlledService.getControlleds(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.controlledId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {

      if (result) {
        this.ControlledService.delete(id).subscribe(() => {
          this.getControlleds();
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
    const id = this.ControlledForm.get('controlledId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ControlledService.update(+id,this.ControlledForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-controlled');
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
      this.ControlledService.submit(this.ControlledForm.value).subscribe(response => {
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

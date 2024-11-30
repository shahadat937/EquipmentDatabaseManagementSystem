import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReturnTypeService } from '../../service/ReturnType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { ReturnType } from '../../models/ReturnType';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-returntype',
  templateUrl: './new-returntype.component.html',
  styleUrls: ['./new-returntype.component.sass']
})
export class ReturnTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ReturnTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: ReturnType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<ReturnType> = new MatTableDataSource();

  selection = new SelectionModel<ReturnType>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ReturnTypeService: ReturnTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('returnTypeId'); 
    if (id) {
      this.pageTitle = 'Edit ReturnType';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ReturnTypeService.find(+id).subscribe(
        res => {
          this.ReturnTypeForm.patchValue({          

            returnTypeId: res.returnTypeId,
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
      this.pageTitle = 'Create ReturnType';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getReturnTypes();
  }
  intitializeForm() {
    this.ReturnTypeForm = this.fb.group({
      returnTypeId: [0],
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
    this.getReturnTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getReturnTypes();
  } 

  getReturnTypes() {
    this.isLoading = true;
    this.ReturnTypeService.getReturnTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.returnTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ReturnTypeService.delete(id).subscribe(() => {
          this.getReturnTypes();
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
    const id = this.ReturnTypeForm.get('returnTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ReturnTypeService.update(+id,this.ReturnTypeForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-returntype');
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
      this.ReturnTypeService.submit(this.ReturnTypeForm.value).subscribe(response => {
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

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QuarterlyReturnNoTypeService } from '../../service/QuarterlyReturnNoType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { QuarterlyReturnNoType } from '../../models/QuarterlyReturnNoType';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-quarterlyreturnnotype',
  templateUrl: './new-quarterlyreturnnotype.component.html',
  styleUrls: ['./new-quarterlyreturnnotype.component.sass']
})
export class NewQuarterlyReturnNoTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  QuarterlyReturnNoTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: QuarterlyReturnNoType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<QuarterlyReturnNoType> = new MatTableDataSource();

  selection = new SelectionModel<QuarterlyReturnNoType>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private QuarterlyReturnNoTypeService: QuarterlyReturnNoTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('quarterlyReturnNoTypeId'); 
    if (id) {
      this.pageTitle = 'Edit Quarterly Return No Type';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.QuarterlyReturnNoTypeService.find(+id).subscribe(
        res => {
          this.QuarterlyReturnNoTypeForm.patchValue({          

            quarterlyReturnNoTypeId: res.quarterlyReturnNoTypeId,
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
      this.pageTitle = 'Create Quarterly Return No Type';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getQuarterlyReturnNoTypes();
  }
  intitializeForm() {
    this.QuarterlyReturnNoTypeForm = this.fb.group({
      quarterlyReturnNoTypeId: [0],
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
    this.getQuarterlyReturnNoTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getQuarterlyReturnNoTypes();
  } 

  getQuarterlyReturnNoTypes() {
    this.isLoading = true;
    this.QuarterlyReturnNoTypeService.getQuarterlyReturnNoTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.quarterlyReturnNoTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.QuarterlyReturnNoTypeService.delete(id).subscribe(() => {
          this.getQuarterlyReturnNoTypes();
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
    const id = this.QuarterlyReturnNoTypeForm.get('quarterlyReturnNoTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.QuarterlyReturnNoTypeService.update(+id,this.QuarterlyReturnNoTypeForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-quarterlyreturnnotype');
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
      this.QuarterlyReturnNoTypeService.submit(this.QuarterlyReturnNoTypeForm.value).subscribe(response => {
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

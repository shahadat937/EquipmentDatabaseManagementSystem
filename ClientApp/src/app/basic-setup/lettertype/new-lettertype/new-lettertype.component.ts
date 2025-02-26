import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LetterTypeService } from '../../service/LetterType.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from '../../../../../src/assets/data/master-data';
import { LetterType } from '../../models/LetterType';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from '../../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-new-lettertype',
  templateUrl: './new-lettertype.component.html',
  styleUrls: ['./new-lettertype.component.sass']
})
export class NewLetterTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  LetterTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: LetterType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<LetterType> = new MatTableDataSource();

  selection = new SelectionModel<LetterType>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private LetterTypeService: LetterTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params=>{
      const id = params.get('letterTypeId'); 
      if (id) {
        this.pageTitle = 'Edit Letter Type';
        this.destination = "Edit";
        this.btnText = 'Update';
        this.LetterTypeService.find(+id).subscribe(
          res => {
            this.LetterTypeForm.patchValue({          
  
              letterTypeId: res.letterTypeId,
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
        this.pageTitle = 'Create LetterType';
        this.destination = "Add";
        this.btnText = 'Save';
      }
    })
    this.intitializeForm();
    this.getLetterTypes();
  }
  intitializeForm() {
    this.LetterTypeForm = this.fb.group({
      letterTypeId: [0],
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
    this.getLetterTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getLetterTypes();
  } 

  getLetterTypes() {
    this.isLoading = true;
    this.LetterTypeService.getLetterTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.letterTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.LetterTypeService.delete(id).subscribe(() => {
          this.getLetterTypes();
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
    const id = this.LetterTypeForm.get('letterTypeId')?.value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.LetterTypeService.update(+id,this.LetterTypeForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-lettertype');
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
      this.LetterTypeService.submit(this.LetterTypeForm.value).subscribe(response => {
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

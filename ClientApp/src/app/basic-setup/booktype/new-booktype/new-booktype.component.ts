import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookTypeService } from '../../service/BookType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { BookType } from '../../models/BookType';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-booktype',
  templateUrl: './new-booktype.component.html',
  styleUrls: ['./new-booktype.component.sass']
})
export class NewBookTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  BookTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: BookType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: 1,
    pageSize: 10,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','menuPosition','actions'];
  dataSource: MatTableDataSource<BookType> = new MatTableDataSource();

  selection = new SelectionModel<BookType>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private BookTypeService: BookTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('bookTypeId'); 
    if (id) {
      this.pageTitle = 'Edit Book Type';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.BookTypeService.find(+id).subscribe(
        res => {
          this.BookTypeForm.patchValue({          

            bookTypeId: res.bookTypeId,
            name:  res.name,
            //shortName:  res.shortName,
            remarks:  res.remarks,
            menuPosition:  res.menuPosition,
            isActive:  res.isActive
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Book Type';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getBookTypes();
  }
  intitializeForm() {
    this.BookTypeForm = this.fb.group({
      bookTypeId: [0],
      name: ['', Validators.required],
      //shortName:   [''],
      remarks:  [''],
      menuPosition:  [''],
      isActive: [true]
    })
  }
  
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getBookTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getBookTypes();
  } 

  getBookTypes() {
    this.isLoading = true;
    this.BookTypeService.getBookTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.bookTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.BookTypeService.delete(id).subscribe(() => {
          this.getBookTypes();
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
    const id = this.BookTypeForm.get('bookTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.BookTypeService.update(+id,this.BookTypeForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-booktype');
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
      this.BookTypeService.submit(this.BookTypeForm.value).subscribe(response => {
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

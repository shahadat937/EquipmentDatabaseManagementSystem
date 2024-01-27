import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookUserManualBRInfoService } from '../../service/BookUserManualBRInfo.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { BookUserManualBRInfo } from '../../models/BookUserManualBRInfo';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-new-bookusermanualbrinfo',
  templateUrl: './new-bookusermanualbrinfo.component.html',
  styleUrls: ['./new-bookusermanualbrinfo.component.sass']
})
export class NewBookUserManualBRInfoComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  BookUserManualBRInfoForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  selectedBaseSchoolName:SelectedModel[]; 
  selectedBookType:SelectedModel[]; 
  masterData = MasterData;
  ELEMENT_DATA: BookUserManualBRInfo[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'bookType','bookName','shipName','remarks','actions'];
  dataSource: MatTableDataSource<BookUserManualBRInfo> = new MatTableDataSource();

  selection = new SelectionModel<BookUserManualBRInfo>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private BookUserManualBRInfoService: BookUserManualBRInfoService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('bookUserManualBRInfoId'); 
    if (id) {
      this.pageTitle = 'Edit Book User Manual & BR Info';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.BookUserManualBRInfoService.find(+id).subscribe(
        res => {
          this.BookUserManualBRInfoForm.patchValue({          

            bookUserManualBRInfoId: res.bookUserManualBRInfoId,
            bookTypeId:res.bookTypeId,
            baseSchoolNameId:res.baseSchoolNameId,
            bookName:  res.bookName,
            uploadDocument:  res.uploadDocument,
            remarks:  res.remarks,
            menuPosition:  res.menuPosition,
            isActive:  res.isActive
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Book User Manual & BR Info';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getBookUserManualBRInfos();
    this.getSelectedSchoolByBranchLevelAndThirdLevel();
    this.getSelectedBookType();
  }
  intitializeForm() {
    this.BookUserManualBRInfoForm = this.fb.group({
      bookUserManualBRInfoId: [0],
      bookTypeId:[""],
      baseSchoolNameId:[""],
      bookName: ['', Validators.required],
      uploadDocument:   [''],
      document:[''],
      remarks:  [''],
      menuPosition:  [''],
      isActive: [true]
    })
  }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log(file);
      this.BookUserManualBRInfoForm.patchValue({
        document: file,
      });
    }
  }
  getSelectedSchoolByBranchLevelAndThirdLevel(){
    this.BookUserManualBRInfoService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res=>{
      this.selectedBaseSchoolName=res;
      console.log(res);
    }); 
  }
  getSelectedBookType(){
    this.BookUserManualBRInfoService.getSelectedBookType().subscribe(res=>{
      this.selectedBookType=res
      console.log(res)
      console.log(res)
    }); 
  }
  
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getBookUserManualBRInfos();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getBookUserManualBRInfos();
  } 

  getBookUserManualBRInfos() {
    this.isLoading = true;
    this.BookUserManualBRInfoService.getBookUserManualBRInfos(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.bookUserManualBRInfoId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.BookUserManualBRInfoService.delete(id).subscribe(() => {
          this.getBookUserManualBRInfos();
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
    const id = this.BookUserManualBRInfoForm.get('bookUserManualBRInfoId').value;  
    const formData = new FormData();
    for (const key of Object.keys(this.BookUserManualBRInfoForm.value)) {
      const value = this.BookUserManualBRInfoForm.value[key];
      formData.append(key, value);
    } 
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.BookUserManualBRInfoService.update(+id,formData).subscribe(response => {
           this.router.navigateByUrl('/miscellaneous/add-bookusermanualbrinfo');
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
      this.BookUserManualBRInfoService.submit(formData).subscribe(response => {
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

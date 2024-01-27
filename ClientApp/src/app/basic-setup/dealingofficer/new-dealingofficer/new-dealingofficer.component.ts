import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DealingOfficerService } from '../../service/DealingOfficer.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { DealingOfficer } from '../../models/DealingOfficer';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-new-dealingofficer',
  templateUrl: './new-dealingofficer.component.html',
  styleUrls: ['./new-dealingofficer.component.sass']
})
export class NewDealingOfficerComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  DealingOfficerForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: DealingOfficer[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<DealingOfficer> = new MatTableDataSource();

  selection = new SelectionModel<DealingOfficer>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private DealingOfficerService: DealingOfficerService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('dealingOfficerId'); 
    if (id) {
      this.pageTitle = 'Edit Dealing Officer';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.DealingOfficerService.find(+id).subscribe(
        res => {
          this.DealingOfficerForm.patchValue({          

            dealingOfficerId: res.dealingOfficerId,
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
      this.pageTitle = 'Create Dealing Officer';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getDealingOfficers();
  }
  intitializeForm() {
    this.DealingOfficerForm = this.fb.group({
      dealingOfficerId: [0],
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
    this.getDealingOfficers();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getDealingOfficers();
  } 

  getDealingOfficers() {
    this.isLoading = true;
    this.DealingOfficerService.getDealingOfficers(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.dealingOfficerId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.DealingOfficerService.delete(id).subscribe(() => {
          this.getDealingOfficers();
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
    const id = this.DealingOfficerForm.get('dealingOfficerId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.DealingOfficerService.update(+id,this.DealingOfficerForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-dealingofficer');
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
      this.DealingOfficerService.submit(this.DealingOfficerForm.value).subscribe(response => {
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

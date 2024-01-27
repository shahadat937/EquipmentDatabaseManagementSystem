import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AcquisitionMethodService } from '../../service/AcquisitionMethod.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { AcquisitionMethod } from '../../models/AcquisitionMethod';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-new-acquisition-method',
  templateUrl: './new-acquisition-method.component.html',
  styleUrls: ['./new-acquisition-method.component.sass']
})
export class NewAcquisitionMethodComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  AcquisitionMethodForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: AcquisitionMethod[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<AcquisitionMethod> = new MatTableDataSource();

  selection = new SelectionModel<AcquisitionMethod>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private AcquisitionMethodService: AcquisitionMethodService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('acquisitionMethodId'); 
    if (id) {
      this.pageTitle = 'Edit AcquisitionMethod';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.AcquisitionMethodService.find(+id).subscribe(
        res => {
          this.AcquisitionMethodForm.patchValue({          

            acquisitionMethodId: res.acquisitionMethodId,
            name:  res.name,
            shortName:  res.shortName,
            remarks:  res.remarks,
           // status:  res.status,
            menuPosition:  res.menuPosition,
            isActive:  res.isActive
          });          
        }
      );
    } else {
      this.pageTitle = 'Create AcquisitionMethod';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getAcquisitionMethods();
  }
  intitializeForm() {
    this.AcquisitionMethodForm = this.fb.group({
      acquisitionMethodId: [0],
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
    this.getAcquisitionMethods();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getAcquisitionMethods();
  } 

  getAcquisitionMethods() {
    this.isLoading = true;
    this.AcquisitionMethodService.getAcquisitionMethods(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.acquisitionMethodId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.AcquisitionMethodService.delete(id).subscribe(() => {
          this.getAcquisitionMethods();
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
    const id = this.AcquisitionMethodForm.get('acquisitionMethodId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.AcquisitionMethodService.update(+id,this.AcquisitionMethodForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-acquisitionmethod');
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
      this.AcquisitionMethodService.submit(this.AcquisitionMethodForm.value).subscribe(response => {
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

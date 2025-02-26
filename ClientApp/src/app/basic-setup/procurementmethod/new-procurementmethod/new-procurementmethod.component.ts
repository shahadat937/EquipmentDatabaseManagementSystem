import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcurementMethodService } from '../../service/ProcurementMethod.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from '../../../../../src/assets/data/master-data';
import { ProcurementMethod } from '../../models/ProcurementMethod';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from '../../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-new-procurementmethod',
  templateUrl: './new-procurementmethod.component.html',
  styleUrls: ['./new-procurementmethod.component.sass']
})
export class NewProcurementMethodComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ProcurementMethodForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: ProcurementMethod[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<ProcurementMethod> = new MatTableDataSource();

  selection = new SelectionModel<ProcurementMethod>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ProcurementMethodService: ProcurementMethodService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params=>{
      const id = params.get('procurementMethodId'); 
      if (id) {
        this.pageTitle = 'Edit Procurement Method';
        this.destination = "Edit";
        this.btnText = 'Update';
        this.ProcurementMethodService.find(+id).subscribe(
          res => {
            this.ProcurementMethodForm.patchValue({          
  
              procurementMethodId: res.procurementMethodId,
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
        this.pageTitle = 'Create Procurement Method';
        this.destination = "Add";
        this.btnText = 'Save';
      }
    })
    this.intitializeForm();
    this.getProcurementMethods();
  }
  intitializeForm() {
    this.ProcurementMethodForm = this.fb.group({
      procurementMethodId: [0],
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
    this.getProcurementMethods();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getProcurementMethods();
  } 

  getProcurementMethods() {
    this.isLoading = true;
    this.ProcurementMethodService.getProcurementMethods(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.procurementMethodId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.ProcurementMethodService.delete(id).subscribe(() => {
          this.getProcurementMethods();
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
    const id = this.ProcurementMethodForm.get('procurementMethodId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ProcurementMethodService.update(+id,this.ProcurementMethodForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-procurementmethod');
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
      this.ProcurementMethodService.submit(this.ProcurementMethodForm.value).subscribe(response => {
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

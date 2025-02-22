import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcurementTypeService } from '../../service/ProcurementType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { ProcurementType } from '../../models/ProcurementType';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-procurementtype',
  templateUrl: './new-procurementtype.component.html',
  styleUrls: ['./new-procurementtype.component.sass']
})
export class NewProcurementTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ProcurementTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: ProcurementType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<ProcurementType> = new MatTableDataSource();

  selection = new SelectionModel<ProcurementType>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ProcurementTypeService: ProcurementTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('procurementTypeId'); 
    if (id) {
      this.pageTitle = 'Edit Procurement Type';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ProcurementTypeService.find(+id).subscribe(
        res => {
          this.ProcurementTypeForm.patchValue({          

            procurementTypeId: res.procurementTypeId,
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
      this.pageTitle = 'Create Procurement Type';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getProcurementTypes();
  }
  intitializeForm() {
    this.ProcurementTypeForm = this.fb.group({
      procurementTypeId: [0],
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
    this.getProcurementTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getProcurementTypes();
  } 

  getProcurementTypes() {
    this.isLoading = true;
    this.ProcurementTypeService.getProcurementTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.procurementTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.ProcurementTypeService.delete(id).subscribe(() => {
          this.getProcurementTypes();
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
    const id = this.ProcurementTypeForm.get('procurementTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ProcurementTypeService.update(+id,this.ProcurementTypeForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-procurementtype');
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
      this.ProcurementTypeService.submit(this.ProcurementTypeForm.value).subscribe(response => {
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

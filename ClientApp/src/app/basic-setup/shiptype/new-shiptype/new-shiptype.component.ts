import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ShipTypeService } from '../../service/ShipType.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from '../../../../../src/assets/data/master-data';
import { ShipType } from '../../models/ShipType';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from '../../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-new-shiptype',
  templateUrl: './new-shiptype.component.html',
  styleUrls: ['./new-shiptype.component.sass']
})
export class NewShipTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ShipTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: ShipType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks', 'actions'];
  dataSource: MatTableDataSource<ShipType> = new MatTableDataSource();

  selection = new SelectionModel<ShipType>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ShipTypeService: ShipTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('shipTypeId'); 
      if (id) {
        this.pageTitle = 'Edit ShipType';
        this.destination = "Edit";
        this.btnText = 'Update';
        this.loadShipType(+id);
      } else {
        this.pageTitle = 'Create ShipType';
        this.destination = "Add";
        this.btnText = 'Save';
        // this.intitializeForm();
      }
    });
    this.intitializeForm();
    this.getShipTypes();
  }
  
  loadShipType(id: number) {
    this.ShipTypeService.find(id).subscribe(res => {
      this.ShipTypeForm.patchValue({          
        shipTypeId: res.shipTypeId,
        name: res.name,
        shortName: res.shortName,
        remarks: res.remarks,
        status: res.status,
        menuPosition: res.menuPosition,
        isActive: res.isActive
      });          
    });
  }
  
  intitializeForm() {
    this.ShipTypeForm = this.fb.group({
      shipTypeId: [0],
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
    this.getShipTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getShipTypes();
  } 

  getShipTypes() {
    this.isLoading = true;
    this.ShipTypeService.getShipTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.shipTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.ShipTypeService.delete(id).subscribe(() => {
          this.getShipTypes();
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
    const id = this.ShipTypeForm.get('shipTypeId')?.value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ShipTypeService.update(+id,this.ShipTypeForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-shiptype');
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
      this.ShipTypeService.submit(this.ShipTypeForm.value).subscribe(response => {
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

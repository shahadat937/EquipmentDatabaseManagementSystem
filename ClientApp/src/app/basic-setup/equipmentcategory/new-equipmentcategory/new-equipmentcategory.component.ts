import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EquipmentCategoryService } from '../../service/EquipmentCategory.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from '../../../../../src/assets/data/master-data';
import { EquipmentCategory } from '../../models/EquipmentCategory';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from '../../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-new-equipmentcategory',
  templateUrl: './new-equipmentcategory.component.html',
  styleUrls: ['./new-equipmentcategory.component.sass']
})
export class NewEquipmentCategoryComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  EquipmentCategoryForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  masterData = MasterData;
  ELEMENT_DATA: EquipmentCategory[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser','name','shortName','menuPosition','actions'];
  dataSource: MatTableDataSource<EquipmentCategory> = new MatTableDataSource();

  selection = new SelectionModel<EquipmentCategory>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private EquipmentCategoryService: EquipmentCategoryService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
   this.route.paramMap.subscribe(params=>{
    const id = params.get('equipmentCategoryId'); 
    if (id) {
      this.pageTitle = 'Edit Equipment Category';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.EquipmentCategoryService.find(+id).subscribe(
        res => {
          this.EquipmentCategoryForm.patchValue({          

            equipmentCategoryId: res.equipmentCategoryId,
            baseSchoolNameId:res.baseSchoolNameId,
            groupNameId:res.groupNameId,
            name:  res.name,
            shortName:  res.shortName,
            remarks:  res.remarks,
            menuPosition:  res.menuPosition,
            isActive:  res.isActive
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Equipment Category';
      this.destination = "Add";
      this.btnText = 'Save';
    }
   })
    this.intitializeForm();
    //this.getSelectedGroupName();
    this.getEquipmentCategorys();
  }
  intitializeForm() {
    this.EquipmentCategoryForm = this.fb.group({
      equipmentCategoryId: [0],
      baseSchoolNameId:[],
      groupNameId:[],
      name: ['', Validators.required],
      shortName:   [''],
      remarks:  [''],
      menuPosition:  [''],
      isActive: [true]
    })
  }
  
  // getSelectedGroupName(){
  //   this.EquipmentCategoryService.getSelectedGroupName().subscribe(res=>{
  //     this.selectedGroupName=res
  //     //console.log("Group Name")
  //     //console.log(res)
  //   }); 
  // }
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getEquipmentCategorys();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getEquipmentCategorys();
  } 

  getEquipmentCategorys() {
    this.isLoading = true;
    this.EquipmentCategoryService.getEquipmentCategorys(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.equipmentCategoryId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
  
      if (result) {
        this.EquipmentCategoryService.delete(id).subscribe(() => {
          this.getEquipmentCategorys();
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
    const id = this.EquipmentCategoryForm.get('equipmentCategoryId')?.value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.EquipmentCategoryService.update(+id,this.EquipmentCategoryForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-equipmentcategory');
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
      this.EquipmentCategoryService.submit(this.EquipmentCategoryForm.value).subscribe(response => {
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

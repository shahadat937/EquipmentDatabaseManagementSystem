import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BrandService } from '../../service/Brand.service';
import { SelectedModel } from '../../../../../src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from '../../../../../src/assets/data/master-data';
import { Brand } from '../../models/Brand';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from '../../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-new-brand',
  templateUrl: './new-brand.component.html',
  styleUrls: ['./new-brand.component.sass']
})
export class NewBrandComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  BrandForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: Brand[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: 1,
    pageSize: 10,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<Brand> = new MatTableDataSource();

  selection = new SelectionModel<Brand>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private BrandService: BrandService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params=>{
      const id = params.get('brandId'); 
    if (id) {
      this.pageTitle = 'Edit Brand';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.BrandService.find(+id).subscribe(
        res => {
          this.BrandForm.patchValue({          

            brandId: res.brandId,
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
      this.pageTitle = 'Create Brand';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    })
    this.intitializeForm();
    this.getBrands();
  }
  intitializeForm() {
    this.BrandForm = this.fb.group({
      brandId: [0],
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
    this.getBrands();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getBrands();
  } 

  getBrands() {
    this.isLoading = true;
    this.BrandService.getBrands(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.brandId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {

      if (result) {
        this.BrandService.delete(id).subscribe(() => {
          this.getBrands();
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
    const id = this.BrandForm.get('brandId')?.value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.BrandService.update(+id,this.BrandForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-brand');
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
      this.BrandService.submit(this.BrandForm.value).subscribe(response => {
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

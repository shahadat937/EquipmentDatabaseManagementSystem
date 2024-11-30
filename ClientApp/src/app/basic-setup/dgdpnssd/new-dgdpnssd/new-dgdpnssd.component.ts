import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DgdpNssdService } from '../../service/DgdpNssd.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { MasterData } from 'src/assets/data/master-data';
import { DgdpNssd } from '../../models/DgdpNssd';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-dgdpnssd',
  templateUrl: './new-dgdpnssd.component.html',
  styleUrls: ['./new-dgdpnssd.component.sass']
})
export class NewDgdpNssdComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  DgdpNssdForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: DgdpNssd[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<DgdpNssd> = new MatTableDataSource();

  selection = new SelectionModel<DgdpNssd>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private DgdpNssdService: DgdpNssdService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('dgdpNssdId'); 
    if (id) {
      this.pageTitle = 'Edit Dgdp Nssd';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.DgdpNssdService.find(+id).subscribe(
        res => {
          this.DgdpNssdForm.patchValue({          

            dgdpNssdId: res.dgdpNssdId,
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
      this.pageTitle = 'Create Dgdp Nssd';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getDgdpNssds();
  }
  intitializeForm() {
    this.DgdpNssdForm = this.fb.group({
      dgdpNssdId: [0],
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
    this.getDgdpNssds();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getDgdpNssds();
  } 

  getDgdpNssds() {
    this.isLoading = true;
    this.DgdpNssdService.getDgdpNssds(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.dgdpNssdId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {

      if (result) {
        this.DgdpNssdService.delete(id).subscribe(() => {
          this.getDgdpNssds();
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
    const id = this.DgdpNssdForm.get('dgdpNssdId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.DgdpNssdService.update(+id,this.DgdpNssdForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-dgdpnssd');
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
      this.DgdpNssdService.submit(this.DgdpNssdForm.value).subscribe(response => {
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

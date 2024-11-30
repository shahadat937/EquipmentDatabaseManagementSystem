import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EnvelopeService } from '../../service/Envelope.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { Envelope } from '../../models/Envelope';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-envelope',
  templateUrl: './new-envelope.component.html',
  styleUrls: ['./new-envelope.component.sass']
})
export class NewEnvelopeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  EnvelopeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: Envelope[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<Envelope> = new MatTableDataSource();

  selection = new SelectionModel<Envelope>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private EnvelopeService: EnvelopeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('envelopeId'); 
    if (id) {
      this.pageTitle = 'Edit Envelope';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.EnvelopeService.find(+id).subscribe(
        res => {
          this.EnvelopeForm.patchValue({          

            envelopeId: res.envelopeId,
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
      this.pageTitle = 'Create Envelope';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getEnvelopes();
  }
  intitializeForm() {
    this.EnvelopeForm = this.fb.group({
      envelopeId: [0],
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
    this.getEnvelopes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getEnvelopes();
  } 

  getEnvelopes() {
    this.isLoading = true;
    this.EnvelopeService.getEnvelopes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.envelopeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.EnvelopeService.delete(id).subscribe(() => {
          this.getEnvelopes();
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
    const id = this.EnvelopeForm.get('envelopeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.EnvelopeService.update(+id,this.EnvelopeForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-envelope');
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
      this.EnvelopeService.submit(this.EnvelopeForm.value).subscribe(response => {
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

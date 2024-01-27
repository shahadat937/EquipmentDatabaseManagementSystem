import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PaymentStatusService } from '../../service/PaymentStatus.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { PaymentStatus } from '../../models/PaymentStatus';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-new-paymentstatus',
  templateUrl: './new-paymentstatus.component.html',
  styleUrls: ['./new-paymentstatus.component.sass']
})
export class NewPaymentStatusComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  PaymentStatusForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  masterData = MasterData;
  ELEMENT_DATA: PaymentStatus[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<PaymentStatus> = new MatTableDataSource();

  selection = new SelectionModel<PaymentStatus>(true, []);

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private PaymentStatusService: PaymentStatusService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('paymentStatusId'); 
    if (id) {
      this.pageTitle = 'Edit Payment Status';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.PaymentStatusService.find(+id).subscribe(
        res => {
          this.PaymentStatusForm.patchValue({          

            paymentStatusId: res.paymentStatusId,
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
      this.pageTitle = 'Create Payment Status';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getPaymentStatuses();
  }
  intitializeForm() {
    this.PaymentStatusForm = this.fb.group({
      paymentStatusId: [0],
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
    this.getPaymentStatuses();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getPaymentStatuses();
  } 

  getPaymentStatuses() {
    this.isLoading = true;
    this.PaymentStatusService.getPaymentStatuses(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
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
    const id = row.paymentStatusId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.PaymentStatusService.delete(id).subscribe(() => {
          this.getPaymentStatuses();
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
    const id = this.PaymentStatusForm.get('paymentStatusId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.PaymentStatusService.update(+id,this.PaymentStatusForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-paymentstatus');
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
      this.PaymentStatusService.submit(this.PaymentStatusForm.value).subscribe(response => {
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

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OperationalStateService } from '../../service/OperationalState.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { OperationalState } from '../../models/OperationalState';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-new-statusofdefectivesystem',
  templateUrl: './new-statusofdefectivesystem.component.html',
  styleUrls: ['./new-statusofdefectivesystem.component.sass']
})
export class NewStatusOfDefectiveSystemComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  OperationalStateForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[];
  selectedOperationalStatus:SelectedModel[];
  selectedEquipmentCategory:SelectedModel[];
  masterData = MasterData;
  ELEMENT_DATA: OperationalState[] = [];
  OperationalStateList:OperationalState[];
  showHideDiv = false;
  isLoading = false;
  traineeId:any;
  role:any;
  branchId:any;
  itemCount:any =0;
  selectedReportingMonth:SelectedModel[];
  selectedEquipmentNameByCategory:SelectedModel[];
  selectedReturnType:SelectedModel[];
  selectedEquipmentName:SelectedModel[];
  selectEquipmentName:SelectedModel[];

  constructor(private snackBar: MatSnackBar,private authService: AuthService,private confirmService: ConfirmService,private OperationalStateService: OperationalStateService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
    const id = this.route.snapshot.paramMap.get('operationalStateId'); 
    if (id) {
      this.pageTitle = 'Edit Damage Electrical/Radio Electrical Equipment';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.OperationalStateService.find(+id).subscribe(
        res => {
          this.OperationalStateForm.patchValue({          
            operationalStateId: res.operationalStateId,
              equipmentNameId: res.equipmentNameId,
              dateOfDefect: res.dateOfDefect,
              durationOfDefect: res.durationOfDefect,
              causesOfDefect: res.causesOfDefect,
              stepsTakenByShipsStaff: res.stepsTakenByShipsStaff,
              stepTakenByDockyard: res.stepTakenByDockyard,
              stepTakenByNhq: res.stepTakenByNhq,
              stepTakenByOem: res.stepTakenByOem,
              partNo: res.partNo,
              sparePartsHeldIn: res.sparePartsHeldIn,
              procurementStatusOfSpare: res.procurementStatusOfSpare,
              impactOnOtherSyatem: res.impactOnOtherSyatem,
              requirmentOfTheShip: res.requirmentOfTheShip,
              remarks: res.remarks,
              descriptionOfDefect: res.descriptionOfDefect,
              menuPosition: res.menuPosition,
              isActive: res.isActive
          });     
        }
      );
    } else {
      this.pageTitle = 'Damage Electrical/Radio Electrical Equipment';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.OperationalStateForm = this.fb.group({
      operationalStateId: [0],
      equipmentNameId: [''],
      dateOfDefect: [''],
      durationOfDefect: [''],
      causesOfDefect: [''],
      stepsTakenByShipsStaff: [''],
      stepTakenByDockyard: [''],
      stepTakenByNhq: [''],
      stepTakenByOem: [''],
      partNo: [''],
      sparePartsHeldIn: [''],
      procurementStatusOfSpare: [''],
      impactOnOtherSyatem: [''],
      requirmentOfTheShip: [''],
      remarks: [''],
      descriptionOfDefect: [''],
      menuPosition: [1],
      isActive: [true]
    })
    this.getSelectedEquipmentName();
  }
  getSelectedOperationalStatus(){
    this.OperationalStateService.getSelectedOperationalStatus().subscribe(res=>{
      this.selectedOperationalStatus=res
      console.log(res)
      console.log(res)
    }); 
  }
  getSelectedEquipmentName(){
    this.OperationalStateService.getSelectedEquipmentName().subscribe(res=>{
      this.selectedEquipmentName=res
      this.selectEquipmentName=res
    }); 
  }
  filterByEquipementName(value:any){
    this.selectedEquipmentName=this.selectEquipmentName.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }

  onFileChanged(event){
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log(file);
      this.OperationalStateForm.patchValue({
        doc: file,
      });
    }
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  deleteItem(row) {
    const id = row.OperationalStateId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.OperationalStateService.delete(id).subscribe(() => {
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
    const id = this.OperationalStateForm.get('OperationalStateId').value;  
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.OperationalStateService.update(+id,this.OperationalStateForm.value).subscribe(response => {
           this.router.navigateByUrl('/ships-return/OperationalState-list');
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
      this.OperationalStateService.submit(this.OperationalStateForm.value).subscribe(response => {
        this.router.navigateByUrl('/ships-return/OperationalState-list');
       // this.reloadCurrentRoute();
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

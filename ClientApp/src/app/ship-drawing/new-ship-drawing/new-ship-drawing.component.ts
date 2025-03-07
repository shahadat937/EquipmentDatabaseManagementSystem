import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { Role } from '../../../../src/app/core/models/role';
import { SelectedModel } from '../../../../src/app/core/models/selectedModel';
import { MasterData } from '../../../../src/assets/data/master-data';
import { ShipDrowing } from '../models/ShipDrowing';
import { SelectionModel } from '@angular/cdk/collections';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../../../src/app/core/service/auth.service';
import { ConfirmService } from '../../../../src/app/core/service/confirm.service';
import { ShipDrowingService } from '../../../../src/app/ship-drawing/Services/ShipDrowing.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';
import { BaseSchoolNameService } from '../../../../src/app/security/service/BaseSchoolName.service';
import { SharedService } from '../../../../src/app/shared/shared.service';

@Component({
  selector: 'app-new-ship-drawing',
  templateUrl: './new-ship-drawing.component.html',
  styleUrls: ['./new-ship-drawing.component.sass']
})
export class NewShipDrawingComponent implements OnInit {

  pageTitle: string;
  destination: string;
  btnText: string;
  ShipDrowingForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel: SelectedModel[];
  traineeId: any;
  role: any;
  branchId: any;
  userRole = Role;
  selectedDepartmentName: SelectedModel[];
  ShipDrowingList: SelectedModel[];
  masterData = MasterData;
  organizationId: any;
  selectedCommendingArea: any[];
  selectComandinArea: SelectedModel[]
  selectedBaseName: any[];
  selectBaseName: SelectedModel[]
  commendingAreaId: any;
  selectedBaseSchoolName: any[];
  isAreaCommanderUsers: boolean;


  ELEMENT_DATA: ShipDrowing[] = [];
  isLoading = false;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText = "";

  displayedColumns: string[] = ['ser', 'authority', 'baseName', 'baseSchoolName', 'name', 'fileUpload', 'actions'];
  dataSource: MatTableDataSource<ShipDrowing> = new MatTableDataSource();

  selection = new SelectionModel<ShipDrowing>(true, []);
  selectSchoolName: SelectedModel[];

  constructor(private snackBar: MatSnackBar, private BaseSchoolNameService: BaseSchoolNameService, private authService: AuthService, private confirmService: ConfirmService, private ShipDrowingService: ShipDrowingService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute, public SharedService: SharedService) { }


  ngOnInit(): void {

    this.route.params.subscribe((params) => {
      const id = params['shipDrowingId'];

      if (id) {
        this.pageTitle = 'Edit Ship Drowing';
        this.destination = 'Edit';
        this.btnText = 'Update';

        this.ShipDrowingService.find(+id).subscribe((res) => {
          this.ShipDrowingForm.patchValue({
            shipDrowingId: res.shipDrowingId,
            baseSchoolNameId: res.baseSchoolNameId,
            authorityId: res.authorityId,
            baseNameId: res.baseNameId,
            name: res.name,
            shortName: res.shortName,
            fileUpload: res.fileUpload,
            remarks: res.remarks,
            isActive: res.isActive,
          });

          this.onCommendingAreaSelectionChangeGetBaseName();
          this.onOrganizationSelectionChange();
        });
      } else {
        this.pageTitle = 'Create Ship Drowing';
        this.destination = 'Add';
        this.btnText = 'Save';
        //console.log(this.role);
        // this.ShipDrowingForm.reset();

      }
    });

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId = this.authService.currentUserValue.traineeId.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    // //console.log("test",this.role, this.traineeId, this.branchId);
    this.getShipDrowings();
    this.onOrganizationSelectionChangeGetCommendingArea();
    this.intitializeForm();




    // if (this.role !== this.userRole.SuperAdmin) {
    //   this.ShipDrowingForm.get('departmentNameId').setValue(this.branchId);
    //   this.onDepartmentSelectionChangeGetShipDrowingList();
    // }
    if (this.role === this.userRole.AreaCommander || this.role === this.userRole.FLO || this.role === this.userRole.CSO || this.role === this.userRole.FLOStaff) {
      this.ShipDrowingForm.get('authorityId')?.setValue(this.branchId);
      this.isAreaCommanderUsers = true;
      this.onCommendingAreaSelectionChangeGetBaseName();
    }
    else if (this.role === this.userRole.ShipUser || this.role === this.userRole.ShipStaff || this.role === this.userRole.LOEO || this.role === this.userRole.LOEOWTR) {

      this.getShipInfoById(this.branchId);
    }
  }

  intitializeForm() {
    this.ShipDrowingForm = this.fb.group({
      shipDrowingId: [0],
      authorityId: [],
      baseNameId: [],
      baseSchoolNameId: [''],
      name: [''],
      shortName: [''],
      fileUpload: [''],
      remarks: [''],
      menuPosition: [1],
      doc: [''],
      isActive: [true]
    })
  }

  onOrganizationSelectionChangeGetCommendingArea() {
    this.organizationId = MasterData.UserLevel.navy;
    //console.log(this.organizationId + " organization")
    this.BaseSchoolNameService.getSelectedCommendingArea(this.organizationId).subscribe(res => {
      this.selectedCommendingArea = res
      this.selectComandinArea = res
      //console.log("selected comanding area");
      //  //console.log(this.selectedCommendingArea);
    });
  }
  filterByCommandingArea(value: any) {
    this.selectedCommendingArea = this.selectComandinArea.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  filterByBaseName(value: any) {
    this.selectedBaseSchoolName = this.selectBaseName.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  filterByShipName(value: any) {
    this.selectedBaseSchoolName = this.selectSchoolName.filter(x => x.text.toLowerCase().includes(value.toLowerCase()))
  }
  onOrganizationSelectionChange() {
    var baseNameId = this.ShipDrowingForm.value['baseNameId'];
    // this.ShipInformationService.getSelectedSchoolByBranchLevelAndThirdLevel(baseNameId).subscribe(res=>{
    //   this.selectedBaseSchoolName=res
    //   //console.log(res)
    //   //console.log(res)
    // }); 

    // this.baseNameId=this.UserForm.value['thirdLevel'];
    //console.log(baseNameId);
    this.BaseSchoolNameService.getSelectedSchoolName(baseNameId).subscribe(res => {
      this.selectedBaseSchoolName = res
      this.selectSchoolName = res
    });
  }
  onCommendingAreaSelectionChangeGetBaseName() {
    this.commendingAreaId = this.ShipDrowingForm.value['authorityId'];
    //console.log("comandinf area")
    //console.log(this.commendingAreaId);
    this.BaseSchoolNameService.getSelectedBaseName(this.commendingAreaId).subscribe(res => {
      this.selectedBaseName = res
      this.selectBaseName = res
    });
    //this.getBaseNameList(this.commendingAreaId);

  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getShipDrowings();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getShipDrowings();
  }


  getShipDrowings() {
    if (this.role === this.userRole.AreaCommander) {
      this.ShipDrowingService.getShipDrowingsByAuthorityId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.branchId).subscribe(response => {
        //console.log(response);
        this.dataSource.data = response.items;
        this.paging.length = response.totalItemsCount
        this.isLoading = false;
      })
    }
    else if (this.role == this.userRole.ShipStaff || this.role == this.userRole.ShipUser || this.role == this.userRole.LOEO || this.role == this.userRole.LOEOWTR) {
      this.getShipDrawing(this.branchId);
    }
    else {
      this.getShipDrawing(0)
    }

  }

  getShipDrawing(shipId) {
    this.ShipDrowingService.getShipDrowings(this.paging.pageIndex, this.paging.pageSize, this.searchText, shipId).subscribe(response => {
      //console.log(response);
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
    })
  }

  getShipInfoById(shipId) {
    this.BaseSchoolNameService.getShipInfosById(this.branchId).subscribe(res => {
        this.ShipDrowingForm.get('authorityId')?.setValue(res.secondLevel);
        this.ShipDrowingForm.get('baseNameId')?.setValue(res.thirdLevel);
        this.ShipDrowingForm.get('baseSchoolNameId')?.setValue(shipId);
      
    })
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
  deleteItem(row) {
    const id = row.shipDrowingId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      //console.log(result);
      if (result) {
        this.ShipDrowingService.delete(id).subscribe(() => {
          this.getShipDrowings();
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

  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      //console.log(file);
      this.ShipDrowingForm.patchValue({
        doc: file,
      });
    }
  }

  onSubmit() {
    const id = this.ShipDrowingForm?.get('shipDrowingId')?.value;

    // this.ShipDrowingForm.get('date').setValue((new Date(this.ShipDrowingForm.get('date').value)).toUTCString()) ;
    // this.ProcurementForm.get('tenderopeningDate').setValue((new Date(this.ProcurementForm.get('tenderopeningDate').value)).toUTCString()) ;
    // this.ProcurementForm.get('workOrderDate').setValue((new Date(this.ProcurementForm.get('workOrderDate').value)).toUTCString()) ;
    // this.ProcurementForm.get('dateOfDelivery').setValue((new Date(this.ProcurementForm.get('dateOfDelivery').value)).toUTCString()) ;

    // //console.log(this.ProcurementForm.value)

    const formData = new FormData();
    for (const key of Object.keys(this.ShipDrowingForm.value)) {
      const value = this.ShipDrowingForm.value[key];
      formData.append(key, value);
    }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {

        if (result) {
          this.ShipDrowingService.update(+id, formData).subscribe(response => {
            this.router.navigateByUrl('/ship-drawing/add-shipdrowing');
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
      this.ShipDrowingService.submit(formData).subscribe(response => {
        // this.router.navigateByUrl('/basic-setup/ShipDrowing-list');
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

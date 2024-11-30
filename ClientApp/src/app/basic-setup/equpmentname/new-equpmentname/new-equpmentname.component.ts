import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EqupmentNameService } from '../../service/EqupmentName.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import{MasterData} from 'src/assets/data/master-data';
import { EqupmentName } from '../../models/EqupmentName';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { AuthService } from 'src/app/core/service/auth.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-new-equpmentname',
  templateUrl: './new-equpmentname.component.html',
  styleUrls: ['./new-equpmentname.component.sass']
})
export class NewEqupmentNameComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  EqupmentNameForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[];
  selectedOperationalStatus:SelectedModel[];
  masterData = MasterData;
  ELEMENT_DATA: EqupmentName[] = [];
  equpmentNameList:EqupmentName[];
  showHideDiv = false;
  isLoading = false;
  traineeId:any;
  role:any;
  branchId:any;
  itemCount:any =0;
  groupArrays:{ equipmentCategory: string; courses: any; }[];
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'equipmentCategory','name','shortName','remarks','actions'];
  dataSource: MatTableDataSource<EqupmentName> = new MatTableDataSource();

  selection = new SelectionModel<EqupmentName>(true, []);

  constructor(private snackBar: MatSnackBar,private authService: AuthService,private confirmService: ConfirmService,private EqupmentNameService: EqupmentNameService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, public SharedService: SharedService) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim(); 
    const id = this.route.snapshot.paramMap.get('equpmentNameId'); 
    if (id) {
      this.pageTitle = 'Edit Equpment Name';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.EqupmentNameService.find(+id).subscribe(
        res => {
          this.EqupmentNameForm.patchValue({          

            equpmentNameId: res.equpmentNameId,
            baseSchoolNameId:res.baseSchoolNameId,
            equipmentCategoryId:res.equipmentCategoryId,
            name:  res.name,
            shortName:  res.shortName,
            remarks:  res.remarks,
            menuPosition:  res.menuPosition,
            isActive:  res.isActive
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Equpment Name';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    //this.onOrganizationSelectionChange();
    this.getSelectedEquipmentCategory();
    //this.getEqupmentNames();
    this.getEqupmentNamesWhitoutPage();
  }
  intitializeForm() {
    this.EqupmentNameForm = this.fb.group({
      equpmentNameId: [0],
      baseSchoolNameId:[],
      equipmentCategoryId:[],
      name: ['', Validators.required],
      shortName:   [''],
      remarks:  [''],
      menuPosition:  [''],
      isActive: [true]
    })
  }
  // onOrganizationSelectionChange(){
  //   var baseNameId = this.EqupmentNameForm.value['baseNameId'];
  //   this.EqupmentNameService.getSelectedSchoolByBranchLevelAndThirdLevel(baseNameId).subscribe(res=>{
  //     this.selectedBaseSchoolName=res
  //     console.log(res)
  //     console.log(res)
  //   }); 
  // }
  getSelectedEquipmentCategory(){
    this.EqupmentNameService.getSelectedEquipmentCategory().subscribe(res=>{
      this.selectedOperationalStatus=res
 
    }); 
  }
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    //this.getEqupmentNames();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    //this.getEqupmentNames();
  } 

  // getEqupmentNames() {
  //   this.isLoading = true;
  //   this.EqupmentNameService.getEqupmentNames(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
  //     this.dataSource.data = response.items; 
  //     this.paging.length = response.totalItemsCount    
  //     this.isLoading = false;

  //     // this gives an object with dates as keys
  //     const groups = this.dataSource.data.reduce((groups, courses) => {
  //       const schoolName = courses.equipmentCategory;
  //       if (!groups[schoolName]) {
  //         groups[schoolName] = [];
  //       }
  //       groups[schoolName].push(courses);
  //       return groups;
  //     }, {});

  //     // Edit: to add it in the array format instead
  //     this.groupArrays = Object.keys(groups).map((equipmentCategory) => {
  //       return {
  //         equipmentCategory,
  //         courses: groups[equipmentCategory]
  //       };
  //     });
  //   })
  // }
  getEqupmentNamesWhitoutPage() {
    this.isLoading = true;
    this.EqupmentNameService.getEqupmentNamesWhitoutPage().subscribe(response => {
      
      this.equpmentNameList = response;     
      this.itemCount = response.length;

      //this.paging.length = response.totalItemsCount    
      this.isLoading = false;

      // this gives an object with dates as keys
      const groups = this.equpmentNameList.reduce((groups, courses) => {
        const schoolName = courses.equipmentCategory;
        if (!groups[schoolName]) {
          groups[schoolName] = [];
        }
        groups[schoolName].push(courses);
        return groups;
      }, {});

      // Edit: to add it in the array format instead
      this.groupArrays = Object.keys(groups).map((equipmentCategory) => {
        return {
          equipmentCategory,
          courses: groups[equipmentCategory]
        };
      });
    })
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  deleteItem(row) {
    const id = row.equpmentNameId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      if (result) {
        this.EqupmentNameService.delete(id).subscribe(() => {
          //this.getEqupmentNames();
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
  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }
  printSingle() {
    this.showHideDiv = false;
    this.print();
  }
  print() {
    let printContents, popupWin;
    printContents = document.getElementById("print-routine").innerHTML;
    popupWin = window.open( "top=0,left=0,height=100%,width=auto");
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <style>
          body{  width: 99%;}
            label { font-weight: 400;
                    font-size: 13px;
                    padding: 2px;
                    margin-bottom: 5px;
                  }
            table, td, th {
                  border: 1px solid silver;
                    }
                    table td {
                  font-size: 13px;
                    }
                    .table.table.tbl-by-group.db-li-s-in tr .cl-action{
                      display: none;
                    }
        
                    .table.table.tbl-by-group.db-li-s-in tr td{
                      text-align:center;
                      padding: 0px 5px;
                    }
                    
                  }
                  .table.table.tbl-by-group.db-li-s-in tr .btn-tbl-edit {
                    display:none;
                  }
                  
                    
                    table th {
                  font-size: 13px;
                    }
              table {
                    border-collapse: collapse;
                    width: 98%;
                    }
                th {
                    height: 26px;
                    }
                .header-text{
                  text-align:center;
                }
                .header-text h3{
                  margin:0;
                }
          </style>
        </head>
        <body onload="window.print();window.close()">
          <div class="header-text">
          <h3>Equipment Name List</h3>
          
          </div>
          <br>
          <hr>
          ${printContents}
          
        </body>
      </html>`);
    popupWin.document.close();
  }
  onSubmit() {
    const id = this.EqupmentNameForm.get('equpmentNameId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.EqupmentNameService.update(+id,this.EqupmentNameForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-equpmentname');
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
      this.EqupmentNameService.submit(this.EqupmentNameForm.value).subscribe(response => {
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

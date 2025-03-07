import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { dashboardService } from '../services/dashboard.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { MasterData } from 'src/assets/data/master-data';

@Component({
  selector: 'app-establishmentbytype-list',
  templateUrl: './establishmentbytype-list.component.html',
  styleUrls: ['./establishmentbytype-list.component.sass']
})
export class EstablishmentByTypeListComponent implements OnInit {
  masterData = MasterData;
  isLoading = false;
  shipinfoList: any[] = [];
  roles = Role;
  groupArrays: { schoolName: string; courses: any; }[] = [];
  showHideDiv = false;
  role: string;
  branchId: string;
  searchText = "";
  private searchSubject: Subject<string> = new Subject();

  constructor(
    private snackBar: MatSnackBar,
    private dashboardService: dashboardService,
    private router: Router,
    private confirmService: ConfirmService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.role = this.authService.currentUserValue.role;
    this.branchId = this.authService.currentUserValue.branchId;
    this.searchSubject.pipe(debounceTime(300)).subscribe((searchText) => {
      this.searchText = searchText;
      this.getEstablishmentByShipType(); // Fetch the filtered data when the search term changes
    });
    this.getEstablishmentByShipType();
  }

  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }

  printSingle() {
    this.showHideDiv = false;
    this.print();
  }

  applyFilter(searchText: string) {
    this.searchSubject.next(searchText); // Trigger search filtering
  }

  print() {
    let printContents, popupWin;
    printContents = document.getElementById("print-routine").innerHTML;
    popupWin = window.open("top=0,left=0,height=100%,width=auto");
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
                    .table.table.tbl-by-group.db-li-s-in tr .col.table-img.img-on-hover{
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
          <h3>Total Establishment </h3>
          
          </div>
          <br>
          <hr>
          ${printContents}
          
        </body>
      </html>`);
    popupWin.document.close();
  }

  getEstablishmentByShipType() {
    this.isLoading = true;
    if (this.role === this.roles.AreaCommander || this.role === this.roles.FLO || this.role === this.roles.CSO || this.role === this.roles.FLOStaff) {
      this.dashboardService.getShipInformationListByShipTypeAndCommandingArea(6, this.branchId).subscribe(response => {
        this.filterShipInfo(response);
        this.isLoading = false;
      }, error => {
        this.isLoading = false;
        this.snackBar.open('Error fetching data', 'Close', { duration: 3000 });
      });
    } else {
      this.dashboardService.getShipInformationListByShipType(6).subscribe(response => {
        this.filterShipInfo(response);
        this.isLoading = false;
      }, error => {
        this.isLoading = false;
        this.snackBar.open('Error fetching data', 'Close', { duration: 3000 });
      });
    }
  }

  // Filter the ship information based on the searchText
  filterShipInfo(response: any) {
    const searchTerm = this.searchText.toLowerCase();  // Lowercase the search term for case-insensitive search
    this.shipinfoList = response.filter((item: any) => {
      const shipType = item.shipType ? item.shipType.toLowerCase() : '';  // Safe check for undefined or null
      const schoolName = item.schoolName ? item.schoolName.toLowerCase() : '';  // Safe check for undefined or null
  
      return shipType.includes(searchTerm) || schoolName.includes(searchTerm);  // Adjust fields as needed
    });
  
    const groups = this.shipinfoList.reduce((groups, courses) => {
      const schoolName = courses.schoolName;
      if (!groups[schoolName]) {
        groups[schoolName] = [];
      }
      groups[schoolName].push(courses);
      return groups;
    }, {});
  
    // Group the filtered data
    this.groupArrays = Object.keys(groups).map((schoolName) => {
      return {
        schoolName,
        courses: groups[schoolName]
      };
    });
  }
  
}

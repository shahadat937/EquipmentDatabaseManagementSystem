import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { ActivatedRoute,Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { ShipDrowing } from '../../../../../src/app/basic-setup/models/ShipDrowing';
import { MatSnackBar } from '@angular/material/snack-bar';
import {dashboardService} from '../services/dashboard.service';
import {BaseSchoolNameService} from '../../../security/service/BaseSchoolName.service'
import { ShipDrowingService } from '../../../basic-setup/service/ShipDrowing.service'


@Component({
  selector: 'app-view-shipdrowinglist',
  templateUrl: './view-shipdrowinglist.component.html', 
  styleUrls: ['./view-shipdrowinglist.component.sass']
})
export class ViewShipDrowingListComponent implements OnInit {

  masterData = MasterData;
  isLoading = false;
  shipinfoList:any[];
  baseSchoolNameId:any;
  operationalStatusId:any;
  shipInfoByBase:any;
  selectedBaseName:any;
  shipDrawingList:any[];
  ELEMENT_DATA: ShipDrowing[] = [];
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  groupArrays:{ authority: string; courses: any; }[];

  displayedColumns: string[] = [ 'ser','authority','baseSchoolName','name','fileUpload'];
  dataSource: MatTableDataSource<ShipDrowing> = new MatTableDataSource();

  selection = new SelectionModel<ShipDrowing>(true, []);
  constructor(private snackBar: MatSnackBar,private ShipDrowingService:ShipDrowingService,private BaseSchoolNameService:BaseSchoolNameService,private dashboardService:dashboardService,private route: ActivatedRoute,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.baseSchoolNameId = this.route.snapshot.paramMap.get('baseSchoolNameId'); 
    this.operationalStatusId = this.route.snapshot.paramMap.get('operationalStatusId'); 
    this.getShipDrowings();
  }
 
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getShipDrowings();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getShipDrowings();
  } 


  getShipDrowings(){
    this.ShipDrowingService.getShipDrowings(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.shipDrawingList = response.items;

      const groups =  this.shipDrawingList.reduce((groups, courses) => {
        const schoolName = courses.authority;
        if (!groups[schoolName]) {
          groups[schoolName] = [];
        }
        groups[schoolName].push(courses);
        return groups;
      }, {});

      // Edit: to add it in the array format instead
      this.groupArrays = Object.keys(groups).map((authority) => {
        return {
          authority,
          courses: groups[authority]
        };
      });
   
      // this.paging.length = response.totalItemsCount    
      // this.isLoading = false;
    })
  }
}

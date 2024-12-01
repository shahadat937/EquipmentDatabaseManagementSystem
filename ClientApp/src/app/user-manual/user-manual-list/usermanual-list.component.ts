import { UserManualService } from './../services/UserManual.service';
import { Component, OnInit, ViewChild,ElementRef  } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
// import {UserManual} from '../../models/UserManuals'
// import {UserManualService} from '../../service/UserManual.service'
import { UserManual } from '../models/UserManuals';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {MasterData} from 'src/assets/data/master-data'
import { MatSnackBar } from '@angular/material/snack-bar';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from 'src/app/core/service/auth.service';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
import { Subject, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
// import { SharedServiceService } from 'src/app/shared/shared-service.service';


@Component({
  selector: 'app-usermanual-list',
  templateUrl: './usermanual-list.component.html',
  styleUrls: ['./usermanual-list.component.css']
})
export class UserManualList extends UnsubscribeOnDestroyAdapter implements OnInit {
   masterData = MasterData;
  loading = false;
  ELEMENT_DATA: UserManual[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  private searchSubject: Subject<string> = new Subject<string>();
  private searchSubscription: Subscription;

  role:any;
  traineeId:any;
  branchId:any;

  //groupArrays:{ readingMaterialTitle: string; courses: any; }[];

  displayedColumns: string[] = ['ser','roleName','doc', 'actions'];
  dataSource: MatTableDataSource<UserManual> = new MatTableDataSource();


   selection = new SelectionModel<UserManual>(true, []);

  
  constructor(private snackBar: MatSnackBar, private authService: AuthService,private UserManualService: UserManualService,private readonly sanitizer: DomSanitizer,private router: Router,private confirmService: ConfirmService) {
    super();
  }

  ngOnInit() {
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();

    this.getUserManuals();
    this.searchSubscription = this.searchSubject.pipe(
      debounceTime(300), 
      distinctUntilChanged() 
    ).subscribe(searchText => {
      this.applyFilter(searchText);
    });
    
  }

  onSearchChange(searchValue: string): void {
    this.searchSubject.next(searchValue);
  }
 
  getUserManuals() {
    this.isLoading = true;
    this.UserManualService.getUserManuals(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     
      this.dataSource.data = response.items; 
      // const groups = this.dataSource.data.reduce((groups, courses) => {
      //   const materialTitle = courses.readingMaterialTitle;
      //   if (!groups[materialTitle]) {
      //     groups[materialTitle] = [];
      //   }
      //   groups[materialTitle].push(courses);
      //   return groups;
      // }, {});

      // Edit: to add it in the array format instead
      // this.groupArrays = Object.keys(groups).map((readingMaterialTitle) => {
      //   return {
      //     readingMaterialTitle,
      //     courses: groups[readingMaterialTitle]
      //   };
      // });

      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
  
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getUserManuals();
 
  }
  applyFilter(searchText: any){ 
    this.searchText = searchText.toLowerCase().trim().replace(/\s/g,'');
    this.getUserManuals();
  } 

  safeUrlPic(url: any){ 
    var specifiedUrl = this.sanitizer.bypassSecurityTrustUrl(url); 
    return specifiedUrl;
  }

  deleteItem(row) {
    const id = row.userManualId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      if (result) {
        this.UserManualService.delete(id).subscribe(() => {
          this.getUserManuals();
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 3000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    })
    
  }
}
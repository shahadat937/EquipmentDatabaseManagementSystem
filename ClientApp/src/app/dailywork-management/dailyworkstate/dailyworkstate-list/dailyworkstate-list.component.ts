import { Component, OnInit } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { DailyWorkState } from '../../models/DailyWorkState';
import { DailyWorkStateService } from '../../service/DailyWorkState.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-dailyworkstate-list',
  templateUrl: './dailyworkstate-list.component.html',
  styleUrls: ['./dailyworkstate-list.component.sass']
})
export class DailyWorkStateListComponent implements OnInit {
  filterAction: 'yes' | 'no' = 'yes'; 
  masterData = MasterData;
  ELEMENT_DATA: DailyWorkState[] = [];
  isLoading = false;
  groupArrays: { authorityName: string; courses: any; }[] = [];
  dailyWorkStateList: DailyWorkState[] = [];
  showHideDiv = false;
  yesCount: any = 0;
  noCount: any = 0;
  searchText = "";
  private searchSubject: Subject<string> = new Subject(); 

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 5,
    length: 1
  };

  displayedColumns: string[] = ['ser', 'letterType', 'date', 'workFrom', 'letterNo', 'subject', 'dealingOfficer', 'dealingStaff', 'actions'];
  dataSource: MatTableDataSource<DailyWorkState> = new MatTableDataSource();
  selection = new SelectionModel<DailyWorkState>(true, []);

  constructor(
    private snackBar: MatSnackBar,
    private DailyWorkStateService: DailyWorkStateService,
    private router: Router,
    private confirmService: ConfirmService,
    public SharedService: SharedService
  ) { }

  ngOnInit() {
    this.getDailyWorkStates();
    this.getDailyWorkStatesListByNoAction();

    // Debounce logic for search
    this.searchSubject.pipe(
      debounceTime(300) // Adjust debounce time as needed
    ).subscribe((searchText) => {
      this.searchText = searchText;
      this.getDailyWorkStates();
      // this.getDailyWorkStatesListByNoAction();
    });
  }

  getDailyWorkStates() {
    this.isLoading = true;
    this.DailyWorkStateService.getDailyWorkStates(this.paging.pageIndex, this.paging.pageSize, this.searchText).subscribe(response => {
      this.dataSource.data = response.items;
      this.paging.length = response.totalItemsCount;
      this.isLoading = false;
      this.noCount = response.items.length;
    });
  }

  getDailyWorkStatesListByNoAction() {
    this.isLoading = true;
    this.DailyWorkStateService.getDailyWorkStatesListByNoAction().subscribe(response => {
      this.dailyWorkStateList = response;
      this.yesCount = response.length;
    });
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex;
    this.paging.pageSize = event.pageSize;
    this.paging.pageIndex = this.paging.pageIndex + 1;
    this.getDailyWorkStates();
    this.getDailyWorkStatesListByNoAction();
  }

  applyFilter(searchText: string) {
    this.searchSubject.next(searchText);
  }
  applyFilterByAction(action: 'yes' | 'no') {
    this.filterAction = action;
  }

  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }

  printSingle() {
    this.showHideDiv = false;
    this.print();
  }

  print() {
    // Print logic
  }

  deleteItem(row: any) {
    const id = row.dailyWorkStateId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      if (result) {
        this.DailyWorkStateService.delete(id).subscribe(() => {
          this.getDailyWorkStates();
          this.snackBar.open('Information Deleted Successfully', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        });
      }
    });
  }
}

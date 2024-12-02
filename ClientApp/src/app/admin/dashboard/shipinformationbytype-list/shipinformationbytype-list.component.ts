import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { dashboardService } from '../services/dashboard.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { debounceTime } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-shipinformationbytype-list',
  templateUrl: './shipinformationbytype-list.component.html',
  styleUrls: ['./shipinformationbytype-list.component.sass']
})
export class ShipInformationByTypeListComponent implements OnInit {

  shipinfoList: any[] = [];
  showHideDiv = false;
  groupArrays: { schoolName: string; courses: any[] }[] = [];
  role: string;
  branchId: string;
  searchText = '';
  private searchSubject: Subject<string> = new Subject();

  constructor(
    private snackBar: MatSnackBar,
    private dashboardService: dashboardService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.role = this.authService.currentUserValue.role;
    this.branchId = this.authService.currentUserValue.branchId;

    this.searchSubject.pipe(
      debounceTime(300)
    ).subscribe((searchText) => {
      this.searchText = searchText;
      this.getShipInfoByShipType();
    });

    this.getShipInfoByShipType();
  }

  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }

  applyFilter(searchText: string) {
    this.searchSubject.next(searchText);
  }

  getShipInfoByShipType() {
    if (this.role === 'Area Commander') {
      this.dashboardService.getShipInformationListByShipTypeAndCommandingArea(11, this.branchId).subscribe(response => {
        this.shipinfoList = response;
        this.filterShipInfoList();
        this.groupArrays = this.groupShipInfoBySchoolName(this.shipinfoList);
      });
    } else {
      this.dashboardService.getShipInformationListByShipType(11).subscribe(response => {
        this.shipinfoList = response;
        this.filterShipInfoList();
        this.groupArrays = this.groupShipInfoBySchoolName(this.shipinfoList);
      });
    }
  }

  filterShipInfoList() {
    if (this.searchText.trim()) {
      this.shipinfoList = this.shipinfoList.filter(item =>
        item.shipName.toLowerCase().includes(this.searchText.toLowerCase()) ||
        item.sqnName.toLowerCase().includes(this.searchText.toLowerCase()) ||
        item.osName.toLowerCase().includes(this.searchText.toLowerCase())
      );
    }
  }

  groupShipInfoBySchoolName(shipinfoList: any[]) {
    const groups = shipinfoList.reduce((groups, courses) => {
      const schoolName = courses.schoolName;
      if (!groups[schoolName]) {
        groups[schoolName] = [];
      }
      groups[schoolName].push(courses);
      return groups;
    }, {});

    return Object.keys(groups).map(schoolName => ({
      schoolName,
      courses: groups[schoolName]
    }));
  }
}

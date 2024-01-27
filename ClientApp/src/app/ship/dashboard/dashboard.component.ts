import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SchoolDashboardService } from '../services/SchoolDashboard.service';
import { ActivatedRoute,Router } from '@angular/router';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTooltip,
  ApexYAxis,
  ApexStroke,
  ApexLegend,
  ApexMarkers,
  ApexGrid,
  ApexFill,
  ApexTitleSubtitle,
  ApexNonAxisChartSeries,
  ApexResponsive,
} from 'ng-apexcharts';
import { MasterData } from 'src/assets/data/master-data';
import { environment } from 'src/environments/environment';
import { DatePipe } from '@angular/common';
import { scheduled } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
// import { ReadingMaterialService } from '../../reading-materials/service/readingmaterial.service';
// import { StudentDashboardService } from 'src/app/student/services/StudentDashboard.service';
// import { BaseSchoolNameService } from 'src/app/basic-setup/service/BaseSchoolName.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MatSnackBar } from '@angular/material/snack-bar';

export type avgLecChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
  dataLabels: ApexDataLabels;
  markers: ApexMarkers;
  colors: string[];
  yaxis: ApexYAxis;
  grid: ApexGrid;
  tooltip: ApexTooltip;
  legend: ApexLegend;
  fill: ApexFill;
  title: ApexTitleSubtitle;
};

export type pieChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  legend: ApexLegend;
  dataLabels: ApexDataLabels;
  responsive: ApexResponsive[];
  labels: any;
};

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass'],
})
export class DashboardComponent implements OnInit {
  @ViewChild('chart') chart: ChartComponent;
  public avgLecChartOptions: Partial<avgLecChartOptions>;
  public pieChartOptions: Partial<pieChartOptions>;
   masterData = MasterData;
  loading = false;
  userRole = Role;
  GetSchoolForm: FormGroup;
  isShown: boolean = false ;
  paramBaseSchoolNameId:any;

  dataEnty: any = Role.DataEntry;
  fileUrl:any = environment.fileUrl;
  bulletinList:any;

  courseList:any;

  routineList:any;

  materialList:any;

  nomineeCount:number;
  foreignNomineeCount:number;
  runningOfficerCount:number;
  runningSailorCount:number;
  runningCivilCount:number;
  localCourseCount:number;
  UpcomingCourseCount:number;
  upcomingCourses:any;
  runningCourses:any;
  RoutineBySchool:any;
  RoutineByCourse:any;
  PendingExamEvaluation:any;
  TraineeAbsentList:any;
  TodayRoutineList:any;
  TodayAttendanceList:any;
  ReadIngMaterialList:any;
  shipInfoList:any;
  shipInfoId:any;
  shipEquipList:any;
  // shipInfoId:any;
  InstructorList:any;
  schoolId:any;
  schoolDb:any=1;
  schoolName:any;
  userManual:any;
  dbType:any;
  branchId:any;
  traineeId:any;
  role:any;
  pageTitle:any;
  selectedschool:SelectedModel[];
  groupArrays:{ schoolName: string; courses: any; }[];

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  displayedColumns: string[] = ['ser','course', 'durationFrom', 'durationTo','countWeek','courseSyllabus','actions'];
  displayedRoutineColumns: string[] = ['ser', 'date','classType','duration', 'period', 'subject', 'classLocation', 'name'];
  displayedExamEvaluationColumns: string[] = ['ser', 'course','subject','date','name'];
  displayedAbsentListColumns: string[] = ['ser', 'course','duration','absent','actions'];
  displayedRoutineCountColumns: string[] = ['ser','course','moduleName','routineCount','actions'];
  displayedRoutineAbsentColumns: string[] = ['ser','course','nominated','actions'];
  displayedReadingMaterialColumns: string[] = ['ser','course','materialCount','actions'];
  displayedInstructorColumns: string[] = ['ser','course','instructorCount','actions'];

  constructor(private datepipe: DatePipe, private snackBar: MatSnackBar, private confirmService: ConfirmService, private authService: AuthService,private route: ActivatedRoute,private router: Router,private fb: FormBuilder,private schoolDashboardService: SchoolDashboardService) {}
  ngOnInit() {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    // this.branchId =  this.authService.currentUserValue.branchId.trim();
    this.branchId =  this.authService.currentUserValue.branchId  ? this.authService.currentUserValue.branchId.trim() : "";
    console.log(this.role, this.traineeId, this.branchId)
    // if(this.role == this.userRole.CO){
    //   this.schoolId = this.branchId;
      this.pageTitle = "Ship Dashboard";
    //   this.getRunningCourseDurationByBase(this.branchId); 
    //   this.getUpcomingCourseListByBase(this.branchId);
    //   this.getNominetedTraineeListByBase(this.branchId);
    //   this.getNominetedForeignTraineeListByBase(this.branchId);
    //   this.geCourseTotalOfficerListByBase(this.branchId);
    //   this.isShown = true;
    // }else{
    //   this.schoolId = this.branchId;
    //   this.pageTitle = "School Dashboard";   
    //   // this.getselectedschools();

    //   this.getNominetedTraineeListBySchoolId(this.schoolId);
    //   this.getNominetedForeignTraineeListBySchoolId(this.schoolId);
    //   this.getrunningCourseTotalOfficerListBySchoolId(this.schoolId);
    //   this.getrunningCourseListBySchool(this.schoolId);
    //   this.getUpcomingCourseListBySchool(this.schoolId);
    //   this.getPendingExamEvaluation(this.schoolId);
    //   this.getTraineeAbsentList(this.schoolId);
    //   this.getRoutineInfoByCourse(this.schoolId);
    //   this.getCurrentRoutineBySchool(this.schoolId);
    //   this.getReadingMetarialBySchool(this.schoolId);
    //   this.getInstructorByCourse(this.schoolId);
    //   this.getCurrentAttendanceBySchool(this.schoolId);
    //   // this.getActiveBulletins(this.schoolId);
    //   this.isShown = true;
    // }
    this.schoolId = this.branchId;
    console.log(this.schoolId)
      // this.baseSchoolNameService.find(this.schoolId).subscribe(response => {   
      //   this.schoolName = response.schoolName;
      //   console.log(this.schoolName);
      // })
    

    // this.baseSchoolNameService.getUserManualByRole(this.role).subscribe(response => {   
    //   console.log("user manual");
    //   console.log(response);
    //   this.userManual=response[0].doc;
    //   console.log(this.userManual);
    // })
    this.getShipInformationBySchoolid(this.schoolId);
    this.getShipEquipListBySchoolid(this.schoolId);
    
  }


  // getActiveBulletins(baseSchoolNameId){
  //   this.studentDashboardService.getActiveBulletinList(baseSchoolNameId).subscribe(res=>{
  //     this.bulletinList=res;  
  //     console.log(this.bulletinList);
  //   });
  // }

  // getselectedschools(){
  //   this.ReadingMaterialService.getselectedschools().subscribe(res=>{
  //     this.selectedschool=res;
  //   });
  // }

  getReadingMetarialBySchool(schoolId){
    this.schoolDashboardService.getReadingMetarialBySchool(schoolId).subscribe(response => {   
      this.ReadIngMaterialList=response;
      console.log(response)
    })
  }

  getShipInformationBySchoolid(schoolId){
    this.schoolDashboardService.getShipInformationBySchoolid(schoolId).subscribe(response => {   
      this.shipInfoList=response;
      this.shipInfoId=response[0].shipInformationId;
      console.log(this.shipInfoId)
    })
  }
  getShipEquipListBySchoolid(schoolId){
    this.schoolDashboardService.getShipEquipListBySchoolid(schoolId).subscribe(response => {   
      this.shipEquipList=response;
      // this.shipInfoId=response[0].shipInformationId;
      console.log(this.shipEquipList)
    })
  }
  getCurrentRoutineBySchool(schoolId){
    this.dbType=1;
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getCurrentRoutineBySchool(currentDateTime,schoolId).subscribe(response => {   
      this.TodayRoutineList=response;
      console.log(response)
    })
  }

  getCurrentAttendanceBySchool(schoolId){    
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getCurrentAttendanceBySchool(currentDateTime,schoolId).subscribe(response => {   
      this.TodayAttendanceList=response;
      console.log(response)
      console.log("Param school");
      console.log(this.paramBaseSchoolNameId);
    })
  }

  getNominetedTraineeListBySchoolId(schoolId){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getnominatedCourseListFromSpRequestBySchoolId(currentDateTime,schoolId).subscribe(response => {         
      this.nomineeCount=response.length;
      console.log(response)
    })
  }

  getNominetedTraineeListByBase(schoolId){
    // let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getNominatedTotalTraineeByBaseFromSp(schoolId).subscribe(response => {         
      this.nomineeCount=response.length;
      console.log(response)
    })
  }

  getRunningCourseDurationByBase(baseNameId){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getRunningCourseDurationByBase(currentDateTime,baseNameId).subscribe(response => {         
      this.runningCourses=response;
      console.log("running");
      console.log(this.runningCourses)
      // this gives an object with dates as keys
      const groups = this.runningCourses.reduce((groups, courses) => {
      const schoolName = courses.schoolName;
      if (!groups[schoolName]) {
        groups[schoolName] = [];
      }
      groups[schoolName].push(courses);
        return groups;
      }, {});

      // Edit: to add it in the array format instead
      this.groupArrays = Object.keys(groups).map((schoolName) => {
        return {
          schoolName,
          courses: groups[schoolName]
        };
      });
      console.log("eeee");
      console.log(this.groupArrays);

    })
  }

  getInstructorByCourse(schoolId){
    this.schoolDashboardService.getInstructorByCourse(schoolId).subscribe(response => {         
      this.InstructorList=response;
      console.log(response)
    })
  }

  getNominetedForeignTraineeListBySchoolId(schoolId){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getnominatedForeignTraineeFromSpRequestBySchoolId(currentDateTime,schoolId, this.masterData.OfficerType.Foreign).subscribe(response => {         
      this.foreignNomineeCount=response.length;
      console.log(response)
    })
  }
  getNominetedForeignTraineeListByBase(schoolId){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getNominatedForeignTraineeByTypeAndBase(currentDateTime,schoolId, this.masterData.OfficerType.Foreign).subscribe(response => {         
      this.foreignNomineeCount=response.length;
      console.log(response)
    })
  }

  getrunningCourseTotalOfficerListBySchoolId(schoolId){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getrunningCourseTotalOfficerListBySchoolRequest(currentDateTime, this.masterData.TraineeStatus.officer, schoolId).subscribe(response => {         
      this.runningOfficerCount=response.length;
    })
    this.schoolDashboardService.getrunningCourseTotalOfficerListBySchoolRequest(currentDateTime, this.masterData.TraineeStatus.sailor, schoolId).subscribe(response => {         
      this.runningSailorCount=response.length;
    })
    this.schoolDashboardService.getrunningCourseTotalOfficerListBySchoolRequest(currentDateTime, this.masterData.TraineeStatus.civil, schoolId).subscribe(response => {         
      this.runningCivilCount=response.length;
    })
  }

  geCourseTotalOfficerListByBase(schoolId){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getCourseTotalOfficerListByBase(currentDateTime, this.masterData.TraineeStatus.officer, schoolId).subscribe(response => {         
      this.runningOfficerCount=response.length;
    })
    this.schoolDashboardService.getCourseTotalOfficerListByBase(currentDateTime, this.masterData.TraineeStatus.sailor, schoolId).subscribe(response => {         
      this.runningSailorCount=response.length;
    })
    this.schoolDashboardService.getCourseTotalOfficerListByBase(currentDateTime, this.masterData.TraineeStatus.civil, schoolId).subscribe(response => {         
      this.runningCivilCount=response.length;
    })
  }


  getrunningCourseListBySchool(schoolId){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getrunningCourseListBySchool(currentDateTime, this.masterData.coursetype.LocalCourse, schoolId).subscribe(response => {   
      
      this.localCourseCount=response.length;
      this.runningCourses=response;
      console.log("running");
      console.log(response)

    })
  }

  getUpcomingCourseListBySchool(schoolId){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getUpcomingCourseListBySchool(currentDateTime, this.masterData.coursetype.LocalCourse, schoolId).subscribe(response => {   
      
      this.UpcomingCourseCount=response.length;
      this.upcomingCourses=response;
    })
  }

  getUpcomingCourseListByBase(baseId){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getUpcomingCourseListByBase(currentDateTime,baseId).subscribe(response => {         
      this.UpcomingCourseCount=response.length;
      this.upcomingCourses=response;
    })
  }

  courseWeekGenerate(row){
    console.log(row)
    const id = row.courseDurationId; 

    this.confirmService.confirm('Confirm  message', 'Are You Sure ').subscribe(result => {
      console.log(result);
      if (result) {
        this.schoolDashboardService.genarateCourseWeek(id).subscribe(() => {
          this.getrunningCourseListBySchool(this.schoolId);
          this.snackBar.open('Course Week Generated Successfully ', '', {
            duration: 3000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    })
  }

  getPendingExamEvaluation(schoolId){
    this.schoolDashboardService.getPendingExamEvaluation(schoolId).subscribe(response => {         
      this.PendingExamEvaluation=response;
      console.log(response)
    })
  }

  getTraineeAbsentList(schoolId){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    this.schoolDashboardService.getTraineeAbsentList(currentDateTime,schoolId).subscribe(response => {   
      this.TraineeAbsentList=response;
      console.log("absent list")
      console.log(response)
    })
  }

  getRoutineInfoByCourse(schoolId){
    this.schoolDashboardService.getRoutineByCourse(schoolId).subscribe(response => {         
      this.RoutineByCourse=response;
      console.log(this.RoutineByCourse)
    })
  }
}

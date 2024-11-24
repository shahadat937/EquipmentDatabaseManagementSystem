//import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import {
  CalendarOptions,
  DateSelectArg,
  EventClickArg,
  EventApi,
} from '@fullcalendar/angular';
import { EventInput } from '@fullcalendar/angular';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {CourseDurationService} from '../services/courseduration.service';
import {dashboardService} from '../services/dashboard.service';
import {MasterData} from 'src/assets/data/master-data';
import {CourseDuration} from '../models/courseduration';
import {SpCourseDuration} from '../models/spcourseduration';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Role } from 'src/app/core/models/role';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTooltip,
  ApexYAxis,
  ApexPlotOptions,
  ApexStroke,
  ApexLegend,
  ApexFill,
} from 'ng-apexcharts';
import { DatePipe } from '@angular/common';
import { SpOfficerDetails } from '../models/spofficerdetails';
export type areaChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
  stroke: ApexStroke;
  tooltip: ApexTooltip;
  dataLabels: ApexDataLabels;
  legend: ApexLegend;
  colors: string[];
};

export type barChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  fill: ApexFill;
  colors: string[];
};

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss','./main.component.css'],
})
export class MainComponent implements OnInit {
  @ViewChild('calendar', { static: false })
  // calendar: Calendar | null;
  public addCusForm: FormGroup;
  dialogTitle: string;
  filterOptions = 'All';
  calendarData: any;
  @ViewChild('chart') chart: ChartComponent;
  public areaChartOptions: Partial<areaChartOptions>;
  public barChartOptions: Partial<barChartOptions>;
  masterData = MasterData;
  ELEMENT_DATA: CourseDuration[] = [];
  upcomingLocalCourses:SpCourseDuration[];
  upcomingForeignCourses:SpCourseDuration[];
  runningCourses:SpCourseDuration[];
  runningForeignCourses:SpCourseDuration[];
  isLoading = false;
  courseTypeId=3;
  runningCourseType:number;
  traineeCount:number;
  dbType:any;
  schoolCount:number;
  localCourseCount:number;
  foreignCourseCount:number;
  intServiceCount:number;
  nomineeCount:number;
  shipCount:any;
  shipinfoList:any[];
  boatcount:any;
  boatList:any[];
  establishmentCount:any;
  establishmentList:any;
  stateOfComponent : any[];
  opl: number;
  nonOpl : number;
  stateOfEqupmentName1 : string;
  stateOfEqupmentName2 : string;
  filterItems: string[] = [
    'work',
    'personal',
    'important',
    'travel',
    'friends',
  ];

  calendarEvents: EventInput[];
  tempEvents: EventInput[];

  public filters = [
    { name: 'work', value: 'Work', checked: true },
    { name: 'personal', value: 'Personal', checked: true },
    { name: 'important', value: 'Important', checked: true },
    { name: 'travel', value: 'Travel', checked: true },
    { name: 'friends', value: 'Friends', checked: true },
  ];

  runningOfficerCount:number;
  CountedRunningOfficer:SpOfficerDetails[];
  runningSailorCount:number;
  foreignNomineeCount:number;
  CountedSailorOfficer:SpOfficerDetails[];
  runningCivilCount:number;
  CountedCivilOfficer:SpOfficerDetails[];
  groupArrays:{ schoolName: string; courses: any; }[];
  baseNameListCount:any[];
  CountShipEquipment : any [];
  combatSystemEquipmentCount : any[];
  calendarOptions: CalendarOptions;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";


  displayedColumns: string[] = ['ser','schoolName','course','noOfCandidates','professional','nbcd','durationFrom','durationTo', 'remark', 'actions'];

  displayedUpcomingForeignColumns: string[] = ['ser','courseTitle','courseName','durationFrom','durationTo', 'country', 'actions'];

  dataSource: MatTableDataSource<SpCourseDuration> = new MatTableDataSource();

  selection = new SelectionModel<CourseDuration>(true, []);
  

  constructor(private datepipe: DatePipe,private CourseDurationService: CourseDurationService,private dashboardService: dashboardService) {}
 

  ngOnInit() {

    this.chart1();
    this.chart2();
    this.getLocalCourseCount();
    this.getForeignCourseCount();
    this.getIntServiceCount();
   // this.getSpCourseDurations(3);
    this.getSpTotalTrainee();
    this.getSpSchoolCount();
    this.getnominatedCourseListFromSpRequest();
    this.getrunningCourseTotalOfficerListfromprocedure();


    this.getShipInfoByShipType();
    this.getBoatByShipType();
    this.getEstablishmentByShipType();
    this.getBaseNameListAndCount();
    this.getStateOfEquipments()
  }

  
 
  initializeEvents(){
  }
  inActiveItem(id){
    this.courseTypeId = id;
   // this.getSpCourseDurations(this.courseTypeId);    
  }
  // getSpCourseDurations(id:number) {
  //   this.isLoading = true;
  //   this.courseTypeId = id;
  //   let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
  //   console.log(currentDateTime);
  //   if(this.courseTypeId == this.masterData.coursetype.LocalCourse){
  //   }
  //   else if(this.courseTypeId === this.masterData.coursetype.ForeignCourse){
  //   }
  //   else{
  //   }
    
  // }
 
  getShipInfoByShipType(){
   this.dashboardService.getShipInformationListByShipType(11).subscribe(response => {           
      this.shipCount=response.length;
      this.shipinfoList=response;
    })
  }
  getBoatByShipType(){
    this.dashboardService.getShipInformationListByShipType(9).subscribe(response => {           
       this.boatcount=response.length;
       this.boatList=response;
     })
   }
   getEstablishmentByShipType(){
    this.dashboardService.getShipInformationListByShipType(6).subscribe(response => {           
       this.establishmentCount=response.length;
       this.establishmentList=response;
     })
   }

   getBaseNameListAndCount(){
    this.dashboardService.getBaseNameListAndCount().subscribe(response => {           
       this.baseNameListCount=response;
      
     })
   }

   getEquipmentCountByCategory(stateOfEquipmentId1, stateOfEquipmentId2){
    this.dashboardService.getEquipmentCountByCategory(stateOfEquipmentId1, stateOfEquipmentId2).subscribe(response =>{
      this.CountShipEquipment = response;   
    })
   }
   getCombatSystemEquipmentCount(combatSystemId, stateOfEquipmentId1, stateOfEquipmentId2){
    this.dashboardService.getCombatSystemEquipmentCount(combatSystemId,stateOfEquipmentId1, stateOfEquipmentId2).subscribe(response =>{
      console.log(response);
      this. combatSystemEquipmentCount = response;   
    })
   }

   getStateOfEquipments() {
    this.isLoading = true;
    this.dashboardService.getStateOfEquipments(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {      
      this.stateOfComponent= response.items; 
      this.nonOpl = this.stateOfComponent[0]?.stateOfEquipmentId?? 0
      this.opl = this.stateOfComponent[1]?.stateOfEquipmentId?? 0
      this.stateOfEqupmentName1 = this.stateOfComponent[0]?.name
      this.stateOfEqupmentName2 = this.stateOfComponent[1]?.name
      console.log(this.stateOfEqupmentName1, this.stateOfEqupmentName2)

      if(this.nonOpl && this.opl) {
        this.getEquipmentCountByCategory(this.nonOpl, this.opl)
        this.getCombatSystemEquipmentCount(this.masterData.equepmentCategory.combatSystem,this.nonOpl, this.opl)
      } 
    })
  }


  getnominatedCourseListFromSpRequest(){
  }

  getrunningCourseTotalOfficerListfromprocedure(){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    // this.dashboardService.getrunningCourseTotalOfficerListfromprocedureRequest(currentDateTime, this.masterData.TraineeStatus.officer).subscribe(response => {         
    //   this.runningOfficerCount=response.length;
    // })
    // this.dashboardService.getnominatedForeignTraineeFromSpRequestBySchoolId(currentDateTime, this.masterData.OfficerType.Foreign).subscribe(response => {         
    //     this.foreignNomineeCount=response.length;
    //     console.log(response)
    //   })
    // this.dashboardService.getrunningCourseTotalOfficerListfromprocedureRequest(currentDateTime, this.masterData.TraineeStatus.sailor).subscribe(response => {         
    //   this.runningSailorCount=response.length;
    // })
    // this.dashboardService.getrunningCourseTotalOfficerListfromprocedureRequest(currentDateTime, this.masterData.TraineeStatus.civil).subscribe(response => {         
    //   this.runningCivilCount=response.length;
    // })
  }

  getSpTotalTrainee() {
    // this.dashboardService.getSpTotalTraineeByTraineeStatus().subscribe(response => {   
    //   this.traineeCount=response
    // })
  }
  getSpSchoolCount() {
    // this.dashboardService.getSpSchoolCount().subscribe(response => {   
    //   this.schoolCount=response
    
    // })
  }

  getLocalCourseCount(){
    // let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    // this.dashboardService.getSpRunningCourseDurationsByType(this.masterData.coursetype.LocalCourse,currentDateTime).subscribe(response => {           
    //   this.localCourseCount=response.length;
    // })
  }
  getForeignCourseCount(){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    // this.dashboardService.getSpRunningForeignCourseDurationsByType(this.masterData.coursetype.ForeignCourse,currentDateTime).subscribe(response => {           
    //   this.foreignCourseCount=response.length;
    //   console.log("foreign count"+this.foreignCourseCount)
    // })
  }
  getIntServiceCount(){
    let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    // this.dashboardService.getSpRunningForeignCourseDurationsByType(this.masterData.coursetype.InterService,currentDateTime).subscribe(response => {           
    //   this.intServiceCount=response.length;
    // })
  }
  
  


  



  private chart1() {
    this.areaChartOptions = {
      series: [
        {
          name: 'new students',
          data: [31, 40, 28, 51, 42, 85, 77],
        },
        {
          name: 'old students',
          data: [11, 32, 45, 32, 34, 52, 41],
        },
      ],
      chart: {
        height: 350,
        type: 'area',
        toolbar: {
          show: false,
        },
        foreColor: '#9aa0ac',
      },
      colors: ['#9F8DF1', '#E79A3B'],
      dataLabels: {
        enabled: false,
      },
      stroke: {
        curve: 'smooth',
      },
      xaxis: {
        type: 'datetime',
        categories: [
          '2018-09-19T00:00:00.000Z',
          '2018-09-19T01:30:00.000Z',
          '2018-09-19T02:30:00.000Z',
          '2018-09-19T03:30:00.000Z',
          '2018-09-19T04:30:00.000Z',
          '2018-09-19T05:30:00.000Z',
          '2018-09-19T06:30:00.000Z',
        ],
      },
      legend: {
        show: true,
        position: 'top',
        horizontalAlign: 'center',
        offsetX: 0,
        offsetY: 0,
      },

      tooltip: {
        x: {
          format: 'dd/MM/yy HH:mm',
        },
      },
    };
  }

  private chart2() {
    this.barChartOptions = {
      series: [
        {
          name: 'percent',
          data: [5, 8, 10, 14, 9, 7, 11, 5, 9, 16, 7, 5],
        },
      ],
      chart: {
        height: 320,
        type: 'bar',
        toolbar: {
          show: false,
        },
        foreColor: '#9aa0ac',
      },
      plotOptions: {
        bar: {
          dataLabels: {
            position: 'top', // top, center, bottom
          },
        },
      },
      dataLabels: {
        enabled: true,
        formatter: function (val) {
          return val + '%';
        },
        offsetY: -20,
        style: {
          fontSize: '12px',
          colors: ['#9aa0ac'],
        },
      },

      xaxis: {
        categories: [
          'Jan',
          'Feb',
          'Mar',
          'Apr',
          'May',
          'Jun',
          'Jul',
          'Aug',
          'Sep',
          'Oct',
          'Nov',
          'Dec',
        ],
        position: 'bottom',
        labels: {
          offsetY: 0,
        },
        axisBorder: {
          show: false,
        },
        axisTicks: {
          show: false,
        },
        crosshairs: {
          fill: {
            type: 'gradient',
            gradient: {
              colorFrom: '#D8E3F0',
              colorTo: '#BED1E6',
              stops: [0, 100],
              opacityFrom: 0.4,
              opacityTo: 0.5,
            },
          },
        },
        tooltip: {
          enabled: true,
          offsetY: -35,
        },
      },
      fill: {
        type: 'gradient',
        colors: ['#4F86F8', '#4F86F8'],
        gradient: {
          shade: 'light',
          type: 'horizontal',
          shadeIntensity: 0.25,
          gradientToColors: undefined,
          inverseColors: true,
          opacityFrom: 1,
          opacityTo: 1,
          stops: [50, 0, 100, 100],
        },
      },
      yaxis: {
        axisBorder: {
          show: false,
        },
        axisTicks: {
          show: false,
        },
        labels: {
          show: false,
          formatter: function (val) {
            return val + '%';
          },
        },
      },
    };
  }
}

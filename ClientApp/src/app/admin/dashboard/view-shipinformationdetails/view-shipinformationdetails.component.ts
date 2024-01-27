import { Component, OnInit,ViewChild,ElementRef  } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
// import { ShipInformation } from '../../../course-management/models/ShipInformation';
// import { ShipInformationService } from '../../../course-management/service/ShipInformation.service';
import {ShipInformationService} from '../../../ship-management/service/ShipInformation.service';
import {ShipInformation} from '../../../ship-management/models/ShipInformation';
import { SelectionModel } from '@angular/cdk/collections';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from '../../../core/service/confirm.service';
//import{MasterData} from 'src/assets/data/master-data'
import{MasterData} from '../../../../assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';

@Component({
  selector: 'app-view-shipinformationdetails',
  templateUrl: './view-shipinformationdetails.component.html',
  styleUrls: ['./view-shipinformationdetails.component.sass']
})
export class ViewShipInformationDetailsComponent implements OnInit {

   masterData = MasterData;
  loading = false;
  userRole = Role;
  ELEMENT_DATA: ShipInformation[] = [];
  isLoading = false;
  shipInformationId: number;
  address:any;
  breadthBmld:any;
  callSign:any;
  class:any;
  contactNo:any;
  cruisingSpeed:any;
  dateOfCommission:any;
  depth:any;
  displacementFullLoad:any;
  displacementLightWeight:any;
  draftAft:any;
  draftFwd:any;
  economicSpeed:any;
  freshWaterCapacity:any;
  fualCapacity:any;
  lengthHom:any;
  lengthLoa:any;
  luboilCapacity:any;
  manufacturer:any;
  maximumContinuousSpeed:any;
  maximumSpeed:any;
  nickName:any;
  authorityName:any;
  baseName:any;
  baseSchoolName:any;
  operationalStatus:any;
  shipType:any;
  sqnName:any;
  fileUpload:any;
  remarks:any;
  dbType:any
  // courseNameId: number;
  // courseTypeId: number;
  // courseTitle: string;
  // courseName:string;
  // noOfCandidates:string;
  // baseSchoolNameId: number;
  // durationFrom:Date;
  // durationTo:Date;
  // professional: string;
  // nbcd: string;
  // remark: string;
  // schoolDb:any;
  branchId:any;
  traineeId:any;
  role:any;
  showHideDiv = false;
  // instituteName: string;
  // groupId: number;
  // group: string;
  // passingYear: string;
  // result: string;
  // outOfResult: string;
  // ShipInformation: string;
  // status:number;           
  // additionaInformation: string;
  // examTypeValues:SelectedModel[]; 
  // groupValues:SelectedModel[]; 
  // boardValues:SelectedModel[]; 

  constructor(private route: ActivatedRoute,private authService: AuthService,private snackBar: MatSnackBar,private ShipInformationService: ShipInformationService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId, this.branchId)

    const id = this.route.snapshot.paramMap.get('shipInformationId'); 
     this.dbType = Number(this.route.snapshot.paramMap.get('dbType')); 
    // this.courseTypeId = Number(this.route.snapshot.paramMap.get('courseTypeId'));
    //console.log(this.schoolDb)
    this.ShipInformationService.find(+id).subscribe( res => {
      this.shipInformationId = res.shipInformationId
       this.address=res.address,
       this.breadthBmld =res.breadthBmld,
       this.callSign =res.callSign,
       this.class =res.class,
       this.contactNo =res.contactNo,
       this.cruisingSpeed =res.cruisingSpeed,
       this.dateOfCommission =res.dateOfCommission,
       this.depth =res.depth,
       this.displacementFullLoad =res.displacementFullLoad,
       this.displacementLightWeight =res.displacementLightWeight,
       this.draftAft= res.draftAft,
       this.draftFwd =res.draftFwd,
       this.economicSpeed = res.economicSpeed,
       this.freshWaterCapacity =res.freshWaterCapacity,
       this.fualCapacity =res.fualCapacity,
       this.lengthHom =res.lengthHom,
       this.lengthLoa =res.lengthLoa,
       this.luboilCapacity =res.luboilCapacity,
       this.manufacturer =res.manufacturer,
       this.maximumContinuousSpeed =res.maximumContinuousSpeed,
       this.maximumSpeed =res.maximumSpeed,
       this.nickName =res.nickName,
       this.authorityName =res.authorityName,
       this.baseName =res.baseName,
       this.baseSchoolName =res.baseSchoolName,
       this.operationalStatus =res.operationalStatus,
       this.shipType =res.shipType,
       this.sqnName =res.sqnName,
       this.fileUpload =res.fileUpload,
       this.remarks =res.remarks
      // this.courseNameId = res.courseNameId,
      // this.courseTitle = res.courseTitle,
      // this.courseName=res.courseName,
      // this.noOfCandidates = res.noOfCandidates,
      // this.baseSchoolNameId = res.baseSchoolNameId,
      // this.durationFrom = res.durationFrom,    
      // this.durationTo = res.durationTo,
      // this.professional = res.professional,
      // this.nbcd = res.nbcd,
      // this.remark = res.remark
      // this.groupId = res.groupId,
      // this.passingYear = res.passingYear,
      // this.result = res.result,
      // this.outOfResult = res.outOfResult,
      // this.ShipInformation = res.ShipInformation,
      // this.status = res.status,            
      // this.additionaInformation = res.additionaInformation  
      console.log("res");
      console.log(res);      
    })
   
    // this.getExamType();
    // this.getBoard();
    // this.getGroup();
  }
  toggle(){
    this.showHideDiv = !this.showHideDiv;
  }
  printSingle(){
    this.showHideDiv= false;
    this.print();
  }
  print(){ 
     
    let printContents, popupWin;
    printContents = document.getElementById('print-routine').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
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
                  text-align:center;
                    }
                    table td {
                  font-size: 13px;
                  text-align:left;
                    }

                    table th {
                  font-size: 13px;
                  text-align:left;
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
          <h3>Ship Information Details</h3>

          </div>
          <br>
          <hr>
          ${printContents}
          
        </body>
      </html>`
    );
    popupWin.document.close();

}


}

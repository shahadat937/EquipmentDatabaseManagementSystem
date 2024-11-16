import { Injectable } from '@angular/core';

import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { SpCourseDuration } from '../models/spcourseduration';
import { SpTotalTrainee } from '../models/sptotaltrainee';
import { SpOfficerDetails } from '../models/spofficerdetails';
import {IStateOfEquipmentPagination,StateOfEquipmentPagination } from 'src/app/basic-setup/models/StateOfEquipmentPagination'
import { StateOfEquipment } from 'src/app/basic-setup/models/StateOfEquipment';


import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class dashboardService {
  baseUrl = environment.apiUrl;
  SpCourseDurations: SpCourseDuration[] = [];
  SpTotalTrainees: SpTotalTrainee[] = [];
  constructor(private http: HttpClient) { }
  StateOfEquipments: StateOfEquipment[] = [];
  StateOfEquipmentPagination = new StateOfEquipmentPagination();

  getTrainingSyllabusListByParams(baseSchoolNameId,courseNameId,bnaSubjectNameId) {

    return this.http.get<any[]>(this.baseUrl + '/training-syllabus/get-trainingSyllabusListByParamsFromSpRequest?baseSchoolNameId='+baseSchoolNameId+'&courseNameId='+courseNameId+'&bnaSubjectNameId='+bnaSubjectNameId).pipe(
      map(response => {
        
        return response;
      })
    ); 
  }

  getBaseNameListAndCount() {
    return this.http.get<any>(this.baseUrl + '/dashboard/get-basenamelistandcount').pipe(
      map(response => {
        return response;
      })
    ); 
  }

  getEquipmentCountByCategory(stateOfEquipmentId1, stateOfEquipmentId2){
    return this.http.get<any>(this.baseUrl+ `/ship-equipment-info/get-ship-equipment-count-by-category/${stateOfEquipmentId1}/${stateOfEquipmentId2}`).pipe(
      map (response =>{
        return response;
      })
    )
  }

  getStateOfEquipments(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IStateOfEquipmentPagination>(this.baseUrl + '/state-of-equipment/get-StateOfEquipments', { observe: 'response', params })
    .pipe(
      map(response => {
        this.StateOfEquipments = [...this.StateOfEquipments, ...response.body.items];
        this.StateOfEquipmentPagination = response.body;
        return this.StateOfEquipmentPagination;
      })
    );
   
  }


  getShipInformationListByShipType(shipTypeId) {
    return this.http.get<any>(this.baseUrl + '/dashboard/get-shipinformation-byshiptype-fromprocedure?shipTypeId='+shipTypeId).pipe(
      map(response => {
        return response;
      })
    ); 
  }


  getSchoolNameById(baseSchoolNameId) {
    return this.http.get<any>(this.baseUrl + '/base-School-name/get-baseSchoolNameDetail/'+baseSchoolNameId).pipe(
      map(response => {
        return response;
      })
    ); 
  }
  getShipInfoByBase(authorityId,operationalStatusId) {
    return this.http.get<any>(this.baseUrl + '/dashboard/get-shipinfobybase?authorityId='+authorityId+'&operationalStatusId='+operationalStatusId).pipe(
      map(response => {
        return response;
      })
    ); 
  }
  
}

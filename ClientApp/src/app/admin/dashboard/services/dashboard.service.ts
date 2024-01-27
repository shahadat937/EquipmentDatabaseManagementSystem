import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { SpCourseDuration } from '../models/spcourseduration';
import { SpTotalTrainee } from '../models/sptotaltrainee';
import { SpOfficerDetails } from '../models/spofficerdetails';

import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class dashboardService {
  baseUrl = environment.apiUrl;
  SpCourseDurations: SpCourseDuration[] = [];
  SpTotalTrainees: SpTotalTrainee[] = [];
  constructor(private http: HttpClient) { }

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

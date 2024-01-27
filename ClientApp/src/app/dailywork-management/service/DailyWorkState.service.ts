import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IDailyWorkStatePagination,DailyWorkStatePagination } from '../models/DailyWorkStatePagination'
import { DailyWorkState } from '../models/DailyWorkState';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class DailyWorkStateService {
  baseUrl = environment.apiUrl;
  DailyWorkStates: DailyWorkState[] = [];
  DailyWorkStatePagination = new DailyWorkStatePagination();
  constructor(private http: HttpClient) { }

  getDailyWorkStates(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IDailyWorkStatePagination>(this.baseUrl + '/daily-work-state/get-DailyWorkStates', { observe: 'response', params })
    .pipe(
      map(response => {
        this.DailyWorkStates = [...this.DailyWorkStates, ...response.body.items];
        this.DailyWorkStatePagination = response.body;
        return this.DailyWorkStatePagination;
      })
    );
   
  }
  getDailyWorkStatesListByNoAction( ){
    return this.http.get<DailyWorkState[]>(this.baseUrl + '/daily-work-state/get-dailyWorkStateListByActionTakenYes');
   }
  // getDailyWorkStatesListByNoAction(pageNumber, pageSize, searchText) { 

  //   let params = new HttpParams();

  //   params = params.append('searchText', searchText.toString());
  //   params = params.append('pageNumber', pageNumber.toString());
  //   params = params.append('pageSize', pageSize.toString());

    
  //   return this.http.get<IDailyWorkStatePagination>(this.baseUrl + '/daily-work-state/get-dailyWorkStateListForActionTakenNo', { observe: 'response', params })
  //   .pipe(
  //     map(response => {
  //       this.DailyWorkStates = [...this.DailyWorkStates, ...response.body.items];
  //       this.DailyWorkStatePagination = response.body;
  //       return this.DailyWorkStatePagination;
  //     })
  //   );
   
  // }

  find(id: number) {
    return this.http.get<DailyWorkState>(this.baseUrl + '/daily-work-state/get-DailyWorkStateDetail/' + id);
  }
  // getSelectedSchoolByBranchLevelAndThirdLevel(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedShipName')
  // }
  getSelectedLetterType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/letter-type/get-selectedLetterType')
  }
  getSelectedDealingOfficer(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/dealing-officer/get-selectedDealingOfficer')
  }
  getSelectedActionTaken(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/action-taken/get-selectedActionTaken')
  }
  getSelectedPriority(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/priority/get-selectedPriority')
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/daily-work-state/update-DailyWorkState/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/daily-work-state/save-DailyWorkState', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/daily-work-state/delete-DailyWorkState/'+id);
  }

}

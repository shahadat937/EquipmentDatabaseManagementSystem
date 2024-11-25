import { environment } from './../../../environments/environment';
import { SelectedModel } from './../../core/models/selectedModel';
import { OverallShipStatus } from './../models/OverallStatusOfShip';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
// import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
  })
  export class OverallStatusOfShip{
    baseUrl = environment.apiUrl;
    OverallStatusOfShip: OverallShipStatus[]=[]
    constructor(private http: HttpClient) { }

    getOverallStatusofShip(pageNumber, pageSize, searchText){
      let params = new HttpParams();
      params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());


    return this.http.get<SelectedModel[]>(this.baseUrl + '/overallstatus-ship/get-StatusOfShip')
    }
    find(id: number) {
      return this.http.get<OverallShipStatus>(this.baseUrl + '/overallstatus-ship/get-StatusOfShipsDetail/' + id);

    }
    getSelectedSchoolByBranchLevelAndThirdLevel(){
      return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedShipName')
    }
    getSelectedOperationalStatus(){
      return this.http.get<SelectedModel[]>(this.baseUrl + '/operational-status/get-selectedOperationalStatus')
    }
    update(id: number,model: any) {
      return this.http.put(this.baseUrl + '/overallstatus-ship/update-OperationalState/'+id, model);
    }
    submit(model: any) {
      return this.http.post(this.baseUrl + '/overallstatus-ship/save-StatusOfShip', model);
    } 
    delete(id:number){
      return this.http.delete(this.baseUrl + '/overallstatus-ship/delete-StatusOfShips/'+id);
    }
  }
  
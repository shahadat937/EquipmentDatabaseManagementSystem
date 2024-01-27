import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IOperationalStatePagination,OperationalStatePagination } from '../models/OperationalStatePagination'
import { OperationalState } from '../models/OperationalState';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class OperationalStateService {
  baseUrl = environment.apiUrl;
  OperationalStates: OperationalState[] = [];
  OperationalStatePagination = new OperationalStatePagination();
  constructor(private http: HttpClient) { }

  getOperationalStates(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IOperationalStatePagination>(this.baseUrl + '/operational-state/get-OperationalStates', { observe: 'response', params })
    .pipe(
      map(response => {
        this.OperationalStates = [...this.OperationalStates, ...response.body.items];
        this.OperationalStatePagination = response.body;
        return this.OperationalStatePagination;
      })
    );
   
  }
  getSelectedOperationalStatus(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/operational-status/get-selectedOperationalStatus')
  }
  find(id: number) {
    return this.http.get<OperationalState>(this.baseUrl + '/operational-state/get-OperationalStateDetail/' + id);
  }

  getSelectedEquipmentName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equpment-name/get-selectedEqupmentName')
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/operational-state/update-OperationalState/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/operational-state/save-OperationalState', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/operational-state/delete-OperationalState/'+id);
  }

}

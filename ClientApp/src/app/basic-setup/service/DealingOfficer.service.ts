import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IDealingOfficerPagination,DealingOfficerPagination } from '../models/DealingOfficerPagination'
import { DealingOfficer } from '../models/DealingOfficer';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class DealingOfficerService {
  baseUrl = environment.apiUrl;
  DealingOfficers: DealingOfficer[] = [];
  DealingOfficerPagination = new DealingOfficerPagination();
  constructor(private http: HttpClient) { }

  getDealingOfficers(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IDealingOfficerPagination>(this.baseUrl + '/dealing-officer/get-DealingOfficers', { observe: 'response', params })
    .pipe(
      map(response => {
        this.DealingOfficers = [...this.DealingOfficers, ...response.body.items];
        this.DealingOfficerPagination = response.body;
        return this.DealingOfficerPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<DealingOfficer>(this.baseUrl + '/dealing-officer/get-DealingOfficerDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/dealing-officer/update-DealingOfficer/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/dealing-officer/save-DealingOfficer', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/dealing-officer/delete-DealingOfficer/'+id);
  }

}

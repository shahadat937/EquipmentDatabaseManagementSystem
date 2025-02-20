import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../src/environments/environment';
import {IReportingYearPagination, ReportingYearPagination } from '../models/ReportingYearPagination'
import { ReportingYear } from '../models/ReportingYear';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ReportingYearService {
  baseUrl = environment.apiUrl;
  ReportingYears: ReportingYear[] = [];
  ReportingYearPagination = new ReportingYearPagination();
  constructor(private http: HttpClient) { }

  getReportingYears(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IReportingYearPagination>(this.baseUrl + '/reporting-Year/get-ReportingYears', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ReportingYears = [...this.ReportingYears, ...response.body.items];
        this.ReportingYearPagination = response.body;
        return this.ReportingYearPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ReportingYear>(this.baseUrl + '/reporting-Year/get-ReportingYearDetail/' + id);
  }


  getSelectedReportingYear(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/reporting-Year/get-selectedReportingYear')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/reporting-Year/update-ReportingYear/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/reporting-Year/save-ReportingYear', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/reporting-Year/delete-ReportingYear/'+id);
  }

}

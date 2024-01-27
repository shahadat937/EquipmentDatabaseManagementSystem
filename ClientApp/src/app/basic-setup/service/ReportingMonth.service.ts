import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IReportingMonthPagination,ReportingMonthPagination } from '../models/ReportingMonthPagination'
import { ReportingMonth } from '../models/ReportingMonth';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ReportingMonthService {
  baseUrl = environment.apiUrl;
  ReportingMonths: ReportingMonth[] = [];
  ReportingMonthPagination = new ReportingMonthPagination();
  constructor(private http: HttpClient) { }

  getReportingMonths(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IReportingMonthPagination>(this.baseUrl + '/reporting-month/get-ReportingMonths', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ReportingMonths = [...this.ReportingMonths, ...response.body.items];
        this.ReportingMonthPagination = response.body;
        return this.ReportingMonthPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ReportingMonth>(this.baseUrl + '/reporting-month/get-ReportingMonthDetail/' + id);
  }


  getSelectedReportingMonth(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/reporting-month/get-selectedReportingMonth')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/reporting-month/update-ReportingMonth/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/reporting-month/save-ReportingMonth', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/reporting-month/delete-ReportingMonth/'+id);
  }

}

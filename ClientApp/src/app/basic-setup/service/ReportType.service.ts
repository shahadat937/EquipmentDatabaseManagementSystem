import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IReportTypePagination,ReportTypePagination } from '../models/ReportTypePagination'
import { ReportType } from '../models/ReportType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ReportTypeService {
  baseUrl = environment.apiUrl;
  ReportTypes: ReportType[] = [];
  ReportTypePagination = new ReportTypePagination();
  constructor(private http: HttpClient) { }

  getReportTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IReportTypePagination>(this.baseUrl + '/report-type/get-ReportTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ReportTypes = [...this.ReportTypes, ...response.body.items];
        this.ReportTypePagination = response.body;
        return this.ReportTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ReportType>(this.baseUrl + '/report-type/get-ReportTypeDetail/' + id);
  }


  // getSelectedLetterType(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/letter-type/get-selectedLetterType')
  // }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/report-type/update-ReportType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/report-type/save-ReportType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/report-type/delete-ReportType/'+id);
  }

}

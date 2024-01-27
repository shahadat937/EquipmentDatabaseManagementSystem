import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IRunningTimePagination,RunningTimePagination } from '../models/RunningTimePagination'
import { RunningTime } from '../models/RunningTime';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class RunningTimeService {
  baseUrl = environment.apiUrl;
  RunningTimes: RunningTime[] = [];
  RunningTimePagination = new RunningTimePagination();
  constructor(private http: HttpClient) { }

  getRunningTimes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IRunningTimePagination>(this.baseUrl + '/running-time/get-RunningTimes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.RunningTimes = [...this.RunningTimes, ...response.body.items];
        this.RunningTimePagination = response.body;
        return this.RunningTimePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<RunningTime>(this.baseUrl + '/running-time/get-RunningTimeDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/running-time/update-RunningTime/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/running-time/save-RunningTime', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/running-time/delete-RunningTime/'+id);
  }

}

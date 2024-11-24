import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { YearlyReturnPagination, IYearlyReturnPagination} from '../models/YearlyReturnPagination';
import { YearlyReturn } from '../models/YearlyReturn';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';

@Injectable({
    providedIn: 'root'
  })
  export class YearlyReturnService{
    baseUrl = environment.apiUrl;
    
    YearlyReturns: YearlyReturn[] = [];
    YearlyReturnsPagination = new YearlyReturnPagination();
    constructor(private http: HttpClient) { }

    getYearlyReturn(pageNumber, pageSize, searchText){
        let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return this.http.get<IYearlyReturnPagination>(this.baseUrl + '/yearly-return/get-YearlyReturn',{
        observe: 'response', params
    })
    .pipe(
        map(response=>{
            this.YearlyReturns = [...this.YearlyReturns,...response.body.items];
            this.YearlyReturnsPagination = response.body;
            return this.YearlyReturnsPagination
        })
    );
    }
    find(id: number) {
        return this.http.get<YearlyReturn>(this.baseUrl + '/yearly-return/get-YearlyReturnDetail/' + id);
      }
      getSelectedSchoolByBranchLevelAndThirdLevel(){
        return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedShipName')
      }
      getSelectedOperationalStatus(){
        return this.http.get<SelectedModel[]>(this.baseUrl + '/operational-status/get-selectedOperationalStatus')
      }
      getSelectedReportingMonth(){
        return this.http.get<SelectedModel[]>(this.baseUrl + '/reporting-month/get-selectedReportingMonth')
      }

      update(id: number, model: any) {
        // const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        return this.http.put(this.baseUrl + '/yearly-return/update-YearlyReturn/'+id, model);
      }
      submit(model: any) {
        return this.http.post(this.baseUrl + '/yearly-return/save-YearlyReturn', model);
      } 
      delete(id:number){
        return this.http.delete(this.baseUrl + '/yearly-return/delete-YearlyReturn/'+id);
      }

  }
  
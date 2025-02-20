import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { QuarterlyReturnPagination, IQuarterlyReturnPagination} from '../models/QuarterlyReturnPagination';
import { QuarterlyReturn } from '../models/QuarterlyReturn';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';

@Injectable({
    providedIn: 'root'
  })
  export class QuarterlyReturnService{
    baseUrl = environment.apiUrl;
    
    QuarterlyReturns: QuarterlyReturn[] = [];
    // QuarterlyReturns: any[] = [];
    QuarterlyReturnsPagination = new QuarterlyReturnPagination();
    // QuarterlyReturnsPagination : any;
    constructor(private http: HttpClient) { }

    getQuarterlyReturn(pageNumber, pageSize, searchText, shipId){
        let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('shipId', shipId.toString());
    

    return this.http.get<IQuarterlyReturnPagination>(this.baseUrl + '/quarterly-returns/get-QuarterlyReturn',{
        observe: 'response', params
    })
    .pipe(
        map(response=>{
            this.QuarterlyReturns = [...this.QuarterlyReturns,...response.body.items];
            this.QuarterlyReturnsPagination = response.body;
            return this.QuarterlyReturnsPagination
        })
    );
    }

    getQuarterlyReturnByAuthorityId(pageNumber, pageSize, searchText, authorityId){
        let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('authorityId', authorityId.toString());

    return this.http.get<IQuarterlyReturnPagination>(this.baseUrl + '/quarterly-returns/get-QuarterlyReturn-by-AuthorityId',{
        observe: 'response', params
    })
    .pipe(
        map(response=>{
            this.QuarterlyReturns = [...this.QuarterlyReturns,...response.body.items];
            this.QuarterlyReturnsPagination = response.body;
            return this.QuarterlyReturnsPagination
        })
    );
    }
    
    find(id: number) {
        return this.http.get<QuarterlyReturn>(this.baseUrl + '/quarterly-returns/get-QuarterlyReturnDetail/' + id);
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
        return this.http.put(this.baseUrl + '/quarterly-returns/update-QuarterlyReturn/'+id, model);
      }
      submit(model: any) {
        return this.http.post(this.baseUrl + '/quarterly-returns/save-QuarterlyReturn', model);
      } 
      delete(id:number){
        return this.http.delete(this.baseUrl + '/quarterly-returns/delete-QuarterlyReturn/'+id);
      }

      getSelectedReportingYears(){
        return this.http.get<SelectedModel[]>(this.baseUrl + '/reporting-year/get-selectedReportingYear')
      }

  }
  
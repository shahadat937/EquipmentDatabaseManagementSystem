import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../src/environments/environment';
import { Budget } from '../models/Budget';
import {IBudgetPagination,BudgetPagination } from '../models/BudgetPagination'
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  baseUrl = environment.apiUrl;
  Budget: Budget[] = [];
  BudgetPagination = new BudgetPagination();
  constructor(private http: HttpClient) { }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/budget/update-Budget/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/budget/save-Budget', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/budget/delete-Budget/'+id);
  }

   find(id: number) {
      return this.http.get<Budget>(this.baseUrl + '/budget/get-BudgetDetail/' + id);
    }

    getBudget(pageNumber, pageSize, searchText, orderBy, orderDirection) { 
      let params = new HttpParams();
      params = params.append('searchText', searchText.toString().trim());
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());    
      params = params.append('orderBy', orderBy.toString());    
      params = params.append('orderDirection', orderDirection.toString());    
      return this.http.get<IBudgetPagination>(this.baseUrl + '/budget/get-Budget', { observe: 'response', params })
      .pipe(
        map(response => {
          this.Budget = [...this.Budget, ...(response.body?.items || [])];
          this.BudgetPagination = response.body ?? {} as BudgetPagination;
          return this.BudgetPagination;
        })
      );
     
    }
  
    getLeatestFyBudget(pageNumber, pageSize, searchText, orderBy, orderDirection, budgetCode) { 
      let params = new HttpParams();
      params = params.append('searchText', searchText.toString().trim());
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());    
      params = params.append('orderBy', orderBy.toString());    
      params = params.append('orderDirection', orderDirection.toString());    
      params = params.append('budgetCode', budgetCode.toString());    
      return this.http.get<IBudgetPagination>(this.baseUrl + '/budget/get-leatest-fy-budget', { observe: 'response', params })
      .pipe(
        map(response => {
          this.Budget = [...this.Budget, ...(response.body?.items || [])];
          this.BudgetPagination = response.body ?? {} as BudgetPagination;
          return this.BudgetPagination;
        })
      );
     
    }

}

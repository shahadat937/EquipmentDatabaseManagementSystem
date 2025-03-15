import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../src/environments/environment';
import { Budget } from '../models/Budget';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  baseUrl = environment.apiUrl;
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

}

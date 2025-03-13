import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/procurement/update-Procurement/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/budget/save-Budget', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/procurement/delete-Procurement/'+id);
  }

}

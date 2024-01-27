import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IProcurementMethodPagination,ProcurementMethodPagination } from '../models/ProcurementMethodPagination'
import { ProcurementMethod } from '../models/ProcurementMethod';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ProcurementMethodService {
  baseUrl = environment.apiUrl;
  ProcurementMethods: ProcurementMethod[] = [];
  ProcurementMethodPagination = new ProcurementMethodPagination();
  constructor(private http: HttpClient) { }

  getProcurementMethods(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IProcurementMethodPagination>(this.baseUrl + '/procurement-method/get-ProcurementMethods', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ProcurementMethods = [...this.ProcurementMethods, ...response.body.items];
        this.ProcurementMethodPagination = response.body;
        return this.ProcurementMethodPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ProcurementMethod>(this.baseUrl + '/procurement-method/get-ProcurementMethodDetail/' + id);
  }


  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/procurement-method/update-ProcurementMethod/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/procurement-method/save-ProcurementMethod', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/procurement-method/delete-ProcurementMethod/'+id);
  }

}

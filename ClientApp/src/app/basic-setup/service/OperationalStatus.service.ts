import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IOperationalStatusPagination,OperationalStatusPagination } from '../models/OperationalStatusPagination'
import { OperationalStatus } from '../models/OperationalStatus';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class OperationalStatusService {
  baseUrl = environment.apiUrl;
  OperationalStatuss: OperationalStatus[] = [];
  OperationalStatusPagination = new OperationalStatusPagination();
  constructor(private http: HttpClient) { }

  getOperationalStatuss(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IOperationalStatusPagination>(this.baseUrl + '/operational-status/get-OperationalStatuss', { observe: 'response', params })
    .pipe(
      map(response => {
        this.OperationalStatuss = [...this.OperationalStatuss, ...response.body.items];
        this.OperationalStatusPagination = response.body;
        return this.OperationalStatusPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<OperationalStatus>(this.baseUrl + '/operational-status/get-OperationalStatusDetail/' + id);
  }


  getSelectedOperationalStatus(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/operational-status/get-selectedOperationalStatus')
  }
   
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/operational-status/update-OperationalStatus/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/operational-status/save-OperationalStatus', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/operational-status/delete-OperationalStatus/'+id);
  }

}

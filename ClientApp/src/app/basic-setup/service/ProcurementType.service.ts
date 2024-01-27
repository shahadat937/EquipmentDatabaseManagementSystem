import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IProcurementTypePagination,ProcurementTypePagination } from '../models/ProcurementTypePagination'
import { ProcurementType } from '../models/ProcurementType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ProcurementTypeService {
  baseUrl = environment.apiUrl;
  ProcurementTypes: ProcurementType[] = [];
  ProcurementTypePagination = new ProcurementTypePagination();
  constructor(private http: HttpClient) { }

  getProcurementTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IProcurementTypePagination>(this.baseUrl + '/procurement-type/get-ProcurementTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ProcurementTypes = [...this.ProcurementTypes, ...response.body.items];
        this.ProcurementTypePagination = response.body;
        return this.ProcurementTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ProcurementType>(this.baseUrl + '/procurement-type/get-ProcurementTypeDetail/' + id);
  }


  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/procurement-type/update-ProcurementType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/procurement-type/save-ProcurementType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/procurement-type/delete-ProcurementType/'+id);
  }

}

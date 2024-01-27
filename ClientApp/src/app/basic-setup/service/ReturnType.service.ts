import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IReturnTypePagination,ReturnTypePagination } from '../models/ReturnTypePagination'
import { ReturnType } from '../models/ReturnType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ReturnTypeService {
  baseUrl = environment.apiUrl;
  ReturnTypes: ReturnType[] = [];
  ReturnTypePagination = new ReturnTypePagination();
  constructor(private http: HttpClient) { }

  getReturnTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IReturnTypePagination>(this.baseUrl + '/return-type/get-ReturnTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ReturnTypes = [...this.ReturnTypes, ...response.body.items];
        this.ReturnTypePagination = response.body;
        return this.ReturnTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ReturnType>(this.baseUrl + '/return-type/get-ReturnTypeDetail/' + id);
  }


  getSelectedReturnType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/return-type/get-selectedReturnType')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/return-type/update-ReturnType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/return-type/save-ReturnType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/return-type/delete-ReturnType/'+id);
  }

}

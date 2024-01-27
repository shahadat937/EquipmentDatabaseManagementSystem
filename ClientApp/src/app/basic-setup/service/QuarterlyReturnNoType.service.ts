import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IQuarterlyReturnNoTypePagination,QuarterlyReturnNoTypePagination } from '../models/QuarterlyReturnNoTypePagination'
import { QuarterlyReturnNoType } from '../models/QuarterlyReturnNoType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class QuarterlyReturnNoTypeService {
  baseUrl = environment.apiUrl;
  QuarterlyReturnNoTypes: QuarterlyReturnNoType[] = [];
  QuarterlyReturnNoTypePagination = new QuarterlyReturnNoTypePagination();
  constructor(private http: HttpClient) { }

  getQuarterlyReturnNoTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IQuarterlyReturnNoTypePagination>(this.baseUrl + '/quarterly-return-no-type/get-QuarterlyReturnNoTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.QuarterlyReturnNoTypes = [...this.QuarterlyReturnNoTypes, ...response.body.items];
        this.QuarterlyReturnNoTypePagination = response.body;
        return this.QuarterlyReturnNoTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<QuarterlyReturnNoType>(this.baseUrl + '/quarterly-return-no-type/get-QuarterlyReturnNoTypeDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/quarterly-return-no-type/update-QuarterlyReturnNoType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/quarterly-return-no-type/save-QuarterlyReturnNoType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/quarterly-return-no-type/delete-QuarterlyReturnNoType/'+id);
  }

}

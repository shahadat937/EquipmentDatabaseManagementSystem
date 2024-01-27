import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IFcLcPagination,FcLcPagination } from '../models/FcLcPagination'
import { FcLc } from '../models/FcLc';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class FcLcService {
  baseUrl = environment.apiUrl;
  FcLcs: FcLc[] = [];
  FcLcPagination = new FcLcPagination();
  constructor(private http: HttpClient) { }

  getFcLcs(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IFcLcPagination>(this.baseUrl + '/fc-lc/get-FcLcs', { observe: 'response', params })
    .pipe(
      map(response => {
        this.FcLcs = [...this.FcLcs, ...response.body.items];
        this.FcLcPagination = response.body;
        return this.FcLcPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<FcLc>(this.baseUrl + '/fc-lc/get-FcLcDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/fc-lc/update-FcLc/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/fc-lc/save-FcLc', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/fc-lc/delete-FcLc/'+id);
  }

}

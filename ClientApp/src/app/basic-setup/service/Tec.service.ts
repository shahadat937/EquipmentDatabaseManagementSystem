import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {ITecPagination,TecPagination } from '../models/TecPagination'
import { Tec } from '../models/Tec';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class TecService {
  baseUrl = environment.apiUrl;
  Tecs: Tec[] = [];
  TecPagination = new TecPagination();
  constructor(private http: HttpClient) { }

  getTecs(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<ITecPagination>(this.baseUrl + '/tec/get-Tecs', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Tecs = [...this.Tecs, ...response.body.items];
        this.TecPagination = response.body;
        return this.TecPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<Tec>(this.baseUrl + '/tec/get-TecDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/tec/update-Tec/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/tec/save-Tec', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/tec/delete-Tec/'+id);
  }

}

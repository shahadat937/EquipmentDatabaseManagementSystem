import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {ISqnPagination,SqnPagination } from '../models/SqnPagination'
import { Sqn } from '../models/Sqn';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class SqnService {
  baseUrl = environment.apiUrl;
  Sqns: Sqn[] = [];
  SqnPagination = new SqnPagination();
  constructor(private http: HttpClient) { }

  getSqns(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<ISqnPagination>(this.baseUrl + '/sqn/get-Sqns', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Sqns = [...this.Sqns, ...response.body.items];
        this.SqnPagination = response.body;
        return this.SqnPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<Sqn>(this.baseUrl + '/sqn/get-SqnDetail/' + id);
  }


  getSelectedSqn(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/sqn/get-selectedSqn')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/sqn/update-Sqn/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/sqn/save-Sqn', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/sqn/delete-Sqn/'+id);
  }

}

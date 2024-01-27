import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IControlledPagination,ControlledPagination } from '../models/ControlledPagination'
import { Controlled } from '../models/Controlled';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ControlledService {
  baseUrl = environment.apiUrl;
  Controlleds: Controlled[] = [];
  ControlledPagination = new ControlledPagination();
  constructor(private http: HttpClient) { }

  getControlleds(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IControlledPagination>(this.baseUrl + '/controlled/get-Controlleds', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Controlleds = [...this.Controlleds, ...response.body.items];
        this.ControlledPagination = response.body;
        return this.ControlledPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<Controlled>(this.baseUrl + '/controlled/get-ControlledDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/controlled/update-Controlled/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/controlled/save-Controlled', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/controlled/delete-Controlled/'+id);
  }

}

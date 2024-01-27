import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IDgdpNssdPagination,DgdpNssdPagination } from '../models/DgdpNssdPagination'
import { DgdpNssd } from '../models/DgdpNssd';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class DgdpNssdService {
  baseUrl = environment.apiUrl;
  DgdpNssds: DgdpNssd[] = [];
  DgdpNssdPagination = new DgdpNssdPagination();
  constructor(private http: HttpClient) { }

  getDgdpNssds(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IDgdpNssdPagination>(this.baseUrl + '/dgdp-nssd/get-DgdpNssds', { observe: 'response', params })
    .pipe(
      map(response => {
        this.DgdpNssds = [...this.DgdpNssds, ...response.body.items];
        this.DgdpNssdPagination = response.body;
        return this.DgdpNssdPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<DgdpNssd>(this.baseUrl + '/dgdp-nssd/get-DgdpNssdDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/dgdp-nssd/update-DgdpNssd/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/dgdp-nssd/save-DgdpNssd', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/dgdp-nssd/delete-DgdpNssd/'+id);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IEnvelopePagination,EnvelopePagination } from '../models/EnvelopePagination'
import { Envelope } from '../models/Envelope';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class EnvelopeService {
  baseUrl = environment.apiUrl;
  Envelopes: Envelope[] = [];
  EnvelopePagination = new EnvelopePagination();
  constructor(private http: HttpClient) { }

  getEnvelopes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IEnvelopePagination>(this.baseUrl + '/envelope/get-Envelopes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Envelopes = [...this.Envelopes, ...response.body.items];
        this.EnvelopePagination = response.body;
        return this.EnvelopePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<Envelope>(this.baseUrl + '/envelope/get-EnvelopeDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/envelope/update-Envelope/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/envelope/save-Envelope', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/envelope/delete-Envelope/'+id);
  }

}

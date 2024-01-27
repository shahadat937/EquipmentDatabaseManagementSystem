import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {ITenderOpeningDateTypePagination,TenderOpeningDateTypePagination } from '../models/TenderOpeningDateTypePagination'
import { TenderOpeningDateType } from '../models/TenderOpeningDateType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class TenderOpeningDateTypeService {
  baseUrl = environment.apiUrl;
  TenderOpeningDateTypes: TenderOpeningDateType[] = [];
  TenderOpeningDateTypePagination = new TenderOpeningDateTypePagination();
  constructor(private http: HttpClient) { }

  getTenderOpeningDateTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<ITenderOpeningDateTypePagination>(this.baseUrl + '/tender-opening-datetype/get-TenderOpeningDateTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.TenderOpeningDateTypes = [...this.TenderOpeningDateTypes, ...response.body.items];
        this.TenderOpeningDateTypePagination = response.body;
        return this.TenderOpeningDateTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<TenderOpeningDateType>(this.baseUrl + '/tender-opening-datetype/get-TenderOpeningDateTypeDetail/' + id);
  }


  getSelectedTenderOpeningDateType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/tender-opening-datetype/get-selectedTenderOpeningDateType')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/tender-opening-datetype/update-TenderOpeningDateType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/tender-opening-datetype/save-TenderOpeningDateType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/tender-opening-datetype/delete-TenderOpeningDateType/'+id);
  }

}

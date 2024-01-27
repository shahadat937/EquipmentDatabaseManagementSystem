import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {ILetterTypePagination,LetterTypePagination } from '../models/LetterTypePagination'
import { LetterType } from '../models/LetterType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class LetterTypeService {
  baseUrl = environment.apiUrl;
  LetterTypes: LetterType[] = [];
  LetterTypePagination = new LetterTypePagination();
  constructor(private http: HttpClient) { }

  getLetterTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<ILetterTypePagination>(this.baseUrl + '/letter-type/get-LetterTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.LetterTypes = [...this.LetterTypes, ...response.body.items];
        this.LetterTypePagination = response.body;
        return this.LetterTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<LetterType>(this.baseUrl + '/letter-type/get-LetterTypeDetail/' + id);
  }


  getSelectedLetterType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/letter-type/get-selectedLetterType')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/letter-type/update-LetterType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/letter-type/save-LetterType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/letter-type/delete-LetterType/'+id);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IBookTypePagination,BookTypePagination } from '../models/BookTypePagination'
import { BookType } from '../models/BookType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class BookTypeService {
  baseUrl = environment.apiUrl;
  BookTypes: BookType[] = [];
  BookTypePagination = new BookTypePagination();
  constructor(private http: HttpClient) { }

  getBookTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IBookTypePagination>(this.baseUrl + '/book-type/get-BookTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.BookTypes = [...this.BookTypes, ...response.body.items];
        this.BookTypePagination = response.body;
        return this.BookTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<BookType>(this.baseUrl + '/book-type/get-BookTypeDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/book-type/update-BookType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/book-type/save-BookType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/book-type/delete-BookType/'+id);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IBookUserManualBRInfoPagination,BookUserManualBRInfoPagination } from '../models/BookUserManualBRInfoPagination'
import { BookUserManualBRInfo } from '../models/BookUserManualBRInfo';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class BookUserManualBRInfoService {
  baseUrl = environment.apiUrl;
  BookUserManualBRInfos: BookUserManualBRInfo[] = [];
  BookUserManualBRInfoPagination = new BookUserManualBRInfoPagination();
  constructor(private http: HttpClient) { }

  getBookUserManualBRInfos(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IBookUserManualBRInfoPagination>(this.baseUrl + '/book-user-manual-br-info/get-BookUserManualBRInfos', { observe: 'response', params })
    .pipe(
      map(response => {
        this.BookUserManualBRInfos = [...this.BookUserManualBRInfos, ...response.body.items];
        this.BookUserManualBRInfoPagination = response.body;
        return this.BookUserManualBRInfoPagination;
      })
    );
   
  }
  getSelectedSchoolByBranchLevelAndThirdLevel(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedShipName')
  }
  getSelectedBookType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/book-type/get-selectedBookType')
  }
   
  find(id: number) {
    return this.http.get<BookUserManualBRInfo>(this.baseUrl + '/book-user-manual-br-info/get-BookUserManualBRInfoDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/book-user-manual-br-info/update-BookUserManualBRInfo/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/book-user-manual-br-info/save-BookUserManualBRInfo', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/book-user-manual-br-info/delete-BookUserManualBRInfo/'+id);
  }

}

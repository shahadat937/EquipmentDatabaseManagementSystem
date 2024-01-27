import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IGroupNamePagination,GroupNamePagination } from '../models/GroupNamePagination'
import { GroupName } from '../models/GroupName';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class GroupNameService {
  baseUrl = environment.apiUrl;
  GroupNames: GroupName[] = [];
  GroupNamePagination = new GroupNamePagination();
  constructor(private http: HttpClient) { }

  getGroupNames(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IGroupNamePagination>(this.baseUrl + '/group-name/get-GroupNames', { observe: 'response', params })
    .pipe(
      map(response => {
        this.GroupNames = [...this.GroupNames, ...response.body.items];
        this.GroupNamePagination = response.body;
        return this.GroupNamePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<GroupName>(this.baseUrl + '/group-name/get-GroupNameDetail/' + id);
  }


  getSelectedGroupName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/group-name/get-selectedGroupName')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/group-name/update-GroupName/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/group-name/save-GroupName', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/group-name/delete-GroupName/'+id);
  }

}

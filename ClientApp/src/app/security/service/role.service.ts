import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { Role } from '../models/role';
import {IRolePagination, RolePagination } from '../models/rolePagination'

@Injectable({
  providedIn: 'root'
})

export class RoleService {

  baseUrl = environment.securityUrl;
  roles: Role[] = [];
  rolePagination = new RolePagination();
  constructor(private http: HttpClient) { }

  getRoles(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IRolePagination>(this.baseUrl + '/Role/get-roles', { observe: 'response', params })
    .pipe(
      map(response => {
        this.roles = [...this.roles, ...response.body.items];
        this.rolePagination = response.body;
        return this.rolePagination;
      })
    );
  }
  find(id: string) {
    return this.http.get<Role>(this.baseUrl + '/Role/get-roleDetail/' + id);
  }
   

  update(id: string,model: any) {
    //console.log("Test",id, model)
    return this.http.put(this.baseUrl + `/Role/update-role/${id}`, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/role/save-role', model);
  }
  delete(id){
    //console.log(id);
    return this.http.delete(this.baseUrl + '/role/delete-role/'+id);
  }
  getselectedrole(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/role/get-selectedroles')
  }
  getselectedrolesForTrainee(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/role/get-selectedrolesForTrainee')
  }
}

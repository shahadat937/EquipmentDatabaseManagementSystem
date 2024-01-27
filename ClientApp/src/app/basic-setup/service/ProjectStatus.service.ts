import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IProjectStatusPagination,ProjectStatusPagination } from '../models/ProjectStatusPagination'
import { ProjectStatus } from '../models/ProjectStatus';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ProjectStatusService {
  baseUrl = environment.apiUrl;
  ProjectStatuses: ProjectStatus[] = [];
  ProjectStatusPagination = new ProjectStatusPagination();
  constructor(private http: HttpClient) { }

  getProjectStatuses(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IProjectStatusPagination>(this.baseUrl + '/project-status/get-ProjectStatuses', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ProjectStatuses = [...this.ProjectStatuses, ...response.body.items];
        this.ProjectStatusPagination = response.body;
        return this.ProjectStatusPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ProjectStatus>(this.baseUrl + '/project-status/get-ProjectStatusDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/project-status/update-ProjectStatus/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/project-status/save-ProjectStatus', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/project-status/delete-ProjectStatus/'+id);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../src/environments/environment';
import { Project } from '../models/Project';
import {IProjectPagination,ProjectPagination } from '../models/ProjectPagination'
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  baseUrl = environment.apiUrl;
  Project: Project[] = [];
  ProjectPagination = new ProjectPagination();
  constructor(private http: HttpClient) { }


  getProject(pageNumber, pageSize, searchText, orderBy, orderDirection) { 
    let params = new HttpParams();
    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());    
    params = params.append('orderBy', orderBy.toString());    
    params = params.append('orderDirection', orderDirection.toString());    
    return this.http.get<IProjectPagination>(this.baseUrl + '/project/get-Project', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Project = [...this.Project, ...(response.body?.items || [])];
        this.ProjectPagination = response.body ?? {} as ProjectPagination;
        return this.ProjectPagination;
      })
    );
   
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/project/update-Project/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/project/save-Project', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/project/delete-Project/'+id);
  }

   find(id: number) {
      return this.http.get<Project>(this.baseUrl + '/project/get-ProjectDetail/' + id);
    }

    getForeignTrainingProject(pageNumber, pageSize, searchText, orderBy, orderDirection) { 
      let params = new HttpParams();
      params = params.append('searchText', searchText.toString());
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());    
      params = params.append('orderBy', orderBy.toString());    
      params = params.append('orderDirection', orderDirection.toString());    
      return this.http.get<IProjectPagination>(this.baseUrl + '/project/get-Foreign-Training-Project', { observe: 'response', params })
      .pipe(
        map(response => {
          this.Project = [...this.Project, ...(response.body?.items || [])];
          this.ProjectPagination = response.body ?? {} as ProjectPagination;
          return this.ProjectPagination;
        })
      );
     
    }
    getFatTrainingProject(pageNumber, pageSize, searchText, orderBy, orderDirection) { 
      let params = new HttpParams();
      params = params.append('searchText', searchText.toString());
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());    
      params = params.append('orderBy', orderBy.toString());    
      params = params.append('orderDirection', orderDirection.toString());    
      return this.http.get<IProjectPagination>(this.baseUrl + '/project/get-Fat-Project', { observe: 'response', params })
      .pipe(
        map(response => {
          this.Project = [...this.Project, ...(response.body?.items || [])];
          this.ProjectPagination = response.body ?? {} as ProjectPagination;
          return this.ProjectPagination;
        })
      );
     
    }
    getInstallationTrainingProject(pageNumber, pageSize, searchText, orderBy, orderDirection) { 
      let params = new HttpParams();
      params = params.append('searchText', searchText.toString());
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());    
      params = params.append('orderBy', orderBy.toString());    
      params = params.append('orderDirection', orderDirection.toString());    
      return this.http.get<IProjectPagination>(this.baseUrl + '/project/get-Installation-Project', { observe: 'response', params })
      .pipe(
        map(response => {
          this.Project = [...this.Project, ...(response.body?.items || [])];
          this.ProjectPagination = response.body ?? {} as ProjectPagination;
          return this.ProjectPagination;
        })
      );
     
    }
}

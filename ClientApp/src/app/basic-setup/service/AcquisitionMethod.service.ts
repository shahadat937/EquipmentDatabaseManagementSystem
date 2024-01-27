import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IAcquisitionMethodPagination,AcquisitionMethodPagination } from '../models/AcquisitionMethodPagination'
import { AcquisitionMethod } from '../models/AcquisitionMethod';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class AcquisitionMethodService {
  baseUrl = environment.apiUrl;
  AcquisitionMethods: AcquisitionMethod[] = [];
  AcquisitionMethodPagination = new AcquisitionMethodPagination();
  constructor(private http: HttpClient) { }

  getAcquisitionMethods(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IAcquisitionMethodPagination>(this.baseUrl + '/acquisition-method/get-AcquisitionMethods', { observe: 'response', params })
    .pipe(
      map(response => {
        this.AcquisitionMethods = [...this.AcquisitionMethods, ...response.body.items];
        this.AcquisitionMethodPagination = response.body;
        return this.AcquisitionMethodPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<AcquisitionMethod>(this.baseUrl + '/acquisition-method/get-AcquisitionMethodDetail/' + id);
  }


  getSelectedAcquisitionMethod(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/acquisition-method/get-AcquisitionMethodDetail')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/acquisition-method/update-AcquisitionMethod/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/acquisition-method/save-AcquisitionMethod', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/acquisition-method/delete-AcquisitionMethod/'+id);
  }

}

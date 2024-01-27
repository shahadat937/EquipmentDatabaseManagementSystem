import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IStateOfEquipmentPagination,StateOfEquipmentPagination } from '../models/StateOfEquipmentPagination'
import { StateOfEquipment } from '../models/StateOfEquipment';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class StateOfEquipmentService {
  baseUrl = environment.apiUrl;
  StateOfEquipments: StateOfEquipment[] = [];
  StateOfEquipmentPagination = new StateOfEquipmentPagination();
  constructor(private http: HttpClient) { }

  getStateOfEquipments(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IStateOfEquipmentPagination>(this.baseUrl + '/state-of-equipment/get-StateOfEquipments', { observe: 'response', params })
    .pipe(
      map(response => {
        this.StateOfEquipments = [...this.StateOfEquipments, ...response.body.items];
        this.StateOfEquipmentPagination = response.body;
        return this.StateOfEquipmentPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<StateOfEquipment>(this.baseUrl + '/state-of-equipment/get-StateOfEquipmentDetail/' + id);
  }


  getSelectedStateOfEquipment(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/state-of-equipment/get-selectedStateOfEquipment')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/state-of-equipment/update-StateOfEquipment/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/state-of-equipment/save-StateOfEquipment', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/state-of-equipment/delete-StateOfEquipment/'+id);
  }

}

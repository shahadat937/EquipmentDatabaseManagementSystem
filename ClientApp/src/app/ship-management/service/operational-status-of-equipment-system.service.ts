import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { OperationalStatusPagination } from '../models/OperationalStatusPagination';
import { environment } from 'src/environments/environment';
import { SelectedModel } from 'src/app/core/models/selectedModel';




@Injectable({
  providedIn: 'root'
})
export class OperationalStatusOfEquipmentSystemService {

  baseUrl = environment.apiUrl;
  OperationalStatusPaginationInfos: any[] = [];
  OperationalStatusPaginationInfoPagination = new OperationalStatusPagination();
  constructor(private http: HttpClient) { 

  }
  getOperationalStatusOfEquipment(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    console.log(params);


    
    return this.http.get<any>(this.baseUrl + '/operational-status-of-equipment-system/get-operationalStatusOfEquipmentSystem', { observe: 'response', params })
    .pipe(
      map(response => {
        console.log(response);
        this.OperationalStatusPaginationInfos = [...this.OperationalStatusPaginationInfos, ...response.body.items];
        this.OperationalStatusPaginationInfoPagination = response.body;
        return this.OperationalStatusPaginationInfoPagination;
      })
    );
   
  }
  getSelectedShipName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedShipName')
  }
  getSelectedEquipmentName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equpment-name/get-selectedEqupmentName')
  }
  getSelectedStateOfEquipment(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/state-of-equipment/get-selectedStateOfEquipment')
  }
  delete(id:number){
    console.log(id);
    return this.http.delete(this.baseUrl + '/operational-status-of-equipment-system/delete-operationalStatusOfEquipmentSystem/'+id);
  }
  find(id: any) {
    console.log(id);
    return this.http.get<any>(this.baseUrl + '/operational-status-of-equipment-system/get-operationalStatusOfEquipmentSystem/'+id);
  }

  update(id: number,model: any) {
    
    return this.http.put(this.baseUrl + '/operational-status-of-equipment-system/update-operationalStatusOfEquipmentSystem/'+id, model);
  }

  submit(model: any) {
    return this.http.post(this.baseUrl + '/operational-status-of-equipment-system/save-operationalStatusOfEquipmentSystem', model);
  } 
}

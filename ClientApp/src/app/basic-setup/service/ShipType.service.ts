import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IShipTypePagination,ShipTypePagination } from '../models/ShipTypePagination'
import { ShipType } from '../models/ShipType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ShipTypeService {
  baseUrl = environment.apiUrl;
  ShipTypes: ShipType[] = [];
  ShipTypePagination = new ShipTypePagination();
  constructor(private http: HttpClient) { }

  getShipTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IShipTypePagination>(this.baseUrl + '/ship-type/get-ShipTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShipTypes = [...this.ShipTypes, ...response.body.items];
        this.ShipTypePagination = response.body;
        return this.ShipTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ShipType>(this.baseUrl + '/ship-type/get-ShipTypeDetail/' + id);
  }


  getSelectedShipType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/ship-type/get-selectedShipType')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/ship-type/update-ShipType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/ship-type/save-ShipType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/ship-type/delete-ShipType/'+id);
  }

}

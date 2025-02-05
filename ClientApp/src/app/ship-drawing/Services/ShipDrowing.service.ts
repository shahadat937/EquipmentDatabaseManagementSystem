import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../src/environments/environment';
import {IShipDrowingPagination,ShipDrowingPagination } from '../models/ShipDrowingPagination'
import { ShipDrowing } from '../models/ShipDrowing';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ShipDrowingService {
  baseUrl = environment.apiUrl;
  ShipDrowings: ShipDrowing[] = [];
  ShipDrowingPagination = new ShipDrowingPagination();
  constructor(private http: HttpClient) { }

  getShipDrowings(pageNumber, pageSize, searchText, shipId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('shipId', shipId.toString());
    // params = params.append('departmentNameId', departmentNameId.toString());

    
    return this.http.get<IShipDrowingPagination>(this.baseUrl + '/ship-drowing/get-ShipDrowings', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShipDrowings = [...this.ShipDrowings, ...response.body.items];
        this.ShipDrowingPagination = response.body;
        return this.ShipDrowingPagination;
      })
    );
   
  }
  getShipDrowingsByAuthorityId(pageNumber, pageSize, searchText, authorityId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('authorityId', authorityId.toString());
    // params = params.append('departmentNameId', departmentNameId.toString());

    
    return this.http.get<IShipDrowingPagination>(this.baseUrl + '/ship-drowing/get-ShipDrowings-By-AuthorityId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShipDrowings = [...this.ShipDrowings, ...response.body.items];
        this.ShipDrowingPagination = response.body;
        return this.ShipDrowingPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ShipDrowing>(this.baseUrl + '/ship-drowing/get-ShipDrowingDetail/' + id);
  }


  getselecteddivision(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/notice-board/get-selectedShipDrowing')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/ship-drowing/update-ShipDrowing/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/ship-drowing/save-ShipDrowing', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/ship-drowing/delete-ShipDrowing/'+id);
  }

}

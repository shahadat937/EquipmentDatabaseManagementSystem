import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IShipInformationPagination,ShipInformationPagination } from '../models/ShipInformationPagination'
import { ShipInformation } from '../models/ShipInformation';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ShipInformationService {
  baseUrl = environment.apiUrl;
  ShipInformations: ShipInformation[] = [];
  ShipInformationPagination = new ShipInformationPagination();
  constructor(private http: HttpClient) { }

  getShipInformations(pageNumber, pageSize, searchText, shipId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('shipId', shipId.toString());

    
    return this.http.get<IShipInformationPagination>(this.baseUrl + '/ship-information/get-ship-informations', { observe: 'response', params })
    .pipe(
      map(response => {
        console.log(response)
        this.ShipInformations = [...this.ShipInformations, ...response.body.items];
        this.ShipInformationPagination = response.body;
        return this.ShipInformationPagination;
      })
    );
   
  }
  getShipInformationsByAuthorityId(pageNumber, pageSize, searchText, authorityId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('authorityId', authorityId.toString());

    
    return this.http.get<IShipInformationPagination>(this.baseUrl + '/ship-information/get-ship-informations-by-authorityid', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShipInformations = [...this.ShipInformations, ...response.body.items];
        this.ShipInformationPagination = response.body;
        return this.ShipInformationPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ShipInformation>(this.baseUrl + '/ship-information/get-ship-information-detail/' + id);
  }

  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }

  getSelectedOrganizationByBranchLevel(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedOrganizationByBranchLevel')
  }
  getSelectedSchoolByBranchLevelAndThirdLevel(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedBaseSchoolNameByBranchLevelAndThirdLevel?baseNameId='+baseNameId)
  }
  getSelectedSqn(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/sqn/get-selectedSqn')
  }
  getSelectedOperationalStatus(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/operational-status/get-selectedOperationalStatus')
  }
  getSelectedShipType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/ship-type/get-selectedShipType')
  }


  getSelectedShipInformation(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/ship-information/get-selected-ship-information')
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/ship-information/update-ship-information/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/ship-information/save-ship-information', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/ship-information/delete-ship-information/'+id);
  }

}

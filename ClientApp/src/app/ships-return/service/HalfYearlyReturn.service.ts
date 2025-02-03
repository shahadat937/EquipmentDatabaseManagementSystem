import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../src/environments/environment';
import {IHalfYearlyReturnPagination,HalfYearlyReturnPagination } from '../models/HalfYearlyReturnPagination'
import { HalfYearlyReturn } from '../models/HalfYearlyReturn';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
import { ShipEquipmentInfo } from '../../../../src/app/ship-management/models/ShipEquipmentInfo';
import { shipEquipmentInfoList } from '../models/shipEquipmentInfoList';
@Injectable({
  providedIn: 'root'
})
export class HalfYearlyReturnService {
  baseUrl = environment.apiUrl;
  HalfYearlyReturns: HalfYearlyReturn[] = [];
  HalfYearlyReturnPagination = new HalfYearlyReturnPagination();
  constructor(private http: HttpClient) { }

  getHalfYearlyReturns(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IHalfYearlyReturnPagination>(this.baseUrl + '/half-yearly-return/get-HalfYearlyReturns', { observe: 'response', params })
    .pipe(
      map(response => {
        this.HalfYearlyReturns = [...this.HalfYearlyReturns, ...response.body.items];
        this.HalfYearlyReturnPagination = response.body;
        return this.HalfYearlyReturnPagination;
      })
    );
   
  }

  getHalfYearlyReturnsByAuthorityId(pageNumber, pageSize, searchText, authorityId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('authorityId', authorityId.toString());

    
    return this.http.get<IHalfYearlyReturnPagination>(this.baseUrl + '/half-yearly-return/get-HalfYearlyReturns-by-AuthorityId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.HalfYearlyReturns = [...this.HalfYearlyReturns, ...response.body.items];
        this.HalfYearlyReturnPagination = response.body;
        return this.HalfYearlyReturnPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<HalfYearlyReturn>(this.baseUrl + '/half-yearly-return/get-HalfYearlyReturnDetail/' + id);
  }
  getSelectedSchoolByBranchLevelAndThirdLevel(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedShipName')
  }
  
  getSelectedEquipmentNameByCategory(equipmentCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equpment-name/get-selectedEqupmentNameByCategory?equepmentCategoryId='+equipmentCategoryId)
  }
  getShipEquipmentInfoListForHalfYearly(equipmentCategoryId,equpmentNameId, shipId){
    return this.http.get<shipEquipmentInfoList[]>(this.baseUrl + '/ship-equipment-info/get-shipEquipmentInfoListForHalfYearly?equipmentCategoryId='+equipmentCategoryId+'&equpmentNameId='+equpmentNameId+'&shipId='+shipId)
  }
 
  getSelectedEquipmentCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-category/get-selectedEquipmentCategory')
  }
  getSelectedReportingMonth(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/reporting-month/get-selectedReportingMonth')
  }
 
  getSelectedBrand(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/brand/get-selectedBrand')
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/half-yearly-return/update-HalfYearlyReturn/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/half-yearly-return/save-HalfYearlyReturn', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/half-yearly-return/delete-HalfYearlyReturn/'+id);
  }

}

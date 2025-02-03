import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IMonthlyReturnPagination,MonthlyReturnPagination } from '../models/MonthlyReturnPagination'
import { MonthlyReturn } from '../models/MonthlyReturn';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class MonthlyReturnService {
  baseUrl = environment.apiUrl;
  MonthlyReturns: MonthlyReturn[] = [];
  MonthlyReturnPagination = new MonthlyReturnPagination();
  constructor(private http: HttpClient) { }

  getMonthlyReturns(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IMonthlyReturnPagination>(this.baseUrl + '/monthly-return/get-MonthlyReturns', { observe: 'response', params })
    .pipe(
      map(response => {
        console.log(response)
        this.MonthlyReturns = [...this.MonthlyReturns, ...response.body.items];
        this.MonthlyReturnPagination = response.body;
        return this.MonthlyReturnPagination;
      })
    );
   
  }

  getMonthlyReturnsByAuthorityId(pageNumber, pageSize, searchText, authorityId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('authorityId', authorityId.toString());

    
    return this.http.get<IMonthlyReturnPagination>(this.baseUrl + '/monthly-return/get-MonthlyReturns-by-AuthorityId', { observe: 'response', params })
    .pipe(
      map(response => {
        console.log(response)
        this.MonthlyReturns = [...this.MonthlyReturns, ...response.body.items];
        this.MonthlyReturnPagination = response.body;
        return this.MonthlyReturnPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<MonthlyReturn>(this.baseUrl + '/monthly-return/get-MonthlyReturnDetail/' + id);
  }
  getSelectedSchoolByBranchLevelAndThirdLevel(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedShipName')
  }
  // getSelectedSchoolName(baseNameId){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  // }

  // getSelectedOrganizationByBranchLevel(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedOrganizationByBranchLevel')
  // }
  // getSelectedSchoolByBranchLevelAndThirdLevel(baseNameId){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedBaseSchoolNameByBranchLevelAndThirdLevel?baseNameId='+baseNameId)
  // }
  // getSelectedSqn(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/sqn/get-selectedSqn')
  // }
  // getSelectedOperationalStatus(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/operational-status/get-selectedOperationalStatus')
  // }
  // getSelectedShipType(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/ship-type/get-selectedShipType')
  // }operational-status/get-selectedOperationalStatus
  getSelectedEquipmentNameByCategory(equipmentCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equpment-name/get-selectedEqupmentNameByCategory?equepmentCategoryId='+equipmentCategoryId)
  }

  getSelectedModelByShip(baseSchoolNameId, equipmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + `/ship-equipment-info/get-selectedShipEquipmentModelByShip?baseSchoolNameId=${baseSchoolNameId}&equipmentNameId=${equipmentNameId}`)
  }
 
  getSelectedOperationalStatus(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/operational-status/get-selectedOperationalStatus')
  }
  getSelectedEquipmentCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-category/get-selectedEquipmentCategory')
  }
  getSelectedReportingMonth(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/reporting-month/get-selectedReportingMonth')
  }
  getSelectedReturnType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/return-type/get-selectedReturnType')
  }

  // update(id: number,model: any) {
  //   const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  //   return this.http.put(this.baseUrl + '/monthly-return/update-MonthlyReturn/'+id, model);
  // }
  update(id: number, model: any) {
    // const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put(this.baseUrl + '/monthly-return/update-MonthlyReturn/'+id, model);
  }
  
  
  submit(model: any) {
    return this.http.post(this.baseUrl + '/monthly-return/save-MonthlyReturn', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/monthly-return/delete-MonthlyReturn/'+id);
  }

}

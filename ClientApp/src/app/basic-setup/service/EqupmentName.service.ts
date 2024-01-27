import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IEqupmentNamePagination,EqupmentNamePagination } from '../models/EqupmentNamePagination'
import { EqupmentName } from '../models/EqupmentName';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class EqupmentNameService {
  baseUrl = environment.apiUrl;
  EqupmentNames: EqupmentName[] = [];
  EqupmentNamePagination = new EqupmentNamePagination();
  constructor(private http: HttpClient) { }

  getEqupmentNames(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IEqupmentNamePagination>(this.baseUrl + '/equpment-name/get-EqupmentNames', { observe: 'response', params })
    .pipe(
      map(response => {
        this.EqupmentNames = [...this.EqupmentNames, ...response.body.items];
        this.EqupmentNamePagination = response.body;
        return this.EqupmentNamePagination;
      })
    );
   
  }
  getEqupmentNamesWhitoutPage() { 

    //let params = new HttpParams();

    // params = params.append('searchText', searchText.toString());
    // params = params.append('pageNumber', pageNumber.toString());
    // params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<any>(this.baseUrl + '/equpment-name/get-equipmentListWithoutPageRequest')
  }
  find(id: number) {
    return this.http.get<EqupmentName>(this.baseUrl + '/equpment-name/get-EqupmentNameDetail/' + id);
  }

  // getSelectedSchoolByBranchLevelAndThirdLevel(baseNameId){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedBaseSchoolNameByBranchLevelAndThirdLevel?baseNameId='+baseNameId)
  // }
  getSelectedEquipmentCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-category/get-selectedEquipmentCategory')
  }
   
  // getSelectedEquipmentName(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-category/get-selectedEquipmentCategory')
  // }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/equpment-name/update-EqupmentName/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/equpment-name/save-EqupmentName', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/equpment-name/delete-EqupmentName/'+id);
  }

}

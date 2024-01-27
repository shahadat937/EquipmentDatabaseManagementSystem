import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IEquipmentCategoryPagination,EquipmentCategoryPagination } from '../models/EquipmentCategoryPagination'
import { EquipmentCategory } from '../models/EquipmentCategory';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class EquipmentCategoryService {
  baseUrl = environment.apiUrl;
  EquipmentCategorys: EquipmentCategory[] = [];
  EquipmentCategoryPagination = new EquipmentCategoryPagination();
  constructor(private http: HttpClient) { }

  getEquipmentCategorys(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IEquipmentCategoryPagination>(this.baseUrl + '/equipment-category/get-EquipmentCategorys', { observe: 'response', params })
    .pipe(
      map(response => {
        this.EquipmentCategorys = [...this.EquipmentCategorys, ...response.body.items];
        this.EquipmentCategoryPagination = response.body;
        return this.EquipmentCategoryPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<EquipmentCategory>(this.baseUrl + '/equipment-category/get-EquipmentCategoryDetail/' + id);
  }


  // getSelectedGroupName(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/group-name/get-selectedGroupName')
  // }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/equipment-category/update-EquipmentCategory/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/equipment-category/save-EquipmentCategory', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/equipment-category/delete-EquipmentCategory/'+id);
  }

}

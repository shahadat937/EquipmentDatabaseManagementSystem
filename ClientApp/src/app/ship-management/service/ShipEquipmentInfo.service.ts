import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../src/environments/environment';
import {IShipEquipmentInfoPagination,ShipEquipmentInfoPagination } from '../models/ShipEquipmentInfoPagination'
import { ShipEquipmentInfo } from '../models/ShipEquipmentInfo';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ShipEquipmentInfoService {
  baseUrl = environment.apiUrl;
  ShipEquipmentInfos: ShipEquipmentInfo[] = [];
  ShipEquipmentInfoPagination : any;
  constructor(private http: HttpClient) { }

  getShipEquipmentInfos(pageNumber, pageSize, searchText,shipId, sortColumn, sortDeriction ) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('shipId', shipId.toString());
    params = params.append('sortColumn', sortColumn.toString());
    params = params.append('sortDeriction', sortDeriction.toString());
  

    
    return this.http.get<IShipEquipmentInfoPagination>(this.baseUrl + '/ship-equipment-info/get-ShipEquipmentInfos', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShipEquipmentInfos = [...this.ShipEquipmentInfos, ...response.body.items];
        this.ShipEquipmentInfoPagination = response.body;

        return this.ShipEquipmentInfoPagination;
      })
    );
   
  }
  getShipEquipmentByCategoryIdAndStateOfEquipmentStatus (pageNumber, pageSize, searchText,categoryId, stateOfEquipmentId ) { 
    let params = new HttpParams();
    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('categoryId', categoryId.toString());
    params = params.append('stateOfEquipmentId', stateOfEquipmentId.toString());

    
    return this.http.get<IShipEquipmentInfoPagination>(this.baseUrl + '/ship-equipment-info/get-ShipEquipmentInfos-by-CategoryId-StateOfEquipmentId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShipEquipmentInfos = [...this.ShipEquipmentInfos, ...response.body.items];
        this.ShipEquipmentInfoPagination = response.body;
        return this.ShipEquipmentInfoPagination;
      })
    );
   
  }

  getShipEquipmentByCategoryIdAndOplNonOplQty (pageNumber, pageSize, searchText,categoryId, isOpl ) { 
    let params = new HttpParams();
    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('categoryId', categoryId.toString());
    params = params.append('isOpl', isOpl);

    
    return this.http.get<IShipEquipmentInfoPagination>(this.baseUrl + '/ship-equipment-info/get-ShipEquipmentInfos-by-CategoryId-and-opl-nonopl-qty', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShipEquipmentInfos = [...this.ShipEquipmentInfos, ...response.body.items];
        this.ShipEquipmentInfoPagination = response.body;
        return this.ShipEquipmentInfoPagination;
      })
    );
   
  }
  getShipEquipmentByCategoryByAuthorityId (pageNumber, pageSize, searchText, authorityId ) { 
    let params = new HttpParams();
    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('authorityId', authorityId.toString());


    
    return this.http.get<IShipEquipmentInfoPagination>(this.baseUrl + '/ship-equipment-info/get-ShipEquipmentInfos-by-authority', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShipEquipmentInfoPagination = response.body;
        return this.ShipEquipmentInfoPagination;
      })
    );
   
  }
  getShipEquipmentByCategoryIdAndStateOfEquipmentAndCommandingAreaId(pageNumber, pageSize, searchText,categoryId, stateOfEquipmentId, commandingAreaId ) { 
    let params = new HttpParams();
    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('categoryId', categoryId.toString());
    params = params.append('stateOfEquipmentId', stateOfEquipmentId.toString());
    params = params.append('commandingAreaId', commandingAreaId.toString());

    
    return this.http.get<IShipEquipmentInfoPagination>(this.baseUrl + '/ship-equipment-info/get-ShipEquipmentInfos-by-CategoryId-StateOfEquipmentId-commandingAreaId', { observe: 'response', params })
    .pipe(
      map(response => {
        // this.ShipEquipmentInfos = [...this.ShipEquipmentInfos, ...response];
        this.ShipEquipmentInfoPagination = response.body;
        return this.ShipEquipmentInfoPagination;
      })
    );
   
  }

  getShipEquipmentByCategoryIdNameIdAndStateOfEquipmentStatus (pageNumber, pageSize, searchText,categoryId, equipmentNameId, stateOfEquipmentId ) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('categoryId', categoryId.toString());
    params = params.append('equipmentNameId', equipmentNameId.toString())
    params = params.append('stateOfEquipmentId', stateOfEquipmentId.toString());
    
    return this.http.get<IShipEquipmentInfoPagination>(this.baseUrl + '/ship-equipment-info/get-ShipEquipmentInfos-by-CategoryId-EquipmentNameId-StateOfEquipmentId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShipEquipmentInfos = [...this.ShipEquipmentInfos, ...response.body.items];
        this.ShipEquipmentInfoPagination = response.body;
        return this.ShipEquipmentInfoPagination;
      })
    );
   
  }

  getShipEquipmentByCategoryIdNameIdStateOfEquipmentStatusAndCommandingAreaId (pageNumber, pageSize, searchText,categoryId, equipmentNameId, stateOfEquipmentId, commandingAreaId ) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('categoryId', categoryId.toString());
    params = params.append('equipmentNameId', equipmentNameId.toString())
    params = params.append('stateOfEquipmentId', stateOfEquipmentId.toString());
    params = params.append('commandingAreaId', commandingAreaId.toString());
    return this.http.get<any>(this.baseUrl + '/ship-equipment-info/get-ShipEquipmentInfos-by-CategoryId-EquipmentNameId-StateOfEquipmentId-CommandingAreaId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShipEquipmentInfoPagination = response.body;
        return this.ShipEquipmentInfoPagination;
      })
    );
   
  }



  find(id: number) {
    return this.http.get<ShipEquipmentInfo>(this.baseUrl + '/ship-equipment-info/get-ShipEquipmentInfoDetail/' + id);
  }

  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }

  getSelectedOrganizationByBranchLevel(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedOrganizationByBranchLevel')
  }
  getSelectedSchoolByBranchLevelAndThirdLevel(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedShipName')
  }
  getSelectedEquipmentCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-category/get-selectedEquipmentCategory')
  }
  getSelectedEquipmentType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-type/get-selectedEquipmentType')
  }
  getSelectedEqupmentName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equpment-name/get-selectedEqupmentName')
  }
  getSelectedEqupmentNameByEquepmentCategory(equipmentCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equpment-name/get-selectedEqupmentNameByCategory?equepmentCategoryId='+equipmentCategoryId)
  }
  getSelectedBrand(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/brand/get-selectedBrand')
  }
  getSelectedStateOfEquipment(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/state-of-equipment/get-selectedStateOfEquipment')
  }
  getSelectedAcquisitionMethod(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/acquisition-method/get-selectedAcquisitionMethod')
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/ship-equipment-info/update-ShipEquipmentInfo/'+id, model);
  }
  submit(model: any) {
    //console.log(model)
    return this.http.post(this.baseUrl + '/ship-equipment-info/save-ShipEquipmentInfo', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/ship-equipment-info/delete-ShipEquipmentInfo/'+id);
  }

}

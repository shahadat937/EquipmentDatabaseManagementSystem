import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IProcurementPagination,ProcurementPagination } from '../models/ProcurementPagination'
import { Procurement } from '../models/Procurement';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ProcurementService {
  baseUrl = environment.apiUrl;
  Procurements: Procurement[] = [];
  ProcurementPagination = new ProcurementPagination();
  constructor(private http: HttpClient) { }

  getProcurements(pageNumber, pageSize, searchText, searchBy) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('searchBy', searchBy.toString())

    
    return this.http.get<IProcurementPagination>(this.baseUrl + '/procurement/get-Procurements', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Procurements = [...this.Procurements, ...response.body.items];
        this.ProcurementPagination = response.body;
        return this.ProcurementPagination;
      })
    );
   
  }

  getProcurementsByProcurementMethodId(pageNumber, pageSize, searchText, procurementMethodId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('procurementMethodId', procurementMethodId.toString())

    
    return this.http.get<IProcurementPagination>(this.baseUrl + '/procurement/get-Procurements-by-procurementMethodId/'+procurementMethodId, { observe: 'response', params })
    .pipe(
      map(response => {
        console.log(response);
        this.Procurements = [...this.Procurements, ...response.body.items];
        this.ProcurementPagination = response.body;
        return this.ProcurementPagination;
      })
    );
   
  }
  getProcurementsByProcurementMethodIdAndAuthorityId(pageNumber, pageSize, searchText, searchBy, procurementMethodId,authorityId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('searchBy', searchBy.toString())
    params = params.append('procurementMethodId', procurementMethodId.toString())
    params = params.append('authorityId' , authorityId.toString())
    
    console.log(params);
    
    return this.http.get<IProcurementPagination>(this.baseUrl + '/procurement/get-Procurements-by-procurementMethodId-authorityId/'+procurementMethodId+"/"+authorityId, { observe: 'response', params })
    .pipe(
      map(response => {
        console.log(response);
        this.Procurements = [...this.Procurements, ...response.body.items];
        this.ProcurementPagination = response.body;
        return this.ProcurementPagination;
      })
    );
   
  }


  find(id: number) {
    return this.http.get<Procurement>(this.baseUrl + '/procurement/get-ProcurementDetail/' + id);
  }
  getProcurementMethods(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<any>(this.baseUrl + '/procurement-method/get-ProcurementMethods', { observe: 'response', params })
    .pipe(
      map(response => {
        return response.body;
      })
    );
   
  }

  // getSelectedSchoolName(baseNameId){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  // }

  // getSelectedOrganizationByBranchLevel(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedOrganizationByBranchLevel')
  // }
  getSelectedSchoolByBranchLevelAndThirdLevel(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedShipName')
  }
  // getSelectedSchoolByBranchLevelAndThirdLevel(baseNameId){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedBaseSchoolNameByBranchLevelAndThirdLevel?baseNameId='+baseNameId)
  // }
  getSelectedProcurementMethod(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/procurement-method/get-selectedProcurementMethod')
  }
  getSelectedEnvelope(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/envelope/get-selectedEnvelope')
  }
  getSelectedProcurementType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/procurement-type/get-selectedProcurementType')
  }
  getSelectedGroupName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/group-name/get-selectedGroupName')
  }
  getSelectedEqupmentName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equpment-name/get-selectedEqupmentName')
  }
  getSelectedControlled(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/controlled/get-selectedControlled')
  }
  getSelectedFcLc(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fc-lc/get-selectedFcLc')
  }
  getSelectedDgdpNssd(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/dgdp-nssd/get-selectedDgdpNssd')
  }
  getSelectedTec(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/tec/get-selectedTec')
  }
  getSelectedTenderOpeningDateType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/tender-opening-datetype/get-selectedTenderOpeningDateType')
  }
  getSelectedPaymentStatus(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/payment-status/get-selectedPaymentStatus')
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/procurement/update-Procurement/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/procurement/save-Procurement', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/procurement/delete-Procurement/'+id);
  }

}

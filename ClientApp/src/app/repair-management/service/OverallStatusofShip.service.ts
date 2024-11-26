import { IOverallStatusOfShipPagination, OverallShipStatusPagination } from './../models/OverallStatusOfShipPagination';
import { environment } from './../../../environments/environment';
import { SelectedModel } from './../../core/models/selectedModel';
import { OverallShipStatus } from './../models/OverallStatusOfShip';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs';
// import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
  })
  export class OverallStatusOfShip{
    baseUrl = environment.apiUrl;
    OverallStatusOfShip: OverallShipStatus[]=[]
    OverallShipStatusPagination = new OverallShipStatusPagination();
    constructor(private http: HttpClient) { }

    getOverallStatusofShip(pageNumber, pageSize, searchText){
      let params = new HttpParams();

      params = params.append('searchText', searchText.toString());
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());
  
      return this.http.get<IOverallStatusOfShipPagination>(this.baseUrl + '/statusof-ship/get-StatusOfShips',{
          observe: 'response', params
      })
      .pipe(
          map(response=>{
              this.OverallStatusOfShip = [...this.OverallStatusOfShip,...response.body.items];
              this.OverallShipStatusPagination = response.body;
              return this.OverallShipStatusPagination
          })
      );

    }
    find(id: number) {
      return this.http.get<OverallShipStatus>(this.baseUrl + '/statusof-ship/get-StatusOfShipsDetail/' + id);
    }
    getSelectedSchoolByBranchLevelAndThirdLevel(){
      return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedShipName')
    }
    getSelectedOperationalStatus(){
      return this.http.get<SelectedModel[]>(this.baseUrl + '/operational-status/get-selectedOperationalStatus')
    }
    update(id: number,model: any) {
      return this.http.put(this.baseUrl + '/statusof-ship/update-StatusOfShips/'+id, model);
    }
    submit(model: any) {
      console.log('is null',model)
      return this.http.post(this.baseUrl + '/statusof-ship/save-StatusOfShip', model);
    }
     
    delete(id:number){
      return this.http.delete(this.baseUrl + '/statusof-ship/delete-StatusOfShips/'+id);
    }
  }
  
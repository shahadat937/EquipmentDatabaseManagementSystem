import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../src/environments/environment';
import { IFinancialYearPagination, FinancialYearPagination } from '../models/FinancialYearPagination'
import { FinancialYear } from '../models/FinancialYear';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FinancialYearService {
  baseUrl = environment.apiUrl;
  FinancialYears: FinancialYear[] = [];
  FinancialYearPagination = new FinancialYearPagination();
  constructor(private http: HttpClient) { }

  getFinancialYears(pageNumber, pageSize, searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());


    return this.http.get<IFinancialYearPagination>(this.baseUrl + '/financial-years/get-FinancialYearss', { observe: 'response', params })
      .pipe(
        map(response => {
          this.FinancialYears = [...this.FinancialYears, ...(response.body?.items || [])];
          this.FinancialYearPagination = response.body ?? {} as FinancialYearPagination ;
          return this.FinancialYearPagination;
        })
      );

  }

  find(id: number) {
    return this.http.get<FinancialYear>(this.baseUrl + '/financial-years/get-FinancialYearsDetail/' + id);
  }

  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/financial-years/update-FinancialYears/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/financial-years/save-FinancialYears', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/financial-years/delete-FinancialYears/' + id);
  }
}

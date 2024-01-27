import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IPaymentStatusPagination,PaymentStatusPagination } from '../models/PaymentStatusPagination'
import { PaymentStatus } from '../models/PaymentStatus';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class PaymentStatusService {
  baseUrl = environment.apiUrl;
  PaymentStatuses: PaymentStatus[] = [];
  PaymentStatusPagination = new PaymentStatusPagination();
  constructor(private http: HttpClient) { }

  getPaymentStatuses(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IPaymentStatusPagination>(this.baseUrl + '/payment-status/get-PaymentStatuses', { observe: 'response', params })
    .pipe(
      map(response => {
        this.PaymentStatuses = [...this.PaymentStatuses, ...response.body.items];
        this.PaymentStatusPagination = response.body;
        return this.PaymentStatusPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<PaymentStatus>(this.baseUrl + '/payment-status/get-PaymentStatusDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/payment-status/update-PaymentStatus/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/payment-status/save-PaymentStatus', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/payment-status/delete-PaymentStatus/'+id);
  }

}

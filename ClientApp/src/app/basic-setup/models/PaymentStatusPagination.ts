import {PaymentStatus} from './PaymentStatus';

export interface IPaymentStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: PaymentStatus[];
}

export class PaymentStatusPagination implements IPaymentStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: PaymentStatus[] = [];
}

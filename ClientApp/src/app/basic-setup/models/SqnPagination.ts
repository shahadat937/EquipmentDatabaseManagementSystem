import {Sqn} from './Sqn';

export interface ISqnPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Sqn[];
}

export class SqnPagination implements ISqnPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Sqn[] = [];
}

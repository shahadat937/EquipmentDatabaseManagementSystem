import {Procurement} from './Procurement';

export interface IProcurementPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Procurement[];
}

export class ProcurementPagination implements IProcurementPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Procurement[] = [];
}

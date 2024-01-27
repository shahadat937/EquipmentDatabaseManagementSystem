import {ProcurementMethod} from './ProcurementMethod';

export interface IProcurementMethodPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProcurementMethod[];
}

export class ProcurementMethodPagination implements IProcurementMethodPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProcurementMethod[] = [];
}

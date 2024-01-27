import {OperationalStatus} from './OperationalStatus';

export interface IOperationalStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: OperationalStatus[];
}

export class OperationalStatusPagination implements IOperationalStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: OperationalStatus[] = [];
}

import {OperationalState} from './OperationalState';

export interface IOperationalStatePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: OperationalState[];
}

export class OperationalStatePagination implements IOperationalStatePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: OperationalState[] = [];
}

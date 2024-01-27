import {Controlled} from './Controlled';

export interface IControlledPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Controlled[];
}

export class ControlledPagination implements IControlledPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Controlled[] = [];
}

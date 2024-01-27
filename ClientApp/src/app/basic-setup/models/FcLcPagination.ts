import {FcLc} from './FcLc';

export interface IFcLcPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FcLc[];
}

export class FcLcPagination implements IFcLcPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FcLc[] = [];
}

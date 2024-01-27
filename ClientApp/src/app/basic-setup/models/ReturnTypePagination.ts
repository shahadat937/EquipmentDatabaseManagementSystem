import {ReturnType} from './ReturnType';

export interface IReturnTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ReturnType[];
}

export class ReturnTypePagination implements IReturnTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ReturnType[] = [];
}

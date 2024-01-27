import {Brand} from './Brand';

export interface IBrandPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Brand[];
}

export class BrandPagination implements IBrandPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Brand[] = [];
}

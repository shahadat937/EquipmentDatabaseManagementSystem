import {Tec} from './Tec';

export interface ITecPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Tec[];
}

export class TecPagination implements ITecPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Tec[] = [];
}

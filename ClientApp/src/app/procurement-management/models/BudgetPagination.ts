import {Budget} from './Budget';

export interface IBudgetPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Budget[];
}

export class BudgetPagination implements IBudgetPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Budget[] = [];
}

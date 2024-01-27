import {MonthlyReturn} from './MonthlyReturn';

export interface IMonthlyReturnPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: MonthlyReturn[];
}

export class MonthlyReturnPagination implements IMonthlyReturnPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: MonthlyReturn[] = [];
}

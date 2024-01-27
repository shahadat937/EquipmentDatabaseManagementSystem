import {HalfYearlyReturn} from './HalfYearlyReturn';

export interface IHalfYearlyReturnPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: HalfYearlyReturn[];
}

export class HalfYearlyReturnPagination implements IHalfYearlyReturnPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: HalfYearlyReturn[] = [];
}

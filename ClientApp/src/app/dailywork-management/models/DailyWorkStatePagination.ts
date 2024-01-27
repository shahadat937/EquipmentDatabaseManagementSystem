import {DailyWorkState} from './DailyWorkState';

export interface IDailyWorkStatePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DailyWorkState[];
}

export class DailyWorkStatePagination implements IDailyWorkStatePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DailyWorkState[] = [];
}

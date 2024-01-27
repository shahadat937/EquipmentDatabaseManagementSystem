import {RunningTime} from './RunningTime';

export interface IRunningTimePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: RunningTime[];
}

export class RunningTimePagination implements IRunningTimePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: RunningTime[] = [];
}

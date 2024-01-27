import {QuarterlyReturnNoType} from './QuarterlyReturnNoType';

export interface IQuarterlyReturnNoTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: QuarterlyReturnNoType[];
}

export class QuarterlyReturnNoTypePagination implements IQuarterlyReturnNoTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: QuarterlyReturnNoType[] = [];
}


import { QuarterlyReturn } from "./QuarterlyReturn";

export interface  IQuarterlyReturnPagination{
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: QuarterlyReturn[];
}
export class QuarterlyReturnPagination implements IQuarterlyReturnPagination{
    totalPages: number;
    itemsFrom: number;
    itemsTo: number;
    totalItemsCount: number;
    items: QuarterlyReturn[];
}

import { YearlyReturn } from "./YearlyReturn";

export interface IYearlyReturnPagination{
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: YearlyReturn[];
}
export class YearlyReturnPagination implements IYearlyReturnPagination{
    totalPages: number;
    itemsFrom: number;
    itemsTo: number;
    totalItemsCount: number;
    items: YearlyReturn[];
}
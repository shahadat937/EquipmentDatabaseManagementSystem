import {FinancialYear} from './FinancialYear';

export interface IFinancialYearPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FinancialYear[];
}

export class FinancialYearPagination implements IFinancialYearPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FinancialYear[] = [];
}

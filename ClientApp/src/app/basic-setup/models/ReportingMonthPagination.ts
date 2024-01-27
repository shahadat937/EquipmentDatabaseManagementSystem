import {ReportingMonth} from './ReportingMonth';

export interface IReportingMonthPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ReportingMonth[];
}

export class ReportingMonthPagination implements IReportingMonthPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ReportingMonth[] = [];
}

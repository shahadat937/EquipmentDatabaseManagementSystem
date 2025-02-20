import {ReportingYear} from './ReportingYear';

export interface IReportingYearPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ReportingYear[];
}

export class ReportingYearPagination implements IReportingYearPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ReportingYear[] = [];
}

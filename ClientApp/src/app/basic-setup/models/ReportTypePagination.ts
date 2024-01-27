import {ReportType} from './ReportType';

export interface IReportTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ReportType[];
}

export class ReportTypePagination implements IReportTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ReportType[] = [];
}

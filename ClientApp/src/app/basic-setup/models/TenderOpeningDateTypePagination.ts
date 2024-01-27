import {TenderOpeningDateType} from './TenderOpeningDateType';

export interface ITenderOpeningDateTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: TenderOpeningDateType[];
}

export class TenderOpeningDateTypePagination implements ITenderOpeningDateTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: TenderOpeningDateType[] = [];
}

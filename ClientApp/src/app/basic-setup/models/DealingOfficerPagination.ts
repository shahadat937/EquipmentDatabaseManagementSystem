import {DealingOfficer} from './DealingOfficer';

export interface IDealingOfficerPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DealingOfficer[];
}

export class DealingOfficerPagination implements IDealingOfficerPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DealingOfficer[] = [];
}

import {ProcurementType} from './ProcurementType';

export interface IProcurementTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProcurementType[];
}

export class ProcurementTypePagination implements IProcurementTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProcurementType[] = [];
}

import {EqupmentName} from './EqupmentName';

export interface IEqupmentNamePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: EqupmentName[];
}

export class EqupmentNamePagination implements IEqupmentNamePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: EqupmentName[] = [];
}

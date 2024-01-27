import {DgdpNssd} from './DgdpNssd';

export interface IDgdpNssdPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DgdpNssd[];
}

export class DgdpNssdPagination implements IDgdpNssdPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DgdpNssd[] = [];
}

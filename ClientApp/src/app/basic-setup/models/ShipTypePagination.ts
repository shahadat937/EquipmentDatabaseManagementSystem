import {ShipType} from './ShipType';

export interface IShipTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShipType[];
}

export class ShipTypePagination implements IShipTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShipType[] = [];
}

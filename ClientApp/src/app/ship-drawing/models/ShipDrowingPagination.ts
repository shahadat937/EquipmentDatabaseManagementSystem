import {ShipDrowing} from './ShipDrowing';

export interface IShipDrowingPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShipDrowing[];
}

export class ShipDrowingPagination implements IShipDrowingPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShipDrowing[] = [];
}

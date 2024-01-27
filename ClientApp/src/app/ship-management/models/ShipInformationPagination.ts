import {ShipInformation} from './ShipInformation';

export interface IShipInformationPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShipInformation[];
}

export class ShipInformationPagination implements IShipInformationPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShipInformation[] = [];
}

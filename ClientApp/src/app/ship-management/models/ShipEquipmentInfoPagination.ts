import {ShipEquipmentInfo} from './ShipEquipmentInfo';

export interface IShipEquipmentInfoPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShipEquipmentInfo[];
}

export class ShipEquipmentInfoPagination implements IShipEquipmentInfoPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShipEquipmentInfo[] = [];
}

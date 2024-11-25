import { OverallShipStatus } from "./OverallStatusOfShip";

export interface IOverallStatusOfShipPagination{
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: OverallShipStatus[];
}
export class OverallShipStatusPagination implements IOverallStatusOfShipPagination{
    totalPages: number;
    itemsFrom: number;
    itemsTo: number;
    totalItemsCount: number;
    items: OverallShipStatus[];
}
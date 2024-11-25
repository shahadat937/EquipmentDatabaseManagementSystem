import {OperationalStatusOfEquipmentSystem} from './OperationalStatusOfEquipment';

export interface IOperationalStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: OperationalStatusOfEquipmentSystem[];
}

export class OperationalStatusPagination implements IOperationalStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: OperationalStatusOfEquipmentSystem[] = [];
}

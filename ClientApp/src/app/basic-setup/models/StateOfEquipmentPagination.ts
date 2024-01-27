import {StateOfEquipment} from './StateOfEquipment';

export interface IStateOfEquipmentPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: StateOfEquipment[];
}

export class StateOfEquipmentPagination implements IStateOfEquipmentPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: StateOfEquipment[] = [];
}

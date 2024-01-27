import {EquipmentCategory} from './EquipmentCategory';

export interface IEquipmentCategoryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: EquipmentCategory[];
}

export class EquipmentCategoryPagination implements IEquipmentCategoryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: EquipmentCategory[] = [];
}

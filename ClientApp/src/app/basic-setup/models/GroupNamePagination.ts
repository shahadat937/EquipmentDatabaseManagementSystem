import {GroupName} from './GroupName';

export interface IGroupNamePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: GroupName[];
}

export class GroupNamePagination implements IGroupNamePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: GroupName[] = [];
}

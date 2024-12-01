import { UserManual } from './UserManuals';

export interface IUserManualPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number; 
    totalItemsCount:number;
    items: UserManual[]; 
}

export class UserManualPagination implements IUserManualPagination { 
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: UserManual[] = []; 
}
 
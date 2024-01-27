import {BookUserManualBRInfo} from './BookUserManualBRInfo';

export interface IBookUserManualBRInfoPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: BookUserManualBRInfo[];
}

export class BookUserManualBRInfoPagination implements IBookUserManualBRInfoPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: BookUserManualBRInfo[] = [];
}

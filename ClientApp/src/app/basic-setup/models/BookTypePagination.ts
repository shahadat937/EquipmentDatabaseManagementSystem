import {BookType} from './BookType';

export interface IBookTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: BookType[];
}

export class BookTypePagination implements IBookTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: BookType[] = [];
}
